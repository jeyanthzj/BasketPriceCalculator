using System.Collections.Generic;

namespace ShoppingBasketCalculator.DiscountEngines
{
    public interface IOfferEngineFactory
    {
        List<IOfferEngine> GetOfferEngines();
    }
}