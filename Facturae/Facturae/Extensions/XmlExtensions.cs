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

using System.Xml;

namespace FacturaE.Extensions
{
    /// <summary>
    /// XML extensions
    /// </summary>
    internal static class XmlExtensions
    {
        #region · Methods ·

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

        #endregion

        #region · Private Methods ·

        private static XmlElement FindNode(this XmlNode node, string attributeName, string value)
        {
            var attributeValueInNode = node.GetAttributeValueInNodeOrNull(attributeName);
            
            if (attributeValueInNode != null && attributeValueInNode.Equals(value))
            {
                return (XmlElement)node;
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

        #endregion
    }
}