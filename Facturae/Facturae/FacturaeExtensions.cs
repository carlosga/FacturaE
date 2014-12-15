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

using FacturaE.XAdES;
using FacturaE.Xml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FacturaE
{
    /// <summary>
    /// Facturae extensions
    /// </summary>
    public partial class Facturae
    {
        #region · Static Members ·

        static readonly XmlSerializer FacturaeSerializer = new XmlSerializer(typeof(Facturae));

        #endregion

        #region · Constructors ·

        /// <summary>
        /// Initializes a new instance of the <see cref="Facturae"/> class.
        /// </summary>
        public Facturae()
        {
            this.FileHeader = new FileHeaderType
            {
                Batch         = new BatchType()
              , SchemaVersion = SchemaVersionType.Item32
            };
            this.Parties = new PartiesType
            {
                SellerParty = new BusinessType(this)
              , BuyerParty  = new BusinessType(this)
              , Parent      = this
            };
                
            this.Invoices = new List<InvoiceType>();
            this.SetCurrency(CurrencyCodeType.EUR);
            this.SetIssuer(InvoiceIssuerTypeType.EM);
        }

        #endregion

        #region · Facturae Extensions ·

        /// <summary>
        /// Sets the currency code of the electronic invoice
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <param name="currencyCode">The currency code</param>
        /// <returns></returns>
        public Facturae SetCurrency(CurrencyCodeType currencyCode)
        {
            this.FileHeader.Batch.InvoiceCurrencyCode = currencyCode;

            return this;
        }

        /// <summary>
        /// Sets the electronic invoice schema version
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <param name="schemaVersion">The schema version</param>
        /// <returns></returns>
        public Facturae SetSchemaVersion(SchemaVersionType schemaVersion)
        {
            this.FileHeader.SchemaVersion = schemaVersion;

            return this;
        }
        
        /// <summary>
        /// Sets the electronic invoice issuer
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <param name="issuerType">The issuer type</param>
        /// <returns></returns>
        public Facturae SetIssuer(InvoiceIssuerTypeType issuerType)
        {
            this.FileHeader.InvoiceIssuerType = issuerType;

            return this;
        }

        /// <summary>
        /// Gets the electronic invoice seller
        /// </summary>
        /// <param name="parties"></param>
        /// <returns></returns>
        public BusinessType Seller()
        {
            return this.Parties.SellerParty;
        }

        /// <summary>
        /// Gets the electronic invoice buyer
        /// </summary>
        /// <param name="parties"></param>
        /// <returns></returns>
        public BusinessType Buyer()
        {
            return this.Parties.BuyerParty;
        }
           
        /// <summary>
        /// Calculates the electronic totals
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <returns></returns>
        public Facturae CalculateTotals()
        {
            var firstInvoice = this.Invoices[0];

            this.FileHeader.Modality                     = ((this.Invoices.Count == 1) ? ModalityType.Single : ModalityType.Batch);
            this.FileHeader.Batch.InvoicesCount          = this.Invoices.Count;
            this.FileHeader.Batch.TotalInvoicesAmount    = this.SumTotalAmounts();
            this.FileHeader.Batch.TotalOutstandingAmount = this.SumTotalOutstandingAmount();
            this.FileHeader.Batch.TotalExecutableAmount  = this.SumTotalExecutableAmount();
            this.FileHeader.Batch.BatchIdentifier        = firstInvoice.InvoiceHeader.InvoiceNumber 
                                                         + firstInvoice.InvoiceHeader.InvoiceSeriesCode;

            return this;
        }

        /// <summary>
        /// Sums the invoice total
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <returns>An instance of AmountType</returns>
        public AmountType SumTotalAmounts()
        {
            return new AmountType
            {
                TotalAmount       = Math.Round(this.Invoices.Sum(il => il.InvoiceTotals.InvoiceTotal), 2)
              , EquivalentInEuros = Math.Round(this.Invoices.Sum(il => il.InvoiceTotals.InvoiceTotal), 2)
            };
        }

        /// <summary>
        /// Sums the electronic invoice outstanding amount
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <returns>An instance of AmountType</returns>
        public AmountType SumTotalOutstandingAmount()
        {
            return new AmountType
            {
                TotalAmount       = Math.Round(this.Invoices.Sum(il => il.InvoiceTotals.TotalOutstandingAmount), 2)
              , EquivalentInEuros = Math.Round(this.Invoices.Sum(il => il.InvoiceTotals.TotalOutstandingAmount), 2)
            };
        }

        /// <summary>
        /// Sums the electronic invoice executable amount
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <returns>An instance of AmountType</returns>
        public AmountType SumTotalExecutableAmount()
        {
            return new AmountType
            {
                TotalAmount       = Math.Round(this.Invoices.Sum(il => il.InvoiceTotals.TotalExecutableAmount), 2)
              , EquivalentInEuros = Math.Round(this.Invoices.Sum(il => il.InvoiceTotals.TotalExecutableAmount), 2)
            };
        }
        
        /// <summary>
        /// Creates a new invoice
        /// </summary>
        /// <param name="eInvoice"></param>
        /// <returns></returns>
        public InvoiceType CreateInvoice()
        {
            var invoice = new InvoiceType
            {
                Parent           = this
              , InvoiceHeader    = new InvoiceHeaderType()
              , InvoiceTotals    = new InvoiceTotalsType()
              , InvoiceIssueData = new InvoiceIssueDataType()
              , Items            = new List<InvoiceLineType>()
            };

            this.Invoices.Add(invoice);

            return invoice;
        }

        #endregion

        #region · XML Validation Extensions ·

        /// <summary>
        /// Validates the electronic invoice XML
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <returns></returns>
        public Facturae Validate()
        {
            XmlDocument document = this.ToXmlDocument();
            
            document.Schemas.Add(XsdSchemas.BuildSchemaSet(document.NameTable));
            document.Validate(XmlValidationEventHandler);

            return this;
        }

        static void XmlValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error)
            {
                throw e.Exception;
            }
            else
            {
                Trace.Write(String.Format("Warning while validating XML '{0}'", e.Message));
            }
        }

        #endregion

        #region · XML Signature Extensions ·

        /// <summary>
        /// Signs the electronic invoice using the given certificate.
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <param name="certificate">The certificate</param>
        /// <returns></returns>
        public XAdESSignatureVerifier Sign(X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate cannot be null");
            }

            return this.Sign(certificate, (RSA)certificate.PrivateKey);
        }

        /// <summary>
        /// Signs the electronic invoice using the given certificate & RSA key
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <param name="certificate">The certificate</param>
        /// <param name="key">The RSA Key</param>
        /// <returns></returns>
        public XAdESSignatureVerifier Sign(X509Certificate2 certificate, RSA key)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate cannot be null");
            }
            if (key == null)
            {
                throw new ArgumentNullException("key cannot be null");
            }

            var document  = this.ToXmlDocument();
            var signedXml = new XAdESSignedXml(document);

            // Set the key to sign
            signedXml.SigningKey = key;

            signedXml.SetSignatureInfo()
                     .SetKeyInfo(certificate, (RSA)certificate.PublicKey.Key)   // Key Info
                     .SetQualifyingPropertiesObject(certificate)                // Add XAdES references
                     .ComputeSignature();                                       // Compute Signature
            
            // Import the signed XML node 
            document.DocumentElement.AppendChild(document.ImportNode(signedXml.GetXml(), true));            

            return new XAdESSignatureVerifier(document);
        }

        #endregion

        #region · IO Extensions ·

        /// <summary>
        /// Writes the electronic invoice to the given path
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <param name="path">The file path</param>
        /// <returns></returns>
        public Facturae WriteToFile(string path)
        {
            this.ToXmlDocument().Save(path);

            return this;
        }

        #endregion

        #region · Private Extensions ·

        private string ToXml()
        {
            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false)
            };
            
            using (MemoryStream buffer = new MemoryStream())
            { 
                using (XmlWriter writer = XmlWriter.Create(buffer, settings))
                {
                    FacturaeSerializer.Serialize(writer, this, XsdSchemas.CreateXadesSerializerNamespace());
                }

                return Encoding.UTF8.GetString(buffer.ToArray());
            }
        }

        private XmlDocument ToXmlDocument()
        {
            var document = new XmlDocument { PreserveWhitespace = true };

            document.LoadXml(this.ToXml());

            return document;
        }
       
        #endregion
    }

    public partial class InvoiceType
    {
        #region · InvoiceType Extensions ·

        /// <summary>
        /// Set the invoice series code
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="seriesCode">The invoice series code</param>
        /// <returns></returns>
        public InvoiceType SetInvoiceSeries(string seriesCode)
        {
            this.InvoiceHeader.InvoiceSeriesCode = seriesCode;

            return this;
        }

        /// <summary>
        /// Sets the invoice number
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="invoiceNumber">The invoice number</param>
        /// <returns></returns>
        public InvoiceType SetInvoiceNumber(string invoiceNumber)
        {
            this.InvoiceHeader.InvoiceNumber = invoiceNumber;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as a complete invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public InvoiceType IsComplete()
        {
            this.InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.Complete;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as a abbreviated invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public InvoiceType IsAbbreviated()
        {
            this.InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.Abbreviated;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as a self invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public InvoiceType IsSelfInvoice()
        {
            this.InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.SelfInvoice;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as a original invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public InvoiceType IsOriginal()
        {
            this.InvoiceHeader.InvoiceClass = InvoiceClassType.Original;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as a corrective invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public InvoiceType IsCorrective()
        {
            this.InvoiceHeader.InvoiceClass = InvoiceClassType.Corrective;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as summary of original invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public InvoiceType IsSummaryOriginal()
        {
            this.InvoiceHeader.InvoiceClass = InvoiceClassType.SummaryOriginal;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as copy of original invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public InvoiceType IsCopyOfOriginal()
        {
            this.InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfOriginal;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as copy of corrective invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public InvoiceType IsCopyOfCorrective()
        {
            this.InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfCorrective;
            
            return this;
        }

        /// <summary>
        /// Sets the invoice class as copy of summary invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public InvoiceType IsCopyOfSummary()
        {
            this.InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfSummary;

            return this;
        }

        /// <summary>
        /// Sets the invoice issue date
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="date">The invoice issue date</param>
        /// <returns></returns>
        public InvoiceType GiveIssueDate(DateTime date)
        {
            this.InvoiceIssueData.IssueDate = date;

            return this;
        }

        /// <summary>
        /// Sets the invoice operation date
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="date">The invoice operation date</param>
        /// <returns></returns>
        public InvoiceType SetOperationDate(DateTime date)
        {
            this.InvoiceIssueData.OperationDate = date;
            
            return this;
        }

        /// <summary>
        /// Set the invoice place of issue
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="description">The place of issue description</param>
        /// <param name="postCode">The place of issue post code</param>
        /// <returns></returns>
        public InvoiceType SetPlaceOfIssue(string description, string postCode)
        {
            this.InvoiceIssueData.PlaceOfIssue = new PlaceOfIssueType
            {
                PlaceOfIssueDescription = description
              , PostCode                = postCode
            };
            
            return this;
        }

        /// <summary>
        /// Sets the invoicing period of an invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns></returns>
        public InvoiceType SetInvoicingPeriod(DateTime startDate, DateTime endDate)
        {
            this.InvoiceIssueData.InvoicingPeriod = new PeriodDates
            {
                StartDate = startDate
              , EndDate   = endDate
            };
            
            return this;
        }

        /// <summary>
        /// Sets the invoice currency
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="currency">The invoice currency</param>
        /// <returns></returns>
        public InvoiceType SetCurrency(CurrencyCodeType currency)
        {
            this.InvoiceIssueData.InvoiceCurrencyCode = currency;
            
            return this;
        }

        /// <summary>
        /// Sets the invoice currency exchange rate
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="exchangeRate">The exchange rate</param>
        /// <param name="exchangeDate">The exchange date</param>
        /// <returns></returns>
        public InvoiceType SetExchangeRate(double exchangeRate, DateTime exchangeDate)
        {
            this.InvoiceIssueData.ExchangeRateDetails = new ExchangeRateDetailsType
            {
                ExchangeRate = exchangeRate
            };
            
            return this;
        }

        /// <summary>
        /// Sets the invoice tax currency
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="taxCurrency">The invoice currency</param>
        /// <returns></returns>
        public InvoiceType SetTaxCurrency(CurrencyCodeType taxCurrency)
        {
            this.InvoiceIssueData.TaxCurrencyCode = taxCurrency;
                        
            return this;
        }

        /// <summary>
        /// Sets the invoice language
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="language">The invoice language</param>
        /// <returns></returns>
        public InvoiceType SetLanguage(LanguageCodeType language)
        {
            this.InvoiceIssueData.LanguageName = language;

            return this;
        }

        public InvoiceLineType AddInvoiceItem(string articleCode, string productDescription)
        {
            var item = new InvoiceLineType
            {
                Parent          = this
              , ArticleCode     = articleCode
              , ItemDescription = productDescription
            };

            this.Items.Add(item);

            return item;
        }

        /// <summary>
        /// Calculates the invoice totals
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public Facturae CalculateTotals()
        {
            double subsidyAmount = 0;

            // Taxes Outputs
            var q = from tax in this.Items.SelectMany(x => x.TaxesOutputs)
                    group tax by tax.TaxRate into g
                    select new TaxOutputType
                    {
                        TaxRate     = g.Key,
                        TaxableBase = new AmountType 
                        { 
                            TotalAmount       = Math.Round(g.Sum(gtax => gtax.TaxableBase.TotalAmount), 2),
                            EquivalentInEuros = Math.Round(g.Sum(gtax => gtax.TaxableBase.EquivalentInEuros), 2)
                        },
                        TaxAmount   = new AmountType 
                        { 
                            TotalAmount       = Math.Round(g.Sum(gtax => gtax.TaxAmount.TotalAmount), 2),
                            EquivalentInEuros = Math.Round(g.Sum(gtax => gtax.TaxAmount.EquivalentInEuros), 2)
                        },
                        TaxTypeCode = TaxTypeCodeType.Item01,
                    };

            this.TaxesOutputs = q.ToList();

            // Invoice totals
            this.InvoiceTotals = new InvoiceTotalsType();

            // Calculate totals
            this.InvoiceTotals.TotalGrossAmount = Math.Round(this.Items.Sum(it => it.GrossAmount), 2);

            this.CalculateGeneralDiscountTotals();

            this.CalculateGeneralSurchargesTotals();

            this.InvoiceTotals.TotalGrossAmountBeforeTaxes = Math.Round(this.InvoiceTotals.TotalGrossAmount      
                                                                      - this.InvoiceTotals.TotalGeneralDiscounts 
                                                                      + this.InvoiceTotals.TotalGeneralSurcharges, 2);

            this.CalculatePaymentsOnAccountTotals();

            this.CalculateReimbursableExpensesTotals();
            
            // Total impuestos retenidos.
            this.CalculateTotalTaxesWithheldTotals();

            // Sum of different fields Tax Amounts + Total Equivalence 
            // Surcharges. Always to two decimal points.
            this.InvoiceTotals.TotalTaxOutputs = Math.Round(this.CalculateTaxOutputTotal(), 2);

            this.InvoiceTotals.InvoiceTotal = Math.Round(this.InvoiceTotals.TotalGrossAmountBeforeTaxes   
                                                       + this.InvoiceTotals.TotalTaxOutputs               
                                                       - this.InvoiceTotals.TotalTaxesWithheld, 2);

            // Total de gastos financieros
#warning TODO: Hacer un extension method para que se pueda indicar
            this.InvoiceTotals.TotalFinancialExpenses = 0;
            
            if (this.InvoiceTotals.Subsidies != null)
            {
                this.CalculateSubsidyAmounts();

                subsidyAmount = Math.Round(this.InvoiceTotals.Subsidies.Sum(s => s.SubsidyAmount), 2);
            }

            this.InvoiceTotals.TotalOutstandingAmount = Math.Round
            (
                this.InvoiceTotals.InvoiceTotal - (subsidyAmount + this.InvoiceTotals.TotalPaymentsOnAccount), 2
            );

            this.InvoiceTotals.TotalExecutableAmount = Math.Round(this.InvoiceTotals.TotalOutstandingAmount
                                                                - this.InvoiceTotals.TotalTaxesWithheld
                                                                + this.InvoiceTotals.TotalReimbursableExpenses
                                                                + this.InvoiceTotals.TotalFinancialExpenses, 2);

            return this.Parent;
        }

        private void CalculateSubsidyAmounts()
        {
            // Rate applied to the Invoice Total.
            this.InvoiceTotals.Subsidies.ForEach
            (
                s => s.SubsidyAmount = Math.Round(this.InvoiceTotals.InvoiceTotal * s.SubsidyRate / 100, 2)
            );
        }

        private void CalculateTotalTaxesWithheldTotals()
        {
            this.InvoiceTotals.TotalTaxesWithheld = Math.Round
            (
                this.Items.Sum
                (
                    il =>
                    {
                        double total = 0;

                        if (il.TaxesWithheld != null)
                        {
                            total = Math.Round(il.TaxesWithheld.Sum(tw => tw.TaxAmount.TotalAmount), 2);
                        }

                        return total;
                    }
                )
            );
        }

        private void CalculateReimbursableExpensesTotals()
        {
            // Total de suplidos
            if (this.InvoiceTotals.ReimbursableExpenses != null)
            {
                this.InvoiceTotals.TotalReimbursableExpenses = Math.Round
                (
                    this.InvoiceTotals.ReimbursableExpenses.Sum(re => re.ReimbursableExpensesAmount), 2
                );
            }
        }

        private void CalculatePaymentsOnAccountTotals()
        {
            if (this.InvoiceTotals.PaymentsOnAccount != null)
            {
                this.InvoiceTotals.TotalPaymentsOnAccount = Math.Round
                (
                    this.InvoiceTotals.PaymentsOnAccount.Sum(poa => poa.PaymentOnAccountAmount), 2
                );
            }
        }

        private void CalculateGeneralSurchargesTotals()
        {
            if (this.InvoiceTotals.GeneralSurcharges != null)
            {
                this.InvoiceTotals.GeneralSurcharges.ForEach
                (
                    gs => gs.ChargeAmount = Math.Round((this.InvoiceTotals.TotalGrossAmount * gs.ChargeRate) / 100, 2)
                );

                this.InvoiceTotals.TotalGeneralSurcharges = Math.Round
                (
                    this.InvoiceTotals.GeneralSurcharges.Sum(gs => gs.ChargeAmount), 2
                );
            }
        }

        private void CalculateGeneralDiscountTotals()
        {
            if (this.InvoiceTotals.GeneralDiscounts != null)
            {
                this.InvoiceTotals.GeneralDiscounts.ForEach
                (
                    gd => gd.DiscountAmount = Math.Round((this.InvoiceTotals.TotalGrossAmount * gd.DiscountRate) / 100, 2)
                );

                this.InvoiceTotals.TotalGeneralDiscounts = Math.Round
                (
                    this.InvoiceTotals.GeneralDiscounts.Sum(gd => gd.DiscountAmount), 2
                );
            }
        }

        private double CalculateTaxOutputTotal()
        {
            return this.TaxesOutputs.Sum(to => to.TaxAmount.TotalAmount);
        }

        #endregion
    }

    public partial class InvoiceLineType
    {
        #region · InvoiceLineType Extensions ·

        public InvoiceLineType GiveQuantity(double quantity)
        {
            this.Quantity = quantity;

            return this;
        }

        public InvoiceLineType GiveUnitPriceWithoutTax(double price)
        {
            this.UnitPriceWithoutTax = price;

            return this;
        }

        public InvoiceLineType GiveDiscount(double discountRate)
        {
            if (this.DiscountsAndRebates == null)
            {
                this.DiscountsAndRebates = new List<DiscountType>();
            }

            var discount = new DiscountType
            {
                DiscountRate   = discountRate
              , DiscountReason = "Descuento"
            };

            this.DiscountsAndRebates.Add(discount);

            return this;
        }

        public InvoiceLineType GiveTax(double taxRate)
        {
            if (this.TaxesOutputs == null)
            {
                this.TaxesOutputs = new List<InvoiceLineTypeTax>();
            }

            var tax = new InvoiceLineTypeTax
            {
                TaxTypeCode = TaxTypeCodeType.Item01
              , TaxRate     = taxRate
            };

            this.TaxesOutputs.Add(tax);

            return this;
        }

        public InvoiceType CalculateTotals()
        {
            double totalDiscounts = 0;
            double totalCharges   = 0;

            this.TotalCost = Math.Round(this.Quantity * this.UnitPriceWithoutTax, 2);

            if (this.DiscountsAndRebates != null)
            {
                this.DiscountsAndRebates.ForEach
                (
                    dar => 
                    {
                        dar.DiscountAmount = Math.Round(this.TotalCost * dar.DiscountRate / 100, 2);
                        totalDiscounts     = Math.Round(totalDiscounts + dar.DiscountAmount, 2);
                    }
                );
            }

            if (this.Charges != null)
            {
                this.Charges.ForEach
                (
                    chr =>
                    {
                        chr.ChargeAmount = Math.Round(this.TotalCost * chr.ChargeRate / 100, 2);
                        totalCharges     = Math.Round(totalCharges + chr.ChargeAmount, 2);
                    }
                );
            }

            this.GrossAmount = this.TotalCost - totalDiscounts + totalCharges;

            this.TaxesOutputs.ForEach
            (
                tax =>
                {
                    if (tax.TaxableBase == null)
                    {
                        tax.TaxableBase = new AmountType();
                    }

                    tax.TaxableBase.TotalAmount       = this.GrossAmount;
                    tax.TaxableBase.EquivalentInEuros = this.GrossAmount;

                    if (tax.TaxAmount == null)
                    {
                        tax.TaxAmount = new AmountType();
                    }

                    tax.TaxAmount.TotalAmount = Math.Round(tax.TaxableBase.TotalAmount * tax.TaxRate / 100, 2);
                }
            );

            return this.Parent;
        }

        #endregion
    }

    public partial class BusinessType
    {
        #region · Fields ·

        private Facturae parent;

        #endregion

        #region · Constructors ·

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessType"/> class.
        /// </summary>
        public BusinessType()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessType"/> class.
        /// </summary>
        public BusinessType(Facturae parent)
        {
            this.parent            = parent;
            this.TaxIdentification = new TaxIdentificationType();
        }

        #endregion

        #region · BusinessType Extensions ·

        public Facturae Invoice()
        {
            return this.parent;
        }

        /// <summary>
        /// Sets the identification of a invoice business party
        /// </summary>
        /// <param name="party">The business party</param>
        /// <param name="identification">The identification</param>
        /// <returns></returns>
        public BusinessType SetIdentification(string identification)
        {
            this.PartyIdentification = identification;
  
            return this;
        }

        /// <summary>
        /// Sets an invoice business party as an individual
        /// </summary>
        /// <param name="party">The business party</param>
        /// <returns></returns>
        public IndividualType IsIndividual()
        {
            var individual = new IndividualType { Parent = this };

            this.Item = individual;

            if (this.TaxIdentification.ResidenceTypeCode == ResidenceTypeCodeType.ResidentInSpain)
            {
                AddressType address = new AddressType();
                    
                address.CountryCode = CountryType.ESP;
                    
                individual.Item = address;
            }
            else
            {
                individual.Item = new OverseasAddressType();
            }

            this.TaxIdentification.PersonTypeCode = PersonTypeCodeType.Individual;

            return individual;
        }

        /// <summary>
        /// Sets the identification number
        /// </summary>
        /// <param name="taxIdentification">The tax identification</param>
        /// <param name="identificationNumber">The identification number</param>
        /// <returns></returns>
        public BusinessType SetIdentificationNumber(string identificationNumber)
        {
            this.TaxIdentification.TaxIdentificationNumber = identificationNumber;
  
            return this;
        }

        /// <summary>
        /// Sets the tax identification as a legal entity identification
        /// </summary>
        /// <param name="taxIdentification">The tax identification</param>
        /// <returns></returns>
        public BusinessType IsLegalEntity()
        {
            this.TaxIdentification.PersonTypeCode = PersonTypeCodeType.LegalEntity;
  
            return this;
        }

        /// <summary>
        /// Sets the tax identification as a foreigner entity identification
        /// </summary>
        /// <param name="taxIdentification">The tax identification</param>
        /// <returns></returns>
        public BusinessType IsForeigner()
        {
            this.TaxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.Foreigner;
  
            return this;
        }

        /// <summary>
        /// Sets the tax identification as an spain tax identification
        /// </summary>
        /// <param name="taxIdentification">The tax identification</param>
        /// <returns></returns>
        public BusinessType IsResidentInSpain()
        {
            this.TaxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.ResidentInSpain;
  
            return this;
        }

        /// <summary>
        /// Sets the tax identification as an EU tax identification
        /// </summary>
        /// <param name="taxIdentification">The tax identification</param>
        /// <returns></returns>
        public BusinessType IsResidentInEU()
        {
            this.TaxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.ResidentInEU;
  
            return this;
        }

        #endregion
    }

    public partial class IndividualType
    {
        #region · Individual Type Extensions ·

        /// <summary>
        /// Gets the individual parent party
        /// </summary>
        /// <param name="individual">The individual</param>
        /// <returns></returns>
        public BusinessType Party()
        {
            return this.Parent;
        }

        /// <summary>
        /// Sets an individual name
        /// </summary>
        /// <param name="individual">The individual</param>
        /// <param name="name">The individual name</param>
        /// <returns></returns>
        public IndividualType SetName(string name)
        {
            this.Name = name;
  
            return this;
        }

        /// <summary>
        /// Sets an individual first surname
        /// </summary>
        /// <param name="individual">The individual</param>
        /// <param name="name">The individual first surname</param>
        /// <returns></returns>
        public IndividualType SetFirstSurname(string firstSurname)
        {
            this.FirstSurname = firstSurname;
  
            return this;
        }

        /// <summary>
        /// Sets an individual second surname
        /// </summary>
        /// <param name="individual">The individual</param>
        /// <param name="name">The individual second surname</param>
        /// <returns></returns>
        public IndividualType SetSecondSurname(string secondSurname)
        {
            this.SecondSurname = secondSurname;
  
            return this;
        }

        /// <summary>
        /// Sets an individual party address
        /// </summary>
        /// <param name="party">The business party</param>
        /// <param name="address">The address</param>
        /// <returns></returns>
        public IndividualType SetAddress(string address)
        {
            if (this.Item is AddressType)
            {
                ((AddressType)this.Item).Address = address;
            }
            else
            {
                ((OverseasAddressType)this.Item).Address = address;
            }

            return this;
        }

        /// <summary>
        /// Sets an individual party post code
        /// </summary>
        /// <param name="party">The business party</param>
        /// <param name="address">The post code</param>
        /// <returns></returns>
        public IndividualType SetPostCode(string postCode)
        {
            if (this.Item is AddressType)
            {
                ((AddressType)this.Item).PostCode = postCode;
            }
            else
            {
                ((OverseasAddressType)this.Item).PostCodeAndTown = postCode;
            }

            return this;
        }

        /// <summary>
        /// Sets an individual party country code
        /// </summary>
        /// <param name="party">The business party</param>
        /// <param name="address">The country code</param>
        /// <returns></returns>
        public IndividualType SetCountryCode(CountryType countryCode)
        {
            if (this.Item is AddressType)
            {
                ((AddressType)this.Item).CountryCode = countryCode;
            }
            else
            {
                ((OverseasAddressType)this.Item).CountryCode = countryCode;
            }

            return this;
        }

        /// <summary>
        /// Sets an individual party town
        /// </summary>
        /// <param name="party">The business party</param>
        /// <param name="address">The town</param>
        /// <returns></returns>
        public IndividualType SetTown(string postCode)
        {
            if (this.Item is AddressType)
            {
                ((AddressType)this.Item).Town = postCode;
            }

            return this;
        }

        /// <summary>
        /// Sets an individual party post code & town 
        /// </summary>
        /// <param name="party">The business party</param>
        /// <param name="address">The post code & towm</param>
        /// <returns></returns>
        public IndividualType SetPostCodeAndTown(string postCodeAndTown)
        {
            if (this.Item is OverseasAddressType)
            {
                ((OverseasAddressType)this.Item).PostCodeAndTown = postCodeAndTown;
            }

            return this;
        }

        /// <summary>
        /// Sets an individual party province
        /// </summary>
        /// <param name="party">The business party</param>
        /// <param name="address">The province</param>
        /// <returns></returns>
        public IndividualType SetProvince(string province)
        {
            if (this.Item is AddressType)
            {
                ((AddressType)this.Item).Province = province;
            }
            else
            {
                ((OverseasAddressType)this.Item).Province = province;
            }

            return this;
        }

        #endregion
    }
}