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

        public static string Format(DistinguishedNameFormat format, string hexstring)
        {
            return Format(format, hexstring.HexToByteArray());
        }

        public static string Format(DistinguishedNameFormat format, byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            using (var dec = new AsnDecoder(buffer))
            {
                var result = Format(format, dec.MoveNext());

                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Trim().Substring(0, result.Length - 2);
                }

                return result;
            }
        }

        private static string Format(DistinguishedNameFormat format, AsnObject root)
        {
            var buffer = new StringBuilder();

            if (root is IEnumerable<AsnObject>)
            {
                foreach (var item in (root as IEnumerable<AsnObject>))
                {
                    buffer.Append(Format(format, item));
                }
            }
            else if (root is AsnObjectIdentifier)
            {
                buffer.AppendFormat("{0}=", TranslateOid(format, (root as AsnObjectIdentifier).Value));
            }
            else if (root is IAsnValueObject)
            {
                buffer.AppendFormat("{0}, ", (root as IAsnValueObject).Value);
            }

            return buffer.ToString();
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

            s_rfc1179 = new Dictionary<string, string>(10);
            s_rfc2253 = new Dictionary<string, string>(10);
            s_rfc2459 = new Dictionary<string, string>(10);

            s_rfc1179.Add("2.5.4.3" , "CN");                        // commonName
            s_rfc1179.Add("2.5.4.6" , "C");                         // countryName
            s_rfc1179.Add("2.5.4.7" , "L");                         // localityName
            s_rfc1179.Add("2.5.4.8" , "ST");                        // stateOrProvinceName
            s_rfc1179.Add("2.5.4.9" , "STREET");                    // streetAddress
            s_rfc1179.Add("2.5.4.10", "O");                         // organizationName
            s_rfc1179.Add("2.5.4.11", "OU");                        // organizationalUnitName

            s_rfc2253 = new Dictionary<string, string>(s_rfc1179);

            s_rfc2253.Add("0.9.2342.19200300.100.1.25", "DC");      // domainComponent
            s_rfc2253.Add("0.9.2342.19200300.100.1.1" , "UID");     // userid

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

        private readonly AsnObject _dn;

        public byte[] RawData => _dn.RawData;

        public X500DistinguishedName(string encodingHex)
            : this(encodingHex.HexToByteArray())
        {
        }

        public X500DistinguishedName(byte[] buffer)
        {
            using (var dec = new AsnDecoder(buffer))
            {
                _dn = dec.MoveNext();
            }
        }

        public string GetPreferredEncoding() => RawData.ByteArrayToHex();

        public string Format(DistinguishedNameFormat format) => Format(format, _dn.RawData);

        public override string ToString() => Format(DistinguishedNameFormat.RFC2253);
    }
}
