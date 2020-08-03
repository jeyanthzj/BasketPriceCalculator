using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingBasketCalculator.Models;
using ShoppingBasketCalculator.Utilities;

namespace ShoppingBasketCalculator.ResourceAccess
{
    /// <summary>
    /// Manages the list of valid products
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly Lazy<Dictionary<string, Product>> _products = new Lazy<Dictionary<string, Product>>(
            () =>
            {
                return JsonHelper.Deserialise<List<Product>>("Products.json")
                    .ToDictionary(product => product.ProductName, product => new Product
                    {
                        ProductId = product.ProductId,
                        Price = product.Price,
                        ProductName = product.ProductName
                    });
            },true
        );

        /// <summary>
        /// Returns the product details for given product
        /// </summary>
        /// <param name="productName"></param>
        /// <returns>The product details</returns>
        public Product GetProduct(string productName)
        {
            var product = _products.Value.FirstOrDefault(pair => pair.Value.ProductName == productName);
            if(string.IsNullOrWhiteSpace(product.Key))
                return null;
            return new Product
            {
                Price = product.Value.Price,
                ProductId = product.Value.ProductId,
                ProductName = productName
            };
        }
    }
}