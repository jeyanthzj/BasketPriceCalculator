using System.Collections.Generic;
using ShoppingBasketCalculator.Models;

namespace ShoppingBasketCalculator.Calculators
{
    public interface IDiscountCalculator
    {
        List<ValidDiscount> GetDiscountsApplied(IList<BasketItem> basketItems);
    }
}