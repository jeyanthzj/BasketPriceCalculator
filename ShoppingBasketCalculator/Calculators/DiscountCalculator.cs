using System.Collections.Generic;
using ShoppingBasketCalculator.DiscountEngines;
using ShoppingBasketCalculator.Models;

namespace ShoppingBasketCalculator.Calculators
{
    /// <summary>
    /// Main class that applies all valid discounts to items in the basket
    /// </summary>
    public class DiscountCalculator : IDiscountCalculator
    {
        private readonly IOfferEngineFactory _offerEngineFactory;

        public DiscountCalculator(IOfferEngineFactory offerEngineFactory)
        {
            _offerEngineFactory = offerEngineFactory;
        }

        /// <summary>
        /// Apply any available offers and return the applied discount
        /// </summary>
        /// <param name="basketItems"></param>
        /// <returns>The applied discount if any</returns>
        public List<ValidDiscount> GetDiscountsApplied(IList<BasketItem> basketItems)
        {
            var discountsApplied = new List<ValidDiscount>();
            //Get a list of offer engines
            var offerEngines = _offerEngineFactory.GetOfferEngines();

            //Run discount function of all offer engines on the given basket of items
            foreach (var discountEngine in offerEngines)
            {
                var validDiscount = discountEngine.Apply(basketItems);
                if(validDiscount != null) //A valid discount exists
                    discountsApplied.Add(validDiscount);
            }
            return discountsApplied;
        }
    }
}