// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FacturaE.DataType;

namespace FacturaE;

/// <summary>
/// Facturae extensions
/// </summary>
public partial class Facturae
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Facturae"/> class.
    /// </summary>
    public Facturae()
    {
        FileHeader = new FileHeaderType
        {
            Batch         = new BatchType(),
            SchemaVersion = SchemaVersionType.Item322
        };
        Parties = new PartiesType
        {
            SellerParty = new BusinessType(this),
            BuyerParty  = new BusinessType(this),
            Parent      = this
        };

        Invoices = new List<InvoiceType>();
        SetCurrency(CurrencyCodeType.EUR);
        SetIssuer(InvoiceIssuerTypeType.Seller);
    }

    /// <summary>
    /// Sets the currency code of the electronic invoice.
    /// </summary>
    /// <param name="eInvoice">The electronic invoice.</param>
    /// <param name="currencyCode">The currency code.</param>
    /// <returns></returns>
    public Facturae SetCurrency(CurrencyCodeType currencyCode)
    {
        FileHeader.Batch.InvoiceCurrencyCode = currencyCode;

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
        FileHeader.SchemaVersion = schemaVersion;

        return this;
    }

    /// <summary>
    /// Sets the electronic invoice issuer.
    /// </summary>
    /// <param name="issuerType">The issuer type.</param>
    /// <returns></returns>
    public Facturae SetIssuer(InvoiceIssuerTypeType issuerType)
    {
        FileHeader.InvoiceIssuerType = issuerType;

        return this;
    }

    /// <summary>
    /// Gets the electronic invoice seller
    /// </summary>
    /// <returns>The seller party.</returns>
    public BusinessType Seller()
    {
        return Parties.SellerParty;
    }

    /// <summary>
    /// Gets the electronic invoice buyer
    /// </summary>
    /// <returns>The buyer party.</returns>
    public BusinessType Buyer()
    {
        return Parties.BuyerParty;
    }

    /// <summary>
    /// Calculates the electronic totals.
    /// </summary>
    /// <returns></returns>
    public Facturae CalculateTotals()
    {
        var firstInvoice = Invoices[0];

        FileHeader.Modality                     = ((Invoices.Count == 1) ? ModalityType.Single : ModalityType.Batch);
        FileHeader.Batch.InvoicesCount          = Invoices.Count;
        FileHeader.Batch.TotalInvoicesAmount    = SumTotalAmounts();
        FileHeader.Batch.TotalOutstandingAmount = SumTotalOutstandingAmount();
        FileHeader.Batch.TotalExecutableAmount  = SumTotalExecutableAmount();
        FileHeader.Batch.BatchIdentifier        = firstInvoice.InvoiceHeader.InvoiceNumber
                                                + firstInvoice.InvoiceHeader.InvoiceSeriesCode;

        return this;
    }

    /// <summary>
    /// Sums the invoice total.
    /// </summary>
    /// <returns>An instance of AmountType.</returns>
    public AmountType SumTotalAmounts()
    {
        var totalAmount = Invoices.Sum(il => il.InvoiceTotals.InvoiceTotal).Round();

        return new AmountType
        {
            TotalAmount                = totalAmount,
            EquivalentInEuros          = totalAmount,
            EquivalentInEurosSpecified = true
        };
    }

    /// <summary>
    /// Sums the electronic invoice outstanding amount.
    /// </summary>
    /// <returns>An instance of AmountType.</returns>
    public AmountType SumTotalOutstandingAmount()
    {
        var totalAmount = Invoices.Sum(il => il.InvoiceTotals.TotalOutstandingAmount).Round();

        return new AmountType
        {
            TotalAmount                = totalAmount,
            EquivalentInEuros          = totalAmount,
            EquivalentInEurosSpecified = true
        };
    }

    /// <summary>
    /// Sums the electronic invoice executable amount.
    /// </summary>
    /// <returns>An instance of AmountType.</returns>
    public AmountType SumTotalExecutableAmount()
    {
        var totalAmount = Invoices.Sum(il => il.InvoiceTotals.TotalExecutableAmount).Round();

        return new AmountType
        {
            TotalAmount                = totalAmount,
            EquivalentInEuros          = totalAmount,
            EquivalentInEurosSpecified = true
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
            Parent           = this,
            InvoiceHeader    = new InvoiceHeaderType(),
            InvoiceTotals    = new InvoiceTotalsType(),
            InvoiceIssueData = new InvoiceIssueDataType(),
            Items            = new List<InvoiceLineType>()
        };

        Invoices.Add(invoice);

        return invoice;
    }
}

public partial class InvoiceType
{
    /// <summary>
    /// Set the invoice series code.
    /// </summary>
    /// <param name="seriesCode">The invoice series code.</param>
    /// <returns></returns>
    public InvoiceType SetInvoiceSeries(string seriesCode)
    {
        InvoiceHeader.InvoiceSeriesCode = seriesCode;

        return this;
    }

    /// <summary>
    /// Sets the invoice number.
    /// </summary>
    /// <param name="invoiceNumber">The invoice number.</param>
    /// <returns></returns>
    public InvoiceType SetInvoiceNumber(string invoiceNumber)
    {
        InvoiceHeader.InvoiceNumber = invoiceNumber;

        return this;
    }

    /// <summary>
    /// Sets the invoice class as a complete invoice.
    /// </summary>
    /// <returns></returns>
    public InvoiceType IsComplete()
    {
        InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.CompleteInvoce;

        return this;
    }

    /// <summary>
    /// Sets the invoice class as a abbreviated invoice.
    /// </summary>
    /// <returns></returns>
    public InvoiceType IsAbbreviated()
    {
        InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.Abbreviated;

        return this;
    }

    /// <summary>
    /// Sets the invoice class as a self invoice.
    /// </summary>
    /// <returns></returns>
    public InvoiceType IsSelfInvoice()
    {
        InvoiceHeader.InvoiceDocumentType = InvoiceDocumentTypeType.SelfInvoice;

        return this;
    }

    /// <summary>
    /// Sets the invoice class as a original invoice.
    /// </summary>
    /// <returns></returns>
    public InvoiceType IsOriginal()
    {
        InvoiceHeader.InvoiceClass = InvoiceClassType.OriginalInvoice;

        return this;
    }

    /// <summary>
    /// Sets the invoice class as a corrective invoice.
    /// </summary>
    /// <returns></returns>
    public InvoiceType IsCorrective()
    {
        InvoiceHeader.InvoiceClass = InvoiceClassType.Corrective;

        return this;
    }

    /// <summary>
    /// Sets the invoice class as summary of original invoice.
    /// </summary>
    /// <returns></returns>
    public InvoiceType IsSummaryOriginal()
    {
        InvoiceHeader.InvoiceClass = InvoiceClassType.Summary;

        return this;
    }

    /// <summary>
    /// Sets the invoice class as copy of original invoice.
    /// </summary>
    /// <returns></returns>
    public InvoiceType IsCopyOfOriginal()
    {
        InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfTheOriginal;

        return this;
    }

    /// <summary>
    /// Sets the invoice class as copy of corrective invoice.
    /// </summary>
    /// <returns></returns>
    public InvoiceType IsCopyOfCorrective()
    {
        InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfTheCorrective;

        return this;
    }

    /// <summary>
    /// Sets the invoice class as copy of summary invoice.
    /// </summary>
    /// <returns></returns>
    public InvoiceType IsCopyOfSummary()
    {
        InvoiceHeader.InvoiceClass = InvoiceClassType.CopyOfTheSummary;

        return this;
    }

    /// <summary>
    /// Sets the invoice issue date.
    /// </summary>
    /// <param name="date">The invoice issue date.</param>
    /// <returns></returns>
    public InvoiceType SetIssueDate(DateTime date)
    {
        InvoiceIssueData.IssueDate = date;

        return this;
    }

    /// <summary>
    /// Sets the invoice operation date.
    /// </summary>
    /// <param name="date">The invoice operation date.</param>
    /// <returns></returns>
    public InvoiceType SetOperationDate(DateTime date)
    {
        InvoiceIssueData.OperationDate = date;

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
        InvoiceIssueData.PlaceOfIssue = new PlaceOfIssueType
        {
            PlaceOfIssueDescription = description,
            PostCode                = postCode
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
        InvoiceIssueData.InvoicingPeriod = new PeriodDates
        {
            StartDate = startDate,
            EndDate   = endDate
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
        InvoiceIssueData.InvoiceCurrencyCode = currency;

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
        InvoiceIssueData.ExchangeRateDetails = new ExchangeRateDetailsType
        {
            ExchangeRate     = exchangeRate,
            ExchangeRateDate = exchangeDate
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
        InvoiceIssueData.TaxCurrencyCode = taxCurrency;

        return this;
    }

    /// <summary>
    /// Sets the invoice language.
    /// </summary>
    /// <param name="language">The invoice language.</param>
    /// <returns></returns>
    public InvoiceType SetLanguage(LanguageCodeType language)
    {
        InvoiceIssueData.LanguageName = language;

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
            Parent          = this,
            ArticleCode     = productCode,
            ItemDescription = productDescription
        };

        Items.Add(item);

        return item;
    }

    /// <summary>
    /// Calculates the invoice totals.
    /// </summary>
    /// <returns></returns>
    public Facturae CalculateTotals()
    {
        DoubleUpToEightDecimalType subsidyAmount = 0;

        // Taxes Outputs
        var q = from tax in Items.SelectMany(x => x.TaxesOutputs)
                group tax by new { tax.TaxTypeCode, tax.TaxRate, tax.EquivalenceSurcharge } into g
                select new TaxOutputType
                {
                    TaxRate     = g.Key.TaxRate,
                    TaxableBase = new AmountType
                    {
                        TotalAmount                = g.Sum(gtax => gtax.TaxableBase.TotalAmount).Round(),
                        EquivalentInEuros          = g.Sum(gtax => gtax.TaxableBase.EquivalentInEuros).Round(),
                        EquivalentInEurosSpecified = true
                    },
                    TaxAmount   = new AmountType
                    {
                        TotalAmount                = g.Sum(gtax => gtax.TaxAmount.TotalAmount).Round(),
                        EquivalentInEuros          = g.Sum(gtax => gtax.TaxAmount.EquivalentInEuros).Round(),
                        EquivalentInEurosSpecified = true
                    },
                    EquivalenceSurcharge          = g.Key.EquivalenceSurcharge,
                    EquivalenceSurchargeSpecified = g.Any(gtax => gtax.EquivalenceSurchargeSpecified),
                    EquivalenceSurchargeAmount    = new AmountType
                    {
                        TotalAmount                = g.Where(gtax => gtax.EquivalenceSurchargeSpecified)
                                                        .Sum(gtax => gtax.EquivalenceSurchargeAmount.TotalAmount).Round(),
                        EquivalentInEuros          = g.Where(gtax => gtax.EquivalenceSurchargeSpecified)
                                                        .Sum(gtax => gtax.EquivalenceSurchargeAmount.EquivalentInEuros).Round(),
                        EquivalentInEurosSpecified = true
                    },
                    TaxTypeCode = g.Key.TaxTypeCode
                };

        TaxesOutputs = q.OrderBy(x => x.TaxTypeCode).ThenBy(x => x.TaxRate).ThenBy(x => x.EquivalenceSurcharge).ToList();

        // Taxes Withheld
        var w = from tax in Items.SelectMany(x => x.TaxesWithheld)
                group tax by new { tax.TaxTypeCode, tax.TaxRate } into g
                select new TaxType
                {
                    TaxRate     = g.Key.TaxRate,
                    TaxableBase = new AmountType
                    {
                        TotalAmount       = g.Sum(gtax => gtax.TaxableBase.TotalAmount).Round(),
                        EquivalentInEuros = g.Sum(gtax => gtax.TaxableBase.EquivalentInEuros).Round()
                    },
                    TaxAmount   = new AmountType
                    {
                        TotalAmount       = g.Sum(gtax => gtax.TaxAmount.TotalAmount).Round(),
                        EquivalentInEuros = g.Sum(gtax => gtax.TaxAmount.EquivalentInEuros).Round()
                    },
                    TaxTypeCode = g.Key.TaxTypeCode
                };

        TaxesWithheld = w.OrderBy(x => x.TaxTypeCode).ThenBy(x => x.TaxRate).ToList();

        // Invoice totals
        InvoiceTotals = new InvoiceTotalsType();

        // Calculate totals
        InvoiceTotals.TotalGrossAmount = Items.Sum(it => it.GrossAmount).Round();

        CalculateGeneralDiscountTotals();

        CalculateGeneralSurchargesTotals();

        InvoiceTotals.TotalGrossAmountBeforeTaxes = (InvoiceTotals.TotalGrossAmount
                                                    - InvoiceTotals.TotalGeneralDiscounts
                                                    + InvoiceTotals.TotalGeneralSurcharges).Round();

        CalculatePaymentsOnAccountTotals();

        CalculateReimbursableExpensesTotals();

        // Total impuestos retenidos
        InvoiceTotals.TotalTaxesWithheld = CalculateTaxWithheldTotal();

        // Sum of different fields Tax Amounts + Total Equivalence
        // Surcharges. Always to two decimal points.
        InvoiceTotals.TotalTaxOutputs = CalculateTaxOutputTotal();

        // Result: TotalGrossAmountBeforeTaxes + TotalTaxOutputs - TotalTaxesWithheld. Up to eight decimal points.
        InvoiceTotals.InvoiceTotal = ((InvoiceTotals.TotalGrossAmountBeforeTaxes + InvoiceTotals.TotalTaxOutputs)
                                    - InvoiceTotals.TotalTaxesWithheld).Round();

        // Total de gastos financieros
        InvoiceTotals.TotalFinancialExpenses = 0;

        // Amounts withheld by the payer  subject to the normal fulfilment of the transaction.
        var amountsWithheld = InvoiceTotals.AmountsWithheld?.WithholdingAmount ?? 0;

        if (InvoiceTotals.Subsidies is not null)
        {
            CalculateSubsidyAmounts();

            subsidyAmount = InvoiceTotals.Subsidies.Sum(s => s.SubsidyAmount).Round();
        }

        // Result: InvoiceTotal - (SubsidyAmount + TotalPaymentsOnAccount).
        InvoiceTotals.TotalOutstandingAmount = (InvoiceTotals.InvoiceTotal  - (subsidyAmount + InvoiceTotals.TotalPaymentsOnAccount)).Round();

        // Result: TotalOutstandingAmount - WithholdingAmount - PaymentInKindAmount + Reimbursable expenses + Financial expenses.
        InvoiceTotals.TotalExecutableAmount  = (InvoiceTotals.TotalOutstandingAmount
                                              - amountsWithheld
                                              + InvoiceTotals.TotalReimbursableExpenses
                                              + InvoiceTotals.TotalFinancialExpenses).Round();

        return Parent;
    }

    private void CalculateSubsidyAmounts()
    {
        // Rate applied to the Invoice Total.
        InvoiceTotals.Subsidies.ForEach(s => s.SubsidyAmount = (InvoiceTotals.InvoiceTotal * s.SubsidyRate / 100).Round());
    }

    private void CalculateReimbursableExpensesTotals()
    {
        // Total de suplidos
        if (InvoiceTotals.ReimbursableExpenses is not null)
        {
            InvoiceTotals.TotalReimbursableExpenses =
                InvoiceTotals.ReimbursableExpenses.Sum(re => re.ReimbursableExpensesAmount).Round();
        }
    }

    private void CalculatePaymentsOnAccountTotals()
    {
        if (InvoiceTotals.PaymentsOnAccount is not null)
        {
            InvoiceTotals.TotalPaymentsOnAccount =
                InvoiceTotals.PaymentsOnAccount.Sum(poa => poa.PaymentOnAccountAmount).Round();
        }
    }

    private void CalculateGeneralSurchargesTotals()
    {
        if (InvoiceTotals.GeneralSurcharges is not null)
        {
            InvoiceTotals.GeneralSurcharges.ForEach
            (
                gs => gs.ChargeAmount = ((InvoiceTotals.TotalGrossAmount * gs.ChargeRate) / 100).Round()
            );

            InvoiceTotals.TotalGeneralSurcharges = InvoiceTotals.GeneralSurcharges.Sum(gs => gs.ChargeAmount).Round();
        }
    }

    private void CalculateGeneralDiscountTotals()
    {
        if (InvoiceTotals.GeneralDiscounts is not null)
        {
            InvoiceTotals.GeneralDiscounts.ForEach
            (
                gd => gd.DiscountAmount = ((InvoiceTotals.TotalGrossAmount * gd.DiscountRate) / 100).Round()
            );

            InvoiceTotals.TotalGeneralDiscounts = InvoiceTotals.GeneralDiscounts.Sum(gd => gd.DiscountAmount).Round();
        }
    }

    private DoubleUpToEightDecimalType CalculateTaxOutputTotal()
    {
        return TaxesOutputs?.Sum(to => to.TaxAmount.TotalAmount + (to.EquivalenceSurchargeAmount?.TotalAmount ?? 0.0)).Round() ?? 0;
    }

    private DoubleUpToEightDecimalType CalculateTaxWithheldTotal()
    {
        return TaxesWithheld?.Sum(to => to.TaxAmount.TotalAmount).Round() ?? 0;
    }
}

public partial class InvoiceLineType
{
    public InvoiceLineType GiveQuantity(double quantity)
    {
        Quantity = quantity;

        return this;
    }

    public InvoiceLineType GiveUnitPriceWithoutTax(DoubleUpToEightDecimalType price)
    {
        UnitPriceWithoutTax = price;

        return this;
    }

    public InvoiceLineType GiveDiscount(DoubleUpToEightDecimalType discountRate, string discountReason = "Descuento")
    {
        if (DiscountsAndRebates is null)
        {
            DiscountsAndRebates = new List<DiscountType>();
        }

        var discount = new DiscountType
        {
            DiscountRate          = discountRate,
            DiscountRateSpecified = true,
            DiscountReason        = discountReason
        };

        DiscountsAndRebates.Add(discount);

        return this;
    }

    public InvoiceLineType GiveVATRate(DoubleUpToEightDecimalType taxRate, DoubleTwoDecimalType? equivalenceSurcharge = null)
    {
        AddTaxOutput(taxRate, equivalenceSurcharge, TaxTypeCodeType.ValueAddedTax);

        return this;
    }

    public InvoiceLineType GiveTaxRate(DoubleUpToEightDecimalType taxRate, TaxTypeCodeType taxType)
    {
        if (taxType.IsTaxWithheld())
        {
            AddTaxWithheld(taxRate, taxType);
        }
        else
        {
            AddTaxOutput(taxRate, null, taxType);
        }

        return this;
    }

    private void AddTaxOutput(DoubleUpToEightDecimalType taxRate, DoubleTwoDecimalType? equivalenceSurcharge, TaxTypeCodeType taxType)
    {
        if (TaxesOutputs is null)
        {
            TaxesOutputs = new List<InvoiceLineTypeTax>(1);
        }

        var tax = new InvoiceLineTypeTax
        {
            TaxTypeCode                   = taxType,
            TaxRate                       = taxRate,
            EquivalenceSurcharge          = (equivalenceSurcharge.HasValue) ? equivalenceSurcharge.Value.Value : 0.0,
            EquivalenceSurchargeSpecified = equivalenceSurcharge.HasValue
        };

        TaxesOutputs.Add(tax);
    }

    private void AddTaxWithheld(DoubleUpToEightDecimalType taxRate, TaxTypeCodeType taxType)
    {
        if (TaxesWithheld is null)
        {
            TaxesWithheld = new List<TaxType>(1);
        }

        var tax = new TaxType
        {
            TaxTypeCode = taxType,
            TaxRate     = taxRate
        };

        TaxesWithheld.Add(tax);
    }

    public InvoiceType CalculateTotals()
    {
        DoubleUpToEightDecimalType totalDiscounts = 0;
        DoubleUpToEightDecimalType totalCharges   = 0;

        TotalCost = (Quantity * UnitPriceWithoutTax).Round();

        if (DiscountsAndRebates is not null)
        {
            foreach (var dar in DiscountsAndRebates)
            {
                dar.DiscountAmount = (TotalCost * dar.DiscountRate / 100).Round();
                totalDiscounts     = (totalDiscounts + dar.DiscountAmount).Round();
            }
        }

        if (Charges is not null)
        {
            foreach (var chr in Charges)
            {
                chr.ChargeAmount = (TotalCost * chr.ChargeRate / 100).Round();
                totalCharges     = (totalCharges + chr.ChargeAmount).Round();
            }
        }

        // Result: TotalCost - DiscountAmount + ChargeAmount. Up to eight decimal points
        GrossAmount = TotalCost - totalDiscounts + totalCharges;

        TaxesOutputs.ForEach
        (
            tax =>
            {
                if (tax.TaxableBase is null)
                {
                    tax.TaxableBase = new AmountType();
                }

                tax.TaxableBase.TotalAmount                = GrossAmount;
                tax.TaxableBase.EquivalentInEuros          = GrossAmount;
                tax.TaxableBase.EquivalentInEurosSpecified = true;

                if (tax.TaxAmount is null)
                {
                    tax.TaxAmount = new AmountType();
                }

                tax.TaxAmount.TotalAmount       = (tax.TaxableBase.TotalAmount * tax.TaxRate / 100).Round();
                tax.TaxAmount.EquivalentInEuros = tax.TaxAmount.TotalAmount;

                if (tax.EquivalenceSurchargeSpecified)
                {
                    if (tax.EquivalenceSurchargeAmount is null)
                    {
                        tax.EquivalenceSurchargeAmount = new AmountType();
                    }

                    tax.EquivalenceSurchargeAmount.TotalAmount       = (tax.TaxableBase.TotalAmount * tax.EquivalenceSurcharge / 100).Round();
                    tax.EquivalenceSurchargeAmount.EquivalentInEuros = tax.EquivalenceSurchargeAmount.TotalAmount;
                    tax.EquivalenceSurchargeAmount.EquivalentInEurosSpecified = true;
                }
            }
        );

        var discounts = DiscountsAndRebates?.Sum(x => x.DiscountAmount).Round() ?? 0;

        TaxesWithheld.ForEach
        (
            tax =>
            {
                if (tax.TaxableBase is null)
                {
                    tax.TaxableBase = new AmountType();
                }

                tax.TaxableBase.TotalAmount                = GrossAmount;
                tax.TaxableBase.EquivalentInEuros          = GrossAmount;
                tax.TaxableBase.EquivalentInEurosSpecified = true;

                if (tax.TaxAmount is null)
                {
                    tax.TaxAmount = new AmountType();
                }

                tax.TaxAmount.TotalAmount                = (tax.TaxableBase.TotalAmount * tax.TaxRate / 100).Round();
                tax.TaxAmount.EquivalentInEuros          = tax.TaxAmount.TotalAmount;
                tax.TaxAmount.EquivalentInEurosSpecified = true;
            }
        );

        return Parent;
    }
}

public partial class BusinessType
{
    private Facturae _parent;

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
        _parent           = parent;
        TaxIdentification = new TaxIdentificationType();
    }

    public Facturae Invoice()
    {
        return _parent;
    }

    /// <summary>
    /// Sets the identification of a invoice business party.
    /// </summary>
    /// <param name="identification">The identification.</param>
    /// <returns></returns>
    public BusinessType SetIdentification(string identification)
    {
        PartyIdentification = identification;

        return this;
    }

    /// <summary>
    /// Sets an invoice business party as an individual
    /// </summary>
    /// <returns></returns>
    public IndividualType AsIndividual()
    {
        var individual = new IndividualType { Parent = this };

        Item = individual;

        individual.Item = TaxIdentification.ResidenceTypeCode == ResidenceTypeCodeType.ResidentInSpain
            ? new AddressType { CountryCode = CountryType.ESP }
                : new OverseasAddressType();

        TaxIdentification.PersonTypeCode = PersonTypeCodeType.Individual;

        return individual;
    }

    /// <summary>
    /// Sets the identification number.
    /// </summary>
    /// <param name="identificationNumber">The identification number.</param>
    /// <returns></returns>
    public BusinessType SetIdentificationNumber(string identificationNumber)
    {
        TaxIdentification.TaxIdentificationNumber = identificationNumber;

        return this;
    }

    /// <summary>
    /// Sets an invoice business party as a legal entity.
    /// </summary>
    /// <returns></returns>
    public LegalEntityType AsLegalEntity()
    {
        var legalEntity = new LegalEntityType(this);

        Item = legalEntity;

        if (TaxIdentification is not null)
        {
            TaxIdentification.PersonTypeCode = PersonTypeCodeType.LegalEntity;

            legalEntity.Item = TaxIdentification.ResidenceTypeCode == ResidenceTypeCodeType.ResidentInSpain
                ? new AddressType { CountryCode = CountryType.ESP }
                    : new OverseasAddressType();
        }

        return legalEntity;
    }

    /// <summary>
    /// Sets the tax identification as a foreigner entity identification.
    /// </summary>
    /// <returns></returns>
    public BusinessType AsForeigner()
    {
        TaxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.Foreigner;

        return this;
    }

    /// <summary>
    /// Sets the tax identification as an spain tax identification.
    /// </summary>
    /// <returns></returns>
    public BusinessType AsResidentInSpain()
    {
        TaxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.ResidentInSpain;

        return this;
    }

    /// <summary>
    /// Sets the tax identification as an EU tax identification.
    /// </summary>
    /// <returns></returns>
    public BusinessType AsResidentInEU()
    {
        TaxIdentification.ResidenceTypeCode = ResidenceTypeCodeType.ResidentInEU;

        return this;
    }

    /// <summary>
    /// Adds an administrative centre.
    /// </summary>
    /// <returns>The new administrative centre</returns>
    public AdministrativeCentreType AddAdministrativeCentre()
    {
        var centre = new AdministrativeCentreType(this);

        if (AdministrativeCentres is null)
        {
            AdministrativeCentres = new List<AdministrativeCentreType>();
        }

        AdministrativeCentres.Add(centre);

        if (TaxIdentification is not null)
        {
            centre.Item = TaxIdentification.ResidenceTypeCode == ResidenceTypeCodeType.ResidentInSpain
                ? new AddressType { CountryCode = CountryType.ESP }
                    : new OverseasAddressType();
        }

        return centre;
    }
}

public partial class IndividualType
{
    /// <summary>
    /// Gets the individual parent party.
    /// </summary>
    /// <returns></returns>
    public BusinessType Party()
    {
        return Parent;
    }

    /// <summary>
    /// Sets an individual name.
    /// </summary>
    /// <param name="name">The individual name.</param>
    /// <returns></returns>
    public IndividualType SetName(string name)
    {
        Name = name;

        return this;
    }

    /// <summary>
    /// Sets an individual first surname.
    /// </summary>
    /// <param name="firstSurname">The individual first surname.</param>
    /// <returns></returns>
    public IndividualType SetFirstSurname(string firstSurname)
    {
        FirstSurname = firstSurname;

        return this;
    }

    /// <summary>
    /// Sets an individual second surname.
    /// </summary>
    /// <param name="secondSurname">The individual second surname.</param>
    /// <returns></returns>
    public IndividualType SetSecondSurname(string secondSurname)
    {
        SecondSurname = secondSurname;

        return this;
    }

    /// <summary>
    /// Sets an individual party address.
    /// </summary>
    /// <param name="address">The address.</param>
    /// <returns></returns>
    public IndividualType SetAddress(string address)
    {
        if (Item is AddressType)
        {
            (Item as AddressType).Address = address;
        }
        else
        {
            (Item as OverseasAddressType).Address = address;
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
        if (Item is AddressType)
        {
            (Item as AddressType).PostCode = postCode;
        }
        else
        {
            (Item as OverseasAddressType).PostCodeAndTown = postCode;
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
        if (Item is AddressType)
        {
            (Item as AddressType).CountryCode = countryCode;
        }
        else
        {
            (Item as OverseasAddressType).CountryCode = countryCode;
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
        if (Item is AddressType)
        {
            (Item as AddressType).Town = town;
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
        if (Item is OverseasAddressType)
        {
            (Item as OverseasAddressType).PostCodeAndTown = postCodeAndTown;
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
        if (Item is AddressType)
        {
            (Item as AddressType).Province = province;
        }
        else
        {
            (Item as OverseasAddressType).Province = province;
        }

        return this;
    }
}

public partial class AdministrativeCentreType
{
    private BusinessType _parent;

    public AdministrativeCentreType()
    {
    }

    public AdministrativeCentreType(BusinessType parent)
    {
        _parent = parent;
    }

    public BusinessType Party()
    {
        return _parent;
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

        RoleTypeCode          = role;
        RoleTypeCodeSpecified = true;

        return this;
    }

    public AdministrativeCentreType SetCentreCode(string centreCode)
    {
        CentreCode = centreCode;

        return this;
    }

    public AdministrativeCentreType SetCentreDescription(string centreDescription)
    {
        CentreDescription = centreDescription;

        return this;
    }

    public AdministrativeCentreType SetFirstSurname(string firstSurname)
    {
        FirstSurname = firstSurname;

        return this;
    }

    public AdministrativeCentreType SetLogicalOperationalPoint(string logicalOperationalPoint)
    {
        LogicalOperationalPoint = logicalOperationalPoint;

        return this;
    }

    public AdministrativeCentreType SetName(string name)
    {
        Name = name;

        return this;
    }

    public AdministrativeCentreType SetPhysicalGLN(string physicalGLN)
    {
        PhysicalGLN = physicalGLN;

        return this;
    }

    public AdministrativeCentreType SetSecondSurname(string secondSurname)
    {
        SecondSurname = secondSurname;

        return this;
    }

    public AdministrativeCentreType SetAddress(string address)
    {
        if (Item is AddressType)
        {
            (Item as AddressType).Address = address;
        }
        else
        {
            (Item as OverseasAddressType).Address = address;
        }

        return this;
    }

    public AdministrativeCentreType SetPostCode(string postCode)
    {
        if (Item is AddressType)
        {
            (Item as AddressType).PostCode = postCode;
        }
        else
        {
            (Item as OverseasAddressType).PostCodeAndTown = postCode;
        }

        return this;
    }

    public AdministrativeCentreType SetCountryCode(CountryType countryCode)
    {
        if (Item is AddressType)
        {
            (Item as AddressType).CountryCode = countryCode;
        }
        else
        {
            (Item as OverseasAddressType).CountryCode = countryCode;
        }

        return this;
    }

    public AdministrativeCentreType SetTown(string town)
    {
        if (Item is AddressType)
        {
            (Item as AddressType).Town = town;
        }

        return this;
    }

    public AdministrativeCentreType SetPostCodeAndTown(string postCodeAndTown)
    {
        if (Item is OverseasAddressType)
        {
            (Item as OverseasAddressType).PostCodeAndTown = postCodeAndTown;
        }

        return this;
    }

    public AdministrativeCentreType SetProvince(string province)
    {
        if (Item is AddressType)
        {
            (Item as AddressType).Province = province;
        }
        else
        {
            ((OverseasAddressType)Item).Province = province;
        }

        return this;
    }
}

public partial class LegalEntityType
{
    private BusinessType _parent;

    public LegalEntityType()
    {
    }

    public LegalEntityType(BusinessType parent)
    {
        _parent = parent;
    }

    public BusinessType Party()
    {
        return _parent;
    }

    public LegalEntityType SetCorporateName(string corporateName)
    {
        CorporateName = corporateName;

        return this;
    }

    public LegalEntityType SetTradeName(string tradeName)
    {
        TradeName = tradeName;

        return this;
    }

    public LegalEntityType SetAddress(string address)
    {
        if (Item is AddressType)
        {
            (Item as AddressType).Address = address;
        }
        else
        {
            (Item as OverseasAddressType).Address = address;
        }

        return this;
    }

    public LegalEntityType SetPostCode(string postCode)
    {
        if (Item is AddressType)
        {
            (Item as AddressType).PostCode = postCode;
        }
        else
        {
            (Item as OverseasAddressType).PostCodeAndTown = postCode;
        }

        return this;
    }

    public LegalEntityType SetCountryCode(CountryType countryCode)
    {
        if (Item is AddressType)
        {
            (Item as AddressType).CountryCode = countryCode;
        }
        else
        {
            (Item as OverseasAddressType).CountryCode = countryCode;
        }

        return this;
    }

    public LegalEntityType SetTown(string postCode)
    {
        if (Item is AddressType)
        {
            (Item as AddressType).Town = postCode;
        }

        return this;
    }

    public LegalEntityType SetPostCodeAndTown(string postCodeAndTown)
    {
        if (Item is OverseasAddressType)
        {
            (Item as OverseasAddressType).PostCodeAndTown = postCodeAndTown;
        }

        return this;
    }

    public LegalEntityType SetProvince(string province)
    {
        if (Item is AddressType)
        {
            (Item as AddressType).Province = province;
        }
        else
        {
            (Item as OverseasAddressType).Province = province;
        }

        return this;
    }
}
