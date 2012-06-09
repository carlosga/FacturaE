/* This file is part of Facturae.
 * 
 * Facturae is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Facturae is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Facturae.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Xml;
using nFacturae.Schemas;

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
