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
        private const string FacturaeNamespaceUrl   = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae";
        private const string XmlDsigNamespaceUrl    = "http://www.w3.org/2000/09/xmldsig#";
        private const string XadesNamespaceUrl      = "http://uri.etsi.org/01903/v1.3.2#";
        private const string FacturaePrefix         = "fe";
        private const string XmlDsigPrefix          = "ds";
        private const string XadesPrefix            = "";
        private const string XmlDsigSchemaResource  = "FacturaE.Schemas.xmldsig-core-schema.xsd";
        private const string FacturaeSchemaResource = "FacturaE.Schemas.Facturaev3_2_1.xsd";
        private const string XAdESSchemaResource    = "FacturaE.Schemas.XAdES.xsd";

        internal readonly static XmlSerializerNamespaces XadesSerializerNamespaces = new XmlSerializerNamespaces
        (
            new XmlQualifiedName[] 
            {
                new XmlQualifiedName(XsdSchemas.FacturaePrefix, XsdSchemas.FacturaeNamespaceUrl),
                new XmlQualifiedName(XsdSchemas.XmlDsigPrefix , XsdSchemas.XmlDsigNamespaceUrl),
                new XmlQualifiedName(XsdSchemas.XadesPrefix   , XsdSchemas.XadesNamespaceUrl)
            }
        );

        /// <summary>
        /// Formats a new identifier with the given string
        /// </summary>
        /// <param name="firstPart">First part of the identifier to be generated</param>
        /// <returns>A new identifier</returns>
        internal static string FormatId(string firstPart)
        {
            return $"{firstPart}{DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture)}";
        }

        /// <summary>
        /// Formats a new identifier with the given strings
        /// </summary>
        /// <param name="firstPart">First part of the identifier to be generated</param>
        /// <returns>A new identifier</returns>
        internal static string FormatId(string firstPart, string secondPart)
        {
            return $"{firstPart}-{secondPart}{DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture)}";
        }

        /// <summary>
        /// Creates a new <see cref="XmlNamespaceManager"/> with xades, dsig and facturae namespaces defined.
        /// </summary>
        /// <param name="document">The Xml Document</param>
        /// <returns>A new instance of <see cref="XmlNamespaceManager"/></returns>
        internal static XmlNamespaceManager CreateXadesNamespaceManager(XmlDocument document)
        {
            var nsmgr = new XmlNamespaceManager(document.NameTable);
            
            nsmgr.AddNamespace(XsdSchemas.FacturaePrefix, XsdSchemas.FacturaeNamespaceUrl);
            nsmgr.AddNamespace(XsdSchemas.XmlDsigPrefix , XsdSchemas.XmlDsigNamespaceUrl);
            nsmgr.AddNamespace(XsdSchemas.XadesPrefix   , XsdSchemas.XadesNamespaceUrl);

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

            schemaSet.Add(ReadSchema(XmlDsigSchemaResource));
            schemaSet.Add(ReadSchema(XAdESSchemaResource));
            schemaSet.Add(ReadSchema(FacturaeSchemaResource));
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
