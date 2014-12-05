/* nFacturae - The MIT License (MIT)
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

using nFacturae.Extensions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace nFacturae.Xml
{
    /// <summary>
    /// Custom <see cref="SignedXml"/> implementation.
    /// </summary>
    public sealed class XaDESSignedXml
        : SignedXml
    {
        #region · Fields ·

        private readonly List<DataObject> dataObjects = new List<DataObject>();

        #endregion

        #region · Constructors ·

        /// <summary>
        /// Initializes a new instance of the <see cref="XaDESSignedXml"/> class.
        /// </summary>
        public XaDESSignedXml()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XaDESSignedXml"/> class
        /// with the give <see cref="XmlDocument"/>
        /// </summary>
        /// <param name="document">An instance of <see cref="XmlDocument"/></param>
        public XaDESSignedXml(XmlDocument document)
            : base(document)
        {
        }

        #endregion

        #region · Methods Methods ·

        /// <summary>
        /// Gets the id element.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override XmlElement GetIdElement(XmlDocument doc, string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return null;
            }

            var xmlElement = base.GetIdElement(doc, id);

            if (xmlElement != null)
            {
                return XsdSchemas.FixupNamespaces(doc, xmlElement);
            }

            if (dataObjects.Count > 0)
            {
                if (this.dataObjects != null && this.dataObjects.Count > 0)
                {
                    foreach (DataObject dataObject in this.dataObjects)
                    {
                        var nodeWithSameId = dataObject.GetXml().SelectNodes(".").FindNode("Id", id);

                        if (nodeWithSameId != null)
                        {
                            return XsdSchemas.FixupNamespaces(doc, nodeWithSameId);
                        }
                    }
                }                
            }

            // Search the KeyInfo Node
            if (this.KeyInfo != null)
            {
                var nodeWithSameId = this.KeyInfo.GetXml().SelectNodes(".").FindNode("Id", id);

                if (nodeWithSameId != null)
                {
                    return XsdSchemas.FixupNamespaces(doc, nodeWithSameId);
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
    }
}
