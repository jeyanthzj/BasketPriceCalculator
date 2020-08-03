using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingBasketCalculator.Models;
using ShoppingBasketCalculator.ResourceAccess;

namespace ShoppingBasketCalculator.Builders
{
    /// <summary>
    /// Contains method do build a list of BasketItems
    /// </summary>
    public class BasketBuilder : IBasketBuilder
    {
        private readonly ICurrentOffers _currentOffers;
        private readonly IProductService _productService;

        public BasketBuilder(ICurrentOffers currentOffers, IProductService productService)
        {
            _currentOffers = currentOffers;
            _productService = productService;
        }

        /// <summary>
        /// Builds a list of BasketItems and returns the list
        /// </summary>
        /// <param name="products">space separated products to be added to the basket </param>
        /// <returns>List of BasketItems</returns>
        public List<BasketItem> Build(string[] products)
        {
            //Group same products together
            var groupedProducts = products.GroupBy(p => p)
                .Select(grp => new {ProductName = grp.Key, ProductCount = grp.Count()}).ToList();
            var baskets = new List<BasketItem>();
            
            var invalidProducts = new List<string>(groupedProducts.Count);
            bool skipFurtherProducts = false;
            //Create BasketItem with product list and their current offers
            foreach (var groupedProduct in groupedProducts)
            {
                var product = _productService.GetProduct(groupedProduct.ProductName);
                if (product == null)
                {
                    invalidProducts.Add(groupedProduct.ProductName);
                    skipFurtherProducts = true; //Don't get offers for any further products, as we will throw an exception once accumulating all invalid products
                    continue;
                }

                if (skipFurtherProducts)
                    continue;

                //Get current offers for this product
                var discountType = _currentOffers.GetCurrentOffers()
                    .FirstOrDefault(c => c.ProductId == product.ProductId);

                //Fill the Basket
                baskets.Add(new BasketItem
                {
                    OfferType = discountType,
                    ProductId = product.ProductId,
                    NumberOfItems = groupedProduct.ProductCount,
                    PricePerItem = product.Price,
                    ProductName = product.ProductName
                });
            
            }

            if (invalidProducts.Count > 0)
            {
                throw new Exception($"Invalid products found: {string.Join(',',invalidProducts)}");
            }
            return baskets;
        }
    }
}