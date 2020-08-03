using System.Collections.Generic;
using ShoppingBasketCalculator.Models;

namespace ShoppingBasketCalculator.ResourceAccess
{
    public interface ICurrentOffers
    {
        IEnumerable<Offer> GetCurrentOffers();
    }
}