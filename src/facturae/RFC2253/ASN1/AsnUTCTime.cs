// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text;

namespace ASN1;

public sealed class AsnUTCTime : AsnValueObject<DateTime>
{
    private static readonly string[] s_timeFormats = new string[]
    {
        "yyMMddHHmm",
        "yyMMddHHmmzz",
        "yyMMddHHmmss",
        "yyMMddHHmmsszz",
        "yyMMddHHmmss"
    };

    private static DateTime ToDateTime(ReadOnlySpan<byte> buffer)
    {
        var utcTime = Encoding.UTF8.GetString(buffer).Replace("Z", string.Empty);

        return DateTime.ParseExact(utcTime, s_timeFormats, CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal);
    }

    public AsnUTCTime(AsnIdentifier id, ReadOnlyMemory<byte> buffer) : base(id, buffer)
    {
        Value = ToDateTime(buffer.Span).ToUniversalTime();
    }
}
