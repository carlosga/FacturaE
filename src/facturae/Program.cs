// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FacturaE.XAdES;
using System;
using System.Security.Cryptography.X509Certificates;

namespace FacturaE
{
    public class Program
    {
        static void Main(string[] args)
        {
            var eInvoice = new Facturae();
            var cert     = new X509Certificate2(@"Certificates/ANCERTCCP_FIRMA.p12", "1111");

            // Create a new facturae invoice & sign it
            var isValid = eInvoice
                .Seller()
                    .SetIdentification("00001")
                    .AsResidentInSpain()
                    .SetIdentificationNumber("555888555")
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
                    .SetIdentificationNumber("555888555")
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
                    .SetExchangeRate(1, DateTime.Now)
                    .SetTaxCurrency(CurrencyCodeType.EUR)
                    .SetLanguage(LanguageCodeType.es)
                    .SetPlaceOfIssue(String.Empty, "00000")
                    .IsOriginal()
                    .IsComplete()
                    .SetInvoiceSeries("0")
                    .SetInvoiceNumber("1000")
                    .AddInvoiceItem("XX", "XX")
                        .GiveQuantity(1.0)
                        .GiveUnitPriceWithoutTax(100.01)
                        .GiveDiscount(10.01, "Line Discount")
                        .GiveVATRate(21.00)
                        .GiveTaxRate(9.00, TaxTypeCodeType.PersonalIncomeTax)
                        .CalculateTotals()
                    .AddInvoiceItem("XXX", "XXX")
                        .GiveQuantity(1)
                        .GiveUnitPriceWithoutTax(100.01)
                        .GiveDiscount(10.01)
                        .GiveVATRate(21.00)
                        .GiveTaxRate(9.00, TaxTypeCodeType.PersonalIncomeTax)
                        .CalculateTotals()
                    .CalculateTotals()
                .CalculateTotals()
                .Validate()
                .Sign(cert, ClaimedRole.Supplier)
                .WriteToFile(@"Sample.xsig")
                .CheckSignature();

            System.Console.WriteLine(isValid);
        }
    }
}

