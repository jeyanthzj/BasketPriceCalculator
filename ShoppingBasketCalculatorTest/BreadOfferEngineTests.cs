using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingBasketCalculator.DiscountEngines;
using ShoppingBasketCalculator.Models;
using ShoppingBasketCalculator.ResourceAccess;

namespace ShoppingBasketCalculatorTest
{
    [TestFixture]
    public class BreadOfferEngineTests
    {

        [Test]
        public void BreadOfferEngineShouldReturnDiscountWhenThereIsOne()
        {
            //Arrange
            var breadOffer = new Offer
            {
                DiscountCode = "BuyTwoBeansGetBreadHalfPrice",
                DiscountDetail = "Buy 2 Beans and Get half price bread",
                ProductId = 2
            };
            var basketItems = new List<BasketItem>
            {
                new BasketItem
                {
                    ProductId = 1,
                    OfferType = null,
                    NumberOfItems = 1,
                    PricePerItem = 100,
                    ProductName = "Apples"
                },
                new BasketItem
                {
                    ProductId = 2,
                    OfferType = breadOffer,
                    NumberOfItems = 1,
                    PricePerItem = 80,
                    ProductName = "Bread"
                },
                new BasketItem
                {
                    OfferType = null,
                    ProductId = 4,
                    NumberOfItems = 2,
                    PricePerItem = 65,
                    ProductName = "Beans"
                }
            };
            var currentOffersMock = new Mock<ICurrentOffers>();
            currentOffersMock.Setup(offers => offers.GetCurrentOffers()).Returns(
                new List<Offer>
                {
                    breadOffer
                }
            );

            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(service => service.GetProduct("Milk")).Returns(
                new Product
                {
                    ProductId = 3,
                    Price = 130,
                    ProductName = "Milk"
                }
            );
            productServiceMock.Setup(service => service.GetProduct("Apples")).Returns(
                new Product
                {
                    ProductId = 1,
                    Price = 100,
                    ProductName = "Apples"
                }
            );
            productServiceMock.Setup(service => service.GetProduct("Bread")).Returns(
                new Product
                {
                    ProductId = 2,
                    Price = 80,
                    ProductName = "Bread"
                }
            );
            productServiceMock.Setup(service => service.GetProduct("Beans")).Returns(
                new Product
                {
                    ProductId = 4,
                    Price = 65,
                    ProductName = "Beans"
                }
            );
            ValidDiscount expectedResult = new ValidDiscount
            {
                DiscountAmount = 40,
                OfferType = breadOffer
            };

            //Act
            var sut = new BreadOfferEngine(currentOffersMock.Object);
            var actualResult = sut.Apply(basketItems);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

    }
}
