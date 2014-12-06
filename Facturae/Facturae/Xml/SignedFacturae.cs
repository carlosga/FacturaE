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
using System.Xml;

namespace FacturaE
{
    /// <summary>
    /// Helper class to verify signed invoices signatures.
    /// </summary>
    public sealed class SignedFacturae
    {
        #region · Fields ·

        private XmlDocument signedDocument;

        #endregion

        #region · Constructors ·

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedFacturae"/> class.
        /// </summary>
        public SignedFacturae()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedFacturae"/> class with the given <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="document">Source xml document</param>
        public SignedFacturae(XmlDocument document)
        {
            this.signedDocument = document;
        }

        #endregion

        #region · Methods ·

        /// <summary>
        /// Loads the given XML file
        /// </summary>
        /// <param name="path">The XML file path</param>
        /// <returns>An instance of <see cref="SignedFacturae"/></returns>
        public SignedFacturae Load(string path)
        {
            this.signedDocument = new XmlDocument { PreserveWhitespace = true };
            this.signedDocument.Load(path);

            return this;
        }

        /// <summary>
        /// Loads the given XML document
        /// </summary>
        /// <param name="path">The XML document</param>
        /// <returns>An instance of <see cref="SignedFacturae"/></returns>
        public SignedFacturae Load(XmlDocument document)
        {
            this.signedDocument = document;

            return this;
        }

        /// <summary>
        /// Saves the signed XML to the given path
        /// </summary>
        /// <param name="path">The target file path</param>
        /// <returns>An instance of <see cref="SignedFacturae"/></returns>
        public SignedFacturae WriteToFile(string path)
        {
            this.signedDocument.Save(path);

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
            XaDESSignedXml      signedXml = new XaDESSignedXml(this.signedDocument);
            XmlNamespaceManager nsmgr     = XsdSchemas.CreateXadesNamespaceManager(this.signedDocument);
            
            // Load the signature node.
            signedXml.LoadXml((XmlElement)this.signedDocument.SelectSingleNode("//ds:Signature", nsmgr));
            
            // Check the signature against the passed asymetric key
            // and return the result.
            return signedXml.CheckSignature();
        }

        #endregion
    }
}
