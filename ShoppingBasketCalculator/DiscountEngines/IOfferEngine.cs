using System.Collections.Generic;
using ShoppingBasketCalculator.Models;

namespace ShoppingBasketCalculator.DiscountEngines
{
    public interface IOfferEngine
    {
        ValidDiscount Apply(IList<BasketItem> basketItems);
    }
}
