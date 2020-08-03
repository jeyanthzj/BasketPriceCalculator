using System;
using System.Collections.Generic;
using ShoppingBasketCalculator.Models;
using ShoppingBasketCalculator.Utilities;

namespace ShoppingBasketCalculator.ResourceAccess
{
    /// <summary>
    /// Responsible for managing the current offers
    /// </summary>
    public class CurrentOffers : ICurrentOffers
    {
        private readonly Lazy<IList<Offer>> _discounts = new Lazy<IList<Offer>>(
            () => JsonHelper.Deserialise<List<Offer>>("CurrentOffers.json"),true
        );

        /// <summary>
        /// Get the current offers
        /// </summary>
        /// <returns>List of current offers</returns>
        public IEnumerable<Offer> GetCurrentOffers()
        {
            return _discounts.Value;
        }
    }
}