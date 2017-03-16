// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Text;

namespace ASN1
{
    public sealed class AsnUTCTime
        : AsnValueObject<DateTime>
    {
        private static readonly string[] s_timeFormats = new string[]
        {
            "yyMMddHHmm"
          , "yyMMddHHmmzz"
          , "yyMMddHHmmss"
          , "yyMMddHHmmsszz"
          , "yyMMddHHmmss"
        };

        private static DateTime ToDateTime(byte[] buffer)
        {
            var utcTime = Encoding.UTF8.GetString(buffer).Replace("Z", string.Empty);

            return DateTime.ParseExact(utcTime, s_timeFormats, CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal);
        }

        public AsnUTCTime(AsnIdentifier id, byte[] buffer)
            : base(id, buffer)
        {
            Value = ToDateTime(buffer).ToUniversalTime();
        }
    }
}
