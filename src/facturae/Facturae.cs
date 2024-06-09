﻿// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace FacturaE 
{
    using FacturaE.DataType;
    using FacturaE.XAdES;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    [XmlRootAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml", IsNullable=false)]
    public partial class Facturae 
    {
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public FileHeaderType FileHeader 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PartiesType Parties 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Invoice", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<InvoiceType> Invoices 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ExtensionsType Extensions 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class FileHeaderType 
    {
        public FileHeaderType() 
        {
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public SchemaVersionType SchemaVersion 
        {
            get;
            set;
        } = SchemaVersionType.Item322;
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ModalityType Modality 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public InvoiceIssuerTypeType InvoiceIssuerType 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ThirdPartyType ThirdParty 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public BatchType Batch 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public FactoringAssignmentDataType FactoringAssignmentData 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum SchemaVersionType 
    {
        /// <remarks/>
        [XmlEnumAttribute("3.2.2")]
        Item322,
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
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
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum InvoiceIssuerTypeType 
    {
        /// <summary>
        /// Seller (Sender) / Proveedor (Emisor)
        /// </summary>
        [XmlEnumAttribute("EM")]
        Seller,
        
        /// <summary>
        /// Buyer (Receiver) / Cliente (Receptor)
        /// </summary>
        [XmlEnumAttribute("RE")]
        Buyer,
        
        /// <summary>
        /// Third Party
        /// </summary>
        [XmlEnumAttribute("TE")]
        ThirdParty
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class ThirdPartyType 
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public TaxIdentificationType TaxIdentification 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute("Individual", typeof(IndividualType), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("LegalEntity", typeof(LegalEntityType), Form = XmlSchemaForm.Unqualified)]
        public object Item 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class TaxIdentificationType 
    {
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PersonTypeCodeType PersonTypeCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ResidenceTypeCodeType ResidenceTypeCode {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string TaxIdentificationNumber 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
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
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
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
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AttachmentType
    {
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AttachmentCompressionAlgorithmType AttachmentCompressionAlgorithm 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool AttachmentCompressionAlgorithmSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AttachmentFormatType AttachmentFormat 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AttachmentEncodingType AttachmentEncoding 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool AttachmentEncodingSpecified
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string AttachmentDescription 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string AttachmentData
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
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
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
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
        
        /// <remarks/>
        html,
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
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
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AdditionalDataType 
    {
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string RelatedInvoice 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Attachment", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public AttachmentType[] RelatedDocuments 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceAdditionalInformation 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ExtensionsType Extensions 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class ExtensionsType
    {
        /// <remarks/>
        [XmlAnyElementAttribute()]
        public XmlElement[] Any 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class SpecialTaxableEventType
    {
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public SpecialTaxableEventCodeType SpecialTaxableEventCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string SpecialTaxableEventReason
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum SpecialTaxableEventCodeType
    {
        /// <remarks/>
        [XmlEnumAttribute("01")]
        TaxableAndExemptFromTax,
        
        /// <remarks/>
        [XmlEnumAttribute("02")]
        NonTaxable,
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class DeliveryNoteType
    {
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string DeliveryNoteNumber
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime DeliveryNoteDate
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DeliveryNoteDateSpecified
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class InvoiceLineType
    {
        [XmlIgnore]
        internal InvoiceType Parent
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string IssuerContractReference
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime IssuerContractDate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool IssuerContractDateSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string IssuerTransactionReference 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime IssuerTransactionDate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool IssuerTransactionDateSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ReceiverContractReference 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime ReceiverContractDate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool ReceiverContractDateSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ReceiverTransactionReference 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime ReceiverTransactionDate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool ReceiverTransactionDateSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string FileReference 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime FileDate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool FileDateSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public double SequenceNumber 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool SequenceNumberSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("DeliveryNote", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<DeliveryNoteType> DeliveryNotesReferences 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ItemDescription 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public decimal Quantity 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public UnitOfMeasureType UnitOfMeasure 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool UnitOfMeasureSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType UnitPriceWithoutTax 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalCost 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Discount", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<DiscountType> DiscountsAndRebates 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Charge", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<ChargeType> Charges 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType GrossAmount 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Tax", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<TaxType> TaxesWithheld 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Tax", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<InvoiceLineTypeTax> TaxesOutputs 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PeriodDates LineItemPeriod 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime TransactionDate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TransactionDateSpecified
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string AdditionalLineItemInformation
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public SpecialTaxableEventType SpecialTaxableEvent
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ArticleCode
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ExtensionsType Extensions
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum UnitOfMeasureType 
    {        
        /// <remarks/>
        [XmlEnumAttribute("01")]
        Units,
        
        /// <remarks/>
        [XmlEnumAttribute("02")]
        Hours,
        
        /// <remarks/>
        [XmlEnumAttribute("03")]
        Kilograms,
        
        /// <remarks/>
        [XmlEnumAttribute("04")]
        Liters,
        
        /// <remarks/>
        [XmlEnumAttribute("05")]
        Other,
        
        /// <remarks/>
        [XmlEnumAttribute("06")]
        Boxes,
        
        /// <remarks/>
        [XmlEnumAttribute("07")]
        Trays,
        
        /// <remarks/>
        [XmlEnumAttribute("08")]
        Barrels,
        
        /// <remarks/>
        [XmlEnumAttribute("09")]
        Jerricans,
        
        /// <remarks/>
        [XmlEnumAttribute("10")]
        Bags,
        
        /// <remarks/>
        [XmlEnumAttribute("11")]
        Carboys,
        
        /// <remarks/>
        [XmlEnumAttribute("12")]
        Bottles,
        
        /// <remarks/>
        [XmlEnumAttribute("13")]
        Canisters,
        
        /// <remarks/>
        [XmlEnumAttribute("14")]
        TetraBriks,
        
        /// <remarks/>
        [XmlEnumAttribute("15")]
        Centiliters,
        
        /// <remarks/>
        [XmlEnumAttribute("16")]
        Centimeters,
        
        /// <remarks/>
        [XmlEnumAttribute("17")]
        Bins,
        
        /// <remarks/>
        [XmlEnumAttribute("18")]
        Dozens,
        
        /// <remarks/>
        [XmlEnumAttribute("19")]
        Cases,
        
        /// <remarks/>
        [XmlEnumAttribute("20")]
        Demijohns,
        
        /// <remarks/>
        [XmlEnumAttribute("21")]
        Grams,
        
        /// <remarks/>
        [XmlEnumAttribute("22")]
        Kilometers,
        
        /// <remarks/>
        [XmlEnumAttribute("23")]
        Cans,
        
        /// <remarks/>
        [XmlEnumAttribute("24")]
        Bunches,
        
        /// <remarks/>
        [XmlEnumAttribute("25")]
        Meters,
        
        /// <remarks/>
        [XmlEnumAttribute("26")]
        Milimeters,
        
        /// <remarks/>
        [XmlEnumAttribute("27")]
        SixPacks,
        
        /// <remarks/>
        [XmlEnumAttribute("28")]
        Packages,
        
        /// <remarks/>
        [XmlEnumAttribute("29")]
        Portions,
        
        /// <remarks/>
        [XmlEnumAttribute("30")]
        Rolls,
        
        /// <remarks/>
        [XmlEnumAttribute("31")]
        Envelopes,
        
        /// <remarks/>
        [XmlEnumAttribute("32")]
        Tubs,
        
        /// <remarks/>
        [XmlEnumAttribute("33")]
        CubicMeter,
        
        /// <remarks/>
        [XmlEnumAttribute("34")]
        Second,
        
        /// <remarks/>
        [XmlEnumAttribute("35")]
        Watt,
        
        /// <remarks/>
        [XmlEnumAttribute("36")]
        KilowattPerHour,
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class DiscountType
    {       
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string DiscountReason
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType DiscountRate
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DiscountRateSpecified {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType DiscountAmount {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class ChargeType
    {                
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ChargeReason
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType ChargeRate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool ChargeRateSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType ChargeAmount 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class TaxType 
    {                
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public TaxTypeCodeType TaxTypeCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TaxRate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType TaxableBase 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType TaxAmount 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum TaxTypeCodeType 
    {    
        /// <remarks/>
        [XmlEnumAttribute("01")]
        ValueAddedTax,
        
        /// <remarks/>
        [XmlEnumAttribute("02")]
        IPSI,
        
        /// <remarks/>
        [XmlEnumAttribute("03")]
        IGIC,
        
        /// <remarks/>
        [XmlEnumAttribute("04")]
        PersonalIncomeTax,
        
        /// <remarks/>
        [XmlEnumAttribute("05")]
        Other,
        
        /// <remarks/>
        [XmlEnumAttribute("06")]
        ITPAJD,
        
        /// <remarks/>
        [XmlEnumAttribute("07")]
        IE,
        
        /// <remarks/>
        [XmlEnumAttribute("08")]
        CustomsDuties,
        
        /// <remarks/>
        [XmlEnumAttribute("09")]
        IGTECM,
        
        /// <remarks/>
        [XmlEnumAttribute("10")]
        IECDPCAC,
        
        /// <remarks/>
        [XmlEnumAttribute("11")]
        IIIMAB,
        
        /// <remarks/>
        [XmlEnumAttribute("12")]
        ICIO,
        
        /// <remarks/>
        [XmlEnumAttribute("13")]
        IMVDN,
        
        /// <remarks/>
        [XmlEnumAttribute("14")]
        IMSN,
        
        /// <remarks/>
        [XmlEnumAttribute("15")]
        IMGSN,
        
        /// <remarks/>
        [XmlEnumAttribute("16")]
        IMPN,
        
        /// <remarks/>
        [XmlEnumAttribute("17")]
        REIVA,
        
        /// <remarks/>
        [XmlEnumAttribute("18")]
        REIGIC,
        
        /// <remarks/>
        [XmlEnumAttribute("19")]
        REIPSI,
        
        /// <remarks/>
        [XmlEnumAttribute("20")]
        IPS,
        
        /// <remarks/>
        [XmlEnumAttribute("21")]
        SWUA,
        
        /// <remarks/>
        [XmlEnumAttribute("22")]
        IVPEE,
        
        /// <remarks/>
        [XmlEnumAttribute("23")]
        Item23,
        
        /// <remarks/>
        [XmlEnumAttribute("24")]
        Item24,
        
        /// <remarks/>
        [XmlEnumAttribute("25")]
        IDEC,
        
        /// <remarks/>
        [XmlEnumAttribute("26")]
        Item26,
        
        /// <remarks/>
        [XmlEnumAttribute("27")]
        IGFEI,
        
        /// <remarks/>
        [XmlEnumAttribute("28")]
        IRNR,
        
        /// <remarks/>
        [XmlEnumAttribute("29")]
        CorporationTax,
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AmountType
    {               
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalAmount 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleTwoDecimalType EquivalentInEuros 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool EquivalentInEurosSpecified 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class InvoiceLineTypeTax : TaxOutputType 
    {
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class TaxOutputType 
    {
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public TaxTypeCodeType TaxTypeCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TaxRate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType TaxableBase 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType TaxAmount 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType SpecialTaxableBase 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType SpecialTaxAmount 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleTwoDecimalType EquivalenceSurcharge 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool EquivalenceSurchargeSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType EquivalenceSurchargeAmount 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class PeriodDates {
        
        private System.DateTime startDateField;
        
        private System.DateTime endDateField;
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime StartDate {
            get {
                return this.startDateField;
            }
            set {
                this.startDateField = value;
            }
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime EndDate {
            get {
                return this.endDateField;
            }
            set {
                this.endDateField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class PaymentInKindType 
    {       
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PaymentInKindReason 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType PaymentInKindAmount 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AmountsWithheldType 
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string WithholdingReason 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType WithholdingRate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool WithholdingRateSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType WithholdingAmount 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class ReimbursableExpensesType 
    {
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public TaxIdentificationType ReimbursableExpensesSellerParty 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public TaxIdentificationType ReimbursableExpensesBuyerParty 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime IssueDate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool IssueDateSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceNumber 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceSeriesCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType ReimbursableExpensesAmount 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class PaymentOnAccountType 
    {
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime PaymentOnAccountDate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType PaymentOnAccountAmount
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class SubsidyType 
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string SubsidyDescription 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType SubsidyRate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool SubsidyRateSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType SubsidyAmount 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class InvoiceTotalsType
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalGrossAmount 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Discount", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<DiscountType> GeneralDiscounts 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Charge", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<ChargeType> GeneralSurcharges 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalGeneralDiscounts 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TotalGeneralDiscountsSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalGeneralSurcharges 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TotalGeneralSurchargesSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalGrossAmountBeforeTaxes 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalTaxOutputs 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalTaxesWithheld 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType InvoiceTotal 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Subsidy", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<SubsidyType> Subsidies 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("PaymentOnAccount", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<PaymentOnAccountType> PaymentsOnAccount 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("ReimbursableExpenses", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<ReimbursableExpensesType> ReimbursableExpenses 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleTwoDecimalType TotalFinancialExpenses 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TotalFinancialExpensesSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalOutstandingAmount 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalPaymentsOnAccount 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TotalPaymentsOnAccountSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountsWithheldType AmountsWithheld 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalExecutableAmount 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType TotalReimbursableExpenses 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TotalReimbursableExpensesSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PaymentInKindType PaymentInKind 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class ExchangeRateDetailsType
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleUpToEightDecimalType ExchangeRate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime ExchangeRateDate 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class PlaceOfIssueType 
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PostCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PlaceOfIssueDescription 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class InvoiceIssueDataType 
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime IssueDate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime? OperationDate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool OperationDateSpecified
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PlaceOfIssueType PlaceOfIssue 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PeriodDates InvoicingPeriod 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CurrencyCodeType InvoiceCurrencyCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ExchangeRateDetailsType ExchangeRateDetails 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CurrencyCodeType TaxCurrencyCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public LanguageCodeType LanguageName 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceDescription 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ReceiverTransactionReference 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string FileReference 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ReceiverContractReference 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
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
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
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
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class CorrectiveType
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceNumber 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceSeriesCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ReasonCodeType ReasonCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ReasonDescriptionType ReasonDescription 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PeriodDates TaxPeriod 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CorrectionMethodType CorrectionMethod 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CorrectionMethodDescriptionType CorrectionMethodDescription 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string AdditionalReasonDescription 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime InvoiceIssueDate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool InvoiceIssueDateSpecified 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
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
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
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
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum CorrectionMethodType {
        
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
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
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
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class InvoiceHeaderType
    {               
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceNumber
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string InvoiceSeriesCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public InvoiceDocumentTypeType InvoiceDocumentType 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public InvoiceClassType InvoiceClass 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CorrectiveType Corrective 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum InvoiceDocumentTypeType
    {        
        /// <remarks/>
        [XmlEnumAttribute("FC")]
        CompleteInvoce,
        
        /// <remarks/>
        [XmlEnumAttribute("FA")]
        Abbreviated,
        
        /// <remarks/>
        [XmlEnumAttribute("AF")]
        SelfInvoice,
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum InvoiceClassType
    {        
        /// <remarks/>
        [XmlEnumAttribute("OO")]
        OriginalInvoice,
        
        /// <remarks/>
        [XmlEnumAttribute("OR")]
        Corrective,
        
        /// <remarks/>
        [XmlEnumAttribute("OC")]
        Summary,
        
        /// <remarks/>
        [XmlEnumAttribute("CO")]
        CopyOfTheOriginal,
        
        /// <remarks/>
        [XmlEnumAttribute("CR")]
        CopyOfTheCorrective,
        
        /// <remarks/>
        [XmlEnumAttribute("CC")]
        CopyOfTheSummary
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class InvoiceType 
    {
        [XmlIgnore]
        internal Facturae Parent
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public InvoiceHeaderType InvoiceHeader 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public InvoiceIssueDataType InvoiceIssueData 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Tax", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<TaxOutputType> TaxesOutputs 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Tax", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<TaxType> TaxesWithheld 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public InvoiceTotalsType InvoiceTotals 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("InvoiceLine", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<InvoiceLineType> Items 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Installment", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<InstallmentType> PaymentDetails 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("LegalReference", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<string> LegalLiterals 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AdditionalDataType AdditionalData 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class InstallmentType 
    {               
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime InstallmentDueDate 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public DoubleTwoDecimalType InstallmentAmount 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public PaymentMeansType PaymentMeans 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AccountType AccountToBeCredited 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PaymentReconciliationReference 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AccountType AccountToBeDebited 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string CollectionAdditionalInformation 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string RegulatoryReportingData 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string DebitReconciliationReference 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
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
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AccountType
    {
        /// <remarks/>
        [XmlElementAttribute("AccountNumber", typeof(string), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("IBAN", typeof(string), Form = XmlSchemaForm.Unqualified)]
        [XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string BankCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string BranchCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute("BranchInSpainAddress", typeof(AddressType), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("OverseasBranchAddress", typeof(OverseasAddressType), Form = XmlSchemaForm.Unqualified)]
        public object Item1 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string BIC 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml", IncludeInSchema=false)]
    public enum ItemChoiceType 
    {        
        /// <remarks/>
        [XmlEnumAttribute(":AccountNumber")]
        AccountNumber,
        
        /// <remarks/>
        [XmlEnumAttribute(":IBAN")]
        IBAN,
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AddressType 
    {       
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Address 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PostCode
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Town 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Province 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CountryType CountryCode 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
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
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class OverseasAddressType 
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Address 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PostCodeAndTown 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Province 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CountryType CountryCode 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AdministrativeCentreType 
    {
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string CentreCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public RoleTypeCodeType RoleTypeCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool RoleTypeCodeSpecified 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Name 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string FirstSurname 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string SecondSurname 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute("AddressInSpain", typeof(AddressType), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("OverseasAddress", typeof(OverseasAddressType), Form = XmlSchemaForm.Unqualified)]
        public object Item 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ContactDetailsType ContactDetails 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PhysicalGLN 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string LogicalOperationalPoint 
        {            
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string CentreDescription 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
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
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class ContactDetailsType 
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Telephone 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string TeleFax 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string WebAddress 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ElectronicMail 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string ContactPersons 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string CnoCnae 
        {            
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string INETownCode 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string AdditionalContactDetails 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class BusinessType 
    {
        [XmlIgnore]
        internal PartiesType Parent
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public TaxIdentificationType TaxIdentification 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string PartyIdentification 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("AdministrativeCentre", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public List<AdministrativeCentreType> AdministrativeCentres 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute("Individual", typeof(IndividualType), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("LegalEntity", typeof(LegalEntityType), Form = XmlSchemaForm.Unqualified)]
        public object Item 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class IndividualType
    {
        [XmlIgnore]
        internal BusinessType Parent
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Name
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string FirstSurname
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string SecondSurname
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute("AddressInSpain", typeof(AddressType), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("OverseasAddress", typeof(OverseasAddressType), Form = XmlSchemaForm.Unqualified)]
        public object Item
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ContactDetailsType ContactDetails
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class LegalEntityType
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string CorporateName
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string TradeName
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public RegistrationDataType RegistrationData
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute("AddressInSpain", typeof(AddressType), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("OverseasAddress", typeof(OverseasAddressType), Form = XmlSchemaForm.Unqualified)]
        public object Item
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public ContactDetailsType ContactDetails
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class RegistrationDataType
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Book
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string RegisterOfCompaniesLocation
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Sheet 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Folio 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Section 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Volume 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string AdditionalRegistrationData 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class PartiesType
    {
        [XmlIgnore]
        internal Facturae Parent
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public BusinessType SellerParty 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public BusinessType BuyerParty {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class RepositoryType 
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string RepositoryName 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string URL 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string Reference 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class FactoringAssignmentDocumentType 
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string DocumentCharacter 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string RepresentationIdentity 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string DocumentType 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public RepositoryType Repository 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AssigneeType 
    {        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public TaxIdentificationType TaxIdentification 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute("Individual", typeof(IndividualType), Form = XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("LegalEntity", typeof(LegalEntityType), Form = XmlSchemaForm.Unqualified)]
        public object Item 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class FactoringAssignmentDataType 
    {
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AssigneeType Assignee 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlArrayAttribute(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("Installment", Form = XmlSchemaForm.Unqualified, IsNullable=false)]
        public InstallmentType[] PaymentDetails 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string FactoringAssignmentClauses 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute("FactoringAssignmentDocument", Form = XmlSchemaForm.Unqualified)]
        public FactoringAssignmentDocumentType[] FactoringAssignmentDocument 
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class BatchType 
    {
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public string BatchIdentifier 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public long InvoicesCount 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType TotalInvoicesAmount 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType TotalOutstandingAmount 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public AmountType TotalExecutableAmount 
        {
            get;
            set;
        }
        
        /// <remarks/>
        [XmlElementAttribute(Form = XmlSchemaForm.Unqualified)]
        public CurrencyCodeType InvoiceCurrencyCode 
        {
            get;
            set;
        }
    }      
}
