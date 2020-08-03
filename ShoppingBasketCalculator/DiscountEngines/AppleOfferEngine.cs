using System.Collections.Generic;
using System.Linq;
using ShoppingBasketCalculator.Models;
using ShoppingBasketCalculator.ResourceAccess;

namespace ShoppingBasketCalculator.DiscountEngines
{
    /// <summary>
    /// The offer engine that calculates TenPercent off for all apples in the basket
    /// </summary>
    public class AppleOfferEngine : IOfferEngine
    {
        private readonly ICurrentOffers _currentOffers;
        private const string DiscountCode = "TenPercentOff";

        public AppleOfferEngine(ICurrentOffers currentOffers)
        {
            _currentOffers = currentOffers;
        }

        /// <summary>
        /// Iterate the basket items and if any apples found, apply 10% discount
        /// </summary>
        /// <param name="basketItems">List of items in the basket</param>
        /// <returns>The total eligible discount if any</returns>
        public ValidDiscount Apply(IList<BasketItem> basketItems)
        {
            var discountType = _currentOffers.GetCurrentOffers().FirstOrDefault(discount => discount.DiscountCode == DiscountCode);
            var productEligibleForDiscount = basketItems.Where(basket => basket.OfferType?.DiscountCode == DiscountCode)
                .Select(basket => basket)
                .FirstOrDefault();
            if (productEligibleForDiscount != null)
                return new ValidDiscount
                {
                    OfferType = discountType,
                    DiscountAmount = (0.1m) * (productEligibleForDiscount.NumberOfItems *
                                               productEligibleForDiscount.PricePerItem)
                };
            return null;
        }
    }
}