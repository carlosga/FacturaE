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

using FacturaE.Extensions;
using FacturaE.Xml;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace FacturaE.XAdES
{
    /// <summary>
    /// Custom <see cref="SignedXml"/> implementation.
    /// </summary>
    public sealed class XAdESSignedXml
        : SignedXml
    {
        #region · Fields ·

        private readonly List<DataObject> dataObjects = new List<DataObject>();
        private SignerRoleType            signerRole  = null;

        #endregion

        #region · Constructors ·

        /// <summary>
        /// Initializes a new instance of the <see cref="XAdESSignedXml"/> class.
        /// </summary>
        public XAdESSignedXml()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XAdESSignedXml"/> class
        /// with the give <see cref="XmlDocument"/>
        /// </summary>
        /// <param name="document">An instance of <see cref="XmlDocument"/></param>
        public XAdESSignedXml(XmlDocument document)
            : base(document)
        {
        }

        #endregion

        #region · Methods ·

        /// <summary>
        /// Gets the id element.
        /// </summary>
        /// <param name="document">The doc.</param>
        /// <param name="idValue">The id.</param>
        /// <returns></returns>
        public override XmlElement GetIdElement(XmlDocument document, string idValue)
        {
            if (String.IsNullOrEmpty(idValue))
            {
                return null;
            }

            var xmlElement = base.GetIdElement(document, idValue);
            var nsmgr = XsdSchemas.CreateXadesNamespaceManager(document);

            if (xmlElement != null)
            {
                return XsdSchemas.FixupNamespaces(document, xmlElement);
            }

            if (dataObjects.Count > 0)
            {
                if (this.dataObjects != null && this.dataObjects.Count > 0)
                {
                    foreach (DataObject dataObject in this.dataObjects)
                    {
                        var nodeWithSameId = dataObject.GetXml().SelectNodes(".", nsmgr).FindNode("Id", idValue);

                        if (nodeWithSameId != null)
                        {
                            return XsdSchemas.FixupNamespaces(document, nodeWithSameId);
                        }
                    }
                }                
            }

            // Search the KeyInfo Node
            if (this.KeyInfo != null)
            {
                var nodeWithSameId = this.KeyInfo.GetXml().SelectNodes(".", nsmgr).FindNode("Id", idValue);

                if (nodeWithSameId != null)
                {
                    return XsdSchemas.FixupNamespaces(document, nodeWithSameId);
                }
            }

            return null;
        }

        /// <summary>
        /// Adds a <see cref="T:System.Security.Cryptography.Xml.DataObject"/> object to the list of objects to be signed.
        /// </summary>
        /// <param name="dataObject">The <see cref="T:System.Security.Cryptography.Xml.DataObject"/> object to add to the list of objects to be signed.</param>
        public new void AddObject(DataObject dataObject)
        {
            base.AddObject(dataObject);
            this.dataObjects.Add(dataObject);
        }

        #endregion

        #region · Signature Helper Methods ·

        public XAdESSignedXml SetSignatureInfo()
        {
            // Set nodes identifiers
            this.Signature.Id  = XsdSchemas.FormatId("Signature");
            this.SignedInfo.Id = XsdSchemas.FormatId("Signature", "SignedInfo");

            return this;
        }

        public XAdESSignedXml SetSignerRole(SignerRoleType signerRole)
        {
            this.signerRole = signerRole;

            return this;
        }

        public XAdESSignedXml SetKeyInfo(X509Certificate2 certificate, RSA key)
        {
            this.KeyInfo    = new KeyInfo();
            this.KeyInfo.Id = XsdSchemas.FormatId("Certificate");

            this.KeyInfo.AddClause(new KeyInfoX509Data(certificate));
            this.KeyInfo.AddClause(new RSAKeyValue(key));

            this.AddReference(new Reference { Uri = String.Format("#{0}", this.KeyInfo.Id) });

            this.SetQualifyingPropertiesObject(certificate);

            return this;
        }

        public Reference SetSignatureTransformReference()
        {
            Reference reference = new Reference(String.Empty);

            reference.Id = XsdSchemas.FormatId("Reference", "ID-");
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());

            this.AddReference(reference);

            return reference;
        }

        #endregion

        #region · XAdES Methods ·

        private XAdESSignedXml SetQualifyingPropertiesObject(X509Certificate2 certificate)
        {
            var qualifyingProperties      = CreateQualifyingProperties();
            var signedProperties          = qualifyingProperties.CreateSignedProperties(this);
            var signedSignatureProperties = signedProperties.CreateSignedSignatureProperties();

            signedSignatureProperties.SetSigningTime()
                                     .SetSignerRole(this.signerRole)
                                     .SetSigningCertificate(certificate)
                                     .SetSignaturePolicyIdentifier();

            return this.SetSignedDataObjectProperties(signedProperties)
                       .SetSignatureDataObject(qualifyingProperties)
                       .SetSignedPropertiesReference(signedProperties);
        }

        private XAdESSignedXml SetSignedPropertiesReference(SignedPropertiesType signedProperties)
        {            
            var reference = new Reference
            {
                Id   = XsdSchemas.FormatId("SignedPropertiesID")
              , Uri  = String.Format("#{0}", signedProperties.Id)
              , Type = "http://uri.etsi.org/01903#SignedProperties"
            };

            this.AddReference(reference);

            return this;
        }

        private XAdESSignedXml SetSignatureDataObject(QualifyingPropertiesType qualifyingProperties)
        {
            var document   = qualifyingProperties.ToXmlDocument();
            var nsMgr      = XsdSchemas.CreateXadesNamespaceManager(document);
            var dataObject = new DataObject();

            dataObject.Id = XsdSchemas.FormatId(this.Signature.Id, "Object");
            dataObject.Data = document.DocumentElement.SelectNodes(".", nsMgr);

            this.AddObject(dataObject);

            return this;
        }

        private XAdESSignedXml SetSignedDataObjectProperties(SignedPropertiesType signedProperties)
        {
            var transformReference = this.SetSignatureTransformReference();

            signedProperties.SignedDataObjectProperties = new SignedDataObjectPropertiesType
            {
                DataObjectFormat = new DataObjectFormatType[]
                {
                    new DataObjectFormatType
                    {
                        Description     = "Description"
                      , MimeType        = "text/xml"
                      , ObjectReference = "#" + transformReference.Id
                    }
                }
            };

            return this;
        }

        private QualifyingPropertiesType CreateQualifyingProperties()
        {
            return new QualifyingPropertiesType
            {
                Target = String.Format("#{0}", this.Signature.Id)
            };
        }

        #endregion
    }
}
