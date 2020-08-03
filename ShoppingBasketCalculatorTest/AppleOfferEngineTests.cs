using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingBasketCalculator.DiscountEngines;
using ShoppingBasketCalculator.Models;
using ShoppingBasketCalculator.ResourceAccess;

namespace ShoppingBasketCalculatorTest
{
    [TestFixture]
    public class AppleOfferEngineTests
    {
        [Test]
        public void AppleOfferEngineShouldReturnDiscountWhenThereIsOne()
        {
            //Arrange
            var appleDiscountOffer = new Offer
            {
                DiscountCode = "TenPercentOff",
                DiscountDetail = "Apples 10% off",
                ProductId = 1
            };
            var basketItems = new List<BasketItem>
            {
                new BasketItem
                {
                    ProductId = 1,
                    OfferType = appleDiscountOffer,
                    NumberOfItems = 1,
                    PricePerItem = 100,
                    ProductName = "Apples"
                },
                new BasketItem
                {
                    ProductId = 2,
                    OfferType = null,
                    NumberOfItems = 1,
                    PricePerItem = 130,
                    ProductName = "Bread"
                },
                new BasketItem
                {
                    OfferType = null,
                    ProductId = 3,
                    NumberOfItems = 1,
                    PricePerItem = 80,
                    ProductName = "Milk"
                }
            };
            var currentOffersMock = new Mock<ICurrentOffers>();
            currentOffersMock.Setup(offers => offers.GetCurrentOffers()).Returns(
                new List<Offer>
                {
                    appleDiscountOffer
                }
            );
            ValidDiscount expectedResult = new ValidDiscount
            {
                DiscountAmount = 10,
                OfferType = appleDiscountOffer
            };

            //Act
            var sut = new AppleOfferEngine(currentOffersMock.Object);
            var actualResult = sut.Apply(basketItems);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

    }
}
