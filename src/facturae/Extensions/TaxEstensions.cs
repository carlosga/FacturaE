// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace FacturaE;

internal static class TaxExtensions
{
    internal static bool IsTaxWithheld(this TaxTypeCodeType taxType)
    {
        return (taxType == TaxTypeCodeType.PersonalIncomeTax);
    }
}
