using System;
using ShoppingBasketCalculator.Builders;
using ShoppingBasketCalculator.Calculators;
using ShoppingBasketCalculator.DiscountEngines;
using ShoppingBasketCalculator.ResourceAccess;
using Unity;

namespace ShoppingBasketCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            RegisterClasses(container);
            var basketCalculator = container.Resolve<IBasketPriceCalculator>();
            try
            {
                var result = basketCalculator.Calculate(args);
                foreach (var item in result)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            Console.ReadLine();
        }

        private static void RegisterClasses(IUnityContainer container)
        {
            container.RegisterType<IBasketPriceCalculator, BasketPriceCalculator>();
            container.RegisterType<IDiscountCalculator, DiscountCalculator>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IBasketBuilder, BasketBuilder>();
            container.RegisterType<ICurrentOffers, CurrentOffers>();
            container.RegisterType<IOfferEngineFactory, OfferEngineFactory>();
            container.RegisterType<IOfferEngine, AppleOfferEngine>("Apple");
            container.RegisterType<IOfferEngine, BreadOfferEngine>("Bread");
        }
    }
}