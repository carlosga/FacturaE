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

using System.Xml;

namespace nFacturae.Extensions
{
    /// <summary>
    /// XML extensions
    /// </summary>
    internal static class XmlExtensions
    {
        #region · Methods ·
        		
        internal static XmlElement CreateNode(this XmlDocument document, string prefix, string nodeName, string nameSpace, XmlElement rootNode)
        {
            var result = document.CreateElement(prefix, nodeName, nameSpace);
            
            rootNode.AppendChild(result);

            return result;
        }

        internal static XmlElement CreateNode(this XmlDocument document, string prefix, string nodeName, string text, string nameSpace, XmlElement rootNode)
        {
            var newNode = document.CreateNode(prefix, nodeName, nameSpace, rootNode);
            
            newNode.InnerText = text;

            return newNode;
        }

        internal static XmlElement CreateNode(this XmlDocument document, string prefix, string nodeName, string attName, string attValue, string nameSpace, XmlElement rootNode)
        {			
            var newNode = document.CreateNode(prefix, nodeName, nameSpace, rootNode);            

            newNode.SetAttribute(attName, attValue);

            return newNode;
        }

        internal static XmlElement FindNode(this XmlNodeList nodeList, string attributeName, string value)
        {
            if (nodeList == null 
                || nodeList.Count == 0)
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

        #endregion

        #region · Private Methods ·

        private static XmlElement FindNode(this XmlNode node, string attributeName, string value)
        {
            var attributeValueInNode = node.GetAttributeValueInNodeOrNull(attributeName);
			
			if (attributeValueInNode != null 
				&& attributeValueInNode.Equals(value))
			{
				return (XmlElement)node;
			}
			
            return node.ChildNodes.FindNode(attributeName, value);
        }

        private static string GetAttributeValueInNodeOrNull(this XmlNode node, string attributeName)
        {
            var xmlAttributeCollection = node.Attributes;
			
            if (xmlAttributeCollection != null)
            {
                var attribute = xmlAttributeCollection[attributeName];
            
				if (attribute != null) 
				{
					return attribute.Value;
				}
            }
			
            return null;
        }        

        #endregion
    }
}