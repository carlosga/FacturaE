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

namespace FacturaE.Extensions
{
    /// <summary>
    /// Facturae extensions
    /// </summary>
    public static class FacturaeExtensions
    {
        #region · Static Members ·

        static readonly XmlSerializer FacturaeSerializer = new XmlSerializer(typeof(Facturae));

        #endregion

        #region · Facturae Extensions ·

        /// <summary>
        /// Sets the currency code of the electronic invoice
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <param name="currencyCode">The currency code</param>
        /// <returns></returns>
        public static Facturae SetCurrency(this Facturae eInvoice, CurrencyCodeType currencyCode)
        {
            eInvoice.VerifyHeader();

            eInvoice.FileHeader.Batch.InvoiceCurrencyCode = currencyCode;

            return eInvoice;
        }

        /// <summary>
        /// Sets the electronic invoice schema version
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <param name="schemaVersion">The schema version</param>
        /// <returns></returns>
        public static Facturae SetSchemaVersion(this Facturae eInvoice, SchemaVersionType schemaVersion)
        {
            eInvoice.VerifyHeader();

            eInvoice.FileHeader.SchemaVersion = schemaVersion;

            return eInvoice;
        }
        
        /// <summary>
        /// Sets the electronic invoice issuer
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <param name="issuerType">The issuer type</param>
        /// <returns></returns>
        public static Facturae SetIssuer(this Facturae eInvoice, InvoiceIssuerTypeType issuerType)
        {
            eInvoice.VerifyHeader();

            eInvoice.FileHeader.InvoiceIssuerType = issuerType;

            return eInvoice;
        }
            
        /// <summary>
        /// Calculates the electronic totals
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <returns></returns>
        public static Facturae CalculateTotals(this Facturae eInvoice)
        {
            InvoiceType firstInvoice = eInvoice.Invoices[0];

            eInvoice.VerifyHeader();
                                    
            eInvoice.FileHeader.Modality                     = ((eInvoice.Invoices.Count == 1) ? ModalityType.Single : ModalityType.Batch);
            eInvoice.FileHeader.Batch.InvoicesCount          = eInvoice.Invoices.Count;
            eInvoice.FileHeader.Batch.TotalInvoicesAmount    = eInvoice.SumTotalAmounts();
            eInvoice.FileHeader.Batch.TotalOutstandingAmount = eInvoice.SumTotalOutstandingAmount();
            eInvoice.FileHeader.Batch.TotalExecutableAmount  = eInvoice.SumTotalExecutableAmount();
            eInvoice.FileHeader.Batch.BatchIdentifier        = String.Format("{0}{1}{2}"
                                                                           , String.Empty
                                                                           , firstInvoice.InvoiceHeader.InvoiceNumber
                                                                           , firstInvoice.InvoiceHeader.InvoiceSeriesCode);

            return eInvoice;
        }

        /// <summary>
        /// Sums the invoice total
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <returns>An instance of AmountType</returns>
        public static AmountType SumTotalAmounts(this Facturae eInvoice)
        {
            AmountType amount = new AmountType();

            amount.TotalAmount       = Math.Round(eInvoice.Invoices.Sum(il => il.InvoiceTotals.InvoiceTotal), 2);
            amount.EquivalentInEuros = Math.Round(eInvoice.Invoices.Sum(il => il.InvoiceTotals.InvoiceTotal), 2);

            return amount;
        }

        /// <summary>
        /// Sums the electronic invoice outstanding amount
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <returns>An instance of AmountType</returns>
        public static AmountType SumTotalOutstandingAmount(this Facturae eInvoice)
        {
            AmountType amount = new AmountType();

            amount.TotalAmount       = Math.Round(eInvoice.Invoices.Sum(il => il.InvoiceTotals.TotalOutstandingAmount), 2);
            amount.EquivalentInEuros = Math.Round(eInvoice.Invoices.Sum(il => il.InvoiceTotals.TotalOutstandingAmount), 2);

            return amount;
        }

        /// <summary>
        /// Sums the electronic invoice executable amount
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <returns>An instance of AmountType</returns>
        public static AmountType SumTotalExecutableAmount(this Facturae eInvoice)
        {
            AmountType amount = new AmountType();

            amount.TotalAmount       = Math.Round(eInvoice.Invoices.Sum(il => il.InvoiceTotals.TotalExecutableAmount), 2);
            amount.EquivalentInEuros = Math.Round(eInvoice.Invoices.Sum(il => il.InvoiceTotals.TotalExecutableAmount), 2);

            return amount;
        }
        
        /// <summary>
        /// Creates a new invoice
        /// </summary>
        /// <param name="eInvoice"></param>
        /// <returns></returns>
        public static InvoiceType CreateInvoice(this Facturae eInvoice)
        {
            InvoiceType invoice = new InvoiceType();

            invoice.Parent           = eInvoice;
            invoice.InvoiceHeader    = new InvoiceHeaderType();
            invoice.InvoiceTotals    = new InvoiceTotalsType();
            invoice.InvoiceIssueData = new InvoiceIssueDataType();

            eInvoice.Invoices.Add(invoice);

            return invoice;
        }

        #endregion

        #region · InvoiceType Extensions ·

        /// <summary>
        /// Set the invoice series code
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="seriesCode">The invoice series code</param>
        /// <returns></returns>
        public static InvoiceType SetInvoiceSeries(this InvoiceType invoice, string seriesCode)
        {
            invoice.InvoiceHeader.InvoiceSeriesCode = seriesCode;

            return invoice;
        }

        /// <summary>
        /// Sets the invoice number
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="invoiceNumber">The invoice number</param>
        /// <returns></returns>
        public static InvoiceType SetInvoiceNumber(this InvoiceType invoice, string invoiceNumber)
        {
            invoice.InvoiceHeader.InvoiceNumber = invoiceNumber;

            return invoice;
        }

        /// <summary>
        /// Sets the invoice class as a complete invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public static InvoiceType IsComplete(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.Complete;

            return invoice;
        }

        /// <summary>
        /// Sets the invoice class as a abbreviated invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public static InvoiceType IsAbbreviated(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.Abbreviated;

            return invoice;
        }

        /// <summary>
        /// Sets the invoice class as a self invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public static InvoiceType IsSelfInvoice(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.SelfInvoice;

            return invoice;
        }

        /// <summary>
        /// Sets the invoice class as a original invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public static InvoiceType IsOriginal(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceClass = InvoiceClassType.Original;

            return invoice;
        }

        /// <summary>
        /// Sets the invoice class as a corrective invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public static InvoiceType IsCorrective(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceClass = InvoiceClassType.Corrective;

            return invoice;
        }

        /// <summary>
        /// Sets the invoice class as summary of original invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public static InvoiceType IsSummaryOriginal(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceClass = InvoiceClassType.SummaryOriginal;

            return invoice;
        }

        /// <summary>
        /// Sets the invoice class as copy of original invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public static InvoiceType IsCopyOfOriginal(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfOriginal;

            return invoice;
        }

        /// <summary>
        /// Sets the invoice class as copy of corrective invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public static InvoiceType IsCopyOfCorrective(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfCorrective;
            
            return invoice;
        }

        /// <summary>
        /// Sets the invoice class as copy of summary invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns></returns>
        public static InvoiceType IsCopyOfSummary(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfSummary;

            return invoice;
        }

        /// <summary>
        /// Sets the invoice issue date
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="date">The invoice issue date</param>
        /// <returns></returns>
        public static InvoiceType GiveIssueDate(this InvoiceType invoice, DateTime date)
        {
            invoice.InvoiceIssueData.IssueDate = date;

            return invoice;
        }

        /// <summary>
        /// Sets the invoice operation date
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="date">The invoice operation date</param>
        /// <returns></returns>
        public static InvoiceType SetOperationDate(this InvoiceType invoice, DateTime date)
        {
            invoice.InvoiceIssueData.OperationDate = date;
            
            return invoice;
        }

        /// <summary>
        /// Set the invoice place of issue
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="description">The place of issue description</param>
        /// <param name="postCode">The place of issue post code</param>
        /// <returns></returns>
        public static InvoiceType SetPlaceOfIssue(this InvoiceType invoice, string description, string postCode)
        {
            invoice.InvoiceIssueData.PlaceOfIssue = new PlaceOfIssueType
            {
                PlaceOfIssueDescription = description
              , PostCode                = postCode
            };
            
            return invoice;
        }

        /// <summary>
        /// Sets the invoicing period of an invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns></returns>
        public static InvoiceType SetInvoicingPeriod(this InvoiceType invoice, DateTime startDate, DateTime endDate)
        {
            invoice.InvoiceIssueData.InvoicingPeriod = new PeriodDates
            {
                StartDate = startDate
              , EndDate   = endDate
            };
            
            return invoice;
        }

        /// <summary>
        /// Sets the invoice currency
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="currency">The invoice currency</param>
        /// <returns></returns>
        public static InvoiceType SetCurrency(this InvoiceType invoice, CurrencyCodeType currency)
        {
            invoice.InvoiceIssueData.InvoiceCurrencyCode = currency;
            
            return invoice;
        }

        /// <summary>
        /// Sets the invoice currency exchange rate
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="exchangeRate">The exchange rate</param>
        /// <param name="exchangeDate">The exchange date</param>
        /// <returns></returns>
        public static InvoiceType SetExchangeRate(this InvoiceType invoice, double exchangeRate, DateTime exchangeDate)
        {
            invoice.InvoiceIssueData.ExchangeRateDetails = new ExchangeRateDetailsType
            {
                ExchangeRate = exchangeRate
            };
            
            return invoice;
        }

        /// <summary>
        /// Sets the invoice tax currency
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="taxCurrency">The invoice currency</param>
        /// <returns></returns>
        public static InvoiceType SetTaxCurrency(this InvoiceType invoice, CurrencyCodeType taxCurrency)
        {
            invoice.InvoiceIssueData.TaxCurrencyCode = taxCurrency;
                        
            return invoice;
        }

        /// <summary>
        /// Sets the invoice language
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <param name="language">The invoice language</param>
        /// <returns></returns>
        public static InvoiceType SetLanguage(this InvoiceType invoice, LanguageCodeType language)
        {
            invoice.InvoiceIssueData.LanguageName = language;

            return invoice;
        }

        /// <summary>
        /// Gets the items of an invoice
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static InvoiceType Items(this InvoiceType invoice)
        {
            if (invoice.Items == null)
            {
                invoice.Items = new List<InvoiceLineType>();
            }

            return invoice;
        }

        /// <summary>
        /// Calculates the invoice totals
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static Facturae CalculateTotals(this InvoiceType invoice)
        {
            double                   subsidyAmount = 0;
            List<InvoiceLineTypeTax> rawLineTaxes  = new List<InvoiceLineTypeTax>();
                                    
            // Grab raw taxes from invoice lines
            invoice.Items.ForEach(item => item.TaxesOutputs.ForEach(itax => rawLineTaxes.Add(itax)));

            // Taxes Outputs
            var q = from tax in rawLineTaxes
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

            invoice.TaxesOutputs = q.ToList();

            // Invoice totals
            invoice.InvoiceTotals = new InvoiceTotalsType();

            // Calculate totals
            invoice.InvoiceTotals.TotalGrossAmount = Math.Round
            (
                invoice.Items.Sum(it => it.GrossAmount), 2
            );
                        
            if (invoice.InvoiceTotals.GeneralDiscounts != null)
            {
                invoice.InvoiceTotals.GeneralDiscounts.ForEach
                (
                    gd => gd.DiscountAmount = Math.Round((invoice.InvoiceTotals.TotalGrossAmount * gd.DiscountRate) / 100, 2)
                );

                invoice.InvoiceTotals.TotalGeneralDiscounts = Math.Round
                (
                    invoice.InvoiceTotals.GeneralDiscounts.Sum(gd => gd.DiscountAmount), 2
                );
            }
            
            if (invoice.InvoiceTotals.GeneralSurcharges != null)
            {
                invoice.InvoiceTotals.GeneralSurcharges.ForEach
                (
                    gs => gs.ChargeAmount = Math.Round((invoice.InvoiceTotals.TotalGrossAmount * gs.ChargeRate) / 100, 2)
                );

                invoice.InvoiceTotals.TotalGeneralSurcharges = Math.Round
                (
                    invoice.InvoiceTotals.GeneralSurcharges.Sum(gs => gs.ChargeAmount), 2
                );
            }

            invoice.InvoiceTotals.TotalGrossAmountBeforeTaxes = Math.Round(invoice.InvoiceTotals.TotalGrossAmount      
                                                                         - invoice.InvoiceTotals.TotalGeneralDiscounts 
                                                                         + invoice.InvoiceTotals.TotalGeneralSurcharges, 2);

            if (invoice.InvoiceTotals.PaymentsOnAccount != null)
            {
                invoice.InvoiceTotals.TotalPaymentsOnAccount = Math.Round
                (
                    invoice.InvoiceTotals.PaymentsOnAccount.Sum(poa => poa.PaymentOnAccountAmount), 2
                );
            }

            // Total de suplidos
            if (invoice.InvoiceTotals.ReimbursableExpenses != null)
            {
                invoice.InvoiceTotals.TotalReimbursableExpenses = Math.Round
                (
                    invoice.InvoiceTotals.ReimbursableExpenses.Sum(re => re.ReimbursableExpensesAmount), 2
                );
            }
            
            // Total impuestos retenidos.
            invoice.InvoiceTotals.TotalTaxesWithheld = Math.Round
            (
                invoice.Items.Sum
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

            // Sum of different fields Tax Amounts + Total Equivalence 
            // Surcharges. Always to two decimal points.
            invoice.InvoiceTotals.TotalTaxOutputs = Math.Round
            (
                invoice.TaxesOutputs.Sum(to => to.TaxAmount.TotalAmount), 2
            );

            invoice.InvoiceTotals.InvoiceTotal = Math.Round(invoice.InvoiceTotals.TotalGrossAmountBeforeTaxes   
                                                          + invoice.InvoiceTotals.TotalTaxOutputs               
                                                          - invoice.InvoiceTotals.TotalTaxesWithheld, 2);

            // Total de gastos financieros
#warning TODO: Hacer un extension method para que se pueda indicar
            invoice.InvoiceTotals.TotalFinancialExpenses = 0;
            
            if (invoice.InvoiceTotals.Subsidies != null)
            {
                // Rate applied to the Invoice Total.
                invoice.InvoiceTotals.Subsidies.ForEach
                (
                    s => s.SubsidyAmount = Math.Round(invoice.InvoiceTotals.InvoiceTotal * s.SubsidyRate / 100, 2)
                );

                subsidyAmount = Math.Round(invoice.InvoiceTotals.Subsidies.Sum(s => s.SubsidyAmount), 2);
            }

            invoice.InvoiceTotals.TotalOutstandingAmount = Math.Round
            (
                invoice.InvoiceTotals.InvoiceTotal - (subsidyAmount + invoice.InvoiceTotals.TotalPaymentsOnAccount), 2
            );

            invoice.InvoiceTotals.TotalExecutableAmount = Math.Round(invoice.InvoiceTotals.TotalOutstandingAmount
                                                                   - invoice.InvoiceTotals.TotalTaxesWithheld
                                                                   + invoice.InvoiceTotals.TotalReimbursableExpenses
                                                                   + invoice.InvoiceTotals.TotalFinancialExpenses, 2);

            return invoice.Parent;
        }

        #endregion

        #region · InvoiceLineType Extensions ·

        public static InvoiceLineType AddInvoiceItem(this InvoiceType invoice, string articleCode, string productDescription)
        {
            InvoiceLineType item = new InvoiceLineType
            {
                Parent          = invoice
              , ArticleCode     = articleCode
              , ItemDescription = productDescription
            };

            invoice.Items.Add(item);

            return item;
        }

        public static InvoiceLineType GiveQuantity(this InvoiceLineType invoiceLine, double quantity)
        {
            invoiceLine.Quantity = quantity;

            return invoiceLine;
        }

        public static InvoiceLineType GiveUnitPriceWithoutTax(this InvoiceLineType invoiceLine, double price)
        {
            invoiceLine.UnitPriceWithoutTax = price;

            return invoiceLine;
        }

        public static InvoiceLineType GiveDiscount(this InvoiceLineType invoiceLine, double discountRate)
        {
            if (invoiceLine.DiscountsAndRebates == null)
            {
                invoiceLine.DiscountsAndRebates = new List<DiscountType>();
            }

            DiscountType discount = new DiscountType
            {
                DiscountRate   = discountRate
              , DiscountReason = "Descuento"
            };

            invoiceLine.DiscountsAndRebates.Add(discount);

            return invoiceLine;
        }

        public static InvoiceLineType GiveTax(this InvoiceLineType invoiceLine, double taxRate)
        {
            if (invoiceLine.TaxesOutputs == null)
            {
                invoiceLine.TaxesOutputs = new List<InvoiceLineTypeTax>();
            }

            InvoiceLineTypeTax tax = new InvoiceLineTypeTax
            {
                TaxTypeCode = TaxTypeCodeType.Item01
              , TaxRate     = taxRate
            };

            invoiceLine.TaxesOutputs.Add(tax);

            return invoiceLine;
        }

        public static InvoiceType CalculateTotals(this InvoiceLineType invoiceLine)
        {
            double totalDiscounts = 0;
            double totalCharges   = 0;

            invoiceLine.TotalCost = Math.Round(invoiceLine.Quantity * invoiceLine.UnitPriceWithoutTax, 2);

            if (invoiceLine.DiscountsAndRebates != null)
            {
                invoiceLine.DiscountsAndRebates.ForEach
                (
                    dar => 
                    {
                        dar.DiscountAmount = Math.Round(invoiceLine.TotalCost * dar.DiscountRate / 100, 2);
                        totalDiscounts     = Math.Round(totalDiscounts + dar.DiscountAmount, 2);
                    }
                );
            }

            if (invoiceLine.Charges != null)
            {
                invoiceLine.Charges.ForEach
                (
                    chr =>
                    {
                        chr.ChargeAmount = Math.Round(invoiceLine.TotalCost * chr.ChargeRate / 100, 2);
                        totalCharges     = Math.Round(totalCharges + chr.ChargeAmount, 2);
                    }
                );
            }

            invoiceLine.GrossAmount = invoiceLine.TotalCost - totalDiscounts + totalCharges;

            invoiceLine.TaxesOutputs.ForEach
            (
                tax =>
                {
                    if (tax.TaxableBase == null)
                    {
                        tax.TaxableBase = new AmountType();
                    }

                    tax.TaxableBase.TotalAmount       = invoiceLine.GrossAmount;
                    tax.TaxableBase.EquivalentInEuros = invoiceLine.GrossAmount;

                    if (tax.TaxAmount == null)
                    {
                        tax.TaxAmount = new AmountType();
                    }

                    tax.TaxAmount.TotalAmount = Math.Round(tax.TaxableBase.TotalAmount * tax.TaxRate / 100, 2);
                }
            );

            return invoiceLine.Parent;
        }

        #endregion

        #region · PartiesType Extensions ·

        /// <summary>
        /// Gets the electronic invoice business parties
        /// </summary>
        /// <param name="eInvoice"></param>
        /// <returns></returns>
        public static PartiesType Parties(this Facturae eInvoice)
        {
            eInvoice.VerifyHeader();

            eInvoice.Parties.Parent = eInvoice;

            return eInvoice.Parties;
        }

        /// <summary>
        /// Gets the electronic invoice seller
        /// </summary>
        /// <param name="parties"></param>
        /// <returns></returns>
        public static BusinessType Seller(this PartiesType parties)
        {
            BusinessType partie = new BusinessType { Parent = parties };

            parties.SellerParty = partie;
            
            return partie;
        }

        /// <summary>
        /// Gets the electronic invoice buyer
        /// </summary>
        /// <param name="parties"></param>
        /// <returns></returns>
        public static BusinessType Buyer(this PartiesType parties)
        {
            BusinessType partie = new BusinessType { Parent = parties };
            
            parties.BuyerParty = partie;
            
            return partie;
        }

        /// <summary>
        /// Gets the business parties electronic invoice
        /// </summary>
        /// <param name="parties"></param>
        /// <returns></returns>
        public static Facturae Facturae(this PartiesType parties)
        {
            return parties.Parent;
        }

        #endregion

        #region · BusinessType Extensions ·

        public static PartiesType Parties(this BusinessType party)
        {
            return party.Parent;
        }

        /// <summary>
        /// Sets the identification of a invoice business party
        /// </summary>
        /// <param name="party">The business party</param>
        /// <param name="identification">The identification</param>
        /// <returns></returns>
        public static BusinessType SetIdentification(this BusinessType party, string identification)
        {
            party.PartyIdentification = identification;
  
            return party;
        }

        /// <summary>
        /// Gets the tax identification of a invoice business party
        /// </summary>
        /// <param name="party">The business party</param>
        /// <returns></returns>
        public static TaxIdentificationType TaxIdentification(this BusinessType party)
        {
            if (party.TaxIdentification == null)
            {
                party.TaxIdentification = new TaxIdentificationType
                {
                    Parent = party
                };
            }
  
            return party.TaxIdentification;
        }

        /// <summary>
        /// Sets an invoice business party as an individual
        /// </summary>
        /// <param name="party">The business party</param>
        /// <returns></returns>
        public static IndividualType IsIndividual(this BusinessType party)
        {
            IndividualType individual = new IndividualType { Parent = party };

            party.Item = individual;

            if (party.TaxIdentification != null)
            {
                if (party.TaxIdentification.ResidenceTypeCode == ResidenceTypeCodeType.ResidentInSpain)
                {
                    AddressType address = new AddressType();
                    
                    address.CountryCode = CountryType.ESP;
                    
                    individual.Item = address;
                }
                else
                {
                    individual.Item = new OverseasAddressType();
                }
            }
  
            return individual;
        }

        /// <summary>
        /// Sets an individual party address
        /// </summary>
        /// <param name="party">The business party</param>
        /// <param name="address">The address</param>
        /// <returns></returns>
        public static IndividualType SetAddress(this IndividualType individual, string address)
        {
            if (individual.Item is AddressType)
            {
                ((AddressType)individual.Item).Address = address;
            }
            else
            {
                ((OverseasAddressType)individual.Item).Address = address;
            }

            return individual;
        }

        /// <summary>
        /// Sets an individual party post code
        /// </summary>
        /// <param name="party">The business party</param>
        /// <param name="address">The post code</param>
        /// <returns></returns>
        public static IndividualType SetPostCode(this IndividualType individual, string postCode)
        {
            if (individual.Item is AddressType)
            {
                ((AddressType)individual.Item).PostCode = postCode;
            }
            else
            {
                ((OverseasAddressType)individual.Item).PostCodeAndTown = postCode;
            }

            return individual;
        }

        /// <summary>
        /// Sets an individual party country code
        /// </summary>
        /// <param name="party">The business party</param>
        /// <param name="address">The country code</param>
        /// <returns></returns>
        public static IndividualType SetCountryCode(this IndividualType individual, CountryType countryCode)
        {
            if (individual.Item is AddressType)
            {
                ((AddressType)individual.Item).CountryCode = countryCode;
            }
            else
            {
                ((OverseasAddressType)individual.Item).CountryCode = countryCode;
            }

            return individual;
        }

        /// <summary>
        /// Sets an individual party town
        /// </summary>
        /// <param name="party">The business party</param>
        /// <param name="address">The town</param>
        /// <returns></returns>
        public static IndividualType SetTown(this IndividualType individual, string postCode)
        {
            if (individual.Item is AddressType)
            {
                ((AddressType)individual.Item).Town = postCode;
            }

            return individual;
        }

        /// <summary>
        /// Sets an individual party post code & town 
        /// </summary>
        /// <param name="party">The business party</param>
        /// <param name="address">The post code & towm</param>
        /// <returns></returns>
        public static IndividualType SetPostCodeAndTown(this IndividualType individual, string postCodeAndTown)
        {
            if (individual.Item is OverseasAddressType)
            {
                ((OverseasAddressType)individual.Item).PostCodeAndTown = postCodeAndTown;
            }

            return individual;
        }

        /// <summary>
        /// Sets an individual party province
        /// </summary>
        /// <param name="party">The business party</param>
        /// <param name="address">The province</param>
        /// <returns></returns>
        public static IndividualType SetProvince(this IndividualType individual, string province)
        {
            if (individual.Item is AddressType)
            {
                ((AddressType)individual.Item).Province = province;
            }
            else
            {
                ((OverseasAddressType)individual.Item).Province = province;
            }

            return individual;
        }

        #endregion

        #region · TaxIdentificationType Extensions ·

        /// <summary>
        /// Sets the identification number
        /// </summary>
        /// <param name="taxIdentification">The tax identification</param>
        /// <param name="identificationNumber">The identification number</param>
        /// <returns></returns>
        public static TaxIdentificationType SetIdentificationNumber(this TaxIdentificationType taxIdentification, string identificationNumber)
        {
            taxIdentification.TaxIdentificationNumber = identificationNumber;
  
            return taxIdentification;
        }

        /// <summary>
        /// Sets the tax identification as a individual entity identification
        /// </summary>
        /// <param name="taxIdentification">The tax identification</param>
        /// <returns></returns>
        public static TaxIdentificationType IsIndividual(this TaxIdentificationType taxIdentification)
        {
            taxIdentification.PersonTypeCode = PersonTypeCodeType.Individual;
  
            return taxIdentification;
        }

        /// <summary>
        /// Sets the tax identification as a legal entity identification
        /// </summary>
        /// <param name="taxIdentification">The tax identification</param>
        /// <returns></returns>
        public static TaxIdentificationType IsLegalEntity(this TaxIdentificationType taxIdentification)
        {
            taxIdentification.PersonTypeCode = PersonTypeCodeType.LegalEntity;
  
            return taxIdentification;
        }

        /// <summary>
        /// Sets the tax identification as a foreigner entity identification
        /// </summary>
        /// <param name="taxIdentification">The tax identification</param>
        /// <returns></returns>
        public static TaxIdentificationType IsForeigner(this TaxIdentificationType taxIdentification)
        {
            taxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.Foreigner;
  
            return taxIdentification;
        }

        /// <summary>
        /// Sets the tax identification as an spain tax identification
        /// </summary>
        /// <param name="taxIdentification">The tax identification</param>
        /// <returns></returns>
        public static TaxIdentificationType IsResidentInSpain(this TaxIdentificationType taxIdentification)
        {
            taxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.ResidentInSpain;
  
            return taxIdentification;
        }

        /// <summary>
        /// Sets the tax identification as an EU tax identification
        /// </summary>
        /// <param name="taxIdentification">The tax identification</param>
        /// <returns></returns>
        public static TaxIdentificationType IsResidentInEU(this TaxIdentificationType taxIdentification)
        {
            taxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.ResidentInEU;
  
            return taxIdentification;
        }

        /// <summary>
        /// Gets the tax identification parent party
        /// </summary>
        /// <param name="individual">The individual</param>
        /// <returns></returns>
        public static BusinessType Party(this TaxIdentificationType taxIdentification)
        {
            return taxIdentification.Parent;
        }

        #endregion

        #region · IndividualType Extensions ·

        /// <summary>
        /// Sets an individual name
        /// </summary>
        /// <param name="individual">The individual</param>
        /// <param name="name">The individual name</param>
        /// <returns></returns>
        public static IndividualType SetName(this IndividualType individual, string name)
        {
            individual.Name = name;
  
            return individual;
        }

        /// <summary>
        /// Sets an individual first surname
        /// </summary>
        /// <param name="individual">The individual</param>
        /// <param name="name">The individual first surname</param>
        /// <returns></returns>
        public static IndividualType SetFirstSurname(this IndividualType individual, string firstSurname)
        {
            individual.FirstSurname = firstSurname;
  
            return individual;
        }

        /// <summary>
        /// Sets an individual second surname
        /// </summary>
        /// <param name="individual">The individual</param>
        /// <param name="name">The individual second surname</param>
        /// <returns></returns>
        public static IndividualType SetSecondSurname(this IndividualType individual, string secondSurname)
        {
            individual.SecondSurname = secondSurname;
  
            return individual;
        }

        /// <summary>
        /// Gets the individual parent party
        /// </summary>
        /// <param name="individual">The individual</param>
        /// <returns></returns>
        public static BusinessType Party(this IndividualType individual)
        {
            return individual.Parent;
        }

        #endregion

        #region · XML Validation Extensions ·

        /// <summary>
        /// Validates the electronic invoice XML
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <returns></returns>
        public static Facturae Validate(this Facturae eInvoice)
        {
            XmlDocument document = eInvoice.ToXmlDocument();
            
            document.Schemas.Add(XsdSchemas.BuildSchemaSet(document.NameTable));
            document.Validate(XmlValidationEventHandler);

            return eInvoice;
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
        public static XAdESSignatureVerifier Sign(this Facturae eInvoice, X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate cannot be null");
            }

            return eInvoice.Sign(certificate, (RSA)certificate.PrivateKey);
        }

        /// <summary>
        /// Signs the electronic invoice using the given certificate & RSA key
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <param name="certificate">The certificate</param>
        /// <param name="key">The RSA Key</param>
        /// <returns></returns>
        public static XAdESSignatureVerifier Sign(this Facturae eInvoice, X509Certificate2 certificate, RSA key)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate cannot be null");
            }
            if (key == null)
            {
                throw new ArgumentNullException("key cannot be null");
            }

            XmlDocument document = eInvoice.ToXmlDocument();
            XAdESSignedXml signedXml = new XAdESSignedXml(document);

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
        public static Facturae WriteToFile(this Facturae eInvoice, string path)
        {
            eInvoice.ToXmlDocument().Save(path);

            return eInvoice;
        }

        #endregion

        #region · Private Extensions ·

        private static Facturae VerifyHeader(this Facturae eInvoice)
        {
            if (eInvoice.FileHeader == null)
            {
                eInvoice.FileHeader               = new FileHeaderType();
                eInvoice.FileHeader.Batch         = new BatchType();
                eInvoice.FileHeader.SchemaVersion = SchemaVersionType.Item32;
                eInvoice.Parties                  = new PartiesType();
                eInvoice.Parties.SellerParty      = new BusinessType();
                eInvoice.Parties.BuyerParty       = new BusinessType();
                eInvoice.Invoices                 = new List<InvoiceType>();

                eInvoice.SetCurrency(CurrencyCodeType.EUR);
                eInvoice.SetIssuer(InvoiceIssuerTypeType.EM);
            }

            return eInvoice;
        }

        private static string ToXml(this Facturae eInvoice)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
                        
            settings.Encoding = new UTF8Encoding(false);
            
            using (MemoryStream buffer = new MemoryStream())
            { 
                using (XmlWriter writer = XmlWriter.Create(buffer, settings))
                {
                    FacturaeSerializer.Serialize(writer, eInvoice, XsdSchemas.CreateXadesSerializerNamespace());
                }

                return Encoding.UTF8.GetString(buffer.ToArray());
            }
        }

        private static XmlDocument ToXmlDocument(this Facturae eInvoice)
        {
            XmlDocument document = new XmlDocument { PreserveWhitespace = true };

            document.LoadXml(eInvoice.ToXml());

            return document;
        }
       
        #endregion
    }
}