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

        private static readonly XmlSerializer FacturaeSerializer = new XmlSerializer(typeof(Facturae));

        private static void XmlValidationEventHandler(object sender, ValidationEventArgs e)
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

        #region · Constructors ·

        /// <summary>
        /// Initializes a new instance of the <see cref="Facturae"/> class.
        /// </summary>
        public Facturae()
        {
            this.FileHeader = new FileHeaderType
            {
                Batch         = new BatchType()
              , SchemaVersion = SchemaVersionType.Item321
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
        /// Sets the currency code of the electronic invoice.
        /// </summary>
        /// <param name="eInvoice">The electronic invoice.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <returns></returns>
        public Facturae SetCurrency(CurrencyCodeType currencyCode)
        {
            this.FileHeader.Batch.InvoiceCurrencyCode = currencyCode;

            return this;
        }

        /// <summary>
        /// Sets the electronic invoice schema version.
        /// </summary>
        /// <param name="eInvoice">The electronic invoice.</param>
        /// <param name="schemaVersion">The schema version.</param>
        /// <returns></returns>
        public Facturae SetSchemaVersion(SchemaVersionType schemaVersion)
        {
            this.FileHeader.SchemaVersion = schemaVersion;

            return this;
        }
        
        /// <summary>
        /// Sets the electronic invoice issuer.
        /// </summary>
        /// <param name="issuerType">The issuer type.</param>
        /// <returns></returns>
        public Facturae SetIssuer(InvoiceIssuerTypeType issuerType)
        {
            this.FileHeader.InvoiceIssuerType = issuerType;

            return this;
        }

        /// <summary>
        /// Gets the electronic invoice seller
        /// </summary>
        /// <returns>The seller party.</returns>
        public BusinessType Seller()
        {
            return this.Parties.SellerParty;
        }

        /// <summary>
        /// Gets the electronic invoice buyer
        /// </summary>
        /// <returns>The buyer party.</returns>
        public BusinessType Buyer()
        {
            return this.Parties.BuyerParty;
        }
           
        /// <summary>
        /// Calculates the electronic totals.
        /// </summary>
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
        /// Sums the invoice total.
        /// </summary>
        /// <returns>An instance of AmountType.</returns>
        public AmountType SumTotalAmounts()
        {
            return new AmountType
            {
                TotalAmount       = Math.Round(this.Invoices.Sum(il => il.InvoiceTotals.InvoiceTotal), 2)
              , EquivalentInEuros = Math.Round(this.Invoices.Sum(il => il.InvoiceTotals.InvoiceTotal), 2)
            };
        }

        /// <summary>
        /// Sums the electronic invoice outstanding amount.
        /// </summary>
        /// <returns>An instance of AmountType.</returns>
        public AmountType SumTotalOutstandingAmount()
        {
            return new AmountType
            {
                TotalAmount       = Math.Round(this.Invoices.Sum(il => il.InvoiceTotals.TotalOutstandingAmount), 2)
              , EquivalentInEuros = Math.Round(this.Invoices.Sum(il => il.InvoiceTotals.TotalOutstandingAmount), 2)
            };
        }

        /// <summary>
        /// Sums the electronic invoice executable amount.
        /// </summary>
        /// <returns>An instance of AmountType.</returns>
        public AmountType SumTotalExecutableAmount()
        {
            return new AmountType
            {
                TotalAmount       = Math.Round(this.Invoices.Sum(il => il.InvoiceTotals.TotalExecutableAmount), 2)
              , EquivalentInEuros = Math.Round(this.Invoices.Sum(il => il.InvoiceTotals.TotalExecutableAmount), 2)
            };
        }
        
        /// <summary>
        /// Creates a new invoice.
        /// </summary>
        /// <returns>The new invoice.</returns>
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
        /// Validates the electronic invoice XML.
        /// </summary>
        /// <returns></returns>
        public Facturae Validate()
        {
            XmlDocument document = this.ToXmlDocument();
            
            document.Schemas.Add(XsdSchemas.BuildSchemaSet(document.NameTable));
            document.Validate(XmlValidationEventHandler);

            return this;
        }

        #endregion

        #region · XML Signature Extensions ·

        /// <summary>
        /// Signs the electronic invoice using the given certificate.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        /// <returns></returns>
        public XAdESSignatureVerifier Sign(X509Certificate2 certificate)
        {
            return this.Sign(certificate, ClaimedRole.Supplier);
        }

        /// <summary>
        /// Signs the electronic invoice using the given certificate & RSA key.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        /// <param name="key">The RSA Key.</param>
        /// <param name="signerRole">Rol del "firmante" de la factura</param>
        /// <returns>The XAdES signature verifier.</returns>
        public XAdESSignatureVerifier Sign(X509Certificate2 certificate, ClaimedRole signerRole)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate cannot be null");
            }

            return this.Sign(certificate, (RSA)certificate.PrivateKey, signerRole);
        }

        /// <summary>
        /// Signs the electronic invoice using the given certificate & RSA key.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        /// <param name="key">The RSA Key.</param>
        /// <param name="signerRole">Rol del "firmante" de la factura</param>
        /// <returns>The XAdES signature verifier.</returns>
        private XAdESSignatureVerifier Sign(X509Certificate2 certificate, RSA key, ClaimedRole signerRole)
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
                     .SetSignerRole(signerRole)                                 // XAdES Signer Role
                     .SetKeyInfo(certificate, (RSA)certificate.PublicKey.Key)   // Key Info
                     .ComputeSignature();                                       // Compute Signature
            
            // Import the signed XML node 
            document.DocumentElement.AppendChild(document.ImportNode(signedXml.GetXml(), true));            

            return new XAdESSignatureVerifier(document);
        }

        #endregion

        #region · IO Extensions ·

        /// <summary>
        /// Writes the electronic invoice to the given path.
        /// </summary>
        /// <param name="path">The file path.</param>
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
        /// Set the invoice series code.
        /// </summary>
        /// <param name="seriesCode">The invoice series code.</param>
        /// <returns></returns>
        public InvoiceType SetInvoiceSeries(string seriesCode)
        {
            this.InvoiceHeader.InvoiceSeriesCode = seriesCode;

            return this;
        }

        /// <summary>
        /// Sets the invoice number.
        /// </summary>
        /// <param name="invoiceNumber">The invoice number.</param>
        /// <returns></returns>
        public InvoiceType SetInvoiceNumber(string invoiceNumber)
        {
            this.InvoiceHeader.InvoiceNumber = invoiceNumber;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as a complete invoice.
        /// </summary>
        /// <returns></returns>
        public InvoiceType IsComplete()
        {
            this.InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.Complete;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as a abbreviated invoice.
        /// </summary>
        /// <returns></returns>
        public InvoiceType IsAbbreviated()
        {
            this.InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.Abbreviated;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as a self invoice.
        /// </summary>
        /// <returns></returns>
        public InvoiceType IsSelfInvoice()
        {
            this.InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.SelfInvoice;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as a original invoice.
        /// </summary>
        /// <returns></returns>
        public InvoiceType IsOriginal()
        {
            this.InvoiceHeader.InvoiceClass = InvoiceClassType.Original;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as a corrective invoice.
        /// </summary>
        /// <returns></returns>
        public InvoiceType IsCorrective()
        {
            this.InvoiceHeader.InvoiceClass = InvoiceClassType.Corrective;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as summary of original invoice.
        /// </summary>
        /// <returns></returns>
        public InvoiceType IsSummaryOriginal()
        {
            this.InvoiceHeader.InvoiceClass = InvoiceClassType.SummaryOriginal;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as copy of original invoice.
        /// </summary>
        /// <returns></returns>
        public InvoiceType IsCopyOfOriginal()
        {
            this.InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfOriginal;

            return this;
        }

        /// <summary>
        /// Sets the invoice class as copy of corrective invoice.
        /// </summary>
        /// <returns></returns>
        public InvoiceType IsCopyOfCorrective()
        {
            this.InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfCorrective;
            
            return this;
        }

        /// <summary>
        /// Sets the invoice class as copy of summary invoice.
        /// </summary>
        /// <returns></returns>
        public InvoiceType IsCopyOfSummary()
        {
            this.InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfSummary;

            return this;
        }

        /// <summary>
        /// Sets the invoice issue date.
        /// </summary>
        /// <param name="date">The invoice issue date.</param>
        /// <returns></returns>
        public InvoiceType SetIssueDate(DateTime date)
        {
            this.InvoiceIssueData.IssueDate = date;

            return this;
        }

        /// <summary>
        /// Sets the invoice operation date.
        /// </summary>
        /// <param name="date">The invoice operation date.</param>
        /// <returns></returns>
        public InvoiceType SetOperationDate(DateTime date)
        {
            this.InvoiceIssueData.OperationDate = date;
            
            return this;
        }

        /// <summary>
        /// Set the invoice place of issue.
        /// </summary>
        /// <param name="description">The place of issue description.</param>
        /// <param name="postCode">The place of issue post code.</param>
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
        /// Sets the invoicing period of an invoice.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
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
        /// Sets the invoice currency.
        /// </summary>
        /// <param name="currency">The invoice currency.</param>
        /// <returns></returns>
        public InvoiceType SetCurrency(CurrencyCodeType currency)
        {
            this.InvoiceIssueData.InvoiceCurrencyCode = currency;
            
            return this;
        }

        /// <summary>
        /// Sets the invoice currency exchange rate.
        /// </summary>
        /// <param name="exchangeRate">The exchange rate.</param>
        /// <param name="exchangeDate">The exchange date.</param>
        /// <returns></returns>
        public InvoiceType SetExchangeRate(double exchangeRate, DateTime exchangeDate)
        {
            this.InvoiceIssueData.ExchangeRateDetails = new ExchangeRateDetailsType
            {
                ExchangeRate     = exchangeRate
              , ExchangeRateDate = exchangeDate
            };
            
            return this;
        }

        /// <summary>
        /// Sets the invoice tax currency.
        /// </summary>
        /// <param name="taxCurrency">The invoice currency.</param>
        /// <returns></returns>
        public InvoiceType SetTaxCurrency(CurrencyCodeType taxCurrency)
        {
            this.InvoiceIssueData.TaxCurrencyCode = taxCurrency;
                        
            return this;
        }

        /// <summary>
        /// Sets the invoice language.
        /// </summary>
        /// <param name="language">The invoice language.</param>
        /// <returns></returns>
        public InvoiceType SetLanguage(LanguageCodeType language)
        {
            this.InvoiceIssueData.LanguageName = language;

            return this;
        }

        /// <summary>
        /// Adds a new line to an invoice.
        /// </summary>
        /// <param name="productCode">The product code.</param>
        /// <param name="productDescription">The product description.</param>
        /// <returns></returns>
        public InvoiceLineType AddInvoiceItem(string productCode, string productDescription)
        {
            var item = new InvoiceLineType
            {
                Parent          = this
              , ArticleCode     = productCode
              , ItemDescription = productDescription
            };

            this.Items.Add(item);

            return item;
        }

        /// <summary>
        /// Calculates the invoice totals.
        /// </summary>
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
#warning TODO: Implement as an extension method
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

        public InvoiceLineType GiveDiscount(double discountRate, string discountReason = "Descuento")
        {
            if (this.DiscountsAndRebates == null)
            {
                this.DiscountsAndRebates = new List<DiscountType>();
            }

            var discount = new DiscountType
            {
                DiscountRate   = discountRate
              , DiscountReason = discountReason
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
            this.parent             = parent;
            this.TaxIdentification  = new TaxIdentificationType();
        }

        #endregion

        #region · BusinessType Extensions ·

        public Facturae Invoice()
        {
            return this.parent;
        }

        /// <summary>
        /// Sets the identification of a invoice business party.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <returns></returns>
        public BusinessType SetIdentification(string identification)
        {
            this.PartyIdentification = identification;
  
            return this;
        }

        /// <summary>
        /// Sets an invoice business party as an individual
        /// </summary>
        /// <returns></returns>
        public IndividualType AsIndividual()
        {
            var individual = new IndividualType { Parent = this };

            this.Item = individual;

            if (this.TaxIdentification.ResidenceTypeCode == ResidenceTypeCodeType.ResidentInSpain)
            {
                individual.Item = new AddressType { CountryCode = CountryType.ESP };
            }
            else
            {
                individual.Item = new OverseasAddressType();
            }

            this.TaxIdentification.PersonTypeCode = PersonTypeCodeType.Individual;

            return individual;
        }

        /// <summary>
        /// Sets the identification number.
        /// </summary>
        /// <param name="identificationNumber">The identification number.</param>
        /// <returns></returns>
        public BusinessType SetIdentificationNumber(string identificationNumber)
        {
            this.TaxIdentification.TaxIdentificationNumber = identificationNumber;
  
            return this;
        }

        /// <summary>
        /// Sets an invoice business party as a legal entity.
        /// </summary>
        /// <returns></returns>
        public LegalEntityType AsLegalEntity()
        {
            var legalEntity = new LegalEntityType(this);

            this.Item = legalEntity;

            if (this.TaxIdentification != null)
            {
                this.TaxIdentification.PersonTypeCode = PersonTypeCodeType.LegalEntity;

                if (this.TaxIdentification.ResidenceTypeCode == ResidenceTypeCodeType.ResidentInSpain)
                {
                    legalEntity.Item = new AddressType { CountryCode = CountryType.ESP };
                }
                else
                {
                    legalEntity.Item = new OverseasAddressType();
                }
            }

            return legalEntity;
        }

        /// <summary>
        /// Sets the tax identification as a foreigner entity identification.
        /// </summary>
        /// <returns></returns>
        public BusinessType AsForeigner()
        {
            this.TaxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.Foreigner;
  
            return this;
        }

        /// <summary>
        /// Sets the tax identification as an spain tax identification.
        /// </summary>
        /// <returns></returns>
        public BusinessType AsResidentInSpain()
        {
            this.TaxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.ResidentInSpain;
  
            return this;
        }

        /// <summary>
        /// Sets the tax identification as an EU tax identification.
        /// </summary>
        /// <returns></returns>
        public BusinessType AsResidentInEU()
        {
            this.TaxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.ResidentInEU;
  
            return this;
        }

        /// <summary>
        /// Adds an administrative centre.
        /// </summary>
        /// <returns>The new administrative centre</returns>
        public AdministrativeCentreType AddAdministrativeCentre()
        {
            var centre = new AdministrativeCentreType(this);

            if (this.AdministrativeCentres == null)
            {
                this.AdministrativeCentres = new List<AdministrativeCentreType>();
            }
            
            this.AdministrativeCentres.Add(centre);

            if (this.TaxIdentification != null)
            {
                if (this.TaxIdentification.ResidenceTypeCode == ResidenceTypeCodeType.ResidentInSpain)
                {
                    centre.Item = new AddressType { CountryCode = CountryType.ESP };
                }
                else
                {
                    centre.Item = new OverseasAddressType();
                }
            }

            return centre;
        } 

        #endregion
    }

    public partial class IndividualType
    {
        #region · Individual Type Extensions ·

        /// <summary>
        /// Gets the individual parent party.
        /// </summary>
        /// <returns></returns>
        public BusinessType Party()
        {
            return this.Parent;
        }

        /// <summary>
        /// Sets an individual name.
        /// </summary>
        /// <param name="name">The individual name.</param>
        /// <returns></returns>
        public IndividualType SetName(string name)
        {
            this.Name = name;
  
            return this;
        }

        /// <summary>
        /// Sets an individual first surname.
        /// </summary>
        /// <param name="firstSurname">The individual first surname.</param>
        /// <returns></returns>
        public IndividualType SetFirstSurname(string firstSurname)
        {
            this.FirstSurname = firstSurname;
  
            return this;
        }

        /// <summary>
        /// Sets an individual second surname.
        /// </summary>
        /// <param name="secondSurname">The individual second surname.</param>
        /// <returns></returns>
        public IndividualType SetSecondSurname(string secondSurname)
        {
            this.SecondSurname = secondSurname;
  
            return this;
        }

        /// <summary>
        /// Sets an individual party address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public IndividualType SetAddress(string address)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).Address = address;
            }
            else
            {
                (this.Item as OverseasAddressType).Address = address;
            }

            return this;
        }

        /// <summary>
        /// Sets an individual party post code.
        /// </summary>
        /// <param name="postCode">The post code.</param>
        /// <returns></returns>
        public IndividualType SetPostCode(string postCode)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).PostCode = postCode;
            }
            else
            {
                (this.Item as OverseasAddressType).PostCodeAndTown = postCode;
            }

            return this;
        }

        /// <summary>
        /// Sets an individual party country code.
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns></returns>
        public IndividualType SetCountryCode(CountryType countryCode)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).CountryCode = countryCode;
            }
            else
            {
                (this.Item as OverseasAddressType).CountryCode = countryCode;
            }

            return this;
        }

        /// <summary>
        /// Sets an individual party town
        /// </summary>
        /// <param name="town">The town</param>
        /// <returns></returns>
        public IndividualType SetTown(string town)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).Town = town;
            }

            return this;
        }

        /// <summary>
        /// Sets an individual party post code & town.
        /// </summary>
        /// <param name="postCodeAndTown">The post code & town.</param>
        /// <returns></returns>
        public IndividualType SetPostCodeAndTown(string postCodeAndTown)
        {
            if (this.Item is OverseasAddressType)
            {
                (this.Item as OverseasAddressType).PostCodeAndTown = postCodeAndTown;
            }

            return this;
        }

        /// <summary>
        /// Sets an individual party province.
        /// </summary>
        /// <param name="province">The province.</param>
        /// <returns></returns>
        public IndividualType SetProvince(string province)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).Province = province;
            }
            else
            {
                (this.Item as OverseasAddressType).Province = province;
            }

            return this;
        }

        #endregion
    }

    public partial class AdministrativeCentreType
    {
        #region · Fields ·

        private BusinessType parent;

        #endregion

        #region · Constructors ·

        public AdministrativeCentreType()
        {
        }

        public AdministrativeCentreType(BusinessType parent)
        {
            this.parent = parent;
        }

        #endregion

        #region · Public Methods ·

        public BusinessType Party()
        {
            return this.parent;
        }

        public AdministrativeCentreType SetRoleCodeType(string roleCodeType)
        {
            var role = RoleTypeCodeType.Item01;

            switch (roleCodeType)
            {
                case "02":
                    role = RoleTypeCodeType.Item02;
                    break;

                case "03":
                    role = RoleTypeCodeType.Item03;
                    break;

                case "04":
                    role = RoleTypeCodeType.Item04;
                    break;

                case "05":
                    role = RoleTypeCodeType.Item05;
                    break;

                case "06":
                    role = RoleTypeCodeType.Item06;
                    break;

                case "07":
                    role = RoleTypeCodeType.Item07;
                    break;

                case "08":
                    role = RoleTypeCodeType.Item08;
                    break;

                case "09":
                    role = RoleTypeCodeType.Item09;
                    break;
            }

            this.RoleTypeCode          = role;
            this.RoleTypeCodeSpecified = true;

            return this;
        }

        public AdministrativeCentreType SetCentreCode(string centreCode)
        {
            this.CentreCode = centreCode;

            return this;
        }

        public AdministrativeCentreType SetCentreDescription(string centreDescription)
        {
            this.CentreDescription = centreDescription;

            return this;
        }

        public AdministrativeCentreType SetFirstSurname(string firstSurname)
        {
            this.FirstSurname = firstSurname;

            return this;
        }

        public AdministrativeCentreType SetLogicalOperationalPoint(string logicalOperationalPoint)
        {
            this.LogicalOperationalPoint = logicalOperationalPoint;

            return this;
        }

        public AdministrativeCentreType SetName(string name)
        {
            this.Name = name;

            return this;
        }

        public AdministrativeCentreType SetPhysicalGLN(string physicalGLN)
        {
            this.PhysicalGLN = physicalGLN;

            return this;
        }

        public AdministrativeCentreType SetSecondSurname(string secondSurname)
        {
            this.SecondSurname = secondSurname;

            return this;
        }

        public AdministrativeCentreType SetAddress(string address)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).Address = address;
            }
            else
            {
                (this.Item as OverseasAddressType).Address = address;
            }

            return this;
        }

        public AdministrativeCentreType SetPostCode(string postCode)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).PostCode = postCode;
            }
            else
            {
                (this.Item as OverseasAddressType).PostCodeAndTown = postCode;
            }

            return this;
        }

        public AdministrativeCentreType SetCountryCode(CountryType countryCode)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).CountryCode = countryCode;
            }
            else
            {
                (this.Item as OverseasAddressType).CountryCode = countryCode;
            }

            return this;
        }

        public AdministrativeCentreType SetTown(string town)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).Town = town;
            }

            return this;
        }

        public AdministrativeCentreType SetPostCodeAndTown(string postCodeAndTown)
        {
            if (this.Item is OverseasAddressType)
            {
                (this.Item as OverseasAddressType).PostCodeAndTown = postCodeAndTown;
            }

            return this;
        }

        public AdministrativeCentreType SetProvince(string province)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).Province = province;
            }
            else
            {
                ((OverseasAddressType)this.Item).Province = province;
            }

            return this;
        }

        #endregion
    }

    public partial class LegalEntityType
    {
        #region · Fields ·

        private BusinessType parent;

        #endregion

        #region · Constructors ·

        public LegalEntityType()
        {
        }

        public LegalEntityType(BusinessType parent)
        {
            this.parent = parent;
        }

        #endregion

        #region · LegalEntity Type Extensions ·

        public BusinessType Party()
        {
            return this.parent;
        }

        public LegalEntityType SetCorporateName(string corporateName)
        {
            this.CorporateName = corporateName;

            return this;
        }

        public LegalEntityType SetTradeName(string tradeName)
        {
            this.TradeName = tradeName;

            return this;
        }

        public LegalEntityType SetAddress(string address)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).Address = address;
            }
            else
            {
                (this.Item as OverseasAddressType).Address = address;
            }

            return this;
        }

        public LegalEntityType SetPostCode(string postCode)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).PostCode = postCode;
            }
            else
            {
                (this.Item as OverseasAddressType).PostCodeAndTown = postCode;
            }

            return this;
        }

        public LegalEntityType SetCountryCode(CountryType countryCode)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).CountryCode = countryCode;
            }
            else
            {
                (this.Item as OverseasAddressType).CountryCode = countryCode;
            }

            return this;
        }

        public LegalEntityType SetTown(string postCode)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).Town = postCode;
            }

            return this;
        }

        public LegalEntityType SetPostCodeAndTown(string postCodeAndTown)
        {
            if (this.Item is OverseasAddressType)
            {
                (this.Item as OverseasAddressType).PostCodeAndTown = postCodeAndTown;
            }

            return this;
        }

        public LegalEntityType SetProvince(string province)
        {
            if (this.Item is AddressType)
            {
                (this.Item as AddressType).Province = province;
            }
            else
            {
                (this.Item as OverseasAddressType).Province = province;
            }

            return this;
        }

        #endregion
    }
}