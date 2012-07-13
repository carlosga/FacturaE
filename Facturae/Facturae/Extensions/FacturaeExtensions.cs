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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using nFacturae.Xml;

namespace nFacturae.Extensions
{
    /// <summary>
    /// Facturae extensions
    /// </summary>
    public static class FacturaeExtensions
    {
        #region · Constants ·

        const string PolicyIdentifier = "http://www.facturae.es/politica_de_firma_formato_facturae/politica_de_firma_formato_facturae_v3_1.pdf";

        #endregion

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
        public static Facturae SetchemaVersion(this Facturae eInvoice, SchemaVersionType schemaVersion)
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
                                    
            eInvoice.FileHeader.Modality                        = ((eInvoice.Invoices.Count == 1) ? ModalityType.Single : ModalityType.Batch);
            eInvoice.FileHeader.Batch.InvoicesCount             = eInvoice.Invoices.Count;
            eInvoice.FileHeader.Batch.TotalInvoicesAmount       = eInvoice.SumTotalAmounts();
            eInvoice.FileHeader.Batch.TotalOutstandingAmount    = eInvoice.SumTotalOutstandingAmount();
            eInvoice.FileHeader.Batch.TotalExecutableAmount     = eInvoice.SumTotalExecutableAmount();
            eInvoice.FileHeader.Batch.BatchIdentifier           = String.Format
            (
                "{0}{1}{2}", 
                String.Empty, 
                firstInvoice.InvoiceHeader.InvoiceNumber, 
                firstInvoice.InvoiceHeader.InvoiceSeriesCode
            );

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

            amount.TotalAmount          = Math.Round(eInvoice.Invoices.Sum(il => il.InvoiceTotals.InvoiceTotal), 2);
            amount.EquivalentInEuros    = Math.Round(eInvoice.Invoices.Sum(il => il.InvoiceTotals.InvoiceTotal), 2);

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

            amount.TotalAmount          = Math.Round(eInvoice.Invoices.Sum(il => il.InvoiceTotals.TotalOutstandingAmount), 2);
            amount.EquivalentInEuros    = Math.Round(eInvoice.Invoices.Sum(il => il.InvoiceTotals.TotalOutstandingAmount), 2);

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

            amount.TotalAmount          = Math.Round(eInvoice.Invoices.Sum(il => il.InvoiceTotals.TotalExecutableAmount), 2);
            amount.EquivalentInEuros    = Math.Round(eInvoice.Invoices.Sum(il => il.InvoiceTotals.TotalExecutableAmount), 2);

            return amount;
        }
        
        public static InvoiceType CreateInvoice(this Facturae eInvoice)
        {
            InvoiceType invoice = new InvoiceType();

            invoice.Parent              = eInvoice;
            invoice.InvoiceHeader       = new InvoiceHeaderType();
            invoice.InvoiceTotals       = new InvoiceTotalsType();
            invoice.InvoiceIssueData    = new InvoiceIssueDataType();

            eInvoice.Invoices.Add(invoice);

            return invoice;
        }

        #endregion

        #region · InvoiceType Extensions ·

        public static InvoiceType SetInvoiceNumber(this InvoiceType invoice, string invoiceNumber)
        {
            invoice.InvoiceHeader.InvoiceNumber = invoiceNumber;

            return invoice;
        }

        public static InvoiceType SetInvoiceSeriesCode(this InvoiceType invoice, string seriesCode)
        {
            invoice.InvoiceHeader.InvoiceSeriesCode = seriesCode;

            return invoice;
        }

        public static InvoiceType IsComplete(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.Complete;

            return invoice;
        }

        public static InvoiceType IsAbbreviated(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.Abbreviated;

            return invoice;
        }

        public static InvoiceType IsSelfInvoice(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.SelfInvoice;

            return invoice;
        }

        public static InvoiceType IsOriginal(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceClass = InvoiceClassType.Original;

            return invoice;
        }

        public static InvoiceType IsCorrective(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceClass = InvoiceClassType.Corrective;

            return invoice;
        }

        public static InvoiceType IsSummaryOriginal(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceClass = InvoiceClassType.SummaryOriginal;

            return invoice;
        }

        public static InvoiceType IsCopyOfOriginal(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfOriginal;

            return invoice;
        }

        public static InvoiceType IsCopyOfCorrective(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfCorrective;
            
            return invoice;
        }

        public static InvoiceType IsCopyOfSummary(this InvoiceType invoice)
        {
            invoice.InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfSummary;

            return invoice;
        }

        public static InvoiceType GiveIssueDate(this InvoiceType invoice, DateTime date)
        {
            invoice.InvoiceIssueData.IssueDate = date;

            return invoice;
        }

        public static InvoiceType GiveOperationDate(this InvoiceType invoice, DateTime date)
        {
            invoice.InvoiceIssueData.OperationDate = date;
            
            return invoice;
        }

        public static InvoiceType SetPlaceOfIssue(this InvoiceType invoice, string description, string postCode)
        {
            invoice.InvoiceIssueData.PlaceOfIssue = new PlaceOfIssueType();

            invoice.InvoiceIssueData.PlaceOfIssue.PlaceOfIssueDescription   = description;
            invoice.InvoiceIssueData.PlaceOfIssue.PostCode                  = postCode;
            
            return invoice;
        }

        public static InvoiceType GiveInvoicingPeriod(this InvoiceType invoice, DateTime startDate, DateTime endDate)
        {
            invoice.InvoiceIssueData.InvoicingPeriod = new PeriodDates();

            invoice.InvoiceIssueData.InvoicingPeriod.StartDate  = startDate;
            invoice.InvoiceIssueData.InvoicingPeriod.EndDate    = endDate;
            
            return invoice;
        }

        public static InvoiceType SetCurrency(this InvoiceType invoice, CurrencyCodeType currency)
        {
            invoice.InvoiceIssueData.InvoiceCurrencyCode = currency;
            
            return invoice;
        }

        public static InvoiceType SetExchangeRate(this InvoiceType invoice, double exchangeRate, DateTime exchangeDate)
        {
            invoice.InvoiceIssueData.ExchangeRateDetails = new ExchangeRateDetailsType();
            
            invoice.InvoiceIssueData.ExchangeRateDetails.ExchangeRate = exchangeRate;
            
            return invoice;
        }

        public static InvoiceType SetTaxCurrency(this InvoiceType invoice, CurrencyCodeType currencyType)
        {
            invoice.InvoiceIssueData.TaxCurrencyCode = currencyType;
                        
            return invoice;
        }

        public static InvoiceType SetLanguage(this InvoiceType invoice, LanguageCodeType language)
        {
            invoice.InvoiceIssueData.LanguageName = language;

            return invoice;
        }

        public static InvoiceType Items(this InvoiceType invoice)
        {
            if (invoice.Items == null)
            {
                invoice.Items = new List<InvoiceLineType>();
            }

            return invoice;
        }

        public static Facturae CalculateTotals(this InvoiceType invoice)
        {
            double                      subsidyAmount   = 0;
            List<InvoiceLineTypeTax>    rawLineTaxes    = new List<InvoiceLineTypeTax>();
                                    
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
                            TotalAmount         = Math.Round(g.Sum(gtax => gtax.TaxableBase.TotalAmount), 2),
                            EquivalentInEuros   = Math.Round(g.Sum(gtax => gtax.TaxableBase.EquivalentInEuros), 2)
                        },
                        TaxAmount   = new AmountType 
                        { 
                            TotalAmount         = Math.Round(g.Sum(gtax => gtax.TaxAmount.TotalAmount), 2),
                            EquivalentInEuros   = Math.Round(g.Sum(gtax => gtax.TaxAmount.EquivalentInEuros), 2)
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

            invoice.InvoiceTotals.TotalGrossAmountBeforeTaxes = Math.Round
            (
                invoice.InvoiceTotals.TotalGrossAmount      - 
                invoice.InvoiceTotals.TotalGeneralDiscounts + 
                invoice.InvoiceTotals.TotalGeneralSurcharges, 2
            );

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

            invoice.InvoiceTotals.InvoiceTotal = Math.Round
            (
                invoice.InvoiceTotals.TotalGrossAmountBeforeTaxes   +
                invoice.InvoiceTotals.TotalTaxOutputs               -
                invoice.InvoiceTotals.TotalTaxesWithheld, 2
            );

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

            invoice.InvoiceTotals.TotalExecutableAmount = Math.Round
            (
                invoice.InvoiceTotals.TotalOutstandingAmount    - 
                invoice.InvoiceTotals.TotalTaxesWithheld        + 
                invoice.InvoiceTotals.TotalReimbursableExpenses + 
                invoice.InvoiceTotals.TotalFinancialExpenses, 2
            );

            return invoice.Parent;
        }

        #endregion

        #region · InvoiceLineType Extensions ·

        public static InvoiceLineType AddInvoiceItem(this InvoiceType invoice, string articleCode, string productDescription)
        {
            InvoiceLineType item = new InvoiceLineType();

            item.Parent             = invoice;
            item.ArticleCode        = articleCode;
            item.ItemDescription    = productDescription;

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

            DiscountType discount = new DiscountType();

            discount.DiscountRate   = discountRate;
            discount.DiscountReason = "Descuento";

            invoiceLine.DiscountsAndRebates.Add(discount);

            return invoiceLine;
        }

        public static InvoiceLineType GiveTax(this InvoiceLineType invoiceLine, double taxRate)
        {
            if (invoiceLine.TaxesOutputs == null)
            {
                invoiceLine.TaxesOutputs = new List<InvoiceLineTypeTax>();
            }

            InvoiceLineTypeTax tax = new InvoiceLineTypeTax();

            tax.TaxTypeCode = TaxTypeCodeType.Item01;
            tax.TaxRate     = taxRate;

            invoiceLine.TaxesOutputs.Add(tax);

            return invoiceLine;
        }

        public static InvoiceType CalculateTotals(this InvoiceLineType invoiceLine)
        {
            double totalDiscounts   = 0;
            double totalCharges     = 0;

            invoiceLine.TotalCost = Math.Round(invoiceLine.Quantity * invoiceLine.UnitPriceWithoutTax, 2);

            if (invoiceLine.DiscountsAndRebates != null)
            {
                invoiceLine.DiscountsAndRebates.ForEach
                (
                    dar => 
                    {
                        dar.DiscountAmount  = Math.Round(invoiceLine.TotalCost * dar.DiscountRate / 100, 2);
                        totalDiscounts      = Math.Round(totalDiscounts + dar.DiscountAmount, 2);
                    }
                );
            }

            if (invoiceLine.Charges != null)
            {
                invoiceLine.Charges.ForEach
                (
                    chr =>
                    {
                        chr.ChargeAmount    = Math.Round(invoiceLine.TotalCost * chr.ChargeRate / 100, 2);
                        totalCharges        = Math.Round(totalCharges + chr.ChargeAmount, 2);
                    }
                );
            }

            invoiceLine.GrossAmount = invoiceLine.TotalCost 
                                      - totalDiscounts
                                      + totalCharges;

            invoiceLine.TaxesOutputs.ForEach
            (
                tax =>
                {
                    if (tax.TaxableBase == null)
                    {
                        tax.TaxableBase = new AmountType();
                    }

                    tax.TaxableBase.TotalAmount         = invoiceLine.GrossAmount;
                    tax.TaxableBase.EquivalentInEuros   = invoiceLine.GrossAmount;

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

        public static PartiesType Parties(this Facturae eInvoice)
        {
            eInvoice.VerifyHeader();

            eInvoice.Parties.Parent = eInvoice;

            return eInvoice.Parties;
        }

        public static BusinessType Seller(this PartiesType parties)
        {
            BusinessType partie = new BusinessType();

            partie.Parent       = parties;
            parties.SellerParty = partie;
            
            return partie;
        }

        public static BusinessType Buyer(this PartiesType parties)
        {
            BusinessType partie = new BusinessType();

            partie.Parent       = parties;
            parties.BuyerParty  = partie;
            
            return partie;
        }

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

        public static BusinessType GiveIdentification(this BusinessType party, string identification)
        {
            party.PartyIdentification = identification;
  
            return party;
        }

        public static TaxIdentificationType TaxIdentification(this BusinessType party)
        {
            party.TaxIdentification         = new TaxIdentificationType();
            party.TaxIdentification.Parent  = party;
  
            return party.TaxIdentification;
        }

        public static IndividualType IsIndividual(this BusinessType party)
        {
            IndividualType individual = new IndividualType();

            party.Item          = individual;
            individual.Parent   = party;

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

        public static IndividualType SetTown(this IndividualType individual, string postCode)
        {
            if (individual.Item is AddressType)
            {
                ((AddressType)individual.Item).Town = postCode;
            }

            return individual;
        }

        public static IndividualType GivePostCodeAndTown(this IndividualType individual, string postCodeAndTown)
        {
            if (individual.Item is OverseasAddressType)
            {
                ((OverseasAddressType)individual.Item).PostCodeAndTown = postCodeAndTown;
            }

            return individual;
        }

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

        public static TaxIdentificationType GiveIdentificationNumber(this TaxIdentificationType taxIdentification, string identificationNumber)
        {
            taxIdentification.TaxIdentificationNumber = identificationNumber;
  
            return taxIdentification;
        }

        public static TaxIdentificationType IsIndividual(this TaxIdentificationType taxIdentification)
        {
            taxIdentification.PersonTypeCode = PersonTypeCodeType.Individual;
  
            return taxIdentification;
        }

        public static TaxIdentificationType IsLegalEntity(this TaxIdentificationType taxIdentification)
        {
            taxIdentification.PersonTypeCode = PersonTypeCodeType.LegalEntity;
  
            return taxIdentification;
        }

        public static TaxIdentificationType IsForeigner(this TaxIdentificationType taxIdentification)
        {
            taxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.Foreigner;
  
            return taxIdentification;
        }

        public static TaxIdentificationType IsResidentInSpain(this TaxIdentificationType taxIdentification)
        {
            taxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.ResidentInSpain;
  
            return taxIdentification;
        }

        public static TaxIdentificationType IsResidentInEU(this TaxIdentificationType taxIdentification)
        {
            taxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.ResidentInEU;
  
            return taxIdentification;
        }

        public static BusinessType Party(this TaxIdentificationType taxIdentification)
        {
            return taxIdentification.Parent;
        }

        #endregion

        #region · IndividualType Extensions ·

        public static IndividualType SetName(this IndividualType individual, string name)
        {
            individual.Name = name;
  
            return individual;
        }

        public static IndividualType SetFirstSurname(this IndividualType individual, string firstSurname)
        {
            individual.FirstSurname = firstSurname;
  
            return individual;
        }

        public static IndividualType SetSecondSurname(this IndividualType individual, string secondSurname)
        {
            individual.SecondSurname = secondSurname;
  
            return individual;
        }

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
            
            document.Schemas.Add(XsdSchemas.GetSchemaSet(document.NameTable));
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
        public static SignedFacturae Sign(this Facturae eInvoice, X509Certificate2 certificate)
        {
            return eInvoice.Sign(certificate, (RSA)certificate.PrivateKey);
        }

        /// <summary>
        /// Signs the electronic invoice using the given certificate & RSA key
        /// </summary>
        /// <param name="eInvoice">The electronic invoice</param>
        /// <param name="certificate">The certificate</param>
        /// <param name="key">The RSA Key</param>
        /// <returns></returns>
        public static SignedFacturae Sign(this Facturae eInvoice, X509Certificate2 certificate, RSA key)
        {
            XmlDocument     document    = eInvoice.ToXmlDocument();
            XaDESSignedXml  signedXml   = new XaDESSignedXml(document);

            // Set the key to sign
            signedXml.SigningKey = key;

            // Set nodes identifiers
            signedXml.Signature.Id  = XsdSchemas.FormatId("Signature");
            signedXml.SignedInfo.Id = XsdSchemas.FormatId(signedXml.Signature.Id, "SignedInfo");

            // Set the reference to sign
            signedXml.AddReference(SetSignatureTransformReference(signedXml, document));

            // Add XAdES node
            signedXml.AddReference(AddXAdESNodes(signedXml, document, certificate));

            // Set the Key Info
            signedXml.AddReference(SetKeyInfo(signedXml, certificate, (RSA)certificate.PublicKey.Key));

            // Compute Signature
            signedXml.ComputeSignature();
            
            // Import the signed XML node 
            document.DocumentElement.AppendChild(document.ImportNode(signedXml.GetXml(), true));
                        
            return new SignedFacturae(document);
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

        #region · Signature Helper Methods ·

        private static Reference SetKeyInfo(XaDESSignedXml signedXml, X509Certificate2 certificate, RSA key)
        {   
            signedXml.KeyInfo       = new KeyInfo();
            signedXml.KeyInfo.Id    = XsdSchemas.FormatId(signedXml.Signature.Id, "KeyInfo");

            signedXml.KeyInfo.AddClause(new KeyInfoX509Data(certificate));
            signedXml.KeyInfo.AddClause(new RSAKeyValue(key));

            return new Reference
            {
                Uri = String.Format("#{0}", signedXml.KeyInfo.Id),
            };
        }

        private static Reference SetSignatureTransformReference(XaDESSignedXml signedXml, XmlDocument document)
        {
            Reference reference = new Reference(String.Empty);

            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());

            return reference;
        }

        #endregion

        #region · XaDES Node Methods ·

        private static Reference AddXAdESNodes(XaDESSignedXml signedXml, XmlDocument document, X509Certificate2 certificate)
        {
            var qualifyingPropertiesNode    = AddQualifyingPropertiesNode(signedXml, document);
            var signedPropertiesNode        = AddSignedPropertiesNode(signedXml, document, qualifyingPropertiesNode);
            var signedSignatureProperties   = AddSignedSignaturePropertiesNode(document, signedPropertiesNode);

            AddSigningTimeNode(document, signedSignatureProperties);
            AddSigningCertificate(document, signedSignatureProperties, certificate);           
            AddSignaturePolicyIdentifier(document, signedSignatureProperties, certificate);
			
			CreateDataObject(signedXml, qualifyingPropertiesNode);

            return CreateSignedPropertiesReference(signedXml, signedPropertiesNode);
        }

        private static Reference CreateSignedPropertiesReference(XaDESSignedXml signedXml, XmlElement signedPropertiesNode)
        {
            return new Reference
            {
                Id      = XsdSchemas.FormatId(signedXml.Signature.Id, "SignedPropertiesReference"),
                Uri     = String.Format("#{0}", signedPropertiesNode.GetAttribute("Id")),
                Type    = "http://uri.etsi.org/01903/v1.3.2#SignedProperties"
            };
        }

        private static void CreateDataObject(XaDESSignedXml signedXml, XmlElement element)
        {
            var dataObject = new DataObject();

            dataObject.Data = element.SelectNodes(".");
            
            signedXml.AddObject(dataObject);
        }

        private static XmlElement AddQualifyingPropertiesNode(XaDESSignedXml signedXml, XmlDocument document)
        {            
            var result = document.CreateElement(XsdSchemas.XadesPrefix, "QualifyingProperties", XsdSchemas.XadesNamespaceUrl);
            result.SetAttribute("Target", String.Format("#{0}", signedXml.Signature.Id));          
            
            return result;
        }

        private static XmlElement AddSignedPropertiesNode(XaDESSignedXml signedXml, XmlDocument document, XmlElement qualifyingPropertiesNode)
        {
            var signedPropertiesNode = document.CreateNode(XsdSchemas.XadesPrefix, "SignedProperties", XsdSchemas.XadesNamespaceUrl, qualifyingPropertiesNode);
            signedPropertiesNode.SetAttribute("Id", XsdSchemas.FormatId(signedXml.Signature.Id, "SignedProperties"));
            
			return signedPropertiesNode;
        }

        private static XmlElement AddSignedSignaturePropertiesNode(XmlDocument document, XmlElement propertiesNode)
        {
            var signedSignaturePropertiesNode = document.CreateNode(XsdSchemas.XadesPrefix, "SignedSignatureProperties", XsdSchemas.XadesNamespaceUrl, propertiesNode);
            return signedSignaturePropertiesNode;
        }

        private static void AddSigningTimeNode(XmlDocument document, XmlElement signedSignaturePropertiesNode)
        {
            document.CreateNode(XsdSchemas.XadesPrefix, "SigningTime", XsdSchemas.NowInCanonicalRepresentation(),
                				XsdSchemas.XadesNamespaceUrl, signedSignaturePropertiesNode);
        }

        private static void AddSigningCertificate(XmlDocument document, XmlElement signedSignatureProperties, X509Certificate2 certificate)
        {
            var signingCertificateNode = document.CreateNode(XsdSchemas.XadesPrefix, "SigningCertificate", XsdSchemas.XadesNamespaceUrl, signedSignatureProperties);
            var certNode 			   = document.CreateNode(XsdSchemas.XadesPrefix, "Cert", XsdSchemas.XadesNamespaceUrl, signingCertificateNode);

            AddCertDigestNode(document, certNode, certificate);
            AddIssuerSerialNode(document, certNode, certificate);
        }

        private static void AddCertDigestNode(XmlDocument document, XmlElement certNode, X509Certificate2 certificate)
        {
            var certDigestNode = document.CreateNode(XsdSchemas.XadesPrefix, "CertDigest", XsdSchemas.XadesNamespaceUrl, certNode);
			
            document.CreateNode(XsdSchemas.XmlDsigPrefix, "DigestMethod", "Algorithm", SignedXml.XmlDsigSHA1Url, SignedXml.XmlDsigNamespaceUrl, certDigestNode);

            var digestValue = certificate.RawData.ComputeSHA1Hash().ToBase64String();
			
            document.CreateNode(XsdSchemas.XmlDsigPrefix, "DigestValue", digestValue, SignedXml.XmlDsigNamespaceUrl, certDigestNode);
        }

        private static void AddIssuerSerialNode(XmlDocument document, XmlElement certNode, X509Certificate2 certificate)
        {
            var issuerSerialNode = document.CreateNode(XsdSchemas.XadesPrefix, "IssuerSerial", XsdSchemas.XadesNamespaceUrl, certNode);

            document.CreateNode(XsdSchemas.XmlDsigPrefix, "X509IssuerName", certificate.Issuer,
            					SignedXml.XmlDsigNamespaceUrl, issuerSerialNode);

            document.CreateNode(XsdSchemas.XmlDsigPrefix, "X509SerialNumber", Int32.Parse(certificate.SerialNumber, NumberStyles.HexNumber).ToString(),
                                SignedXml.XmlDsigNamespaceUrl, issuerSerialNode);
        }
        
        private static void AddSignaturePolicyIdentifier(XmlDocument document, XmlElement signedSignatureProperties, X509Certificate2 certificate)
        {
            var signaturePolicyIdentifierNode = document.CreateNode(XsdSchemas.XadesPrefix, "SignaturePolicyIdentifier", XsdSchemas.XadesNamespaceUrl, signedSignatureProperties);
            var signaturePolicyId 			  = document.CreateNode(XsdSchemas.XadesPrefix, "SignaturePolicyId", XsdSchemas.XadesNamespaceUrl, signaturePolicyIdentifierNode);
            var policyIdNode 				  = document.CreateNode(XsdSchemas.XadesPrefix, "SigPolicyId", XsdSchemas.XadesNamespaceUrl, signaturePolicyId);
            var identifierNode 				  = document.CreateNode(XsdSchemas.XadesPrefix, "Identifier", PolicyIdentifier, XsdSchemas.XadesNamespaceUrl, policyIdNode);
            var descriptionNode 			  = document.CreateNode(XsdSchemas.XadesPrefix, "Description", "facturae31", XsdSchemas.XadesNamespaceUrl, policyIdNode);			
			
			identifierNode.SetAttribute("Qualifier", "OIDAsURI");

            AddSigPolicyHash(document, signaturePolicyId, certificate);
        }

        private static void AddSigPolicyHash(XmlDocument document, XmlElement signaturePolicyId, X509Certificate2 certificate)
        {
            var signaturePolicyIdNode = document.CreateNode(XsdSchemas.XadesPrefix, "SigPolicyHash", XsdSchemas.XadesNamespaceUrl, signaturePolicyId);
			
            document.CreateNode(XsdSchemas.XmlDsigPrefix, "DigestMethod", "Algorithm", SignedXml.XmlDsigSHA1Url, SignedXml.XmlDsigNamespaceUrl, signaturePolicyIdNode);

            var digestValue = ReadPolicyFile().ComputeSHA1Hash().ToBase64String();
			
            document.CreateNode(XsdSchemas.XmlDsigPrefix, "DigestValue", digestValue, SignedXml.XmlDsigNamespaceUrl, signaturePolicyIdNode);
        }

        private static byte[] ReadPolicyFile()
        {
            Assembly    currentAssembly = Assembly.GetExecutingAssembly();
            string      resourceName    = "nFacturae.Policies.politica_de_firma_formato_facturae_v3_1.pdf";

            using (Stream stream = currentAssembly.GetManifestResourceStream(resourceName))
            {
                byte[] buffer = new byte[stream.Length];
 
                stream.Read(buffer, 0, (int)stream.Length);
            
                return buffer;
            }
        }

        #endregion

        #region · Private Extensions ·

        private static Facturae VerifyHeader(this Facturae eInvoice)
        {
            if (eInvoice.FileHeader == null)
            {
                eInvoice.FileHeader                 = new FileHeaderType();
                eInvoice.FileHeader.Batch           = new BatchType();
                eInvoice.FileHeader.SchemaVersion   = SchemaVersionType.Item32;
                eInvoice.Parties                    = new PartiesType();
                eInvoice.Parties.SellerParty        = new BusinessType();
                eInvoice.Parties.BuyerParty         = new BusinessType();
                eInvoice.Invoices                   = new List<InvoiceType>();

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
                    FacturaeExtensions.FacturaeSerializer.Serialize(writer, eInvoice, XsdSchemas.CreateXadesSerializerNamespace());
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