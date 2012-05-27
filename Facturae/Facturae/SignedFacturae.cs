using System.Xml;
using ElectronicInvoice.Schemas;
using ElectronicInvoice.Xml;

namespace ElectronicInvoice
{
    public sealed class SignedFacturae
    {
        #region · Fields ·

        private XmlDocument signedDocument;

        #endregion

        #region · Constructors ·

        public SignedFacturae()
        {
        }

        public SignedFacturae(XmlDocument document)
        {
            this.signedDocument = document;
        }

        #endregion

        #region · Methods ·

        public SignedFacturae Load(string path)
        {
            this.signedDocument = new XmlDocument { PreserveWhitespace = true };
            this.signedDocument.Load(path);

            return this;
        }

        public SignedFacturae Load(XmlDocument document)
        {
            this.signedDocument = document;

            return this;
        }

        public SignedFacturae WriteToFile(string path)
        {
            this.signedDocument.Save(path);

            return this;
        }

        /// <summary>
        /// Verify the signature against an asymetric 
        /// algorithm and return the result.
        /// </summary>
        /// <param name="eInvoice"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        /// <remarks>http://social.msdn.microsoft.com/Forums/hu-HU/netfxbcl/thread/d6a4fe9f-7d2e-419c-ab19-9e57c75ba90f</remarks>
        public bool CheckSignature()
        {
            XaDESSignedXml      signedXml   = new XaDESSignedXml(this.signedDocument);
            XmlNamespaceManager nsmgr       = XsdSchemas.CreateXadesNamespaceManager(this.signedDocument);
            
            // Load the signature node.
            signedXml.LoadXml((XmlElement)this.signedDocument.SelectSingleNode("//ds:Signature", nsmgr));
            
            // Check the signature against the passed asymetric key
            // and return the result.
            return signedXml.CheckSignature();
        }

        #endregion
    }
}
