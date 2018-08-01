// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using ASN1;

namespace X500
{
    public sealed class X500DistinguishedName
    {
        private static readonly Dictionary<string, string> s_rfc1179;
        private static readonly Dictionary<string, string> s_rfc2253;
        private static readonly Dictionary<string, string> s_rfc2459;

        static X500DistinguishedName()
        {
            // http://www.alvestrand.no/objectid/2.5.4.html
            // https://www.ietf.org/rfc/rfc2253.txt
            // CN      commonName             (2.5.4.3)
            // C       countryName            (2.5.4.6)
            // L       localityName           (2.5.4.7)
            // ST      stateOrProvinceName    (2.5.4.8)
            // STREET  streetAddress          (2.5.4.9)
            // O       organizationName       (2.5.4.10)
            // OU      organizationalUnitName (2.5.4.11)
            // DC      domainComponent        (0.9.2342.19200300.100.1.25)
            // UID     userId                 (0.9.2342.19200300.100.1.1)

            // RFC1779
            s_rfc1179 = new Dictionary<string, string>(10);

            s_rfc1179.Add("2.5.4.3" , "CN");                        // commonName
            s_rfc1179.Add("2.5.4.6" , "C");                         // countryName
            s_rfc1179.Add("2.5.4.7" , "L");                         // localityName
            s_rfc1179.Add("2.5.4.8" , "ST");                        // stateOrProvinceName
            s_rfc1179.Add("2.5.4.9" , "STREET");                    // streetAddress
            s_rfc1179.Add("2.5.4.10", "O");                         // organizationName
            s_rfc1179.Add("2.5.4.11", "OU");                        // organizationalUnitName

            // RFC2253
            s_rfc2253 = new Dictionary<string, string>(s_rfc1179);

            s_rfc2253.Add("0.9.2342.19200300.100.1.25", "DC");      // domainComponent
            s_rfc2253.Add("0.9.2342.19200300.100.1.1" , "UID");     // userid

            // RFC2459
            s_rfc2459 = new Dictionary<string, string>(s_rfc2253);

            s_rfc2459.Add("2.5.4.4"             , "SN");            // surname
            s_rfc2459.Add("2.5.4.5"             , "SERIALNUMBER");  // serialNumber
            s_rfc2459.Add("2.5.4.12"            , "T");             // title
            s_rfc2459.Add("2.5.4.42"            , "G");             // givenName
            s_rfc2459.Add("2.5.4.43"            , "I");             // initials
            s_rfc2459.Add("2.5.4.44"            , "GENERATION");    // generationQualifier
            s_rfc2459.Add("2.5.4.46"            , "dnQualifier");   // dnQualifier
            s_rfc2459.Add("1.2.840.113549.1.9.1", "E");             // e-mailAddress
        }

        public static string Format(DistinguishedNameFormat format, string hexstring)
        {
            return Format(format, hexstring.HexToByteArray());
        }

        public static string Format(DistinguishedNameFormat format, byte[] rawData)
        {
            if (rawData == null)
            {
                throw new ArgumentNullException(nameof(rawData));
            }
            if (rawData.Length == 0)
            {
                throw new ArgumentException("rawData cannot be empty");
            }

            var decoder = new AsnDecoder(rawData);
            var builder = new StringBuilder();

            Format(format, decoder.MoveNext(), ref builder);

            return builder.ToString();
        }

        private static void Format(DistinguishedNameFormat format, AsnObject root, ref StringBuilder builder)
        {
            if (root is IEnumerable<AsnObject>)
            {
                foreach (var item in (root as IEnumerable<AsnObject>))
                {
                    Format(format, item, ref builder);
                }
            }
            else if (root is AsnObjectIdentifier)
            {
                if (builder.Length > 0)
                {
                    builder.Append(",");
                }
                builder.Append($"{TranslateOid(format, (root as AsnObjectIdentifier).Value)}=");
            }
            else if (root is IAsnValueObject)
            {
                builder.Append($"{(root as IAsnValueObject).Value}");
            }
        }

        private static string TranslateOid(DistinguishedNameFormat format, string oid)
        {
            Dictionary<string, string> oids = null;

            switch (format)
            {
            case DistinguishedNameFormat.RFC1779:
                oids = s_rfc1179;
                break;
            case DistinguishedNameFormat.RFC2253:
                oids = s_rfc2253;
                break;
            case DistinguishedNameFormat.RFC2459:
                oids = s_rfc2459;
                break;
            }

            if (oids.ContainsKey(oid))
            {
                return oids[oid];
            }

            return oid;
        }

        private readonly byte[] _rawData;

        public byte[] RawData => _rawData;

        public X500DistinguishedName(string encodingHex)
            : this(encodingHex.HexToByteArray())
        {
        }

        public X500DistinguishedName(byte[] rawData)
        {
            _rawData = rawData;
        }

        //public string GetPreferredEncoding() => _rawData.AsSpan().ByteArrayToHex();

        public string Format(DistinguishedNameFormat format) => Format(format, _rawData);

        public override string ToString() => Format(DistinguishedNameFormat.RFC2253);
    }
}
