// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
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
        private static readonly string s_FacturaeNamespaceUrl   = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml";
        private static readonly string s_XmlDsigNamespaceUrl    = "http://www.w3.org/2000/09/xmldsig#";
        private static readonly string s_FacturaePrefix         = "fe";
        private static readonly string s_XmlDsigPrefix          = "ds";
        private static readonly string s_XmlDsigSchemaResource  = "FacturaE.Schemas.xmldsig-core-schema.xsd";
        private static readonly string s_FacturaeSchemaResource = "FacturaE.Schemas.Facturaev3_2_2.xsd";

        internal static readonly XmlQualifiedName[] Namespaces = new XmlQualifiedName[] 
        {
            new XmlQualifiedName(XsdSchemas.s_FacturaePrefix, XsdSchemas.s_FacturaeNamespaceUrl),
            new XmlQualifiedName(XsdSchemas.s_XmlDsigPrefix , XsdSchemas.s_XmlDsigNamespaceUrl),
        };

        internal readonly static XmlSerializerNamespaces XadesSerializerNamespaces = new XmlSerializerNamespaces(Namespaces);

        /// <summary>
        /// Formats a new identifier with the given string
        /// </summary>
        /// <param name="firstPart">First part of the identifier to be generated</param>
        /// <returns>A new identifier</returns>
        internal static string FormatId(string firstPart)
        {
            return $"{firstPart}-{DateTime.Now.ToString("yyyyMMddmmssfff", CultureInfo.InvariantCulture)}";
        }

        /// <summary>
        /// Formats a new identifier with the given strings
        /// </summary>
        /// <param name="firstPart">First part of the identifier to be generated</param>
        /// <returns>A new identifier</returns>
        internal static string FormatId(string firstPart, string secondPart)
        {
            return $"{firstPart}-{secondPart}-{DateTime.Now.ToString("yyyyMMddmmssfff", CultureInfo.InvariantCulture)}";
        }

        /// <summary>
        /// Creates a new <see cref="XmlNamespaceManager"/> with xades, dsig and facturae namespaces defined.
        /// </summary>
        /// <param name="document">The Xml Document</param>
        /// <returns>A new instance of <see cref="XmlNamespaceManager"/></returns>
        internal static XmlNamespaceManager CreateXadesNamespaceManager(XmlDocument document)
        {
            return CreateXadesNamespaceManager(document.NameTable);
        }

        internal static XmlNamespaceManager CreateXadesNamespaceManager(XmlNameTable nameTable)
        {
            var nsmgr = new XmlNamespaceManager(nameTable);
            
            nsmgr.AddNamespace(XsdSchemas.s_FacturaePrefix, XsdSchemas.s_FacturaeNamespaceUrl);
            nsmgr.AddNamespace(XsdSchemas.s_XmlDsigPrefix , XsdSchemas.s_XmlDsigNamespaceUrl);

            return nsmgr;
        }

        /// <summary>
        /// From mono SignedXml class
        /// https://github.com/mono/mono/blob/master/mcs/class/System.Security/System.Security.Cryptography.Xml/SignedXml.cs
        /// </summary>
        /// <param name="envDoc"></param>
        /// <param name="inputElement"></param>
        /// <returns></returns>
        internal static XmlElement FixupNamespaces(XmlDocument envDoc, XmlElement inputElement)
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
        internal static XmlSchemaSet BuildSchemaSet(XmlNameTable nt)
        {
            var schemaSet = new XmlSchemaSet(nt);

            schemaSet.Add(ReadSchema(s_FacturaeSchemaResource));
            schemaSet.Add(ReadSchema(s_XmlDsigSchemaResource));
            schemaSet.Compile();

            return schemaSet;
        }

        private static XmlSchema ReadSchema(string resourceName)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();

            return XmlSchema.Read(currentAssembly.GetManifestResourceStream(resourceName), SchemaSetValidationEventHandler);
        }

        private static void SchemaSetValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error)
            {
                throw e.Exception;
            }
            else
            {
                Trace.Write($"Warning while validating XML Schema '{e.Message}'");
            }
        }
    }
}
