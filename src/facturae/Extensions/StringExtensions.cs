// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;

namespace System
{
    /// <summary>
    /// String conversions helper.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a byte buffer from the given hexadecimal string.
        /// </summary>
        /// <param name="hexString">The hex string.</param>
        /// <returns></returns>
        internal static byte[] HexToByteArray(this string hexString, string delimiter = null)
        {
            var hex = hexString;

            if (!String.IsNullOrEmpty(delimiter))
            {
                hex = hex.Replace(delimiter, String.Empty);
            }

            if ((hex.Length % 2) != 0)
            {
                throw new ArgumentException("Invalid hex string");
            }

            byte[] buffer = new byte[hex.Length / 2];

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (Byte.Parse(hex.Substring(i * 2, 2), NumberStyles.HexNumber));
            }

            return buffer;
        }

        internal static string Quote(this string value, string quote = "")
        {
            if (value == null)
            {
                return value;
            }

            return $"{quote}{value}{quote}";
        } 
    }
}
