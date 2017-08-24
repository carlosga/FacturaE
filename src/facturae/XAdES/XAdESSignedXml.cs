// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FacturaE.Extensions;
using FacturaE.Xml;
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
        private readonly List<DataObject> _dataObjects = new List<DataObject>();
        private ClaimedRole               _signerRole;

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

        /// <summary>
        /// Gets the id element.
        /// </summary>
        /// <param name="document">The doc.</param>
        /// <param name="idValue">The id.</param>
        /// <returns></returns>
        public override XmlElement GetIdElement(XmlDocument document, string idValue)
        {
            if (string.IsNullOrEmpty(idValue))
            {
                return null;
            }

            var xmlElement = base.GetIdElement(document, idValue);
            var nsmgr      = XsdSchemas.CreateXadesNamespaceManager(document);

            if (xmlElement != null)
            {
                return XsdSchemas.FixupNamespaces(document, xmlElement);
            }

            if (_dataObjects != null && _dataObjects.Count > 0)
            {
                foreach (DataObject dataObject in _dataObjects)
                {
                    var nodeWithSameId = dataObject.GetXml().SelectNodes(".", nsmgr).FindNode("Id", idValue);

                    if (nodeWithSameId != null)
                    {
                        return XsdSchemas.FixupNamespaces(document, nodeWithSameId);
                    }
                }
            }

            // Search the KeyInfo Node
            if (KeyInfo != null)
            {
                var nodeWithSameId = KeyInfo.GetXml().SelectNodes(".", nsmgr).FindNode("Id", idValue);

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
            _dataObjects.Add(dataObject);
        }

        public XAdESSignedXml SetSignatureInfo()
        {
            // Set nodes identifiers
            Signature.Id  = XsdSchemas.FormatId("Signature");
            SignedInfo.Id = XsdSchemas.FormatId("Signature", "SignedInfo");

            return this;
        }

        public XAdESSignedXml SetSignerRole(ClaimedRole signerRole)
        {
            _signerRole = signerRole;

            return this;
        }

        public XAdESSignedXml SetKeyInfo(X509Certificate2 certificate, RSA key)
        {
            KeyInfo = new KeyInfo { Id = XsdSchemas.FormatId("Certificate") };

            KeyInfo.AddClause(new KeyInfoX509Data(certificate));
            KeyInfo.AddClause(new RSAKeyValue(key));

            AddReference(new Reference { Uri = $"#{KeyInfo.Id}" });

            SetQualifyingPropertiesObject(certificate);

            return this;
        }

        private XAdESSignedXml SetQualifyingPropertiesObject(X509Certificate2 certificate)
        {
            var qualifyingProperties      = CreateQualifyingProperties();
            var signedProperties          = qualifyingProperties.CreateSignedProperties(this);
            var signedSignatureProperties = signedProperties.CreateSignedSignatureProperties();

            signedSignatureProperties
                .SetSigningTime()
                .SetSignerRole(_signerRole)
                .SetSigningCertificate(certificate)
                .SetSignaturePolicyIdentifier();

            return SetSignedDataObjectProperties(signedProperties)
                  .SetSignatureDataObject(qualifyingProperties)
                  .SetSignedPropertiesReference(signedProperties);
        }

        private XAdESSignedXml SetSignedPropertiesReference(SignedPropertiesType signedProperties)
        {            
            var reference = new Reference
            {
                Id   = XsdSchemas.FormatId("SignedPropertiesID")
              , Uri  = $"#{signedProperties.Id}"
              , Type = "http://uri.etsi.org/01903#SignedProperties"
            };

            AddReference(reference);

            return this;
        }

        private XAdESSignedXml SetSignatureDataObject(QualifyingPropertiesType qualifyingProperties)
        {
            var document   = qualifyingProperties.ToXmlDocument();
            var nsMgr      = XsdSchemas.CreateXadesNamespaceManager(document);
            var dataObject = new DataObject
            {
                Id   = XsdSchemas.FormatId(Signature.Id, "Object"),
                Data = document.DocumentElement.SelectNodes(".", nsMgr)
            };

            AddObject(dataObject);

            return this;
        }

        private XAdESSignedXml SetSignedDataObjectProperties(SignedPropertiesType signedProperties)
        {
            var transformReference = SetSignatureTransformReference();

            signedProperties.SignedDataObjectProperties = new SignedDataObjectPropertiesType
            {
                DataObjectFormat = new DataObjectFormatType[]
                {
                    new DataObjectFormatType
                    {
                        Description     = "Description"
                      , MimeType        = "text/xml"
                      , ObjectReference = $"#{transformReference.Id}"
                    }
                }
            };

            return this;
        }

        private Reference SetSignatureTransformReference()
        {
            var reference = new Reference(string.Empty);

            reference.Id = XsdSchemas.FormatId("Reference", "ID-");
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());

            AddReference(reference);

            return reference;
        }

        private QualifyingPropertiesType CreateQualifyingProperties()
        {
            return new QualifyingPropertiesType { Target = $"#{Signature.Id}" };
        }
    }
}
