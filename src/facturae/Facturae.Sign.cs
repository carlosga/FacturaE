// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FacturaE.XAdES;
using FacturaE.Xml;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FacturaE;

/// <summary>
/// Facturae extensions
/// </summary>
public partial class Facturae
{
    private static readonly XmlSerializer     s_Serializer     = new(typeof(Facturae));
    private static readonly Encoding          s_Encoding       = new UTF8Encoding(false);
    private static readonly XmlWriterSettings s_WriterSettings = new() 
    { 
        Encoding         = s_Encoding, 
        ConformanceLevel = ConformanceLevel.Auto 
    };

    private static void XmlValidationEventHandler(object sender, ValidationEventArgs e)
    {
        if (e.Severity is XmlSeverityType.Error)
        {
            throw e.Exception;
        }
        else
        {
            Trace.Write($"Warning while validating XML '{e.Message}'");
        }
    }

    /// <summary>
    /// Validates the electronic invoice XML.
    /// </summary>
    /// <returns></returns>
    public Facturae Validate()
    {
        XmlDocument document = ToXmlDocument();

        document.Schemas.Add(XsdSchemas.BuildSchemaSet(document.NameTable));
        document.Validate(XmlValidationEventHandler);

        return this;
    }

    /// <summary>
    /// Signs the electronic invoice using the given certificate & RSA key.
    /// </summary>
    /// <param name="certificate">The certificate.</param>
    /// <param name="signerRole">Rol del "firmante" de la factura</param>
    /// <returns>The XAdES signature verifier.</returns>
    public XAdESSignatureVerifier Sign(X509Certificate2 certificate, ClaimedRole signerRole = ClaimedRole.Supplier)
    {
        if (certificate is null)
        {
            throw new ArgumentNullException(nameof(certificate), "certificate cannot be null");
        }
        var privateKey = certificate.GetRSAPrivateKey();
        if (privateKey is null)
        {
            throw new ArgumentNullException(nameof(certificate), "the certificate private key cannot be null");
        }
        var publicKey = certificate.GetRSAPublicKey();
        if (publicKey is null)
        {
            throw new ArgumentNullException(nameof(certificate), "the certificate public key cannot be null");
        }

        var document  = ToXmlDocument();
        var signedXml = new XAdESSignedXml(document);

        // Set the key to sign
        signedXml.SigningKey = privateKey;

        signedXml
            .SetSignatureInfo()
            .SetSignerRole(signerRole)          // XAdES Signer Role
            .SetKeyInfo(certificate, publicKey) // Key Info
            .ComputeSignature();                // Compute Signature

        // Import the signed XML node
        document.DocumentElement.AppendChild(document.ImportNode(signedXml.GetXml(), true));

        return new XAdESSignatureVerifier(document);
    }

    /// <summary>
    /// Writes the electronic invoice to the given path.
    /// </summary>
    /// <param name="path">The file path.</param>
    /// <returns></returns>
    public Facturae WriteToFile(string path)
    {
        File.WriteAllText(path, ToXml());

        return this;
    }

    private string ToXml()
    {        
        using var buffer = new MemoryStream();
        using var writer = XmlWriter.Create(buffer, s_WriterSettings);

        s_Serializer.Serialize(writer, this, XsdSchemas.XadesSerializerNamespaces);

        return s_Encoding.GetString(buffer.GetBuffer());
    }

    private XmlDocument ToXmlDocument()
    {
        var document = new XmlDocument { PreserveWhitespace = true };

        document.LoadXml(ToXml());

        return document;
    }
}
