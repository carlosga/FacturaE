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
    public void GeneralRegimenTaxesTest()
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
        instance.CalculateTotals();
    }
}