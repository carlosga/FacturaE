// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FacturaE.Xml;
using X500;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace FacturaE.XAdES;

/// <summary>
/// XAdES Extension Methods
/// </summary>
internal static class XAdESExtensions
{
    private static readonly string PolicyIdentifier = "http://www.facturae.es/politica_de_firma_formato_facturae/politica_de_firma_formato_facturae_v3_1.pdf";
    private static readonly string PolicyResource   = "FacturaE.Policies.politica_de_firma_formato_facturae_v3_1.pdf";
    private static readonly byte[] PolicyHash       = Assembly.GetExecutingAssembly().GetManifestResourceStream(PolicyResource).ComputeSHA1Hash();

    private static readonly XmlSerializer     s_Serializer     = new(typeof(QualifyingPropertiesType));
    private static readonly Encoding          s_Encoding       = new UTF8Encoding(false);
    private static readonly XmlWriterSettings s_WriterSettings = new() { Encoding = s_Encoding };

    internal static SignedPropertiesType CreateSignedProperties(
        this QualifyingPropertiesType properties,
        XAdESSignedXml                signedXml)
    {
        var id = XsdSchemas.FormatId(signedXml.Signature.Id, "SignedProperties");

        properties.SignedProperties = new SignedPropertiesType { Id = id };

        return properties.SignedProperties;
    }

    internal static SignedSignaturePropertiesType CreateSignedSignatureProperties(this SignedPropertiesType properties)
    {
        properties.SignedSignatureProperties = new SignedSignaturePropertiesType();

        return properties.SignedSignatureProperties;
    }

    internal static SignedSignaturePropertiesType SetSigningTime(this SignedSignaturePropertiesType properties)
    {
        properties.SigningTime          = DateTime.Now;
        properties.SigningTimeSpecified = true;

        return properties;
    }

    internal static SignedSignaturePropertiesType SetSignerRole(
        this SignedSignaturePropertiesType properties,
        ClaimedRole                        signerRole)
    {
        properties.SignerRole = new SignerRoleType { ClaimedRoles = new List<ClaimedRole> { signerRole } };

        return properties;
    }

    internal static SignedSignaturePropertiesType SetSigningCertificate(
        this SignedSignaturePropertiesType properties,
        X509Certificate2                   certificate)
    {
        Debug.Assert(certificate is not null);

        properties.SigningCertificate =
        [
            new CertIDType
            {
                CertDigest = new DigestAlgAndValueType
                {
                    DigestMethod = new DigestMethodType { Algorithm = SignedXml.XmlDsigSHA1Url },
                    DigestValue  = certificate.RawData.ComputeSHA1Hash()
                },
                IssuerSerial = new X509IssuerSerialType
                {
                    // X509IssuerName   = certificate.IssuerName.Name, 
                    X509IssuerName   = X500.X500DistinguishedName.Format(DistinguishedNameFormat.RFC2253, certificate.IssuerName.RawData),
                    X509SerialNumber = BigInteger.Parse(certificate.SerialNumber, NumberStyles.HexNumber).ToString()
                }
            }
        ];

        return properties;
    }

    internal static SignaturePolicyIdentifierType SetSignaturePolicyIdentifier(this SignedSignaturePropertiesType properties)
    {
        properties.SignaturePolicyIdentifier = new SignaturePolicyIdentifierType
        {
            Item = new SignaturePolicyIdType
            {
                SigPolicyId = new ObjectIdentifierType
                {
                    Identifier  = new IdentifierType { Value = PolicyIdentifier },
                    Description = "Política de Firma FacturaE v3.1"
                },
                SigPolicyHash = new DigestAlgAndValueType
                {
                    DigestMethod = new DigestMethodType { Algorithm = SignedXml.XmlDsigSHA1Url },
                    DigestValue  = PolicyHash
                }
            }
        };

        return properties.SignaturePolicyIdentifier;
    }

    internal static string ToXml(this QualifyingPropertiesType properties)
    {
        var buffer = new StringBuilder();

        using var writer = XmlWriter.Create(buffer, s_WriterSettings);

        s_Serializer.Serialize(writer, properties, XsdSchemas.XadesSerializerNamespaces);

        return buffer.ToString();
    }

    internal static XmlDocument ToXmlDocument(this QualifyingPropertiesType properties)
    {
        var document = new XmlDocument { PreserveWhitespace = true };

        document.LoadXml(properties.ToXml());

        return document;
    }
}
