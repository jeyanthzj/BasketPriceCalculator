using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using ShoppingBasketCalculator.Enums;
using ShoppingBasketCalculator.Models;
using ShoppingBasketCalculator.Utilities;

namespace ShoppingBasketCalculator.ResourceAccess
{
    // public class DiscountService : IDiscountService
    // {
    //     private readonly Lazy<Dictionary<int, DiscountType>> _discounts = new Lazy<Dictionary<int, DiscountType>>(
    //         () =>
    //         {
    //            return JsonHelper.Deserialise<List<Discount>>("CurrentOffers.json")
    //                 .ToDictionary(discount => discount.ProductId, discount => discount.DiscountType);
    //         },true
    //         );
    //     public Discount GetDiscountForProduct(int productId)
    //     {
    //         var discount = _discounts.Value.FirstOrDefault(pair => pair.Key.Equals(productId));
    //         return new Discount
    //         {
    //             ProductId = productId,
    //             DiscountType = discount.Value
    //         };
    //     }
    // }
}