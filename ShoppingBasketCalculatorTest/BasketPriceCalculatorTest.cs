using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ShoppingBasketCalculator.Builders;
using ShoppingBasketCalculator.Calculators;
using ShoppingBasketCalculator.Models;

namespace ShoppingBasketCalculatorTest
{
    public class BasketPriceCalculatorTest
    {
        [Test]
        public void BasketCalculatorShouldCorrectlyCalculateFinalOutput()
        {
            var products = new[] { "Apples", "Milk", "Bread" };
            var basketBuilder = new Mock<IBasketBuilder>();
            var discountType = new Offer{DiscountCode = "TenPercentOff", DiscountDetail = "Apples 10% off", ProductId = 1};
            var basketItems = new List<BasketItem>
            {
                new BasketItem
                {
                    ProductId = 1,
                    OfferType = discountType,
                    NumberOfItems = 1,
                    PricePerItem = 100
                },
                new BasketItem
                {
                    ProductId = 2,
                    OfferType = null,
                    NumberOfItems = 1,
                    PricePerItem = 130,
                },
                new BasketItem
                {
                    OfferType = null,
                    ProductId = 3,
                    NumberOfItems = 1,
                    PricePerItem = 80
                }
            };
            basketBuilder.Setup(builder => builder.Build(products)).Returns(
                basketItems
            );
            var discountCalculator = new Mock<IDiscountCalculator>();

            var validDiscounts = new List<ValidDiscount>
            {
                new ValidDiscount
                {
                    DiscountAmount = 10.0m,
                    OfferType = discountType
                }
            };
            discountCalculator.Setup(calculator => calculator.GetDiscountsApplied(basketItems)).Returns(
                validDiscounts);
            var expectedResult = new[]
            {
                "Subtotal: £3.10",
                "Apples 10% off: -10p",
                "Total: £3.00"
            };
            var sut = new BasketPriceCalculator(discountCalculator.Object, basketBuilder.Object);
            var actualResult = sut.Calculate(products);

            CollectionAssert.AreEqual(expectedResult.ToList(), actualResult.ToList());
        }
    }
}