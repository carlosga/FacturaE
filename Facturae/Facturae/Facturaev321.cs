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

namespace FacturaE
{
    using FacturaE.DataType;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    [XmlRootAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae", IsNullable = false)]
    public partial class Facturae
    {

        private FileHeaderType fileHeaderField;

        private PartiesType partiesField;

        private List<InvoiceType> invoicesField;

        private ExtensionsType extensionsField;

        private SignatureType signatureField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public FileHeaderType FileHeader
        {
            get
            {
                return this.fileHeaderField;
            }
            set
            {
                this.fileHeaderField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PartiesType Parties
        {
            get
            {
                return this.partiesField;
            }
            set
            {
                this.partiesField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Invoice", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<InvoiceType> Invoices
        {
            get
            {
                return this.invoicesField;
            }
            set
            {
                this.invoicesField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ExtensionsType Extensions
        {
            get
            {
                return this.extensionsField;
            }
            set
            {
                this.extensionsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature
        {
            get
            {
                return this.signatureField;
            }
            set
            {
                this.signatureField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class FileHeaderType
    {

        private SchemaVersionType schemaVersionField;

        private ModalityType modalityField;

        private InvoiceIssuerTypeType invoiceIssuerTypeField;

        private ThirdPartyType thirdPartyField;

        private BatchType batchField;

        private FactoringAssignmentDataType factoringAssignmentDataField;

        public FileHeaderType()
        {
            this.schemaVersionField = SchemaVersionType.Item321;
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public SchemaVersionType SchemaVersion
        {
            get
            {
                return this.schemaVersionField;
            }
            set
            {
                this.schemaVersionField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ModalityType Modality
        {
            get
            {
                return this.modalityField;
            }
            set
            {
                this.modalityField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public InvoiceIssuerTypeType InvoiceIssuerType
        {
            get
            {
                return this.invoiceIssuerTypeField;
            }
            set
            {
                this.invoiceIssuerTypeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ThirdPartyType ThirdParty
        {
            get
            {
                return this.thirdPartyField;
            }
            set
            {
                this.thirdPartyField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public BatchType Batch
        {
            get
            {
                return this.batchField;
            }
            set
            {
                this.batchField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public FactoringAssignmentDataType FactoringAssignmentData
        {
            get
            {
                return this.factoringAssignmentDataField;
            }
            set
            {
                this.factoringAssignmentDataField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum SchemaVersionType
    {
        /// <remarks/>
        [XmlEnumAttribute("3.2.1")]
        Item321,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum ModalityType
    {
        /// <remarks/>
        [XmlEnumAttribute("I")]
        Single,

        /// <remarks/>
        [XmlEnumAttribute("L")]
        Batch,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum InvoiceIssuerTypeType
    {

        /// <remarks/>
        EM,

        /// <remarks/>
        RE,

        /// <remarks/>
        TE,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class ThirdPartyType
    {

        private TaxIdentificationType taxIdentificationField;

        private object itemField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public TaxIdentificationType TaxIdentification
        {
            get
            {
                return this.taxIdentificationField;
            }
            set
            {
                this.taxIdentificationField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Individual", typeof(IndividualType), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("LegalEntity", typeof(LegalEntityType), Form = XmlSchemaForm.Unqualified)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class TaxIdentificationType
    {

        private PersonTypeCodeType personTypeCodeField;

        private ResidenceTypeCodeType residenceTypeCodeField;

        private string taxIdentificationNumberField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PersonTypeCodeType PersonTypeCode
        {
            get
            {
                return this.personTypeCodeField;
            }
            set
            {
                this.personTypeCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ResidenceTypeCodeType ResidenceTypeCode
        {
            get
            {
                return this.residenceTypeCodeField;
            }
            set
            {
                this.residenceTypeCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string TaxIdentificationNumber
        {
            get
            {
                return this.taxIdentificationNumberField;
            }
            set
            {
                this.taxIdentificationNumberField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum PersonTypeCodeType
    {

        /// <remarks/>
        [XmlEnumAttribute("F")]
        Individual,

        /// <remarks/>
        [XmlEnumAttribute("J")]
        LegalEntity
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum ResidenceTypeCodeType
    {

        /// <remarks/>
        [XmlEnumAttribute("E")]
        Foreigner,

        /// <remarks/>
        [XmlEnumAttribute("R")]
        ResidentInSpain,

        /// <remarks/>
        [XmlEnumAttribute("U")]
        ResidentInEU,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("Object", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class ObjectType
    {

        private XmlNode[] anyField;

        private string idField;

        private string mimeTypeField;

        private string encodingField;

        /// <remarks/>
        [XmlTextAttribute()]
        [XmlAnyElementAttribute()]
        public XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string MimeType
        {
            get
            {
                return this.mimeTypeField;
            }
            set
            {
                this.mimeTypeField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "anyURI")]
        public string Encoding
        {
            get
            {
                return this.encodingField;
            }
            set
            {
                this.encodingField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("SPKIData", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SPKIDataType
    {

        private byte[] sPKISexpField;

        private System.Xml.XmlElement anyField;

        /// <remarks/>
        [XmlElementAttribute("SPKISexp", DataType = "base64Binary")]
        public byte[] SPKISexp
        {
            get
            {
                return this.sPKISexpField;
            }
            set
            {
                this.sPKISexpField = value;
            }
        }

        /// <remarks/>
        [XmlAnyElementAttribute()]
        public System.Xml.XmlElement Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("PGPData", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class PGPDataType
    {

        private object[] itemsField;

        private ItemsChoiceType1[] itemsElementNameField;

        /// <remarks/>
        [XmlAnyElementAttribute()]
        [XmlElementAttribute("PGPKeyID", typeof(byte[]), DataType = "base64Binary")]
        [XmlElementAttribute("PGPKeyPacket", typeof(byte[]), DataType = "base64Binary")]
        [XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("ItemsElementName")]
        [XmlIgnoreAttribute()]
        public ItemsChoiceType1[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceType1
    {

        /// <remarks/>
        [XmlEnumAttribute("##any:")]
        Item,

        /// <remarks/>
        PGPKeyID,

        /// <remarks/>
        PGPKeyPacket,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class X509IssuerSerialType
    {

        private string x509IssuerNameField;

        private string x509SerialNumberField;

        /// <remarks/>
        public string X509IssuerName
        {
            get
            {
                return this.x509IssuerNameField;
            }
            set
            {
                this.x509IssuerNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(DataType = "integer")]
        public string X509SerialNumber
        {
            get
            {
                return this.x509SerialNumberField;
            }
            set
            {
                this.x509SerialNumberField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("X509Data", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class X509DataType
    {

        private object[] itemsField;

        private ItemsChoiceType[] itemsElementNameField;

        /// <remarks/>
        [XmlAnyElementAttribute()]
        [XmlElementAttribute("X509CRL", typeof(byte[]), DataType = "base64Binary")]
        [XmlElementAttribute("X509Certificate", typeof(byte[]), DataType = "base64Binary")]
        [XmlElementAttribute("X509IssuerSerial", typeof(X509IssuerSerialType))]
        [XmlElementAttribute("X509SKI", typeof(byte[]), DataType = "base64Binary")]
        [XmlElementAttribute("X509SubjectName", typeof(string))]
        [XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("ItemsElementName")]
        [XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {

        /// <remarks/>
        [XmlEnumAttribute("##any:")]
        Item,

        /// <remarks/>
        X509CRL,

        /// <remarks/>
        X509Certificate,

        /// <remarks/>
        X509IssuerSerial,

        /// <remarks/>
        X509SKI,

        /// <remarks/>
        X509SubjectName,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("RetrievalMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class RetrievalMethodType
    {

        private List<TransformType> transformsField;

        private string uRIField;

        private string typeField;

        /// <remarks/>
        [XmlArrayItemAttribute("Transform", IsNullable = false)]
        public List<TransformType> Transforms
        {
            get
            {
                return this.transformsField;
            }
            set
            {
                this.transformsField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "anyURI")]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "anyURI")]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("Transform", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class TransformType
    {

        private object[] itemsField;

        private string[] textField;

        private string algorithmField;

        /// <remarks/>
        [XmlAnyElementAttribute()]
        [XmlElementAttribute("XPath", typeof(string))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("RSAKeyValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class RSAKeyValueType
    {

        private byte[] modulusField;

        private byte[] exponentField;

        /// <remarks/>
        [XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Modulus
        {
            get
            {
                return this.modulusField;
            }
            set
            {
                this.modulusField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Exponent
        {
            get
            {
                return this.exponentField;
            }
            set
            {
                this.exponentField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("DSAKeyValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class DSAKeyValueType
    {

        private byte[] pField;

        private byte[] qField;

        private byte[] gField;

        private byte[] yField;

        private byte[] jField;

        private byte[] seedField;

        private byte[] pgenCounterField;

        /// <remarks/>
        [XmlElementAttribute(DataType = "base64Binary")]
        public byte[] P
        {
            get
            {
                return this.pField;
            }
            set
            {
                this.pField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Q
        {
            get
            {
                return this.qField;
            }
            set
            {
                this.qField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(DataType = "base64Binary")]
        public byte[] G
        {
            get
            {
                return this.gField;
            }
            set
            {
                this.gField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(DataType = "base64Binary")]
        public byte[] J
        {
            get
            {
                return this.jField;
            }
            set
            {
                this.jField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Seed
        {
            get
            {
                return this.seedField;
            }
            set
            {
                this.seedField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(DataType = "base64Binary")]
        public byte[] PgenCounter
        {
            get
            {
                return this.pgenCounterField;
            }
            set
            {
                this.pgenCounterField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("KeyValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class KeyValueType
    {

        private object itemField;

        private List<string> textField;

        /// <remarks/>
        [XmlAnyElementAttribute()]
        [XmlElementAttribute("DSAKeyValue", typeof(DSAKeyValueType))]
        [XmlElementAttribute("RSAKeyValue", typeof(RSAKeyValueType))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [XmlTextAttribute()]
        public List<string> Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("KeyInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class KeyInfoType
    {

        private object[] itemsField;

        private ItemsChoiceType2[] itemsElementNameField;

        private string[] textField;

        private string idField;

        /// <remarks/>
        [XmlAnyElementAttribute()]
        [XmlElementAttribute("KeyName", typeof(string))]
        [XmlElementAttribute("KeyValue", typeof(KeyValueType))]
        [XmlElementAttribute("MgmtData", typeof(string))]
        [XmlElementAttribute("PGPData", typeof(PGPDataType))]
        [XmlElementAttribute("RetrievalMethod", typeof(RetrievalMethodType))]
        [XmlElementAttribute("SPKIData", typeof(SPKIDataType))]
        [XmlElementAttribute("X509Data", typeof(X509DataType))]
        [XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("ItemsElementName")]
        [XmlIgnoreAttribute()]
        public ItemsChoiceType2[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }

        /// <remarks/>
        [XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceType2
    {

        /// <remarks/>
        [XmlEnumAttribute("##any:")]
        Item,

        /// <remarks/>
        KeyName,

        /// <remarks/>
        KeyValue,

        /// <remarks/>
        MgmtData,

        /// <remarks/>
        PGPData,

        /// <remarks/>
        RetrievalMethod,

        /// <remarks/>
        SPKIData,

        /// <remarks/>
        X509Data,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("SignatureValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignatureValueType
    {

        private string idField;

        private byte[] valueField;

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [XmlTextAttribute(DataType = "base64Binary")]
        public byte[] Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("DigestMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class DigestMethodType
    {

        private XmlNode[] anyField;

        private string algorithmField;

        /// <remarks/>
        [XmlTextAttribute()]
        [XmlAnyElementAttribute()]
        public XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class ReferenceType
    {

        private List<TransformType> transformsField;

        private DigestMethodType digestMethodField;

        private byte[] digestValueField;

        private string idField;

        private string uRIField;

        private string typeField;

        /// <remarks/>
        [XmlArrayItemAttribute("Transform", IsNullable = false)]
        public List<TransformType> Transforms
        {
            get
            {
                return this.transformsField;
            }
            set
            {
                this.transformsField = value;
            }
        }

        /// <remarks/>
        public DigestMethodType DigestMethod
        {
            get
            {
                return this.digestMethodField;
            }
            set
            {
                this.digestMethodField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(DataType = "base64Binary")]
        public byte[] DigestValue
        {
            get
            {
                return this.digestValueField;
            }
            set
            {
                this.digestValueField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "anyURI")]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "anyURI")]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("SignatureMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignatureMethodType
    {

        private string hMACOutputLengthField;

        private XmlNode[] anyField;

        private string algorithmField;

        /// <remarks/>
        [XmlElementAttribute(DataType = "integer")]
        public string HMACOutputLength
        {
            get
            {
                return this.hMACOutputLengthField;
            }
            set
            {
                this.hMACOutputLengthField = value;
            }
        }

        /// <remarks/>
        [XmlTextAttribute()]
        [XmlAnyElementAttribute()]
        public XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("CanonicalizationMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class CanonicalizationMethodType
    {

        private XmlNode[] anyField;

        private string algorithmField;

        /// <remarks/>
        [XmlTextAttribute()]
        [XmlAnyElementAttribute()]
        public XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("SignedInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignedInfoType
    {

        private CanonicalizationMethodType canonicalizationMethodField;

        private SignatureMethodType signatureMethodField;

        private List<ReferenceType> referenceField;

        private string idField;

        /// <remarks/>
        public CanonicalizationMethodType CanonicalizationMethod
        {
            get
            {
                return this.canonicalizationMethodField;
            }
            set
            {
                this.canonicalizationMethodField = value;
            }
        }

        /// <remarks/>
        public SignatureMethodType SignatureMethod
        {
            get
            {
                return this.signatureMethodField;
            }
            set
            {
                this.signatureMethodField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Reference")]
        public List<ReferenceType> Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignatureType
    {

        private SignedInfoType signedInfoField;

        private SignatureValueType signatureValueField;

        private KeyInfoType keyInfoField;

        private List<ObjectType> objectField;

        private string idField;

        /// <remarks/>
        public SignedInfoType SignedInfo
        {
            get
            {
                return this.signedInfoField;
            }
            set
            {
                this.signedInfoField = value;
            }
        }

        /// <remarks/>
        public SignatureValueType SignatureValue
        {
            get
            {
                return this.signatureValueField;
            }
            set
            {
                this.signatureValueField = value;
            }
        }

        /// <remarks/>
        public KeyInfoType KeyInfo
        {
            get
            {
                return this.keyInfoField;
            }
            set
            {
                this.keyInfoField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Object")]
        public List<ObjectType> Object
        {
            get
            {
                return this.objectField;
            }
            set
            {
                this.objectField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class AttachmentType
    {

        private AttachmentCompressionAlgorithmType attachmentCompressionAlgorithmField;

        private bool attachmentCompressionAlgorithmFieldSpecified;

        private AttachmentFormatType attachmentFormatField;

        private AttachmentEncodingType attachmentEncodingField;

        private bool attachmentEncodingFieldSpecified;

        private string attachmentDescriptionField;

        private string attachmentDataField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AttachmentCompressionAlgorithmType AttachmentCompressionAlgorithm
        {
            get
            {
                return this.attachmentCompressionAlgorithmField;
            }
            set
            {
                this.attachmentCompressionAlgorithmField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool AttachmentCompressionAlgorithmSpecified
        {
            get
            {
                return this.attachmentCompressionAlgorithmFieldSpecified;
            }
            set
            {
                this.attachmentCompressionAlgorithmFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AttachmentFormatType AttachmentFormat
        {
            get
            {
                return this.attachmentFormatField;
            }
            set
            {
                this.attachmentFormatField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AttachmentEncodingType AttachmentEncoding
        {
            get
            {
                return this.attachmentEncodingField;
            }
            set
            {
                this.attachmentEncodingField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool AttachmentEncodingSpecified
        {
            get
            {
                return this.attachmentEncodingFieldSpecified;
            }
            set
            {
                this.attachmentEncodingFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string AttachmentDescription
        {
            get
            {
                return this.attachmentDescriptionField;
            }
            set
            {
                this.attachmentDescriptionField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string AttachmentData
        {
            get
            {
                return this.attachmentDataField;
            }
            set
            {
                this.attachmentDataField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum AttachmentCompressionAlgorithmType
    {

        /// <remarks/>
        ZIP,

        /// <remarks/>
        GZIP,

        /// <remarks/>
        NONE,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum AttachmentFormatType
    {

        /// <remarks/>
        xml,

        /// <remarks/>
        doc,

        /// <remarks/>
        gif,

        /// <remarks/>
        rtf,

        /// <remarks/>
        pdf,

        /// <remarks/>
        xls,

        /// <remarks/>
        jpg,

        /// <remarks/>
        bmp,

        /// <remarks/>
        tiff,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum AttachmentEncodingType
    {

        /// <remarks/>
        BASE64,

        /// <remarks/>
        BER,

        /// <remarks/>
        DER,

        /// <remarks/>
        NONE,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class AdditionalDataType
    {

        private string relatedInvoiceField;

        private List<AttachmentType> relatedDocumentsField;

        private string invoiceAdditionalInformationField;

        private ExtensionsType extensionsField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string RelatedInvoice
        {
            get
            {
                return this.relatedInvoiceField;
            }
            set
            {
                this.relatedInvoiceField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Attachment", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<AttachmentType> RelatedDocuments
        {
            get
            {
                return this.relatedDocumentsField;
            }
            set
            {
                this.relatedDocumentsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceAdditionalInformation
        {
            get
            {
                return this.invoiceAdditionalInformationField;
            }
            set
            {
                this.invoiceAdditionalInformationField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ExtensionsType Extensions
        {
            get
            {
                return this.extensionsField;
            }
            set
            {
                this.extensionsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class ExtensionsType
    {

        private List<XmlElement> anyField;

        /// <remarks/>
        [XmlAnyElementAttribute()]
        public List<XmlElement> Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class SpecialTaxableEventType
    {

        private SpecialTaxableEventCodeType specialTaxableEventCodeField;

        private string specialTaxableEventReasonField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public SpecialTaxableEventCodeType SpecialTaxableEventCode
        {
            get
            {
                return this.specialTaxableEventCodeField;
            }
            set
            {
                this.specialTaxableEventCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string SpecialTaxableEventReason
        {
            get
            {
                return this.specialTaxableEventReasonField;
            }
            set
            {
                this.specialTaxableEventReasonField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum SpecialTaxableEventCodeType
    {

        /// <remarks/>
        [XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [XmlEnumAttribute("02")]
        Item02,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class DeliveryNoteType
    {

        private string deliveryNoteNumberField;

        private System.DateTime deliveryNoteDateField;

        private bool deliveryNoteDateFieldSpecified;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string DeliveryNoteNumber
        {
            get
            {
                return this.deliveryNoteNumberField;
            }
            set
            {
                this.deliveryNoteNumberField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime DeliveryNoteDate
        {
            get
            {
                return this.deliveryNoteDateField;
            }
            set
            {
                this.deliveryNoteDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DeliveryNoteDateSpecified
        {
            get
            {
                return this.deliveryNoteDateFieldSpecified;
            }
            set
            {
                this.deliveryNoteDateFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class InvoiceLineType
    {

        private string issuerContractReferenceField;

        private System.DateTime issuerContractDateField;

        private bool issuerContractDateFieldSpecified;

        private string issuerTransactionReferenceField;

        private System.DateTime issuerTransactionDateField;

        private bool issuerTransactionDateFieldSpecified;

        private string receiverContractReferenceField;

        private System.DateTime receiverContractDateField;

        private bool receiverContractDateFieldSpecified;

        private string receiverTransactionReferenceField;

        private System.DateTime receiverTransactionDateField;

        private bool receiverTransactionDateFieldSpecified;

        private string fileReferenceField;

        private System.DateTime fileDateField;

        private bool fileDateFieldSpecified;

        private double sequenceNumberField;

        private bool sequenceNumberFieldSpecified;

        private List<DeliveryNoteType> deliveryNotesReferencesField;

        private string itemDescriptionField;

        private double quantityField;

        private UnitOfMeasureType unitOfMeasureField;

        private bool unitOfMeasureFieldSpecified;

        private DoubleUpToEightDecimalType unitPriceWithoutTaxField;

        private DoubleUpToEightDecimalType totalCostField;

        private List<DiscountType> discountsAndRebatesField;

        private List<ChargeType> chargesField;

        private DoubleUpToEightDecimalType grossAmountField;

        private List<TaxType> taxesWithheldField;

        private List<InvoiceLineTypeTax> taxesOutputsField;

        private PeriodDates lineItemPeriodField;

        private System.DateTime transactionDateField;

        private bool transactionDateFieldSpecified;

        private string additionalLineItemInformationField;

        private SpecialTaxableEventType specialTaxableEventField;

        private string articleCodeField;

        private ExtensionsType extensionsField;
        internal InvoiceType Parent
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string IssuerContractReference
        {
            get
            {
                return this.issuerContractReferenceField;
            }
            set
            {
                this.issuerContractReferenceField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime IssuerContractDate
        {
            get
            {
                return this.issuerContractDateField;
            }
            set
            {
                this.issuerContractDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool IssuerContractDateSpecified
        {
            get
            {
                return this.issuerContractDateFieldSpecified;
            }
            set
            {
                this.issuerContractDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string IssuerTransactionReference
        {
            get
            {
                return this.issuerTransactionReferenceField;
            }
            set
            {
                this.issuerTransactionReferenceField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime IssuerTransactionDate
        {
            get
            {
                return this.issuerTransactionDateField;
            }
            set
            {
                this.issuerTransactionDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool IssuerTransactionDateSpecified
        {
            get
            {
                return this.issuerTransactionDateFieldSpecified;
            }
            set
            {
                this.issuerTransactionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ReceiverContractReference
        {
            get
            {
                return this.receiverContractReferenceField;
            }
            set
            {
                this.receiverContractReferenceField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime ReceiverContractDate
        {
            get
            {
                return this.receiverContractDateField;
            }
            set
            {
                this.receiverContractDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool ReceiverContractDateSpecified
        {
            get
            {
                return this.receiverContractDateFieldSpecified;
            }
            set
            {
                this.receiverContractDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ReceiverTransactionReference
        {
            get
            {
                return this.receiverTransactionReferenceField;
            }
            set
            {
                this.receiverTransactionReferenceField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime ReceiverTransactionDate
        {
            get
            {
                return this.receiverTransactionDateField;
            }
            set
            {
                this.receiverTransactionDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool ReceiverTransactionDateSpecified
        {
            get
            {
                return this.receiverTransactionDateFieldSpecified;
            }
            set
            {
                this.receiverTransactionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string FileReference
        {
            get
            {
                return this.fileReferenceField;
            }
            set
            {
                this.fileReferenceField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime FileDate
        {
            get
            {
                return this.fileDateField;
            }
            set
            {
                this.fileDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool FileDateSpecified
        {
            get
            {
                return this.fileDateFieldSpecified;
            }
            set
            {
                this.fileDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public double SequenceNumber
        {
            get
            {
                return this.sequenceNumberField;
            }
            set
            {
                this.sequenceNumberField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool SequenceNumberSpecified
        {
            get
            {
                return this.sequenceNumberFieldSpecified;
            }
            set
            {
                this.sequenceNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("DeliveryNote", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<DeliveryNoteType> DeliveryNotesReferences
        {
            get
            {
                return this.deliveryNotesReferencesField;
            }
            set
            {
                this.deliveryNotesReferencesField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ItemDescription
        {
            get
            {
                return this.itemDescriptionField;
            }
            set
            {
                this.itemDescriptionField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public double Quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public UnitOfMeasureType UnitOfMeasure
        {
            get
            {
                return this.unitOfMeasureField;
            }
            set
            {
                this.unitOfMeasureField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool UnitOfMeasureSpecified
        {
            get
            {
                return this.unitOfMeasureFieldSpecified;
            }
            set
            {
                this.unitOfMeasureFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType UnitPriceWithoutTax
        {
            get
            {
                return this.unitPriceWithoutTaxField;
            }
            set
            {
                this.unitPriceWithoutTaxField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalCost
        {
            get
            {
                return this.totalCostField;
            }
            set
            {
                this.totalCostField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Discount", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<DiscountType> DiscountsAndRebates
        {
            get
            {
                return this.discountsAndRebatesField;
            }
            set
            {
                this.discountsAndRebatesField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Charge", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<ChargeType> Charges
        {
            get
            {
                return this.chargesField;
            }
            set
            {
                this.chargesField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType GrossAmount
        {
            get
            {
                return this.grossAmountField;
            }
            set
            {
                this.grossAmountField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Tax", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<TaxType> TaxesWithheld
        {
            get
            {
                return this.taxesWithheldField;
            }
            set
            {
                this.taxesWithheldField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Tax", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<InvoiceLineTypeTax> TaxesOutputs
        {
            get
            {
                return this.taxesOutputsField;
            }
            set
            {
                this.taxesOutputsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PeriodDates LineItemPeriod
        {
            get
            {
                return this.lineItemPeriodField;
            }
            set
            {
                this.lineItemPeriodField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime TransactionDate
        {
            get
            {
                return this.transactionDateField;
            }
            set
            {
                this.transactionDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TransactionDateSpecified
        {
            get
            {
                return this.transactionDateFieldSpecified;
            }
            set
            {
                this.transactionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string AdditionalLineItemInformation
        {
            get
            {
                return this.additionalLineItemInformationField;
            }
            set
            {
                this.additionalLineItemInformationField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public SpecialTaxableEventType SpecialTaxableEvent
        {
            get
            {
                return this.specialTaxableEventField;
            }
            set
            {
                this.specialTaxableEventField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ArticleCode
        {
            get
            {
                return this.articleCodeField;
            }
            set
            {
                this.articleCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ExtensionsType Extensions
        {
            get
            {
                return this.extensionsField;
            }
            set
            {
                this.extensionsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum UnitOfMeasureType
    {

        /// <remarks/>
        [XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [XmlEnumAttribute("07")]
        Item07,

        /// <remarks/>
        [XmlEnumAttribute("08")]
        Item08,

        /// <remarks/>
        [XmlEnumAttribute("09")]
        Item09,

        /// <remarks/>
        [XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [XmlEnumAttribute("12")]
        Item12,

        /// <remarks/>
        [XmlEnumAttribute("13")]
        Item13,

        /// <remarks/>
        [XmlEnumAttribute("14")]
        Item14,

        /// <remarks/>
        [XmlEnumAttribute("15")]
        Item15,

        /// <remarks/>
        [XmlEnumAttribute("16")]
        Item16,

        /// <remarks/>
        [XmlEnumAttribute("17")]
        Item17,

        /// <remarks/>
        [XmlEnumAttribute("18")]
        Item18,

        /// <remarks/>
        [XmlEnumAttribute("19")]
        Item19,

        /// <remarks/>
        [XmlEnumAttribute("20")]
        Item20,

        /// <remarks/>
        [XmlEnumAttribute("21")]
        Item21,

        /// <remarks/>
        [XmlEnumAttribute("22")]
        Item22,

        /// <remarks/>
        [XmlEnumAttribute("23")]
        Item23,

        /// <remarks/>
        [XmlEnumAttribute("24")]
        Item24,

        /// <remarks/>
        [XmlEnumAttribute("25")]
        Item25,

        /// <remarks/>
        [XmlEnumAttribute("26")]
        Item26,

        /// <remarks/>
        [XmlEnumAttribute("27")]
        Item27,

        /// <remarks/>
        [XmlEnumAttribute("28")]
        Item28,

        /// <remarks/>
        [XmlEnumAttribute("29")]
        Item29,

        /// <remarks/>
        [XmlEnumAttribute("30")]
        Item30,

        /// <remarks/>
        [XmlEnumAttribute("31")]
        Item31,

        /// <remarks/>
        [XmlEnumAttribute("32")]
        Item32,

        /// <remarks/>
        [XmlEnumAttribute("33")]
        Item33,

        /// <remarks/>
        [XmlEnumAttribute("34")]
        Item34,

        /// <remarks/>
        [XmlEnumAttribute("35")]
        Item35,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class DiscountType
    {

        private string discountReasonField;

        private DoubleUpToEightDecimalType discountRateField;

        private bool discountRateFieldSpecified;

        private DoubleUpToEightDecimalType discountAmountField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string DiscountReason
        {
            get
            {
                return this.discountReasonField;
            }
            set
            {
                this.discountReasonField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType DiscountRate
        {
            get
            {
                return this.discountRateField;
            }
            set
            {
                this.discountRateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DiscountRateSpecified
        {
            get
            {
                return this.discountRateFieldSpecified;
            }
            set
            {
                this.discountRateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType DiscountAmount
        {
            get
            {
                return this.discountAmountField;
            }
            set
            {
                this.discountAmountField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class ChargeType
    {

        private string chargeReasonField;

        private DoubleUpToEightDecimalType chargeRateField;

        private bool chargeRateFieldSpecified;

        private DoubleUpToEightDecimalType chargeAmountField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ChargeReason
        {
            get
            {
                return this.chargeReasonField;
            }
            set
            {
                this.chargeReasonField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType ChargeRate
        {
            get
            {
                return this.chargeRateField;
            }
            set
            {
                this.chargeRateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool ChargeRateSpecified
        {
            get
            {
                return this.chargeRateFieldSpecified;
            }
            set
            {
                this.chargeRateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType ChargeAmount
        {
            get
            {
                return this.chargeAmountField;
            }
            set
            {
                this.chargeAmountField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class TaxType
    {

        private TaxTypeCodeType taxTypeCodeField;

        private DoubleUpToEightDecimalType taxRateField;

        private AmountType taxableBaseField;

        private AmountType taxAmountField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public TaxTypeCodeType TaxTypeCode
        {
            get
            {
                return this.taxTypeCodeField;
            }
            set
            {
                this.taxTypeCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TaxRate
        {
            get
            {
                return this.taxRateField;
            }
            set
            {
                this.taxRateField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType TaxableBase
        {
            get
            {
                return this.taxableBaseField;
            }
            set
            {
                this.taxableBaseField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType TaxAmount
        {
            get
            {
                return this.taxAmountField;
            }
            set
            {
                this.taxAmountField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum TaxTypeCodeType
    {

        /// <remarks/>
        [XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [XmlEnumAttribute("07")]
        Item07,

        /// <remarks/>
        [XmlEnumAttribute("08")]
        Item08,

        /// <remarks/>
        [XmlEnumAttribute("09")]
        Item09,

        /// <remarks/>
        [XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [XmlEnumAttribute("12")]
        Item12,

        /// <remarks/>
        [XmlEnumAttribute("13")]
        Item13,

        /// <remarks/>
        [XmlEnumAttribute("14")]
        Item14,

        /// <remarks/>
        [XmlEnumAttribute("15")]
        Item15,

        /// <remarks/>
        [XmlEnumAttribute("16")]
        Item16,

        /// <remarks/>
        [XmlEnumAttribute("17")]
        Item17,

        /// <remarks/>
        [XmlEnumAttribute("18")]
        Item18,

        /// <remarks/>
        [XmlEnumAttribute("19")]
        Item19,

        /// <remarks/>
        [XmlEnumAttribute("20")]
        Item20,

        /// <remarks/>
        [XmlEnumAttribute("21")]
        Item21,

        /// <remarks/>
        [XmlEnumAttribute("22")]
        Item22,

        /// <remarks/>
        [XmlEnumAttribute("23")]
        Item23,

        /// <remarks/>
        [XmlEnumAttribute("24")]
        Item24,

        /// <remarks/>
        [XmlEnumAttribute("25")]
        Item25,

        /// <remarks/>
        [XmlEnumAttribute("26")]
        Item26,

        /// <remarks/>
        [XmlEnumAttribute("27")]
        Item27,

        /// <remarks/>
        [XmlEnumAttribute("28")]
        Item28,

        /// <remarks/>
        [XmlEnumAttribute("29")]
        Item29,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class AmountType
    {

        private DoubleUpToEightDecimalType totalAmountField;

        private DoubleTwoDecimalType equivalentInEurosField;

        private bool equivalentInEurosFieldSpecified;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalAmount
        {
            get
            {
                return this.totalAmountField;
            }
            set
            {
                this.totalAmountField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleTwoDecimalType EquivalentInEuros
        {
            get
            {
                return this.equivalentInEurosField;
            }
            set
            {
                this.equivalentInEurosField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool EquivalentInEurosSpecified
        {
            get
            {
                return this.equivalentInEurosFieldSpecified;
            }
            set
            {
                this.equivalentInEurosFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class InvoiceLineTypeTax : TaxOutputType
    {
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class TaxOutputType
    {

        private TaxTypeCodeType taxTypeCodeField;

        private DoubleUpToEightDecimalType taxRateField;

        private AmountType taxableBaseField;

        private AmountType taxAmountField;

        private AmountType specialTaxableBaseField;

        private AmountType specialTaxAmountField;

        private DoubleTwoDecimalType equivalenceSurchargeField;

        private bool equivalenceSurchargeFieldSpecified;

        private AmountType equivalenceSurchargeAmountField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public TaxTypeCodeType TaxTypeCode
        {
            get
            {
                return this.taxTypeCodeField;
            }
            set
            {
                this.taxTypeCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TaxRate
        {
            get
            {
                return this.taxRateField;
            }
            set
            {
                this.taxRateField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType TaxableBase
        {
            get
            {
                return this.taxableBaseField;
            }
            set
            {
                this.taxableBaseField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType TaxAmount
        {
            get
            {
                return this.taxAmountField;
            }
            set
            {
                this.taxAmountField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType SpecialTaxableBase
        {
            get
            {
                return this.specialTaxableBaseField;
            }
            set
            {
                this.specialTaxableBaseField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType SpecialTaxAmount
        {
            get
            {
                return this.specialTaxAmountField;
            }
            set
            {
                this.specialTaxAmountField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleTwoDecimalType EquivalenceSurcharge
        {
            get
            {
                return this.equivalenceSurchargeField;
            }
            set
            {
                this.equivalenceSurchargeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool EquivalenceSurchargeSpecified
        {
            get
            {
                return this.equivalenceSurchargeFieldSpecified;
            }
            set
            {
                this.equivalenceSurchargeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType EquivalenceSurchargeAmount
        {
            get
            {
                return this.equivalenceSurchargeAmountField;
            }
            set
            {
                this.equivalenceSurchargeAmountField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class PeriodDates
    {

        private System.DateTime startDateField;

        private System.DateTime endDateField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime StartDate
        {
            get
            {
                return this.startDateField;
            }
            set
            {
                this.startDateField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime EndDate
        {
            get
            {
                return this.endDateField;
            }
            set
            {
                this.endDateField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class AmountsWithheldType
    {

        private string withholdingReasonField;

        private DoubleUpToEightDecimalType withholdingRateField;

        private bool withholdingRateFieldSpecified;

        private DoubleUpToEightDecimalType withholdingAmountField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string WithholdingReason
        {
            get
            {
                return this.withholdingReasonField;
            }
            set
            {
                this.withholdingReasonField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType WithholdingRate
        {
            get
            {
                return this.withholdingRateField;
            }
            set
            {
                this.withholdingRateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool WithholdingRateSpecified
        {
            get
            {
                return this.withholdingRateFieldSpecified;
            }
            set
            {
                this.withholdingRateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType WithholdingAmount
        {
            get
            {
                return this.withholdingAmountField;
            }
            set
            {
                this.withholdingAmountField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class ReimbursableExpensesType
    {

        private TaxIdentificationType reimbursableExpensesSellerPartyField;

        private TaxIdentificationType reimbursableExpensesBuyerPartyField;

        private System.DateTime issueDateField;

        private bool issueDateFieldSpecified;

        private string invoiceNumberField;

        private string invoiceSeriesCodeField;

        private DoubleUpToEightDecimalType reimbursableExpensesAmountField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public TaxIdentificationType ReimbursableExpensesSellerParty
        {
            get
            {
                return this.reimbursableExpensesSellerPartyField;
            }
            set
            {
                this.reimbursableExpensesSellerPartyField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public TaxIdentificationType ReimbursableExpensesBuyerParty
        {
            get
            {
                return this.reimbursableExpensesBuyerPartyField;
            }
            set
            {
                this.reimbursableExpensesBuyerPartyField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime IssueDate
        {
            get
            {
                return this.issueDateField;
            }
            set
            {
                this.issueDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool IssueDateSpecified
        {
            get
            {
                return this.issueDateFieldSpecified;
            }
            set
            {
                this.issueDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceNumber
        {
            get
            {
                return this.invoiceNumberField;
            }
            set
            {
                this.invoiceNumberField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceSeriesCode
        {
            get
            {
                return this.invoiceSeriesCodeField;
            }
            set
            {
                this.invoiceSeriesCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType ReimbursableExpensesAmount
        {
            get
            {
                return this.reimbursableExpensesAmountField;
            }
            set
            {
                this.reimbursableExpensesAmountField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class PaymentOnAccountType
    {

        private System.DateTime paymentOnAccountDateField;

        private DoubleUpToEightDecimalType paymentOnAccountAmountField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime PaymentOnAccountDate
        {
            get
            {
                return this.paymentOnAccountDateField;
            }
            set
            {
                this.paymentOnAccountDateField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType PaymentOnAccountAmount
        {
            get
            {
                return this.paymentOnAccountAmountField;
            }
            set
            {
                this.paymentOnAccountAmountField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class SubsidyType
    {

        private string subsidyDescriptionField;

        private DoubleUpToEightDecimalType subsidyRateField;

        private bool subsidyRateFieldSpecified;

        private DoubleUpToEightDecimalType subsidyAmountField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string SubsidyDescription
        {
            get
            {
                return this.subsidyDescriptionField;
            }
            set
            {
                this.subsidyDescriptionField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType SubsidyRate
        {
            get
            {
                return this.subsidyRateField;
            }
            set
            {
                this.subsidyRateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool SubsidyRateSpecified
        {
            get
            {
                return this.subsidyRateFieldSpecified;
            }
            set
            {
                this.subsidyRateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType SubsidyAmount
        {
            get
            {
                return this.subsidyAmountField;
            }
            set
            {
                this.subsidyAmountField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class InvoiceTotalsType
    {

        private DoubleUpToEightDecimalType totalGrossAmountField;

        private List<DiscountType> generalDiscountsField;

        private List<ChargeType> generalSurchargesField;

        private DoubleUpToEightDecimalType totalGeneralDiscountsField;

        private bool totalGeneralDiscountsFieldSpecified;

        private DoubleUpToEightDecimalType totalGeneralSurchargesField;

        private bool totalGeneralSurchargesFieldSpecified;

        private DoubleUpToEightDecimalType totalGrossAmountBeforeTaxesField;

        private DoubleUpToEightDecimalType totalTaxOutputsField;

        private double totalTaxesWithheldField;

        private double invoiceTotalField;

        private List<SubsidyType> subsidiesField;

        private List<PaymentOnAccountType> paymentsOnAccountField;

        private List<ReimbursableExpensesType> reimbursableExpensesField;

        private DoubleUpToEightDecimalType totalFinancialExpensesField;

        private bool totalFinancialExpensesFieldSpecified;

        private DoubleUpToEightDecimalType totalOutstandingAmountField;

        private DoubleUpToEightDecimalType totalPaymentsOnAccountField;

        private bool totalPaymentsOnAccountFieldSpecified;

        private AmountsWithheldType amountsWithheldField;

        private DoubleUpToEightDecimalType totalExecutableAmountField;

        private DoubleUpToEightDecimalType totalReimbursableExpensesField;

        private bool totalReimbursableExpensesFieldSpecified;

        /// <summary>
        /// (TGA) Total sum of the gross amounts of the invoice 
        /// items. Always to two decimal points.
        /// </summary>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalGrossAmount
        {
            get
            {
                return this.totalGrossAmountField;
            }
            set
            {
                this.totalGrossAmountField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Discount", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<DiscountType> GeneralDiscounts
        {
            get
            {
                return this.generalDiscountsField;
            }
            set
            {
                this.generalDiscountsField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Charge", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<ChargeType> GeneralSurcharges
        {
            get
            {
                return this.generalSurchargesField;
            }
            set
            {
                this.generalSurchargesField = value;
            }
        }

         /// <summary>
        /// Discounts on the Total Gross Amount. There will be as many blocks of fields GeneralDiscounts as there 
        /// are different discount types applied to the same invoice. 
        /// When there are different taxable bases, they will be applied proportionally, 
        /// the final round-up to the nearest cent being carried out on the tax type of greatest value.
        /// </summary>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalGeneralDiscounts
        {
            get
            {
                return this.totalGeneralDiscountsField;
            }
            set
            {
                this.totalGeneralDiscountsField     = value;
                this.TotalGeneralDiscountsSpecified = true;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TotalGeneralDiscountsSpecified
        {
            get
            {
                return this.totalGeneralDiscountsFieldSpecified;
            }
            set
            {
                this.totalGeneralDiscountsFieldSpecified = value;
            }
        }

        /// <summary>
        /// Sum of different fields  GeneralSurcharges Always to two decimal points.
        /// </summary>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalGeneralSurcharges
        {
            get
            {
                return this.totalGeneralSurchargesField;
            }
            set
            {
                this.totalGeneralSurchargesField     = value;
                this.TotalGeneralSurchargesSpecified = true;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TotalGeneralSurchargesSpecified
        {
            get
            {
                return this.totalGeneralSurchargesFieldSpecified;
            }
            set
            {
                this.totalGeneralSurchargesFieldSpecified = value;
            }
        }

        /// <summary>
        /// Result: TotalGrossAmount - TotalGeneralDiscounts + TotalGeneralSurcharges Always to two decimal points.
        /// </summary>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalGrossAmountBeforeTaxes
        {
            get
            {
                return this.totalGrossAmountBeforeTaxesField;
            }
            set
            {
                this.totalGrossAmountBeforeTaxesField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalTaxOutputs
        {
            get
            {
                return this.totalTaxOutputsField;
            }
            set
            {
                this.totalTaxOutputsField = value;
            }
        }

        /// <summary>
        /// Sum of different fields TaxAmount. Always to two decimal points.
        /// </summary>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalTaxesWithheld
        {
            get
            {
                return this.totalTaxesWithheldField;
            }
            set
            {
                this.totalTaxesWithheldField = value;
            }
        }

        /// <summary>
        /// Result: TotalGrossAmountBeforeTaxes + TotalTaxOutputs - TotalTaxesWithheld. Always to two decimal points.
        /// </summary>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType InvoiceTotal
        {
            get
            {
                return this.invoiceTotalField;
            }
            set
            {
                this.invoiceTotalField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Subsidy", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<SubsidyType> Subsidies
        {
            get
            {
                return this.subsidiesField;
            }
            set
            {
                this.subsidiesField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("PaymentOnAccount", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<PaymentOnAccountType> PaymentsOnAccount
        {
            get
            {
                return this.paymentsOnAccountField;
            }
            set
            {
                this.paymentsOnAccountField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("ReimbursableExpenses", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<ReimbursableExpensesType> ReimbursableExpenses
        {
            get
            {
                return this.reimbursableExpensesField;
            }
            set
            {
                this.reimbursableExpensesField = value;
            }
        }

        /// <summary>
        /// Always to two decimal points.
        /// </summary>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalFinancialExpenses
        {
            get
            {
                return this.totalFinancialExpensesField;
            }
            set
            {
                this.totalFinancialExpensesField     = value;
                this.TotalFinancialExpensesSpecified = true;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TotalFinancialExpensesSpecified
        {
            get
            {
                return this.totalFinancialExpensesFieldSpecified;
            }
            set
            {
                this.totalFinancialExpensesFieldSpecified = value;
            }
        }

        /// <summary>
        /// Result: InvoiceTotal - (SubsidyAmount + TotalPaymentsOnAccount). Always to two decimal points.
        /// </summary>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalOutstandingAmount
        {
            get
            {
                return this.totalOutstandingAmountField;
            }
            set
            {
                this.totalOutstandingAmountField = value;
            }
        }

        /// <summary>
        /// Sum of the fields PaymentOnAccountAmount. Always to two decimal points.
        /// </summary>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalPaymentsOnAccount
        {
            get
            {
                return this.totalPaymentsOnAccountField;
            }
            set
            {
                this.totalPaymentsOnAccountField     = value;
                this.TotalPaymentsOnAccountSpecified = true;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TotalPaymentsOnAccountSpecified
        {
            get
            {
                return this.totalPaymentsOnAccountFieldSpecified;
            }
            set
            {
                this.totalPaymentsOnAccountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountsWithheldType AmountsWithheld
        {
            get
            {
                return this.amountsWithheldField;
            }
            set
            {
                this.amountsWithheldField = value;
            }
        }

        /// <summary>
        /// Result: TotalOutstandingAmount - WithholdingAmount + Reimbursable expenses + Financial expenses. Always to two decimal points
        /// </summary>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalExecutableAmount
        {
            get
            {
                return this.totalExecutableAmountField;
            }
            set
            {
                this.totalExecutableAmountField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalReimbursableExpenses
        {
            get
            {
                return this.totalReimbursableExpensesField;
            }
            set
            {
                this.totalReimbursableExpensesField     = value;
                this.TotalReimbursableExpensesSpecified = true;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TotalReimbursableExpensesSpecified
        {
            get
            {
                return this.totalReimbursableExpensesFieldSpecified;
            }
            set
            {
                this.totalReimbursableExpensesFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class ExchangeRateDetailsType
    {

        private DoubleUpToEightDecimalType exchangeRateField;

        private System.DateTime exchangeRateDateField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType ExchangeRate
        {
            get
            {
                return this.exchangeRateField;
            }
            set
            {
                this.exchangeRateField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime ExchangeRateDate
        {
            get
            {
                return this.exchangeRateDateField;
            }
            set
            {
                this.exchangeRateDateField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class PlaceOfIssueType
    {

        private string postCodeField;

        private string placeOfIssueDescriptionField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PostCode
        {
            get
            {
                return this.postCodeField;
            }
            set
            {
                this.postCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PlaceOfIssueDescription
        {
            get
            {
                return this.placeOfIssueDescriptionField;
            }
            set
            {
                this.placeOfIssueDescriptionField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class InvoiceIssueDataType
    {

        private System.DateTime issueDateField;

        private System.DateTime operationDateField;

        private bool operationDateFieldSpecified;

        private PlaceOfIssueType placeOfIssueField;

        private PeriodDates invoicingPeriodField;

        private CurrencyCodeType invoiceCurrencyCodeField;

        private ExchangeRateDetailsType exchangeRateDetailsField;

        private CurrencyCodeType taxCurrencyCodeField;

        private LanguageCodeType languageNameField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime IssueDate
        {
            get
            {
                return this.issueDateField;
            }
            set
            {
                this.issueDateField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime OperationDate
        {
            get
            {
                return this.operationDateField;
            }
            set
            {
                this.operationDateField     = value;
                this.OperationDateSpecified = true;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool OperationDateSpecified
        {
            get
            {
                return this.operationDateFieldSpecified;
            }
            set
            {
                this.operationDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PlaceOfIssueType PlaceOfIssue
        {
            get
            {
                return this.placeOfIssueField;
            }
            set
            {
                this.placeOfIssueField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PeriodDates InvoicingPeriod
        {
            get
            {
                return this.invoicingPeriodField;
            }
            set
            {
                this.invoicingPeriodField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CurrencyCodeType InvoiceCurrencyCode
        {
            get
            {
                return this.invoiceCurrencyCodeField;
            }
            set
            {
                this.invoiceCurrencyCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ExchangeRateDetailsType ExchangeRateDetails
        {
            get
            {
                return this.exchangeRateDetailsField;
            }
            set
            {
                this.exchangeRateDetailsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CurrencyCodeType TaxCurrencyCode
        {
            get
            {
                return this.taxCurrencyCodeField;
            }
            set
            {
                this.taxCurrencyCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public LanguageCodeType LanguageName
        {
            get
            {
                return this.languageNameField;
            }
            set
            {
                this.languageNameField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum CurrencyCodeType
    {

        /// <remarks/>
        AFN,

        /// <remarks/>
        ALL,

        /// <remarks/>
        AMD,

        /// <remarks/>
        ANG,

        /// <remarks/>
        AOA,

        /// <remarks/>
        ARS,

        /// <remarks/>
        AUD,

        /// <remarks/>
        AWG,

        /// <remarks/>
        AZN,

        /// <remarks/>
        BAD,

        /// <remarks/>
        BBD,

        /// <remarks/>
        BDT,

        /// <remarks/>
        BGN,

        /// <remarks/>
        BHD,

        /// <remarks/>
        BIF,

        /// <remarks/>
        BMD,

        /// <remarks/>
        BND,

        /// <remarks/>
        BOB,

        /// <remarks/>
        BRL,

        /// <remarks/>
        BRR,

        /// <remarks/>
        BSD,

        /// <remarks/>
        BWP,

        /// <remarks/>
        BYR,

        /// <remarks/>
        BZD,

        /// <remarks/>
        CAD,

        /// <remarks/>
        CDF,

        /// <remarks/>
        CDP,

        /// <remarks/>
        CHF,

        /// <remarks/>
        CLP,

        /// <remarks/>
        CNY,

        /// <remarks/>
        COP,

        /// <remarks/>
        CRC,

        /// <remarks/>
        CUP,

        /// <remarks/>
        CVE,

        /// <remarks/>
        CZK,

        /// <remarks/>
        DJF,

        /// <remarks/>
        DKK,

        /// <remarks/>
        DOP,

        /// <remarks/>
        DRP,

        /// <remarks/>
        DZD,

        /// <remarks/>
        EEK,

        /// <remarks/>
        EGP,

        /// <remarks/>
        ESP,

        /// <remarks/>
        ETB,

        /// <remarks/>
        EUR,

        /// <remarks/>
        FJD,

        /// <remarks/>
        FKP,

        /// <remarks/>
        GBP,

        /// <remarks/>
        GEK,

        /// <remarks/>
        GHC,

        /// <remarks/>
        GIP,

        /// <remarks/>
        GMD,

        /// <remarks/>
        GNF,

        /// <remarks/>
        GTQ,

        /// <remarks/>
        GWP,

        /// <remarks/>
        GYD,

        /// <remarks/>
        HKD,

        /// <remarks/>
        HNL,

        /// <remarks/>
        HRK,

        /// <remarks/>
        HTG,

        /// <remarks/>
        HUF,

        /// <remarks/>
        IDR,

        /// <remarks/>
        ILS,

        /// <remarks/>
        INR,

        /// <remarks/>
        IQD,

        /// <remarks/>
        IRR,

        /// <remarks/>
        ISK,

        /// <remarks/>
        JMD,

        /// <remarks/>
        JOD,

        /// <remarks/>
        JPY,

        /// <remarks/>
        KES,

        /// <remarks/>
        KGS,

        /// <remarks/>
        KHR,

        /// <remarks/>
        KMF,

        /// <remarks/>
        KPW,

        /// <remarks/>
        KRW,

        /// <remarks/>
        KWD,

        /// <remarks/>
        KYD,

        /// <remarks/>
        KZT,

        /// <remarks/>
        LAK,

        /// <remarks/>
        LBP,

        /// <remarks/>
        LKR,

        /// <remarks/>
        LRD,

        /// <remarks/>
        LSL,

        /// <remarks/>
        LTL,

        /// <remarks/>
        LVL,

        /// <remarks/>
        LYD,

        /// <remarks/>
        MAD,

        /// <remarks/>
        MDL,

        /// <remarks/>
        MGF,

        /// <remarks/>
        MNC,

        /// <remarks/>
        MNT,

        /// <remarks/>
        MOP,

        /// <remarks/>
        MRO,

        /// <remarks/>
        MUR,

        /// <remarks/>
        MVR,

        /// <remarks/>
        MWK,

        /// <remarks/>
        MXN,

        /// <remarks/>
        MYR,

        /// <remarks/>
        MZM,

        /// <remarks/>
        NGN,

        /// <remarks/>
        NIC,

        /// <remarks/>
        NIO,

        /// <remarks/>
        NIS,

        /// <remarks/>
        NOK,

        /// <remarks/>
        NPR,

        /// <remarks/>
        NZD,

        /// <remarks/>
        OMR,

        /// <remarks/>
        PAB,

        /// <remarks/>
        PEI,

        /// <remarks/>
        PEN,

        /// <remarks/>
        PES,

        /// <remarks/>
        PGK,

        /// <remarks/>
        PHP,

        /// <remarks/>
        PKR,

        /// <remarks/>
        PLN,

        /// <remarks/>
        PYG,

        /// <remarks/>
        QAR,

        /// <remarks/>
        RMB,

        /// <remarks/>
        RON,

        /// <remarks/>
        RUB,

        /// <remarks/>
        RWF,

        /// <remarks/>
        SAR,

        /// <remarks/>
        SBD,

        /// <remarks/>
        SCR,

        /// <remarks/>
        SDP,

        /// <remarks/>
        SEK,

        /// <remarks/>
        SGD,

        /// <remarks/>
        SHP,

        /// <remarks/>
        SKK,

        /// <remarks/>
        SLL,

        /// <remarks/>
        SOL,

        /// <remarks/>
        SOS,

        /// <remarks/>
        SRD,

        /// <remarks/>
        STD,

        /// <remarks/>
        SVC,

        /// <remarks/>
        SYP,

        /// <remarks/>
        SZL,

        /// <remarks/>
        THB,

        /// <remarks/>
        TJS,

        /// <remarks/>
        TMM,

        /// <remarks/>
        TND,

        /// <remarks/>
        TOP,

        /// <remarks/>
        TPE,

        /// <remarks/>
        TRY,

        /// <remarks/>
        TTD,

        /// <remarks/>
        TWD,

        /// <remarks/>
        TZS,

        /// <remarks/>
        UAH,

        /// <remarks/>
        UGS,

        /// <remarks/>
        USD,

        /// <remarks/>
        UYP,

        /// <remarks/>
        UYU,

        /// <remarks/>
        VEF,

        /// <remarks/>
        VND,

        /// <remarks/>
        VUV,

        /// <remarks/>
        WST,

        /// <remarks/>
        XAF,

        /// <remarks/>
        XCD,

        /// <remarks/>
        XOF,

        /// <remarks/>
        YER,

        /// <remarks/>
        ZAR,

        /// <remarks/>
        ZMK,

        /// <remarks/>
        ZWD,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum LanguageCodeType
    {

        /// <remarks/>
        ar,

        /// <remarks/>
        be,

        /// <remarks/>
        bg,

        /// <remarks/>
        ca,

        /// <remarks/>
        cs,

        /// <remarks/>
        da,

        /// <remarks/>
        de,

        /// <remarks/>
        el,

        /// <remarks/>
        en,

        /// <remarks/>
        es,

        /// <remarks/>
        et,

        /// <remarks/>
        eu,

        /// <remarks/>
        fi,

        /// <remarks/>
        fr,

        /// <remarks/>
        ga,

        /// <remarks/>
        gl,

        /// <remarks/>
        hr,

        /// <remarks/>
        hu,

        /// <remarks/>
        @is,

        /// <remarks/>
        it,

        /// <remarks/>
        lv,

        /// <remarks/>
        lt,

        /// <remarks/>
        mk,

        /// <remarks/>
        mt,

        /// <remarks/>
        nl,

        /// <remarks/>
        no,

        /// <remarks/>
        pl,

        /// <remarks/>
        pt,

        /// <remarks/>
        ro,

        /// <remarks/>
        ru,

        /// <remarks/>
        sk,

        /// <remarks/>
        sl,

        /// <remarks/>
        sq,

        /// <remarks/>
        sr,

        /// <remarks/>
        sv,

        /// <remarks/>
        tr,

        /// <remarks/>
        uk,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class CorrectiveType
    {

        private string invoiceNumberField;

        private string invoiceSeriesCodeField;

        private ReasonCodeType reasonCodeField;

        private ReasonDescriptionType reasonDescriptionField;

        private PeriodDates taxPeriodField;

        private CorrectionMethodType correctionMethodField;

        private CorrectionMethodDescriptionType correctionMethodDescriptionField;

        private string additionalReasonDescriptionField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceNumber
        {
            get
            {
                return this.invoiceNumberField;
            }
            set
            {
                this.invoiceNumberField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceSeriesCode
        {
            get
            {
                return this.invoiceSeriesCodeField;
            }
            set
            {
                this.invoiceSeriesCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ReasonCodeType ReasonCode
        {
            get
            {
                return this.reasonCodeField;
            }
            set
            {
                this.reasonCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ReasonDescriptionType ReasonDescription
        {
            get
            {
                return this.reasonDescriptionField;
            }
            set
            {
                this.reasonDescriptionField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PeriodDates TaxPeriod
        {
            get
            {
                return this.taxPeriodField;
            }
            set
            {
                this.taxPeriodField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CorrectionMethodType CorrectionMethod
        {
            get
            {
                return this.correctionMethodField;
            }
            set
            {
                this.correctionMethodField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CorrectionMethodDescriptionType CorrectionMethodDescription
        {
            get
            {
                return this.correctionMethodDescriptionField;
            }
            set
            {
                this.correctionMethodDescriptionField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string AdditionalReasonDescription
        {
            get
            {
                return this.additionalReasonDescriptionField;
            }
            set
            {
                this.additionalReasonDescriptionField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum ReasonCodeType
    {

        /// <remarks/>
        [XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [XmlEnumAttribute("07")]
        Item07,

        /// <remarks/>
        [XmlEnumAttribute("08")]
        Item08,

        /// <remarks/>
        [XmlEnumAttribute("09")]
        Item09,

        /// <remarks/>
        [XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [XmlEnumAttribute("12")]
        Item12,

        /// <remarks/>
        [XmlEnumAttribute("13")]
        Item13,

        /// <remarks/>
        [XmlEnumAttribute("14")]
        Item14,

        /// <remarks/>
        [XmlEnumAttribute("15")]
        Item15,

        /// <remarks/>
        [XmlEnumAttribute("16")]
        Item16,

        /// <remarks/>
        [XmlEnumAttribute("80")]
        Item80,

        /// <remarks/>
        [XmlEnumAttribute("81")]
        Item81,

        /// <remarks/>
        [XmlEnumAttribute("82")]
        Item82,

        /// <remarks/>
        [XmlEnumAttribute("83")]
        Item83,

        /// <remarks/>
        [XmlEnumAttribute("84")]
        Item84,

        /// <remarks/>
        [XmlEnumAttribute("85")]
        Item85,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum ReasonDescriptionType
    {

        /// <remarks/>
        [XmlEnumAttribute("Número de la factura")]
        Númerodelafactura,

        /// <remarks/>
        [XmlEnumAttribute("Serie de la factura")]
        Seriedelafactura,

        /// <remarks/>
        [XmlEnumAttribute("Fecha expedición")]
        Fechaexpedición,

        /// <remarks/>
        [XmlEnumAttribute("Nombre y apellidos/Razón Social-Emisor")]
        NombreyapellidosRazónSocialEmisor,

        /// <remarks/>
        [XmlEnumAttribute("Nombre y apellidos/Razón Social-Receptor")]
        NombreyapellidosRazónSocialReceptor,

        /// <remarks/>
        [XmlEnumAttribute("Identificación fiscal Emisor/obligado")]
        IdentificaciónfiscalEmisorobligado,

        /// <remarks/>
        [XmlEnumAttribute("Identificación fiscal Receptor")]
        IdentificaciónfiscalReceptor,

        /// <remarks/>
        [XmlEnumAttribute("Domicilio Emisor/Obligado")]
        DomicilioEmisorObligado,

        /// <remarks/>
        [XmlEnumAttribute("Domicilio Receptor")]
        DomicilioReceptor,

        /// <remarks/>
        [XmlEnumAttribute("Detalle Operación")]
        DetalleOperación,

        /// <remarks/>
        [XmlEnumAttribute("Porcentaje impositivo a aplicar")]
        Porcentajeimpositivoaaplicar,

        /// <remarks/>
        [XmlEnumAttribute("Cuota tributaria a aplicar")]
        Cuotatributariaaaplicar,

        /// <remarks/>
        [XmlEnumAttribute("Fecha/Periodo a aplicar")]
        FechaPeriodoaaplicar,

        /// <remarks/>
        [XmlEnumAttribute("Clase de factura")]
        Clasedefactura,

        /// <remarks/>
        [XmlEnumAttribute("Literales legales")]
        Literaleslegales,

        /// <remarks/>
        [XmlEnumAttribute("Base imponible")]
        Baseimponible,

        /// <remarks/>
        [XmlEnumAttribute("Cálculo de cuotas repercutidas")]
        Cálculodecuotasrepercutidas,

        /// <remarks/>
        [XmlEnumAttribute("Cálculo de cuotas retenidas")]
        Cálculodecuotasretenidas,

        /// <remarks/>
        [XmlEnumAttribute("Base imponible modificada por devolución de envases / embalajes")]
        Baseimponiblemodificadapordevolucióndeenvasesembalajes,

        /// <remarks/>
        [XmlEnumAttribute("Base imponible modificada por descuentos y bonificaciones")]
        Baseimponiblemodificadapordescuentosybonificaciones,

        /// <remarks/>
        [XmlEnumAttribute("Base imponible modificada por resolución firme, judicial o administrativa")]
        Baseimponiblemodificadaporresoluciónfirmejudicialoadministrativa,

        /// <remarks/>
        [XmlEnumAttribute("Base imponible modificada cuotas repercutidas no satisfechas. Auto de declaración" +
            " de concurso")]
        BaseimponiblemodificadacuotasrepercutidasnosatisfechasAutodedeclaracióndeconcurso,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum CorrectionMethodType
    {

        /// <remarks/>
        [XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [XmlEnumAttribute("04")]
        Item04,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum CorrectionMethodDescriptionType
    {

        /// <remarks/>
        [XmlEnumAttribute("Rectificación íntegra")]
        Rectificacióníntegra,

        /// <remarks/>
        [XmlEnumAttribute("Rectificación por diferencias")]
        Rectificaciónpordiferencias,

        /// <remarks/>
        [XmlEnumAttribute("Rectificación por descuento por volumen de operaciones durante un periodo")]
        Rectificaciónpordescuentoporvolumendeoperacionesduranteunperiodo,

        /// <remarks/>
        [XmlEnumAttribute("Autorizadas por la Agencia Tributaria")]
        AutorizadasporlaAgenciaTributaria,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class InvoiceHeaderType
    {

        private string invoiceNumberField;

        private string invoiceSeriesCodeField;

        private InvoiceDocumentTypeType invoiceDocumentTypeField;

        private InvoiceClassType invoiceClassField;

        private CorrectiveType correctiveField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceNumber
        {
            get
            {
                return this.invoiceNumberField;
            }
            set
            {
                this.invoiceNumberField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceSeriesCode
        {
            get
            {
                return this.invoiceSeriesCodeField;
            }
            set
            {
                this.invoiceSeriesCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public InvoiceDocumentTypeType InvoiceDocumentType
        {
            get
            {
                return this.invoiceDocumentTypeField;
            }
            set
            {
                this.invoiceDocumentTypeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public InvoiceClassType InvoiceClass
        {
            get
            {
                return this.invoiceClassField;
            }
            set
            {
                this.invoiceClassField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CorrectiveType Corrective
        {
            get
            {
                return this.correctiveField;
            }
            set
            {
                this.correctiveField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum InvoiceDocumentTypeType
    {
        /// <remarks/>
        [XmlEnum("FC")]
        Complete,

        /// <remarks/>
        [XmlEnum("FA")]
        Abbreviated,

        /// <remarks/>
        [XmlEnum("AF")]
        SelfInvoice, F,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum InvoiceClassType
    {
        /// <remarks/>
        [XmlEnum("OO")]
        Original,

        /// <remarks/>
        [XmlEnum("OR")]
        Corrective,

        /// <remarks/>
        [XmlEnum("OC")]
        SummaryOriginal,

        /// <remarks/>
        [XmlEnum("CO")]
        CopyOfOriginal,

        /// <remarks/>
        [XmlEnum("CR")]
        CopyOfCorrective,

        /// <remarks/>
        [XmlEnum("CC")]
        CopyOfSummary,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class InvoiceType
    {

        private InvoiceHeaderType invoiceHeaderField;

        private InvoiceIssueDataType invoiceIssueDataField;

        private List<TaxOutputType> taxesOutputsField;

        private List<TaxType> taxesWithheldField;

        private InvoiceTotalsType invoiceTotalsField;

        private List<InvoiceLineType> itemsField;

        private List<InstallmentType> paymentDetailsField;

        private List<string> legalLiteralsField;

        private AdditionalDataType additionalDataField;
        internal Facturae Parent
        {
            get;
            set;
        }
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public InvoiceHeaderType InvoiceHeader
        {
            get
            {
                return this.invoiceHeaderField;
            }
            set
            {
                this.invoiceHeaderField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public InvoiceIssueDataType InvoiceIssueData
        {
            get
            {
                return this.invoiceIssueDataField;
            }
            set
            {
                this.invoiceIssueDataField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Tax", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<TaxOutputType> TaxesOutputs
        {
            get
            {
                return this.taxesOutputsField;
            }
            set
            {
                this.taxesOutputsField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Tax", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<TaxType> TaxesWithheld
        {
            get
            {
                return this.taxesWithheldField;
            }
            set
            {
                this.taxesWithheldField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public InvoiceTotalsType InvoiceTotals
        {
            get
            {
                return this.invoiceTotalsField;
            }
            set
            {
                this.invoiceTotalsField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("InvoiceLine", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<InvoiceLineType> Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Installment", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<InstallmentType> PaymentDetails
        {
            get
            {
                return this.paymentDetailsField;
            }
            set
            {
                this.paymentDetailsField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("LegalReference", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<string> LegalLiterals
        {
            get
            {
                return this.legalLiteralsField;
            }
            set
            {
                this.legalLiteralsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AdditionalDataType AdditionalData
        {
            get
            {
                return this.additionalDataField;
            }
            set
            {
                this.additionalDataField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class InstallmentType
    {

        private System.DateTime installmentDueDateField;

        private DoubleTwoDecimalType installmentAmountField;

        private PaymentMeansType paymentMeansField;

        private AccountType accountToBeCreditedField;

        private string paymentReconciliationReferenceField;

        private AccountType accountToBeDebitedField;

        private string collectionAdditionalInformationField;

        private string regulatoryReportingDataField;

        private string debitReconciliationReferenceField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime InstallmentDueDate
        {
            get
            {
                return this.installmentDueDateField;
            }
            set
            {
                this.installmentDueDateField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleTwoDecimalType InstallmentAmount
        {
            get
            {
                return this.installmentAmountField;
            }
            set
            {
                this.installmentAmountField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PaymentMeansType PaymentMeans
        {
            get
            {
                return this.paymentMeansField;
            }
            set
            {
                this.paymentMeansField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AccountType AccountToBeCredited
        {
            get
            {
                return this.accountToBeCreditedField;
            }
            set
            {
                this.accountToBeCreditedField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PaymentReconciliationReference
        {
            get
            {
                return this.paymentReconciliationReferenceField;
            }
            set
            {
                this.paymentReconciliationReferenceField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AccountType AccountToBeDebited
        {
            get
            {
                return this.accountToBeDebitedField;
            }
            set
            {
                this.accountToBeDebitedField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string CollectionAdditionalInformation
        {
            get
            {
                return this.collectionAdditionalInformationField;
            }
            set
            {
                this.collectionAdditionalInformationField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string RegulatoryReportingData
        {
            get
            {
                return this.regulatoryReportingDataField;
            }
            set
            {
                this.regulatoryReportingDataField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string DebitReconciliationReference
        {
            get
            {
                return this.debitReconciliationReferenceField;
            }
            set
            {
                this.debitReconciliationReferenceField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum PaymentMeansType
    {

        /// <remarks/>
        [XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [XmlEnumAttribute("07")]
        Item07,

        /// <remarks/>
        [XmlEnumAttribute("08")]
        Item08,

        /// <remarks/>
        [XmlEnumAttribute("09")]
        Item09,

        /// <remarks/>
        [XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [XmlEnumAttribute("12")]
        Item12,

        /// <remarks/>
        [XmlEnumAttribute("13")]
        Item13,

        /// <remarks/>
        [XmlEnumAttribute("14")]
        Item14,

        /// <remarks/>
        [XmlEnumAttribute("15")]
        Item15,

        /// <remarks/>
        [XmlEnumAttribute("16")]
        Item16,

        /// <remarks/>
        [XmlEnumAttribute("17")]
        Item17,

        /// <remarks/>
        [XmlEnumAttribute("18")]
        Item18,

        /// <remarks/>
        [XmlEnumAttribute("19")]
        Item19,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class AccountType
    {

        private string itemField;

        private ItemChoiceType itemElementNameField;

        private string bankCodeField;

        private string branchCodeField;

        private object item1Field;

        private string bICField;

        /// <remarks/>
        [XmlElementAttribute("AccountNumber", typeof(string), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("IBAN", typeof(string), Form = XmlSchemaForm.Unqualified)]
        [XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string BankCode
        {
            get
            {
                return this.bankCodeField;
            }
            set
            {
                this.bankCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string BranchCode
        {
            get
            {
                return this.branchCodeField;
            }
            set
            {
                this.branchCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("BranchInSpainAddress", typeof(AddressType), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("OverseasBranchAddress", typeof(OverseasAddressType), Form = XmlSchemaForm.Unqualified)]
        public object Item1
        {
            get
            {
                return this.item1Field;
            }
            set
            {
                this.item1Field = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string BIC
        {
            get
            {
                return this.bICField;
            }
            set
            {
                this.bICField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae", IncludeInSchema = false)]
    public enum ItemChoiceType
    {
        /// <remarks/>
        [XmlEnumAttribute("AccountNumber")]
        AccountNumber,

        /// <remarks/>
        [XmlEnumAttribute("IBAN")]
        IBAN,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class AddressType
    {

        private string addressField;

        private string postCodeField;

        private string townField;

        private string provinceField;

        private CountryType countryCodeField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PostCode
        {
            get
            {
                return this.postCodeField;
            }
            set
            {
                this.postCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Town
        {
            get
            {
                return this.townField;
            }
            set
            {
                this.townField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Province
        {
            get
            {
                return this.provinceField;
            }
            set
            {
                this.provinceField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CountryType CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum CountryType
    {

        /// <remarks/>
        AFG,

        /// <remarks/>
        ALB,

        /// <remarks/>
        DZA,

        /// <remarks/>
        ASM,

        /// <remarks/>
        AND,

        /// <remarks/>
        AGO,

        /// <remarks/>
        AIA,

        /// <remarks/>
        ATG,

        /// <remarks/>
        ARG,

        /// <remarks/>
        ARM,

        /// <remarks/>
        ABW,

        /// <remarks/>
        AUS,

        /// <remarks/>
        AUT,

        /// <remarks/>
        AZE,

        /// <remarks/>
        BHS,

        /// <remarks/>
        BHR,

        /// <remarks/>
        BGD,

        /// <remarks/>
        BRB,

        /// <remarks/>
        BLR,

        /// <remarks/>
        BEL,

        /// <remarks/>
        BLZ,

        /// <remarks/>
        BEN,

        /// <remarks/>
        BMU,

        /// <remarks/>
        BTN,

        /// <remarks/>
        BOL,

        /// <remarks/>
        BIH,

        /// <remarks/>
        BWA,

        /// <remarks/>
        BRA,

        /// <remarks/>
        BRN,

        /// <remarks/>
        BGR,

        /// <remarks/>
        BFA,

        /// <remarks/>
        BDI,

        /// <remarks/>
        KHM,

        /// <remarks/>
        CMR,

        /// <remarks/>
        CAN,

        /// <remarks/>
        CPV,

        /// <remarks/>
        CYM,

        /// <remarks/>
        CAF,

        /// <remarks/>
        TCD,

        /// <remarks/>
        CHL,

        /// <remarks/>
        CHN,

        /// <remarks/>
        COD,

        /// <remarks/>
        COL,

        /// <remarks/>
        COM,

        /// <remarks/>
        COG,

        /// <remarks/>
        COK,

        /// <remarks/>
        CRI,

        /// <remarks/>
        CIV,

        /// <remarks/>
        HRV,

        /// <remarks/>
        CUB,

        /// <remarks/>
        CYP,

        /// <remarks/>
        CZE,

        /// <remarks/>
        DNK,

        /// <remarks/>
        DJI,

        /// <remarks/>
        DMA,

        /// <remarks/>
        DOM,

        /// <remarks/>
        ECU,

        /// <remarks/>
        EGY,

        /// <remarks/>
        SLV,

        /// <remarks/>
        GNQ,

        /// <remarks/>
        ERI,

        /// <remarks/>
        EST,

        /// <remarks/>
        ETH,

        /// <remarks/>
        FLK,

        /// <remarks/>
        FRO,

        /// <remarks/>
        FJI,

        /// <remarks/>
        FIN,

        /// <remarks/>
        FRA,

        /// <remarks/>
        GUF,

        /// <remarks/>
        PYF,

        /// <remarks/>
        GAB,

        /// <remarks/>
        GMB,

        /// <remarks/>
        GEO,

        /// <remarks/>
        GGY,

        /// <remarks/>
        DEU,

        /// <remarks/>
        GHA,

        /// <remarks/>
        GIB,

        /// <remarks/>
        GRC,

        /// <remarks/>
        GRL,

        /// <remarks/>
        GRD,

        /// <remarks/>
        GLP,

        /// <remarks/>
        GUM,

        /// <remarks/>
        GTM,

        /// <remarks/>
        GIN,

        /// <remarks/>
        GNB,

        /// <remarks/>
        GUY,

        /// <remarks/>
        HTI,

        /// <remarks/>
        HND,

        /// <remarks/>
        HKG,

        /// <remarks/>
        HUN,

        /// <remarks/>
        ISL,

        /// <remarks/>
        IND,

        /// <remarks/>
        IDN,

        /// <remarks/>
        IMN,

        /// <remarks/>
        IRN,

        /// <remarks/>
        IRQ,

        /// <remarks/>
        IRL,

        /// <remarks/>
        ISR,

        /// <remarks/>
        ITA,

        /// <remarks/>
        JAM,

        /// <remarks/>
        JEY,

        /// <remarks/>
        JPN,

        /// <remarks/>
        JOR,

        /// <remarks/>
        KAZ,

        /// <remarks/>
        KEN,

        /// <remarks/>
        KIR,

        /// <remarks/>
        PRK,

        /// <remarks/>
        KOR,

        /// <remarks/>
        KWT,

        /// <remarks/>
        KGZ,

        /// <remarks/>
        LAO,

        /// <remarks/>
        LVA,

        /// <remarks/>
        LBN,

        /// <remarks/>
        LSO,

        /// <remarks/>
        LBR,

        /// <remarks/>
        LBY,

        /// <remarks/>
        LIE,

        /// <remarks/>
        LTU,

        /// <remarks/>
        LUX,

        /// <remarks/>
        MAC,

        /// <remarks/>
        MKD,

        /// <remarks/>
        MDG,

        /// <remarks/>
        MWI,

        /// <remarks/>
        MYS,

        /// <remarks/>
        MDV,

        /// <remarks/>
        MLI,

        /// <remarks/>
        MLT,

        /// <remarks/>
        MHL,

        /// <remarks/>
        MTQ,

        /// <remarks/>
        MRT,

        /// <remarks/>
        MUS,

        /// <remarks/>
        MYT,

        /// <remarks/>
        MEX,

        /// <remarks/>
        FSM,

        /// <remarks/>
        MDA,

        /// <remarks/>
        MCO,

        /// <remarks/>
        MNE,

        /// <remarks/>
        MNG,

        /// <remarks/>
        MSR,

        /// <remarks/>
        MAR,

        /// <remarks/>
        MOZ,

        /// <remarks/>
        MMR,

        /// <remarks/>
        NAM,

        /// <remarks/>
        NRU,

        /// <remarks/>
        NPL,

        /// <remarks/>
        NLD,

        /// <remarks/>
        ANT,

        /// <remarks/>
        NCL,

        /// <remarks/>
        NZL,

        /// <remarks/>
        NIC,

        /// <remarks/>
        NER,

        /// <remarks/>
        NGA,

        /// <remarks/>
        NIU,

        /// <remarks/>
        NFK,

        /// <remarks/>
        MNP,

        /// <remarks/>
        NOR,

        /// <remarks/>
        OMN,

        /// <remarks/>
        PAK,

        /// <remarks/>
        PLW,

        /// <remarks/>
        PAN,

        /// <remarks/>
        PNG,

        /// <remarks/>
        PRY,

        /// <remarks/>
        PSE,

        /// <remarks/>
        PER,

        /// <remarks/>
        PHL,

        /// <remarks/>
        PCN,

        /// <remarks/>
        POL,

        /// <remarks/>
        PRT,

        /// <remarks/>
        PRI,

        /// <remarks/>
        QAT,

        /// <remarks/>
        REU,

        /// <remarks/>
        ROU,

        /// <remarks/>
        RUS,

        /// <remarks/>
        RWA,

        /// <remarks/>
        KNA,

        /// <remarks/>
        LCA,

        /// <remarks/>
        VCT,

        /// <remarks/>
        WSM,

        /// <remarks/>
        SMR,

        /// <remarks/>
        STP,

        /// <remarks/>
        SAU,

        /// <remarks/>
        SEN,

        /// <remarks/>
        SRB,

        /// <remarks/>
        SYC,

        /// <remarks/>
        SLE,

        /// <remarks/>
        SGP,

        /// <remarks/>
        SVK,

        /// <remarks/>
        SVN,

        /// <remarks/>
        SLB,

        /// <remarks/>
        SOM,

        /// <remarks/>
        ZAF,

        /// <remarks/>
        ESP,

        /// <remarks/>
        LKA,

        /// <remarks/>
        SHN,

        /// <remarks/>
        SPM,

        /// <remarks/>
        SDN,

        /// <remarks/>
        SUR,

        /// <remarks/>
        SJM,

        /// <remarks/>
        SWZ,

        /// <remarks/>
        SWE,

        /// <remarks/>
        CHE,

        /// <remarks/>
        SYR,

        /// <remarks/>
        TWN,

        /// <remarks/>
        TJK,

        /// <remarks/>
        TZA,

        /// <remarks/>
        THA,

        /// <remarks/>
        TGO,

        /// <remarks/>
        TKL,

        /// <remarks/>
        TON,

        /// <remarks/>
        TTO,

        /// <remarks/>
        TUN,

        /// <remarks/>
        TUR,

        /// <remarks/>
        TKM,

        /// <remarks/>
        TLS,

        /// <remarks/>
        TCA,

        /// <remarks/>
        TUV,

        /// <remarks/>
        UGA,

        /// <remarks/>
        UKR,

        /// <remarks/>
        ARE,

        /// <remarks/>
        GBR,

        /// <remarks/>
        USA,

        /// <remarks/>
        URY,

        /// <remarks/>
        UZB,

        /// <remarks/>
        VUT,

        /// <remarks/>
        VAT,

        /// <remarks/>
        VEN,

        /// <remarks/>
        VNM,

        /// <remarks/>
        VGB,

        /// <remarks/>
        VIR,

        /// <remarks/>
        WLF,

        /// <remarks/>
        ESH,

        /// <remarks/>
        YEM,

        /// <remarks/>
        ZAR,

        /// <remarks/>
        ZMB,

        /// <remarks/>
        ZWE,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class OverseasAddressType
    {

        private string addressField;

        private string postCodeAndTownField;

        private string provinceField;

        private CountryType countryCodeField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PostCodeAndTown
        {
            get
            {
                return this.postCodeAndTownField;
            }
            set
            {
                this.postCodeAndTownField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Province
        {
            get
            {
                return this.provinceField;
            }
            set
            {
                this.provinceField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CountryType CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class AdministrativeCentreType
    {

        private string centreCodeField;

        private RoleTypeCodeType roleTypeCodeField;

        private bool roleTypeCodeFieldSpecified;

        private string nameField;

        private string firstSurnameField;

        private string secondSurnameField;

        private object itemField;

        private ContactDetailsType contactDetailsField;

        private string physicalGLNField;

        private string logicalOperationalPointField;

        private string centreDescriptionField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string CentreCode
        {
            get
            {
                return this.centreCodeField;
            }
            set
            {
                this.centreCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public RoleTypeCodeType RoleTypeCode
        {
            get
            {
                return this.roleTypeCodeField;
            }
            set
            {
                this.roleTypeCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool RoleTypeCodeSpecified
        {
            get
            {
                return this.roleTypeCodeFieldSpecified;
            }
            set
            {
                this.roleTypeCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string FirstSurname
        {
            get
            {
                return this.firstSurnameField;
            }
            set
            {
                this.firstSurnameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string SecondSurname
        {
            get
            {
                return this.secondSurnameField;
            }
            set
            {
                this.secondSurnameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("AddressInSpain", typeof(AddressType), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("OverseasAddress", typeof(OverseasAddressType), Form = XmlSchemaForm.Unqualified)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ContactDetailsType ContactDetails
        {
            get
            {
                return this.contactDetailsField;
            }
            set
            {
                this.contactDetailsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PhysicalGLN
        {
            get
            {
                return this.physicalGLNField;
            }
            set
            {
                this.physicalGLNField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string LogicalOperationalPoint
        {
            get
            {
                return this.logicalOperationalPointField;
            }
            set
            {
                this.logicalOperationalPointField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string CentreDescription
        {
            get
            {
                return this.centreDescriptionField;
            }
            set
            {
                this.centreDescriptionField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public enum RoleTypeCodeType
    {

        /// <remarks/>
        [XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [XmlEnumAttribute("07")]
        Item07,

        /// <remarks/>
        [XmlEnumAttribute("08")]
        Item08,

        /// <remarks/>
        [XmlEnumAttribute("09")]
        Item09,
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class ContactDetailsType
    {

        private string telephoneField;

        private string teleFaxField;

        private string webAddressField;

        private string electronicMailField;

        private string contactPersonsField;

        private string cnoCnaeField;

        private string iNETownCodeField;

        private string additionalContactDetailsField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Telephone
        {
            get
            {
                return this.telephoneField;
            }
            set
            {
                this.telephoneField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string TeleFax
        {
            get
            {
                return this.teleFaxField;
            }
            set
            {
                this.teleFaxField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string WebAddress
        {
            get
            {
                return this.webAddressField;
            }
            set
            {
                this.webAddressField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ElectronicMail
        {
            get
            {
                return this.electronicMailField;
            }
            set
            {
                this.electronicMailField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ContactPersons
        {
            get
            {
                return this.contactPersonsField;
            }
            set
            {
                this.contactPersonsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string CnoCnae
        {
            get
            {
                return this.cnoCnaeField;
            }
            set
            {
                this.cnoCnaeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string INETownCode
        {
            get
            {
                return this.iNETownCodeField;
            }
            set
            {
                this.iNETownCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string AdditionalContactDetails
        {
            get
            {
                return this.additionalContactDetailsField;
            }
            set
            {
                this.additionalContactDetailsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class BusinessType
    {

        private TaxIdentificationType taxIdentificationField;

        private string partyIdentificationField;

        private List<AdministrativeCentreType> administrativeCentresField;

        private object itemField;
        internal PartiesType Parent
        {
            get;
            set;
        }
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public TaxIdentificationType TaxIdentification
        {
            get
            {
                return this.taxIdentificationField;
            }
            set
            {
                this.taxIdentificationField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PartyIdentification
        {
            get
            {
                return this.partyIdentificationField;
            }
            set
            {
                this.partyIdentificationField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("AdministrativeCentre", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<AdministrativeCentreType> AdministrativeCentres
        {
            get
            {
                return this.administrativeCentresField;
            }
            set
            {
                this.administrativeCentresField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Individual", typeof(IndividualType), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("LegalEntity", typeof(LegalEntityType), Form = XmlSchemaForm.Unqualified)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class IndividualType
    {

        private string nameField;

        private string firstSurnameField;

        private string secondSurnameField;

        private object itemField;

        private ContactDetailsType contactDetailsField;
        internal BusinessType Parent
        {
            get;
            set;
        }
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string FirstSurname
        {
            get
            {
                return this.firstSurnameField;
            }
            set
            {
                this.firstSurnameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string SecondSurname
        {
            get
            {
                return this.secondSurnameField;
            }
            set
            {
                this.secondSurnameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("AddressInSpain", typeof(AddressType), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("OverseasAddress", typeof(OverseasAddressType), Form = XmlSchemaForm.Unqualified)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ContactDetailsType ContactDetails
        {
            get
            {
                return this.contactDetailsField;
            }
            set
            {
                this.contactDetailsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class LegalEntityType
    {

        private string corporateNameField;

        private string tradeNameField;

        private RegistrationDataType registrationDataField;

        private object itemField;

        private ContactDetailsType contactDetailsField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string CorporateName
        {
            get
            {
                return this.corporateNameField;
            }
            set
            {
                this.corporateNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string TradeName
        {
            get
            {
                return this.tradeNameField;
            }
            set
            {
                this.tradeNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public RegistrationDataType RegistrationData
        {
            get
            {
                return this.registrationDataField;
            }
            set
            {
                this.registrationDataField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("AddressInSpain", typeof(AddressType), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("OverseasAddress", typeof(OverseasAddressType), Form = XmlSchemaForm.Unqualified)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ContactDetailsType ContactDetails
        {
            get
            {
                return this.contactDetailsField;
            }
            set
            {
                this.contactDetailsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class RegistrationDataType
    {

        private string bookField;

        private string registerOfCompaniesLocationField;

        private string sheetField;

        private string folioField;

        private string sectionField;

        private string volumeField;

        private string additionalRegistrationDataField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Book
        {
            get
            {
                return this.bookField;
            }
            set
            {
                this.bookField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string RegisterOfCompaniesLocation
        {
            get
            {
                return this.registerOfCompaniesLocationField;
            }
            set
            {
                this.registerOfCompaniesLocationField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Sheet
        {
            get
            {
                return this.sheetField;
            }
            set
            {
                this.sheetField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Folio
        {
            get
            {
                return this.folioField;
            }
            set
            {
                this.folioField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Section
        {
            get
            {
                return this.sectionField;
            }
            set
            {
                this.sectionField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Volume
        {
            get
            {
                return this.volumeField;
            }
            set
            {
                this.volumeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string AdditionalRegistrationData
        {
            get
            {
                return this.additionalRegistrationDataField;
            }
            set
            {
                this.additionalRegistrationDataField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class PartiesType
    {

        private BusinessType sellerPartyField;

        private BusinessType buyerPartyField;
        internal Facturae Parent
        {
            get;
            set;
        }
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public BusinessType SellerParty
        {
            get
            {
                return this.sellerPartyField;
            }
            set
            {
                this.sellerPartyField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public BusinessType BuyerParty
        {
            get
            {
                return this.buyerPartyField;
            }
            set
            {
                this.buyerPartyField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class AssigneeType
    {

        private TaxIdentificationType taxIdentificationField;

        private object itemField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public TaxIdentificationType TaxIdentification
        {
            get
            {
                return this.taxIdentificationField;
            }
            set
            {
                this.taxIdentificationField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Individual", typeof(IndividualType), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("LegalEntity", typeof(LegalEntityType), Form = XmlSchemaForm.Unqualified)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class FactoringAssignmentDataType
    {

        private AssigneeType assigneeField;

        private List<InstallmentType> paymentDetailsField;

        private string factoringAssignmentClausesField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AssigneeType Assignee
        {
            get
            {
                return this.assigneeField;
            }
            set
            {
                this.assigneeField = value;
            }
        }

        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Installment", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<InstallmentType> PaymentDetails
        {
            get
            {
                return this.paymentDetailsField;
            }
            set
            {
                this.paymentDetailsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string FactoringAssignmentClauses
        {
            get
            {
                return this.factoringAssignmentClausesField;
            }
            set
            {
                this.factoringAssignmentClausesField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae")]
    public partial class BatchType
    {

        private string batchIdentifierField;

        private long invoicesCountField;

        private AmountType totalInvoicesAmountField;

        private AmountType totalOutstandingAmountField;

        private AmountType totalExecutableAmountField;

        private CurrencyCodeType invoiceCurrencyCodeField;

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string BatchIdentifier
        {
            get
            {
                return this.batchIdentifierField;
            }
            set
            {
                this.batchIdentifierField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public long InvoicesCount
        {
            get
            {
                return this.invoicesCountField;
            }
            set
            {
                this.invoicesCountField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType TotalInvoicesAmount
        {
            get
            {
                return this.totalInvoicesAmountField;
            }
            set
            {
                this.totalInvoicesAmountField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType TotalOutstandingAmount
        {
            get
            {
                return this.totalOutstandingAmountField;
            }
            set
            {
                this.totalOutstandingAmountField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType TotalExecutableAmount
        {
            get
            {
                return this.totalExecutableAmountField;
            }
            set
            {
                this.totalExecutableAmountField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CurrencyCodeType InvoiceCurrencyCode
        {
            get
            {
                return this.invoiceCurrencyCodeField;
            }
            set
            {
                this.invoiceCurrencyCodeField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("Transforms", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class TransformsType
    {

        private List<TransformType> transformField;

        /// <remarks/>
        [XmlElementAttribute("Transform")]
        public List<TransformType> Transform
        {
            get
            {
                return this.transformField;
            }
            set
            {
                this.transformField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("Manifest", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class ManifestType
    {

        private List<ReferenceType> referenceField;

        private string idField;

        /// <remarks/>
        [XmlElementAttribute("Reference")]
        public List<ReferenceType> Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("SignatureProperties", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignaturePropertiesType
    {

        private List<SignaturePropertyType> signaturePropertyField;

        private string idField;

        /// <remarks/>
        [XmlElementAttribute("SignatureProperty")]
        public List<SignaturePropertyType> SignatureProperty
        {
            get
            {
                return this.signaturePropertyField;
            }
            set
            {
                this.signaturePropertyField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRootAttribute("SignatureProperty", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignaturePropertyType
    {

        private List<XmlElement> itemsField;

        private List<string> textField;

        private string targetField;

        private string idField;

        /// <remarks/>
        [XmlAnyElementAttribute()]
        public List<XmlElement> Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [XmlTextAttribute()]
        public List<string> Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "anyURI")]
        public string Target
        {
            get
            {
                return this.targetField;
            }
            set
            {
                this.targetField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
}