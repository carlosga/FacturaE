// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FacturaE.XAdES;
using FacturaE.Xml;
using System.Xml;

namespace FacturaE
{
    /// <summary>
    /// Helper class to verify signed invoices signatures.
    /// </summary>
    public sealed class XAdESSignatureVerifier
    {
        private readonly XmlDocument _signedDocument;

        /// <summary>
        /// Initializes a new instance of the <see cref="XAdESSignatureVerifier"/> class with the given <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="document">Source xml document</param>
        public XAdESSignatureVerifier(XmlDocument document)
        {
            _signedDocument = document;
        }

        /// <summary>
        /// Saves the signed XML to the given path
        /// </summary>
        /// <param name="path">The target file path</param>
        /// <returns>An instance of <see cref="SignedFacturae"/></returns>
        public XAdESSignatureVerifier WriteToFile(string path)
        {
            _signedDocument.Save(path);

            return this;
        }

        /// <summary>
        /// Verify the signature against an asymetric 
        /// algorithm and return the result.
        /// </summary>
        /// <param name="eInvoice"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        /// <remarks>http://social.msdn.microsoft.com/Forums/hu-HU/netfxbcl/thread/d6a4fe9f-7d2e-419c-ab19-9e57c75ba90f</remarks>
        public bool CheckSignature()
        {
            var signedXml = new XAdESSignedXml(_signedDocument);
            var nsmgr     = XsdSchemas.CreateXadesNamespaceManager(_signedDocument);
            
            // Load the signature node.
            signedXml.LoadXml(_signedDocument.SelectSingleNode("//ds:Signature", nsmgr) as XmlElement);
            
            // Check the signature against the passed asymetric key
            // and return the result.
            return signedXml.CheckSignature();
        }
    }
}
