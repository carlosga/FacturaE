using System.Security.Cryptography.X509Certificates;

using FluentAssertions;

namespace FacturaE.Tests;

public sealed class FacturaeTests
{
    [Fact]
    public void SignInvoiceTest()
    {
        var certificate = new X509Certificate2(@"Certificates/facturae.p12", "1234");

        // Create a new facturae invoice & sign it
        var instance = new Facturae()
            .Seller()
                .SetIdentification("00001")
                .AsResidentInSpain()
                .SetIdentificationNumber("35799562Q")
                .AsIndividual()
                    .SetName("JOHN")
                    .SetFirstSurname("DOE")
                    .SetAddress("8585 FIRST STREET")
                    .SetProvince("MADRID")
                    .SetTown("MADRID")
                    .SetPostCode("99900")
                    .SetCountryCode(CountryType.ESP)
                    .Party()
                .Invoice()
            .Buyer()
                .SetIdentification("00002")
                .AsResidentInSpain()
                .SetIdentificationNumber("06990097Y")
                .AsLegalEntity()
                    .SetCorporateName("JOHN")
                    .SetAddress("8585 FIRST STREET")
                    .SetProvince("MADRID")
                    .SetTown("MADRID")
                    .SetPostCode("99900")
                    .SetCountryCode(CountryType.ESP)
                .Party()
                .AddAdministrativeCentre()
                    .SetCentreCode("1")
                    .SetRoleCodeType("02")
                    .SetLogicalOperationalPoint("1233")
                    .SetName("ADMINISTRATION NAME")
                    .SetAddress("1234 Street")
                    .SetProvince("MADRID")
                    .SetTown("MADRID")
                    .SetPostCode("99900")
                    .SetCountryCode(CountryType.ESP)
                .Party()
            .Invoice()
            .CreateInvoice()
                .SetCurrency(CurrencyCodeType.EUR)                
                .SetExchangeRate(1, DateTime.Today)
                .SetTaxCurrency(CurrencyCodeType.EUR)
                .SetLanguage(LanguageCodeType.es)
                .SetPlaceOfIssue(string.Empty, "00000")
                .IsOriginal()
                .IsComplete()
                .SetInvoiceSeries("IN")
                .SetInvoiceNumber("1000")
                .AddInvoiceItem("XX", "XX")
                    .GiveQuantity(1.0M)
                    .GiveUnitPriceWithoutTax(100.01M)
                    .GiveDiscount(10.01M, "Line Discount")
                    .GiveValueAddedTaxRate(21.0M, 5.20M)
                    .GiveTaxRate(9.0M, TaxTypeCodeType.PersonalIncomeTax)
                    .CalculateTotals()
                .AddInvoiceItem("XXX", "XXX")
                    .GiveQuantity(1)
                    .GiveUnitPriceWithoutTax(100.01M)
                    .GiveDiscount(10.01M)
                    .GiveValueAddedTaxRate(21.0M, 5.20M)
                    .GiveTaxRate(9.0M, TaxTypeCodeType.PersonalIncomeTax)
                    .CalculateTotals()
                .CalculateTotals()
            .CalculateTotals();

        var exception1 = Record.Exception(() => instance.Validate());

        Assert.Null(exception1);

        var verifier   = instance.Sign(certificate);
        var exception2 = Record.Exception(() => verifier.CheckSignature());

        Assert.Null(exception2);
    }

    [Fact]
    public void General_Regimen_Taxes_1_Test()
    {
        var instance = new Facturae()
            .SetIssuer(InvoiceIssuerTypeType.Seller)
            .Seller()
                .SetIdentification("00001")
                .AsResidentInSpain()
                .SetIdentificationNumber("A2800056F")
                .AsLegalEntity()
                    .SetCorporateName("Public Limited Company")
                    .SetAddress("Street Alcalá 137")
                    .SetProvince("MADRID")
                    .SetTown("MADRID")
                    .SetPostCode("28001")
                    .SetCountryCode(CountryType.ESP)
                    .Party()
                .Invoice()
            .Buyer()
                .SetIdentification("00002")
                .AsResidentInSpain()
                .SetIdentificationNumber("A4155543L")
                .AsLegalEntity()
                    .SetCorporateName("Prima S.A.")
                    .SetAddress("Street San Vicente 1")
                    .SetProvince("Seville")
                    .SetTown("Seville")
                    .SetPostCode("41008")
                    .SetCountryCode(CountryType.ESP)
                .Party()
                .AddAdministrativeCentre()
                    .SetCentreCode("1")
                    .SetRoleCodeType("02")
                    .SetLogicalOperationalPoint("1233")
                    .SetName("ADMINISTRATION NAME")
                    .SetAddress("1234 Street")
                    .SetProvince("MADRID")
                    .SetTown("MADRID")
                    .SetPostCode("99900")
                    .SetCountryCode(CountryType.ESP)
                .Party()
            .Invoice();

        var invoiceType = instance         
            .CreateInvoice()
                .SetInvoiceNumber("BX-375-09")
                .SetIssueDate(new DateTime(2009, 03, 02))
                .SetCurrency(CurrencyCodeType.EUR)                
                .SetTaxCurrency(CurrencyCodeType.EUR)
                .SetLanguage(LanguageCodeType.es)
                .SetExchangeRate(1, DateTime.Today)
                .SetPlaceOfIssue(string.Empty, "00000")
                .IsOriginal()
                .IsComplete();

        var firstItem = invoiceType
            .AddInvoiceItem()
                .SetIssuerContractReference("C070801")
                .SetIssuerTransactionReference("C0107")
                .SetDescription("Notepads")
                .GiveQuantity(500.0M)
                .GiveUnitOfMeasure(UnitOfMeasureType.Units)
                .GiveUnitPriceWithoutTax(9.156M)
                .GiveValueAddedTaxRate(16.0M);

        firstItem.CalculateTotals();

        firstItem.TaxesOutputs.Count.Should().Be(1);
        firstItem.TotalCost.Should().Be(4578.0M);
        firstItem.GrossAmount.Should().Be(4578.0M);
        
        var fto = firstItem.TaxesOutputs[0];

        fto.Should().NotBeNull();

        fto.TaxTypeCode.Should().Be(TaxTypeCodeType.ValueAddedTax);
        fto.TaxRate.Should().Be(16.0M);
        fto.TaxableBase.Should().NotBeNull();
        fto.TaxableBase.TotalAmount.Should().Be(4578.0M);
        fto.TaxAmount.TotalAmount.Should().Be(732.48M);

        var secondItem = invoiceType
            .AddInvoiceItem()
                .SetIssuerContractReference("C070801")
                .SetIssuerTransactionReference("C0107")
                .SetDescription("Books")
                .GiveQuantity(60.0M)
                .GiveUnitOfMeasure(UnitOfMeasureType.Units)
                .GiveUnitPriceWithoutTax(35M)
                .GiveValueAddedTaxRate(7.0M);

        secondItem.CalculateTotals();

        secondItem.TaxesOutputs.Count.Should().Be(1);
        secondItem.TotalCost.Should().Be(2100.0M);
        secondItem.GrossAmount.Should().Be(2100.0M);
        
        var sto = secondItem.TaxesOutputs[0];

        sto.Should().NotBeNull();

        sto.TaxTypeCode.Should().Be(TaxTypeCodeType.ValueAddedTax);
        sto.TaxRate.Should().Be(7.0M);
        sto.TaxableBase.Should().NotBeNull();
        sto.TaxableBase.TotalAmount.Should().Be(2100.0M);
        sto.TaxAmount.TotalAmount.Should().Be(147.0M);

        invoiceType.CalculateTotals();

        invoiceType.InvoiceTotals.Should().NotBeNull();
        invoiceType.InvoiceTotals.TotalGrossAmount.Should().Be(6678.0M);
        invoiceType.InvoiceTotals.TotalGrossAmountBeforeTaxes.Should().Be(6678.0M);
        invoiceType.InvoiceTotals.TotalTaxOutputs.Should().Be(879.48M);
        invoiceType.InvoiceTotals.TotalTaxesWithheld.Should().Be(0.0M);
        invoiceType.InvoiceTotals.InvoiceTotal.Should().Be(7557.48M);
        invoiceType.InvoiceTotals.TotalOutstandingAmount.Should().Be(7557.48M);
        invoiceType.InvoiceTotals.TotalExecutableAmount.Should().Be(7557.48M);

        instance.CalculateTotals();

        instance.FileHeader.Batch.Should().NotBeNull();
        instance.FileHeader.Batch.BatchIdentifier.Should().Be("A2800056FBX-375-09");
        instance.FileHeader.Batch.InvoicesCount.Should().Be(1);
        instance.FileHeader.Batch.TotalInvoicesAmount.TotalAmount.Should().Be(7557.48M);
        instance.FileHeader.Batch.TotalOutstandingAmount.TotalAmount.Should().Be(7557.48M);
        instance.FileHeader.Batch.TotalExecutableAmount.TotalAmount.Should().Be(7557.48M);
        instance.FileHeader.Batch.InvoiceCurrencyCode.Should().Be(CurrencyCodeType.EUR);
    }

    [Fact]
    public void General_Regimen_Taxes_2_Test()
    {
        var instance = new Facturae()
            .SetIssuer(InvoiceIssuerTypeType.Seller)
            .Seller()
                .SetIdentification("00001")
                .AsResidentInSpain()
                .SetIdentificationNumber("99999999R")
                .AsLegalEntity()
                    .SetCorporateName("John")
                    .SetTradeName("Spanish")
                    .SetAddress("Street Ibiza number 150")
                    .SetProvince("MADRID")
                    .SetTown("MADRID")
                    .SetPostCode("28345")
                    .SetCountryCode(CountryType.ESP)
                    .Party()
                .Invoice()
            .Buyer()
                .SetIdentification("00002")
                .AsResidentInSpain()
                .SetIdentificationNumber("Q2826000H")
                .AsLegalEntity()
                    .SetCorporateName("AEAT")
                    .SetAddress("Street Santa María Magdalena number 16")
                    .SetProvince("Madrid")
                    .SetTown("Madrid")
                    .SetPostCode("28016")
                    .SetCountryCode(CountryType.ESP)
                .Party()
                .AddAdministrativeCentre()
                    .SetCentreCode("1")
                    .SetRoleCodeType("02")
                    .SetLogicalOperationalPoint("1233")
                    .SetName("ADMINISTRATION NAME")
                    .SetAddress("1234 Street")
                    .SetProvince("MADRID")
                    .SetTown("MADRID")
                    .SetPostCode("99900")
                    .SetCountryCode(CountryType.ESP)
                .Party()
            .Invoice();

        var invoiceType = instance         
            .CreateInvoice()
                .SetInvoiceSeries("06001")
                .SetInvoiceNumber("001")
                .SetIssueDate(new DateTime(2006, 12, 12))
                .SetCurrency(CurrencyCodeType.EUR)                
                .SetTaxCurrency(CurrencyCodeType.EUR)
                .SetLanguage(LanguageCodeType.es)
                .SetExchangeRate(1, DateTime.Today)
                .IsOriginal()
                .IsComplete();

        var item1 = invoiceType
            .AddInvoiceItem()
                .SetTransactionDate(new DateTime(2006, 12, 12))
                .SetDescription("Organic Analysis")
                .GiveQuantity(1.0M)
                .GiveUnitOfMeasure(UnitOfMeasureType.Units)
                .GiveUnitPriceWithoutTax(40.10M)
                .GiveValueAddedTaxRate(16.0M);

        item1.CalculateTotals();

        item1.TaxesOutputs.Count.Should().Be(1);
        item1.TotalCost.Should().Be(40.10M);
        item1.GrossAmount.Should().Be(40.10M);
        
        var to1 = item1.TaxesOutputs[0];

        to1.Should().NotBeNull();

        to1.TaxTypeCode.Should().Be(TaxTypeCodeType.ValueAddedTax);
        to1.TaxRate.Should().Be(16.0M);
        to1.TaxableBase.Should().NotBeNull();
        to1.TaxableBase.TotalAmount.Should().Be(40.10M);
        to1.TaxAmount.TotalAmount.Should().Be(6.42M);

        var item2 = invoiceType
            .AddInvoiceItem()
                .SetTransactionDate(new DateTime(2006, 12, 12))
                .SetDescription("Functional Analysis")
                .GiveQuantity(1.0M)
                .GiveUnitOfMeasure(UnitOfMeasureType.Units)
                .GiveUnitPriceWithoutTax(30.0M)
                .GiveValueAddedTaxRate(16.0M);

        item2.CalculateTotals();

        item2.TaxesOutputs.Count.Should().Be(1);
        item2.TotalCost.Should().Be(30.0M);
        item2.GrossAmount.Should().Be(30.0M);
        
        var to2 = item2.TaxesOutputs[0];

        to2.Should().NotBeNull();

        to2.TaxTypeCode.Should().Be(TaxTypeCodeType.ValueAddedTax);
        to2.TaxRate.Should().Be(16.0M);
        to2.TaxableBase.Should().NotBeNull();
        to2.TaxableBase.TotalAmount.Should().Be(30.0M);
        to2.TaxAmount.TotalAmount.Should().Be(4.80M);

        var item3 = invoiceType
            .AddInvoiceItem()
                .SetTransactionDate(new DateTime(2006, 12, 12))
                .SetDescription("Junior Programmer")
                .GiveQuantity(3.0M)
                .GiveUnitOfMeasure(UnitOfMeasureType.Units)
                .GiveUnitPriceWithoutTax(6.0M)
                .GiveValueAddedTaxRate(16.0M);

        item3.CalculateTotals();

        item3.TaxesOutputs.Count.Should().Be(1);
        item3.TotalCost.Should().Be(18.0M);
        item3.GrossAmount.Should().Be(18.0M);
        
        var to3 = item3.TaxesOutputs[0];

        to3.Should().NotBeNull();

        to3.TaxTypeCode.Should().Be(TaxTypeCodeType.ValueAddedTax);
        to3.TaxRate.Should().Be(16.0M);
        to3.TaxableBase.Should().NotBeNull();
        to3.TaxableBase.TotalAmount.Should().Be(18.0M);
        to3.TaxAmount.TotalAmount.Should().Be(2.88M);

        var item4 = invoiceType
            .AddInvoiceItem()
                .SetTransactionDate(new DateTime(2006, 12, 12))
                .SetDescription("Senior Programmer")
                .GiveQuantity(3.0M)
                .GiveUnitOfMeasure(UnitOfMeasureType.Units)
                .GiveUnitPriceWithoutTax(21.6M)
                .GiveValueAddedTaxRate(16.0M);

        item4.CalculateTotals();

        item4.TaxesOutputs.Count.Should().Be(1);
        item4.TotalCost.Should().Be(64.80M);
        item4.GrossAmount.Should().Be(64.80M);
        
        var to4 = item4.TaxesOutputs[0];

        to4.Should().NotBeNull();

        to4.TaxTypeCode.Should().Be(TaxTypeCodeType.ValueAddedTax);
        to4.TaxRate.Should().Be(16.0M);
        to4.TaxableBase.Should().NotBeNull();
        to4.TaxableBase.TotalAmount.Should().Be(64.80M);
        to4.TaxAmount.TotalAmount.Should().Be(10.37M);

        var item5 = invoiceType
            .AddInvoiceItem()
                .SetTransactionDate(new DateTime(2006, 12, 12))
                .SetDescription("Junior Analyst")
                .GiveQuantity(1.0M)
                .GiveUnitOfMeasure(UnitOfMeasureType.Units)
                .GiveUnitPriceWithoutTax(24.0M)
                .GiveValueAddedTaxRate(16.0M);

        item5.CalculateTotals();

        item5.TaxesOutputs.Count.Should().Be(1);
        item5.TotalCost.Should().Be(24.0M);
        item5.GrossAmount.Should().Be(24.0M);
        
        var to5 = item5.TaxesOutputs[0];

        to5.Should().NotBeNull();

        to5.TaxTypeCode.Should().Be(TaxTypeCodeType.ValueAddedTax);
        to5.TaxRate.Should().Be(16.0M);
        to5.TaxableBase.Should().NotBeNull();
        to5.TaxableBase.TotalAmount.Should().Be(24.0M);
        to5.TaxAmount.TotalAmount.Should().Be(3.84M);

       var item6 = invoiceType
            .AddInvoiceItem()
                .SetTransactionDate(new DateTime(2006, 12, 12))
                .SetDescription("Senior Analyst")
                .GiveQuantity(1.0M)
                .GiveUnitOfMeasure(UnitOfMeasureType.Units)
                .GiveUnitPriceWithoutTax(27.0M)
                .GiveValueAddedTaxRate(16.0M);

        item6.CalculateTotals();

        item6.TaxesOutputs.Count.Should().Be(1);
        item6.TotalCost.Should().Be(27.0M);
        item6.GrossAmount.Should().Be(27.0M);
        
        var to6 = item6.TaxesOutputs[0];

        to6.Should().NotBeNull();

        to6.TaxTypeCode.Should().Be(TaxTypeCodeType.ValueAddedTax);
        to6.TaxRate.Should().Be(16.0M);
        to6.TaxableBase.Should().NotBeNull();
        to6.TaxableBase.TotalAmount.Should().Be(27.0M);
        to6.TaxAmount.TotalAmount.Should().Be(4.32M);

        invoiceType.AddGeneralDiscount("Discounts", 10.0M);
        
        invoiceType.CalculateTotals();

        invoiceType.InvoiceTotals.Should().NotBeNull();
        invoiceType.InvoiceTotals.TotalGrossAmount.Should().Be(203.9M);
        invoiceType.InvoiceTotals.TotalGeneralDiscounts.Should().Be(20.39M);
        invoiceType.InvoiceTotals.TotalGrossAmountBeforeTaxes.Should().Be(183.51M);
        invoiceType.InvoiceTotals.TotalTaxOutputs.Should().Be(29.36M);
        invoiceType.InvoiceTotals.TotalTaxesWithheld.Should().Be(0.0M);
        invoiceType.InvoiceTotals.InvoiceTotal.Should().Be(212.87M);
        invoiceType.InvoiceTotals.TotalOutstandingAmount.Should().Be(212.87M);
        invoiceType.InvoiceTotals.TotalExecutableAmount.Should().Be(212.87M);

        instance.CalculateTotals();

        instance.FileHeader.Batch.Should().NotBeNull();
        instance.FileHeader.Batch.BatchIdentifier.Should().Be("99999999R00106001");
        instance.FileHeader.Batch.InvoicesCount.Should().Be(1);
        instance.FileHeader.Batch.TotalInvoicesAmount.TotalAmount.Should().Be(212.87M);
        instance.FileHeader.Batch.TotalOutstandingAmount.TotalAmount.Should().Be(212.87M);
        instance.FileHeader.Batch.TotalExecutableAmount.TotalAmount.Should().Be(212.87M);
        instance.FileHeader.Batch.InvoiceCurrencyCode.Should().Be(CurrencyCodeType.EUR);
    }    
}