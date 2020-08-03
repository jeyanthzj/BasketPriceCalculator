using System.Collections.Generic;
using System.Linq;
using ShoppingBasketCalculator.Builders;
using ShoppingBasketCalculator.Extensions;

namespace ShoppingBasketCalculator.Calculators
{
    /// <summary>
    /// The main class that is called to calculate the price of products to be purchased
    /// </summary>
    public class BasketPriceCalculator : IBasketPriceCalculator
    {
        private readonly IDiscountCalculator _discountCalculator;
        private readonly IBasketBuilder _basketBuilder;
        public BasketPriceCalculator(IDiscountCalculator discountCalculator, IBasketBuilder basketBuilder)
        {
            _discountCalculator = discountCalculator;
            _basketBuilder = basketBuilder;
        }

        /// <summary>
        /// Calculates the total price of products before discount, 
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public IEnumerable<string> Calculate(string[] products)
        {
            //Build the basket
            var basketItems = _basketBuilder.Build(products).ToList();

            //Get discounts that can be applied
            var discountsApplied = _discountCalculator.GetDiscountsApplied(basketItems).ToList();
            
            //We now have everything we need, subtract total basket value from discounts and present it to UI
            var subtotal = basketItems.Sum(basket => basket.NumberOfItems * basket.PricePerItem);
            yield return $"Subtotal: {subtotal.Format()}";

            var discountAmount = discountsApplied.Sum(applied => applied.DiscountAmount);
            if (!discountsApplied.Any())
            {
                yield return "(No offers available)";
            }
            else
            {
                foreach (var discountApplied in discountsApplied)
                {
                    yield return $"{discountApplied.OfferType.DiscountDetail}: -{discountApplied.DiscountAmount.Format()}";
                }
            }

            yield return $"Total: {(subtotal - discountAmount).Format()}";
        }
    }
}