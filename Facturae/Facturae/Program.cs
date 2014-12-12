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

using FacturaE.Extensions;
using System;
using System.Security.Cryptography.X509Certificates;

namespace FacturaE
{
    public class Program
    {
        static void Main(string[] args)
        {
            var eInvoice = new Facturae();
            var cert     = new X509Certificate2(@"Certificates/ANF_PF_Activo.pfx", "12341234");

            // Create a new facturae invoice & sign it
            var isValid = eInvoice
                .Parties()
                    .Seller()
                        .SetIdentification("00001")
                        .TaxIdentification()
                            .IsResidentInSpain()
                            .SetIdentificationNumber("555888555")
                            .Party()
                        .IsIndividual()
                            .SetName("JOHN")
                            .SetFirstSurname("DOE")
                            .SetAddress("8585 FIRST STREET")
                            .SetProvince("MADRID")
                            .SetTown("MADRID")
                            .SetPostCode("99900")
                            .SetCountryCode(CountryType.ESP)
                            .Party()
                        .Parties()
                    .Buyer()
                        .SetIdentification("00002")
                        .TaxIdentification()
                            .IsResidentInSpain()
                            .SetIdentificationNumber("555888555")
                            .Party()
                        .IsIndividual()
                            .SetName("JOHN")
                            .SetFirstSurname("DOE")
                            .SetAddress("8585 FIRST STREET")
                            .SetProvince("MADRID")
                            .SetTown("MADRID")
                            .SetPostCode("99900")
                            .SetCountryCode(CountryType.ESP)
                            .Party()
                    .Parties()
                .Facturae()
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
                .Sign(cert)
                .WriteToFile(@"Sample.xsig")
                .CheckSignature();

            System.Console.WriteLine(isValid);
            System.Console.ReadLine();
        }
    }
}

