using System.Collections.Generic;
using ShoppingBasketCalculator.Models;

namespace ShoppingBasketCalculator.Builders
{
    public interface IBasketBuilder
    {
        List<BasketItem> Build(string[] products);
    }
}