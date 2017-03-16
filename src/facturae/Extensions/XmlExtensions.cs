// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Xml;

namespace FacturaE.Extensions
{
    /// <summary>
    /// XML extensions
    /// </summary>
    internal static class XmlExtensions
    {
        internal static XmlElement FindNode(this XmlNodeList nodeList, string attributeName, string value)
        {
            if (nodeList == null || nodeList.Count == 0)
            {
                return null;
            }

            foreach (XmlNode node in nodeList)
            {
                var nodeWithSameId = node.FindNode(attributeName, value);

                if (nodeWithSameId != null)
                {
                    return nodeWithSameId;
                }
            }

            return null;
        }

        private static XmlElement FindNode(this XmlNode node, string attributeName, string value)
        {
            var attributeValueInNode = node.GetAttributeValueInNodeOrNull(attributeName);
            
            if (attributeValueInNode != null && attributeValueInNode.Equals(value))
            {
                return node as XmlElement;
            }
            
            return node.ChildNodes.FindNode(attributeName, value);
        }

        private static string GetAttributeValueInNodeOrNull(this XmlNode node, string attributeName)
        {
            if (node.Attributes != null)
            {
                var attribute = node.Attributes[attributeName];
            
                if (attribute != null) 
                {
                    return attribute.Value;
                }
            }
            
            return null;
        }        
    }
}