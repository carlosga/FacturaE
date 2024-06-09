namespace System;

internal static class DecimalExtensions
{
    internal static decimal Round(this decimal value, int decimals = 2)
    {
        return Math.Round(value, decimals);
    }
}
