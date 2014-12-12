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

using System;
using System.Globalization;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FacturaE.Xml
{
    /// <summary>
    /// Helper class for dealing with Facturae and xmldsig Schemas
    /// </summary>
    internal static class XsdSchemas
    {
        #region · Constants ·

        internal const string FacturaeNamespaceUrl   = "http://www.facturae.es/Facturae/2009/v3.2/Facturae";
        internal const string XmlDsigNamespaceUrl    = "http://www.w3.org/2000/09/xmldsig#";
        internal const string XadesNamespaceUrl      = "http://uri.etsi.org/01903/v1.3.2#";
        internal const string FacturaePrefix         = "fe";
        internal const string XmlDsigPrefix          = "ds";
        internal const string XadesPrefix            = "";
        internal const string XmlDsigSchemaResource  = "FacturaE.Schemas.xmldsig-core-schema.xsd";
        internal const string FacturaeSchemaResource = "FacturaE.Schemas.Facturaev3_2.xsd";
        internal const string XAdESSchemaResource    = "FacturaE.Schemas.XAdES.xsd";

        #endregion

        #region · Methods ·
        
        /// <summary>
        /// Formats a new identifier with the given string
        /// </summary>
        /// <param name="firstPart">First part of the identifier to be generated</param>
        /// <returns>A new identifier</returns>
        public static string FormatId(string firstPart)
        {
            return String.Format("{0}{1}", firstPart, DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Formats a new identifier with the given strings
        /// </summary>
        /// <param name="firstPart">First part of the identifier to be generated</param>
        /// <returns>A new identifier</returns>
        public static string FormatId(string firstPart, string secondPart)
        {
            return String.Format("{0}-{1}{2}", firstPart, secondPart, DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Creates a new <see cref="XmlNamespaceManager"/> with xades, dsig and facturae namespaces defined.
        /// </summary>
        /// <param name="document">The Xml Document</param>
        /// <returns>A new instance of <see cref="XmlNamespaceManager"/></returns>
        public static XmlNamespaceManager CreateXadesNamespaceManager(XmlDocument document)
        {
            var nsmgr = new XmlNamespaceManager(document.NameTable);
            
            nsmgr.AddNamespace(XsdSchemas.FacturaePrefix, XsdSchemas.FacturaeNamespaceUrl);
            nsmgr.AddNamespace(XsdSchemas.XmlDsigPrefix , XsdSchemas.XmlDsigNamespaceUrl);
            nsmgr.AddNamespace(XsdSchemas.XadesPrefix   , XsdSchemas.XadesNamespaceUrl);

            return nsmgr;
        }

        /// <summary>
        /// Creates a new <see cref="XmlNamespaceManager"/> with desig and facturae namespaces defined.
        /// </summary>
        /// <param name="document">The Xml Document</param>
        /// <returns>A new instance of <see cref="XmlNamespaceManager"/></returns>
        public static XmlSerializerNamespaces CreateXadesSerializerNamespace()
        {
            var ns = new XmlSerializerNamespaces();

            ns.Add(XsdSchemas.FacturaePrefix, XsdSchemas.FacturaeNamespaceUrl);
            ns.Add(XsdSchemas.XmlDsigPrefix , XsdSchemas.XmlDsigNamespaceUrl);
            ns.Add(XsdSchemas.XadesPrefix   , XsdSchemas.XadesNamespaceUrl);

            return ns;
        }

        /// <summary>
        /// From mono SignedXml class
        /// https://github.com/mono/mono/blob/master/mcs/class/System.Security/System.Security.Cryptography.Xml/SignedXml.cs
        /// </summary>
        /// <param name="envDoc"></param>
        /// <param name="inputElement"></param>
        /// <returns></returns>
        public static XmlElement FixupNamespaces(XmlDocument envDoc, XmlElement inputElement)
        {
            var doc   = new XmlDocument { PreserveWhitespace = true };
            var nsmgr = XsdSchemas.CreateXadesNamespaceManager(doc);
            
            doc.LoadXml(inputElement.OuterXml);

            if (envDoc != null)
            {
                foreach (XmlAttribute attr in envDoc.DocumentElement.SelectNodes("namespace::*", nsmgr))
                {
                    if (attr.LocalName == "xml")
                    {
                        continue;
                    }

                    if (attr.LocalName == "xmlns")
                    {
                        continue;
                    }              

                    doc.DocumentElement.SetAttributeNode(doc.ImportNode(attr, true) as XmlAttribute);
                }
            }

            return doc.DocumentElement;
        }

        /// <summary>
        /// Gets the schema set built with the given xml name table.
        /// </summary>
        /// <param name="nt">The name table.</param>
        /// <returns>A instance of <see cref="XmlSchemaSet"/></returns>
        public static XmlSchemaSet BuildSchemaSet(XmlNameTable nt)
        {
            var schemaSet = new XmlSchemaSet(nt);

            schemaSet.XmlResolver = new XmlUrlResolver();
            schemaSet.Add(ReadSchema(XmlDsigSchemaResource));
            schemaSet.Add(ReadSchema(FacturaeSchemaResource));
            schemaSet.Add(ReadSchema(XAdESSchemaResource));
            schemaSet.Compile();

            return schemaSet;
        }

        #endregion

        #region · Private Methods ·

        private static XmlSchema ReadSchema(string resourceName)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            return XmlSchema.Read(currentAssembly.GetManifestResourceStream(resourceName), SchemaSetValidationEventHandler);
        }

        private static void SchemaSetValidationEventHandler(object sender, ValidationEventArgs e)
        {
        }

        #endregion
    }
}
