// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;

namespace System
{
    /// <summary>
    /// String conversions helper.
    /// </summary>
    internal static class StringExtensions
    {
        internal static string Quote(this string value, string quote = "")
        {
            if (value == null)
            {
                return value;
            }

            return quote + value + quote;
        } 
    }
}
