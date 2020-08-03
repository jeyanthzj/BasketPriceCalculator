using System.Collections.Generic;
using System.Linq;
using ShoppingBasketCalculator.Models;
using ShoppingBasketCalculator.ResourceAccess;

namespace ShoppingBasketCalculator.DiscountEngines
{
    /// <summary>
    /// The offer engine that calculates discount on Bread based on the number of beans cans in the basket
    /// </summary>
    public class BreadOfferEngine : IOfferEngine
    {
        private readonly ICurrentOffers _currentOffers;
        private const string DiscountCode = "BuyTwoBeansGetBreadHalfPrice";

        public BreadOfferEngine(ICurrentOffers currentOffers)
        {
            this._currentOffers = currentOffers;
        }

        /// <summary>
        /// Iterates the basket and calculates any discount on bread based on the beans cans in the basket
        /// </summary>
        /// <param name="basketItems">List of items in the basket</param>
        /// <returns>The total eligible discount if any</returns>
        public ValidDiscount Apply(IList<BasketItem> basketItems)
        {
            var discountType = _currentOffers.GetCurrentOffers().FirstOrDefault(discount => discount.DiscountCode == DiscountCode);

            var numberOfBeansCan = basketItems.Where(item => item.ProductName == "Beans")
                .Sum(x => x.NumberOfItems);

            var numberOfBreadLoaf = basketItems.Where(item => item.ProductName == "Bread")
                .Sum(x => x.NumberOfItems);

            if (numberOfBeansCan >= 2 && numberOfBreadLoaf > 0)
            {
                var numbersOfBreadLoafEligibleForHalfPrice = numberOfBeansCan / 2;
                var breadPrice = basketItems.First(item => item.ProductName == "Bread").PricePerItem;
                if (numberOfBreadLoaf <= numbersOfBreadLoafEligibleForHalfPrice) numbersOfBreadLoafEligibleForHalfPrice = numberOfBreadLoaf;
                var discountAmount = numbersOfBreadLoafEligibleForHalfPrice * (breadPrice / 2);
                return new ValidDiscount
                {
                    OfferType = discountType,
                    DiscountAmount = discountAmount
                };
            }

            return null;
        }
    }
}