using System.Diagnostics.CodeAnalysis;

namespace ShoppingBasketCalculator.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class CurrencyDisplayFormatter
    {
        /// <summary>
        /// Format the decimal to the desired currency format
        /// If less than 100 pence then returns in pence, else in Pounds and pence
        /// </summary>
        /// <param name="currency">The decimal value to be formatted</param>
        /// <returns>The formatted string</returns>
        public static string Format(this decimal currency)
        {
            return currency < 100 ? $"{(int)currency}p" : (currency / 100).ToString("C");
        }
    }
}