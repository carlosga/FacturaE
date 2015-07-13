/* FacturaE - The MIT License (MIT)
 * 
 * Copyright (c) 2012-2014 Carlos Guzmán Álvarez
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using FacturaE.Xml;
using Mono.Security;
using Mono.Security.X509;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
        #region · Constants ·

        const string PolicyIdentifier = "http://www.facturae.es/politica_de_firma_formato_facturae/politica_de_firma_formato_facturae_v3_1.pdf";
        const string PolicyResource = "FacturaE.Policies.politica_de_firma_formato_facturae_v3_1.pdf";

        #endregion

        #region · Static Members ·

        static readonly XmlSerializer QualifyingPropertiesSerializer = new XmlSerializer(typeof(QualifyingPropertiesType));

        #endregion

        #region · QualifyingPropertiesType Extensions ·

        public static SignedPropertiesType CreateSignedProperties(this QualifyingPropertiesType qualifyingProperties
                                                                , XAdESSignedXml                signedXml)
        {
            qualifyingProperties.SignedProperties = new SignedPropertiesType
            {
                Id = XsdSchemas.FormatId(signedXml.Signature.Id, "SignedProperties")
            };

            return qualifyingProperties.SignedProperties;
        }

        #endregion

        #region · SignedPropertiesType Extensions ·

        public static SignedSignaturePropertiesType CreateSignedSignatureProperties(this SignedPropertiesType signedProperties)
        {
            signedProperties.SignedSignatureProperties = new SignedSignaturePropertiesType();

            return signedProperties.SignedSignatureProperties;
        }

        #endregion

        #region · SignedSignaturePropertiesType Extensions ·

        public static SignedSignaturePropertiesType SetSigningTime(this SignedSignaturePropertiesType signedSignatureProperties)
        {
            signedSignatureProperties.SigningTime          = DateTime.Now;
            signedSignatureProperties.SigningTimeSpecified = true;

            return signedSignatureProperties;
        }

        public static SignedSignaturePropertiesType SetSignerRole(this SignedSignaturePropertiesType signedSignatureProperties
                                                                , SignerRoleType                     signerRole)
        {
            signedSignatureProperties.SignerRole = signerRole;

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
                        X509IssuerName   = X501.ToString(new ASN1(certificate.IssuerName.RawData)) // RFC2253 Encoded
                      , X509SerialNumber = Int64.Parse(certificate.SerialNumber, NumberStyles.HexNumber).ToString()
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

        #endregion

        #region · Serialization Extensions ·

        public static string ToXml(this QualifyingPropertiesType qualifyingProperties)
        {
            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Encoding = new UTF8Encoding(false);

            using (MemoryStream buffer = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(buffer, settings))
                {
                    QualifyingPropertiesSerializer.Serialize(writer, qualifyingProperties, XsdSchemas.CreateXadesSerializerNamespace());
                }

                return Encoding.UTF8.GetString(buffer.ToArray());
            }
        }

        public static XmlDocument ToXmlDocument(this QualifyingPropertiesType qualifyingProperties)
        {
            XmlDocument document = new XmlDocument { PreserveWhitespace = true };

            document.LoadXml(qualifyingProperties.ToXml());

            return document;
        }

        #endregion

        #region · Private Methods ·

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

        #endregion
    }
}
