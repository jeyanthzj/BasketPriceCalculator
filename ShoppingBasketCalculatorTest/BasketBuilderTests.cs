using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingBasketCalculator.Builders;
using ShoppingBasketCalculator.Models;
using ShoppingBasketCalculator.ResourceAccess;

namespace ShoppingBasketCalculatorTest
{
    [TestFixture]
    public class BasketBuilderTests
    {
        [Test]
        public void BasketBuilderShouldCorrectlyBuildBasket()
        {
            //Arrange
            var products = new[] { "Milk", "Apples", "Milk", "Bread" };
            var productService = new Mock<IProductService>();
            productService.Setup(service => service.GetProduct("Milk")).Returns(
                new Product
                {
                    ProductId = 3,
                    Price = 130,
                    ProductName = "Milk"
                }
            );
            productService.Setup(service => service.GetProduct("Apples")).Returns(
                new Product
                {
                    ProductId = 1,
                    Price = 100,
                    ProductName = "Apples"
                }
            );
            productService.Setup(service => service.GetProduct("Bread")).Returns(
                new Product
                {
                    ProductId = 2,
                    Price = 80,
                    ProductName = "Bread"
                }
            );
            var currentOffers = new Mock<ICurrentOffers>();
            var discountType = new Offer { DiscountCode = "TenPercentOff", DiscountDetail = "Apples 10% off", ProductId = 1 };

            currentOffers.Setup(offers => offers.GetCurrentOffers()).Returns(
                new List<Offer>
                {
                        new Offer
                        {
                            ProductId = 1,
                            DiscountCode = "TenPercentOff",
                            DiscountDetail = "Apples 10% off"
                        }
                });
            var expectedResult = new List<BasketItem>
                {
                    new BasketItem
                    {
                        ProductId = 3,
                        NumberOfItems = 2,
                        PricePerItem = 130,
                        OfferType = null
                    },
                    new BasketItem
                    {
                        ProductId = 1,
                        PricePerItem = 100,
                        NumberOfItems = 1,
                        OfferType = discountType
                    },
                    new BasketItem
                    {
                        ProductId = 2,
                        PricePerItem = 80,
                        NumberOfItems = 1,
                        OfferType = null
                    }
                };

            //Act
            var sut = new BasketBuilder(currentOffers.Object, productService.Object);
            var actualResult = sut.Build(products);


            //Assert
            CollectionAssert.AreEquivalent(expectedResult, actualResult);


        }
    }
}
