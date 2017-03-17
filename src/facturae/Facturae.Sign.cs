// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FacturaE.XAdES;
using FacturaE.Xml;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FacturaE
{
    /// <summary>
    /// Facturae extensions
    /// </summary>
    public partial class Facturae
    {
        private static readonly XmlSerializer FacturaeSerializer = new XmlSerializer(typeof(Facturae));

        private static void XmlValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error)
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
        /// Signs the electronic invoice using the given certificate.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        /// <returns></returns>
        public XAdESSignatureVerifier Sign(X509Certificate2 certificate)
        {
            return Sign(certificate, ClaimedRole.Supplier);
        }

        /// <summary>
        /// Signs the electronic invoice using the given certificate & RSA key.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        /// <param name="key">The RSA Key.</param>
        /// <param name="signerRole">Rol del "firmante" de la factura</param>
        /// <returns>The XAdES signature verifier.</returns>
        public XAdESSignatureVerifier Sign(X509Certificate2 certificate, ClaimedRole signerRole)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate cannot be null");
            }

            return Sign(certificate, (RSA)certificate.PrivateKey, signerRole);
        }

        /// <summary>
        /// Signs the electronic invoice using the given certificate & RSA key.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        /// <param name="key">The RSA Key.</param>
        /// <param name="signerRole">Rol del "firmante" de la factura</param>
        /// <returns>The XAdES signature verifier.</returns>
        private XAdESSignatureVerifier Sign(X509Certificate2 certificate, RSA key, ClaimedRole signerRole)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate cannot be null");
            }
            if (key == null)
            {
                throw new ArgumentNullException("key cannot be null");
            }

            var document  = ToXmlDocument();
            var signedXml = new XAdESSignedXml(document);

            // Set the key to sign
            signedXml.SigningKey = key;

            signedXml.SetSignatureInfo()
                     .SetSignerRole(signerRole)                                 // XAdES Signer Role
                     .SetKeyInfo(certificate, (RSA)certificate.PublicKey.Key)   // Key Info
                     .ComputeSignature();                                       // Compute Signature
            
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
            ToXmlDocument().Save(path);

            return this;
        }

        private string ToXml()
        {
            var settings = new XmlWriterSettings { Encoding = new UTF8Encoding(false) };
            
            using (var buffer = new MemoryStream())
            { 
                using (var writer = XmlWriter.Create(buffer, settings))
                {
                    FacturaeSerializer.Serialize(writer, this, XsdSchemas.CreateXadesSerializerNamespace());
                }

                return Encoding.UTF8.GetString(buffer.ToArray());
            }
        }

        private XmlDocument ToXmlDocument()
        {
            var document = new XmlDocument { PreserveWhitespace = true };

            document.LoadXml(ToXml());

            return document;
        }
    }
}
