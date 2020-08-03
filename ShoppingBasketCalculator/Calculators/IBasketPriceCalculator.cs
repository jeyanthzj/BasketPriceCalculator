using System.Collections.Generic;

namespace ShoppingBasketCalculator.Calculators
{
    public interface IBasketPriceCalculator
    {
        /// <summary>
        /// Calculates the total price of products before discount, 
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        IEnumerable<string> Calculate(string[] products);
    }
}