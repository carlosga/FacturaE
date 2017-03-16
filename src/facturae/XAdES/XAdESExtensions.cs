// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FacturaE.Xml;
using X500;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace FacturaE.XAdES
{
    /// <summary>
    /// XAdES Extension Methods
    /// </summary>
    public static class XAdESExtensions
    {
        const string PolicyIdentifier = "http://www.facturae.es/politica_de_firma_formato_facturae/politica_de_firma_formato_facturae_v3_1.pdf";
        const string PolicyResource = "FacturaE.Policies.politica_de_firma_formato_facturae_v3_1.pdf";

        private static readonly XmlSerializer QualifyingPropertiesSerializer = new XmlSerializer(typeof(QualifyingPropertiesType));
        private static readonly Encoding UTF8Encoding = new UTF8Encoding(false);

        public static SignedPropertiesType CreateSignedProperties(this QualifyingPropertiesType qualifyingProperties
                                                                , XAdESSignedXml                signedXml)
        {
            qualifyingProperties.SignedProperties = new SignedPropertiesType
            {
                Id = XsdSchemas.FormatId(signedXml.Signature.Id, "SignedProperties")
            };

            return qualifyingProperties.SignedProperties;
        }

        public static SignedSignaturePropertiesType CreateSignedSignatureProperties(this SignedPropertiesType signedProperties)
        {
            signedProperties.SignedSignatureProperties = new SignedSignaturePropertiesType();

            return signedProperties.SignedSignatureProperties;
        }

        public static SignedSignaturePropertiesType SetSigningTime(this SignedSignaturePropertiesType signedSignatureProperties)
        {
            signedSignatureProperties.SigningTime          = DateTime.Now;
            signedSignatureProperties.SigningTimeSpecified = true;

            return signedSignatureProperties;
        }

        public static SignedSignaturePropertiesType SetSignerRole(this SignedSignaturePropertiesType signedSignatureProperties
                                                                , ClaimedRole                        signerRole)
        {
            signedSignatureProperties.SignerRole = new SignerRoleType
            {
                ClaimedRoles = new List<ClaimedRole> { signerRole }
            };

            return signedSignatureProperties;
        }

        public static SignedSignaturePropertiesType SetSigningCertificate(this SignedSignaturePropertiesType signedSignatureProperties
                                                                        , X509Certificate2                   certificate)
        {
            Debug.Assert(certificate != null);

            signedSignatureProperties.SigningCertificate = new CertIDType[]
            {
                new CertIDType
                {
                    CertDigest = new DigestAlgAndValueType
                    {
                        DigestMethod = new XAdES.DigestMethodType
                        {
                            Algorithm = SignedXml.XmlDsigSHA1Url
                        }
                      , DigestValue = certificate.RawData.ComputeSHA1Hash()
                    }
                  , IssuerSerial = new XAdES.X509IssuerSerialType
                    {
                        X509IssuerName   = X500.X500DistinguishedName.Format(DistinguishedNameFormat.RFC2253, certificate.IssuerName.RawData)
                      , X509SerialNumber = BigInteger.Parse(certificate.SerialNumber, NumberStyles.HexNumber).ToString()
                    }
                }
            };

            return signedSignatureProperties;
        }

        public static SignaturePolicyIdentifierType SetSignaturePolicyIdentifier(this SignedSignaturePropertiesType signedSignatureProperties)
        {
            signedSignatureProperties.SignaturePolicyIdentifier = new SignaturePolicyIdentifierType
            {
                Item = new SignaturePolicyIdType
                {
                    SigPolicyId = new ObjectIdentifierType
                    {
                        Identifier = new IdentifierType
                        {
                            Value = PolicyIdentifier
                        }
                      , Description = "Política de Firma FacturaE v3.1"
                    }
                  ,
                    SigPolicyHash = new DigestAlgAndValueType
                    {
                        DigestMethod = new XAdES.DigestMethodType
                        {
                            Algorithm = SignedXml.XmlDsigSHA1Url
                        }
                      , DigestValue = ReadPolicyFile().ComputeSHA1Hash()
                    }
                }
            };

            return signedSignatureProperties.SignaturePolicyIdentifier;
        }

        public static string ToXml(this QualifyingPropertiesType qualifyingProperties)
        {
            var settings = new XmlWriterSettings { Encoding = UTF8Encoding };

            using (var buffer = new MemoryStream(8192))
            {
                using (var writer = XmlWriter.Create(buffer, settings))
                {
                    QualifyingPropertiesSerializer.Serialize(writer, qualifyingProperties, XsdSchemas.CreateXadesSerializerNamespace());
                }

                return Encoding.UTF8.GetString(buffer.ToArray());
            }
        }

        public static XmlDocument ToXmlDocument(this QualifyingPropertiesType qualifyingProperties)
        {
            var document = new XmlDocument { PreserveWhitespace = true };

            document.LoadXml(qualifyingProperties.ToXml());

            return document;
        }

        private static byte[] ReadPolicyFile()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            using (Stream stream = currentAssembly.GetManifestResourceStream(PolicyResource))
            {
                byte[] buffer = new byte[stream.Length];

                stream.Read(buffer, 0, (int)stream.Length);

                return buffer;
            }
        }
    }
}
