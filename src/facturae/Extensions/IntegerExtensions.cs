// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace System
{
    internal static class IntegerExtensions
    {
        internal static bool IsBitSet(this int value, byte bitIndex)
        {
            if (bitIndex < 1 || bitIndex > 8)
            {
                throw new ArgumentOutOfRangeException("bitIndex should be between 0 and 7");
            }

            return (value & (1 << (bitIndex - 1))) != 0;
        }
    }
}
