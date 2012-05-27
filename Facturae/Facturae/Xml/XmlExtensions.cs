using System;
using System.Xml;
using System.Xml.Serialization;
using ElectronicInvoice.Schemas;

namespace ElectronicInvoice.Xml
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