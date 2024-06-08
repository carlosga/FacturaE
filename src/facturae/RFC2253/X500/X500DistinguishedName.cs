// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text;
using ASN1;

namespace X500;

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
        s_rfc1179 = new Dictionary<string, string>(7)
        {
            { "2.5.4.3", "CN" },                    // commonName
            { "2.5.4.6", "C" },                     // countryName
            { "2.5.4.7", "L" },                     // localityName
            { "2.5.4.8", "ST" },                    // stateOrProvinceName
            { "2.5.4.9", "STREET" },                // streetAddress
            { "2.5.4.10", "O" },                    // organizationName
            { "2.5.4.11", "OU" }                    // organizationalUnitName
        };

        // RFC2253
        s_rfc2253 = new Dictionary<string, string>(s_rfc1179)
        {
            { "0.9.2342.19200300.100.1.25", "DC" }, // domainComponent
            { "0.9.2342.19200300.100.1.1", "UID" }  // userid
        };

        // RFC2459
        s_rfc2459 = new Dictionary<string, string>(s_rfc2253)
        {
            { "2.5.4.4", "SN" },                    // surname
            { "2.5.4.5", "SERIALNUMBER" },          // serialNumber
            { "2.5.4.12", "T" },                    // title
            { "2.5.4.42", "G" },                    // givenName
            { "2.5.4.43", "I" },                    // initials
            { "2.5.4.44", "GENERATION" },           // generationQualifier
            { "2.5.4.46", "dnQualifier" },          // dnQualifier
            { "1.2.840.113549.1.9.1", "E" }         // e-mailAddress
        };
    }

    public static string Format(DistinguishedNameFormat format, byte[] rawData)
    {
        if (rawData is null)
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
        if (root is IEnumerable<AsnObject> enumeration)
        {
            foreach (var item in enumeration)
            {
                Format(format, item, ref builder);
            }
        }
        else if (root is AsnObjectIdentifier identifier)
        {
            if (builder.Length > 0)
            {
                builder.Append(',');
            }
            builder.Append(TranslateOid(format, identifier.Value + "="));
        }
        else if (root is IAsnValueObject valueObject)
        {
            builder.Append(valueObject.Value.ToString());
        }
    }

    private static string TranslateOid(DistinguishedNameFormat format, string oid)
    {
        return format switch
        {
            DistinguishedNameFormat.RFC1779 => s_rfc1179.ContainsKey(oid) ? s_rfc1179[oid] : oid,
            DistinguishedNameFormat.RFC2253 => s_rfc2253.ContainsKey(oid) ? s_rfc2253[oid] : oid,
            DistinguishedNameFormat.RFC2459 => s_rfc2459.ContainsKey(oid) ? s_rfc2459[oid] : oid,
            _                               => oid,
        };
    }

    public byte[] RawData { get; }

    public X500DistinguishedName(byte[] rawData)
    {
        RawData = rawData;
    }

    public string Format(DistinguishedNameFormat format) => Format(format, RawData);

    public override string ToString() => Format(DistinguishedNameFormat.RFC2253);
}
