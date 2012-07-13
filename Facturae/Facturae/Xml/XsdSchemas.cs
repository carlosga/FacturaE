/* This file is part of Facturae.
 * 
 * Copyright (c) 2012. Carlos Guzmán Álvarez.
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
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace nFacturae.Xml
{
    /// <summary>
    /// Helper class for dealing with Facturae and xmldsig Schemas
    /// </summary>
    internal static class XsdSchemas
    {
        #region · Constants ·

        public const string FacturaeNamespaceUrl    = "http://www.facturae.es/Facturae/2009/v3.2/Facturae";
        public const string XmlDsigNamespaceUrl     = "http://www.w3.org/2000/09/xmldsig#";
        public const string XadesNamespaceUrl       = "http://uri.etsi.org/01903/v1.3.2#";
        public const string FacturaePrefix          = "fe";
        public const string XmlDsigPrefix           = "ds";
        public const string XadesPrefix             = "";
        private const string XmlDsigSchemaResource  = "ElectronicInvoice.Schemas.xmldsig-core-schema.xsd";
        private const string FacturaeSchemaResource = "ElectronicInvoice.Schemas.Facturaev3_2.xsd";

        #endregion

        #region · Members ·

        private static readonly object SyncObject = new object();
        private static XmlSchemaSet FacturaeSchemaSet;

        #endregion

        #region · Static Constructor ·

        static XsdSchemas()
        {
        }

        #endregion

        #region · Methods ·
		
        /// <summary>
        /// Formats a new identifier with the given string
        /// </summary>
        /// <param name="firstPart">First part of the identifier to be generated</param>
        /// <returns>A new identifier</returns>
        public static string FormatId(string firstPart)
        {
            return String.Format("{0}-{1}", firstPart, DateTime.Today.ToString("yyyyMMdd"));
        }

        /// <summary>
        /// Formats a new identifier with the given strings
        /// </summary>
        /// <param name="firstPart">First part of the identifier to be generated</param>
        /// <returns>A new identifier</returns>
        public static string FormatId(string firstPart, string secondPart)
        {
            return String.Format("{0}-{1}{2}", firstPart, secondPart, DateTime.Today.ToString("yyyyMMdd"));
        }
		
        /// <summary>
        /// Converts a Date & Time to their canonical representation
        /// </summary>
        /// <param name="now">Date & Time</param>
        /// <returns>The canonical representation of the given Date & Time</returns>
        public static string DateTimeToCanonicalRepresentation(DateTime now)
        {
            return now.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }

        /// <summary>
        /// Converts the current Date & Time to their canonical representation
        /// </summary>
        /// <param name="now">Date & Time</param>
        /// <returns>The canonical representation of the given Date & Time</returns>
        public static string NowInCanonicalRepresentation()
        {
            return DateTimeToCanonicalRepresentation(DateTime.Now);
        }		
		
        /// <summary>
        /// Creates a new <see cref="XmlNamespaceManager"/> with xades, dsig and facturae namespaces defined.
        /// </summary>
        /// <param name="document">The Xml Document</param>
        /// <returns>A new instance of <see cref="XmlNamespaceManager"/></returns>
        public static XmlNamespaceManager CreateXadesNamespaceManager(XmlDocument document)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(document.NameTable);
            
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
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

            ns.Add(XsdSchemas.FacturaePrefix, XsdSchemas.FacturaeNamespaceUrl);
            ns.Add(XsdSchemas.XmlDsigPrefix , XsdSchemas.XmlDsigNamespaceUrl);

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
            XmlDocument doc = new XmlDocument { PreserveWhitespace = true };

            doc.LoadXml(inputElement.OuterXml);

            if (envDoc != null)
            {
                foreach (XmlAttribute attr in envDoc.DocumentElement.SelectNodes("namespace::*"))
                {
                    if (attr.LocalName == "xml")
                    {
                        continue;
                    }
					
                    if (attr.Prefix == doc.DocumentElement.Prefix)
                    {
                        continue;
                    }

                    doc.DocumentElement.SetAttributeNode(doc.ImportNode(attr, true) as XmlAttribute);
                }
            }

            return doc.DocumentElement;
        }		

        /// <summary>
        /// Gets the schema set.
        /// </summary>
        /// <returns>A instance of <see cref="XmlSchemaSet"/></returns>
        public static XmlSchemaSet GetSchemaSet()
        {
            return GetSchemaSet(null);
        }

        /// <summary>
        /// Gets the schema set built with the given xml name table.
        /// </summary>
        /// <param name="nt">The name table.</param>
        /// <returns>A instance of <see cref="XmlSchemaSet"/></returns>
        public static XmlSchemaSet GetSchemaSet(XmlNameTable nt)
        {
            lock (SyncObject)
            {
                if (FacturaeSchemaSet == null)
                {
                    if (nt != null)
                    {
                        FacturaeSchemaSet = new XmlSchemaSet(nt);
                    }
                    else
                    {
                        FacturaeSchemaSet = new XmlSchemaSet();
                    }

                    FacturaeSchemaSet.XmlResolver = new XmlUrlResolver();
                    FacturaeSchemaSet.Add(ReadSchema(XmlDsigSchemaResource));
                    FacturaeSchemaSet.Add(ReadSchema(FacturaeSchemaResource));
                    FacturaeSchemaSet.Compile();
                }
            }

            return FacturaeSchemaSet;
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
