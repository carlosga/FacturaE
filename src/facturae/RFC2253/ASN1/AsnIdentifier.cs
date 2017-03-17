// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace ASN1
{
    /// <summary>
    /// Class = Bits 8-7 
    ///                  B8 B7
    /// Universal        0  0
    /// Application      0  1
    /// Context-specific 1  0
    /// Private          1  1
    ///
    /// Form 
    /// B6 FORM
    ///  0 PRIMITIVE
    ///  1 CONSTRUCTED
    ///      
    /// Tag   = Bits 5-1
    /// B5 B4 B3 B2 B1 TAG
    ///  0  0  0  0  0
    ///  .  .  .  .  .
    ///  1  1  1  1  0  (0-30) single octet tag
    ///  1  1  1  1  1  (> 30) multiple octet tag, more octets follow
    /// </summary>
    public sealed class AsnIdentifier
    {
        public AsnClass Class
        {
            get;
            private set;
        }

        public AsnForm Form
        {
            get;
            private set;
        }

        public AsnTag Tag
        {
            get;
            private set;
        }

        public int DataLength
        {
            get;
            private set;
        }

        public AsnIdentifier(int octect)
        {
            Form = ((octect.IsBitSet(6) ? AsnForm.Constructed : AsnForm.Primitive));
            Tag  = (AsnTag)(octect & 0x5F);

            if ((!octect.IsBitSet(8) && !octect.IsBitSet(7)))
            {
                Class = AsnClass.Universal;
            }
            else if (!octect.IsBitSet(8) && octect.IsBitSet(7))
            {
                Class = AsnClass.Application;
            }
            else if (octect.IsBitSet(8) && !octect.IsBitSet(7))
            {
                Class = AsnClass.ContextSpecific;
            }
            else if (octect.IsBitSet(8) && octect.IsBitSet(7))
            {
                Class = AsnClass.Private;
            }
        }
    }
}
