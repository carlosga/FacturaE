namespace System;

internal static class DoubleExtensions
{
    internal static double Round(this double value, int decimals = 2)
    {
        return Math.Round(value, decimals);
    }
}
