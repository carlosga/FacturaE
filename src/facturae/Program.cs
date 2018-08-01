// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Running;
using FacturaE.XAdES;
using System;
using System.Security.Cryptography.X509Certificates;

namespace FacturaE
{
    [MemoryDiagnoser]
    [RankColumn]
    public class FacturaeBenchmark
    {
        static readonly X509Certificate2 s_Certificate = new X509Certificate2(@"Certificates/ANCERTCCP_FIRMA.p12", "1111");
        //static readonly string           s_Filename = "sample.xsig";

        [Benchmark]
        public void SignatureBenchmark()
        {
            var eInvoice = new Facturae();

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
                    .SetPlaceOfIssue(string.Empty, "00000")
                    .IsOriginal()
                    .IsComplete()
                    .SetInvoiceSeries("0")
                    .SetInvoiceNumber("1000")
                    .AddInvoiceItem("XX", "XX")
                        .GiveQuantity(1.0)
                        .GiveUnitPriceWithoutTax(100.01)
                        .GiveDiscount(10.01, "Line Discount")
                        .GiveVATRate(21.00, 5.20)
                        .GiveTaxRate(9.00, TaxTypeCodeType.PersonalIncomeTax)
                        .CalculateTotals()
                    .AddInvoiceItem("XXX", "XXX")
                        .GiveQuantity(1)
                        .GiveUnitPriceWithoutTax(100.01)
                        .GiveDiscount(10.01)
                        .GiveVATRate(21.00, 5.20)
                        .GiveTaxRate(9.00, TaxTypeCodeType.PersonalIncomeTax)
                        .CalculateTotals()
                    .CalculateTotals()
                .CalculateTotals()
                //.Validate()
                .Sign(s_Certificate)
                .CheckSignature()
                // .WriteToFile(s_Filename)
                ;
        }

        [Benchmark]
        public void X500FormatterBenchmark()
        {
            X500.X500DistinguishedName.Format(X500.DistinguishedNameFormat.RFC2253, s_Certificate.IssuerName.RawData);
        }
    }    
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<FacturaeBenchmark>();

            // var _serializer         = new XmlSerializer(typeof(Facturae));
            // var _nameTable          = new NameTable();
            // var _nsMgr              = XsdSchemas.CreateXadesNamespaceManager(_nameTable);
            // var _context            = new XmlParserContext(_nameTable, _nsMgr, null, XmlSpace.Preserve, System.Text.Encoding.UTF8);
            // var _namespaces         = new XmlSerializerNamespaces();
            // var s_xmlReaderSettings = new XmlReaderSettings
            // {
            //     ConformanceLevel = ConformanceLevel.Document,
            //     Schemas          = XsdSchemas.BuildSchemaSet(_nameTable),
            //     NameTable        = _nameTable,
            // };

            // foreach (XmlQualifiedName name in XsdSchemas.Namespaces)
            // {
            //     _nsMgr.AddNamespace(name.Name, name.Namespace);
            // }

            // _serializer.UnknownElement += (object sender, XmlElementEventArgs e) => {
            //     Console.WriteLine("Unexpected element: {0} as line {1}, column {2}", e.Element.Name, e.LineNumber, e.LinePosition);
            // };
            
            // _serializer.UnknownAttribute += (object sender, XmlAttributeEventArgs e) => {
            //     Console.WriteLine("Unexpected attribute: {0} as line {1}, column {2}", e.Attr.Name, e.LineNumber, e.LinePosition);
            // };

            // _serializer.UnknownNode += (object sender, XmlNodeEventArgs e) => {
            //     Console.WriteLine("Unexpected node: {0} as line {1}, column {2}", e.NodeType, e.LineNumber, e.LinePosition);
            // };

            // _serializer.UnreferencedObject += (object sender, UnreferencedObjectEventArgs e) => {
            //     Console.WriteLine("Unreferenced objkect with ID {0}", e.UnreferencedId);
            // };

            // using (var stream = new System.IO.FileStream(s_Filename, System.IO.FileMode.Open))
            // {
            //     using (var xmlReader = XmlReader.Create(stream, s_xmlReaderSettings, _context))
            //     {
            //         var face = _serializer.Deserialize(xmlReader) as Facturae;
            //     }
            // }
        }
    }
}


//                  Method |        Mean |       Error |     StdDev | Rank |    Gen 0 |    Gen 1 |  Allocated |
// ----------------------- |------------:|------------:|-----------:|-----:|---------:|---------:|-----------:|
//      SignatureBenchmark | 6,915.97 us | 101.2801 us | 94.7375 us |    2 | 742.1875 | 171.8750 | 2464.33 KB |
//  X500FormatterBenchmark |    14.39 us |   0.2803 us |  0.3442 us |    1 |   1.9073 |        - |    5.91 KB |
