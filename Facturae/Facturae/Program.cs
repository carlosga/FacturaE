using System;
using System.Security.Cryptography.X509Certificates;

namespace ElectronicInvoice
{
    public class Program
    {
        static void Main(string[] args)
        {
            Facturae            eInvoice	= new Facturae();
            X509Certificate2    cert        = new X509Certificate2(@"Certificates/usr0052.p12", "usr0052");
            SignedFacturae      signed      = null;
            bool                isValid     = false;

            signed = eInvoice.GiveCurrencyCode(CurrencyCodeType.EUR)
                    .GiveIssuerType(InvoiceIssuerTypeType.EM)
                    .Parties()
                        .Seller()
                            .GiveIdentification("00001")
                            .TaxIdentification()
                                .IsResidentInSpain()
                                .GiveIdentificationNumber("36142")
                                .Party()
                            .IsIndividual()
                                .GiveName("C")
                                .GiveFirstSurname("G")
                                .GiveSecondSurname("A")
                                .GiveAddress("Vigo")
                                .GiveProvince("Vigo")
                                .GiveTown("Vigo")
                                .GivePostCode("36207")
                                .GiveCountryCode(CountryType.ESP)
                                .Party()
                        .Parties()
                        .Buyer()
                            .GiveIdentification("00002")
                            .TaxIdentification()
                                .IsResidentInSpain()
                                .GiveIdentificationNumber("36142")
                                .Party()
                            .IsIndividual()
                                .GiveName("C")
                                .GiveFirstSurname("G")
                                .GiveSecondSurname("A")
                                .GiveAddress("Vigo")
                                .GiveTown("Vigo")
                                .GiveProvince("Vigo")
                                .GivePostCode("36207")
                                .GiveCountryCode(CountryType.ESP)
                                .Party()
                        .Parties()
                    .Facturae()
                    .Invoices()
                        .CreateInvoice()
                            .GiveCurrency(CurrencyCodeType.EUR)
                            .GiveExchangeRate(1, DateTime.Now)
                            .SetTaxCurrencyCode()
                            .GiveLanguage(LanguageCodeType.es)
                            .GivePlaceOfIssue(String.Empty, "00000")
                            .IsOriginal()
                            .IsComplete()
                            .SetInvoiceSeriesCode("0")
                            .SetInvoiceNumber("1000")
                            .Items()
                                .AddInvoiceItem("XX", "XX")
                                    .GiveQuantity(1)
                                    .GiveUnitPriceWithoutTax(100.01)
                                    .GiveDiscount(10.01)
                                    .GiveTax(18.00)
                                    .CalculateTotals()
                                .AddInvoiceItem("XXX", "XXX")
                                    .GiveQuantity(1)
                                    .GiveUnitPriceWithoutTax(100.01)
                                    .GiveDiscount(10.01)
                                    .GiveTax(18.00)
                                    .CalculateTotals()
                            .CalculateTotals()
                    .CalculateTotals()
                    .Sign(cert);


            isValid = signed.WriteToFile(@"Sample.xml")
                            .CheckSignature();

            System.Console.WriteLine(isValid);
            System.Console.ReadLine();
        }
    }
}

