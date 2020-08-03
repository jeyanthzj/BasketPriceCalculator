using System.Collections.Generic;
using System.Linq;
using Unity;

namespace ShoppingBasketCalculator.DiscountEngines
{
    /// <summary>
    /// The class that is responsible for supplying the list of offer engines
    /// </summary>
    public sealed class OfferEngineFactory : IOfferEngineFactory
    {
        private readonly IUnityContainer _container;

        public OfferEngineFactory(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Returns all the available offer engine classes
        /// </summary>
        /// <returns>List of offer engine classes</returns>
        public List<IOfferEngine> GetOfferEngines()
        {
            return _container.ResolveAll<IOfferEngine>().ToList();
        }
    }
}