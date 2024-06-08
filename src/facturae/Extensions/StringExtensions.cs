// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace System;

/// <summary>
/// String conversions helper.
/// </summary>
internal static class StringExtensions
{
    internal static string Quote(this string value, string quote = "")
    {
        if (value is null)
        {
            return null;
        }
        else if (string.IsNullOrWhiteSpace(quote))
        {
            return value;
        }

        return quote + value + quote;
    }
}
