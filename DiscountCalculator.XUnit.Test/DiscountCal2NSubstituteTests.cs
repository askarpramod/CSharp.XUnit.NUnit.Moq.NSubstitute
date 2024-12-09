using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountCalculator.XUnit.Test
{
    public class DiscountCal2NSubstituteTests
    {
        // Test for valid price and discount percentage with mocked IPriceService
        [Fact]
        public void CalculateDiscountedPrice_ValidInput_ReturnsCorrectPrice()
        {
            // Arrange
            var mockPriceService = Substitute.For<IPriceService>();
            mockPriceService.GetOriginalPrice().Returns(200.00);

            DiscountCal2.SetPriceService(mockPriceService);
            double discountPercentage = 20.0;

            // Act
            double result = DiscountCal2.CalculateDiscountedPrice(discountPercentage);

            // Assert
            Assert.Equal(160.00, result);
        }

        // Test for zero discount percentage with mocked IPriceService
        [Fact]
        public void CalculateDiscountedPrice_ZeroDiscount_ReturnsOriginalPrice()
        {
            // Arrange
            var mockPriceService = Substitute.For<IPriceService>();
            mockPriceService.GetOriginalPrice().Returns(150.00);

            DiscountCal2.SetPriceService(mockPriceService);
            double discountPercentage = 0.0;

            // Act
            double result = DiscountCal2.CalculateDiscountedPrice(discountPercentage);

            // Assert
            Assert.Equal(150.00, result);
        }

        // Test for negative original price (should throw exception)
        [Fact] //failed
        public void CalculateDiscountedPrice_NegativePrice_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var mockPriceService = Substitute.For<IPriceService>();
            mockPriceService.GetOriginalPrice().Returns(-50.00);

            DiscountCal2.SetPriceService(mockPriceService);
            double discountPercentage = 10.0;

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                DiscountCal2.CalculateDiscountedPrice(discountPercentage));

            Assert.Equal("Original price should be equal or greater than 0", exception.Message);
        }

        // Test for discount percentage greater than 100 (should throw exception)
        [Fact] //failed
        public void CalculateDiscountedPrice_DiscountGreaterThan100_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var mockPriceService = Substitute.For<IPriceService>();
            mockPriceService.GetOriginalPrice().Returns(100.00);

            DiscountCal2.SetPriceService(mockPriceService);
            double discountPercentage = 110.0;

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                DiscountCal2.CalculateDiscountedPrice(discountPercentage));

            Assert.Equal("Discount percentage should be between 0 and 100", exception.Message);
        }

        // Test for zero original price (should return 0 regardless of discount percentage)
        [Fact]
        public void CalculateDiscountedPrice_ZeroOriginalPrice_ReturnsZero()
        {
            // Arrange
            var mockPriceService = Substitute.For<IPriceService>();
            mockPriceService.GetOriginalPrice().Returns(0.00);

            DiscountCal2.SetPriceService(mockPriceService);
            double discountPercentage = 50.0;

            // Act
            double result = DiscountCal2.CalculateDiscountedPrice(discountPercentage);

            // Assert
            Assert.Equal(0.00, result);
        }

        // Test for unset price service (should throw exception)
        [Fact]
        public void CalculateDiscountedPrice_UnsetPriceService_ThrowsInvalidOperationException()
        {
            // Arrange
            DiscountCal2.SetPriceService(null);
            double discountPercentage = 20.0;

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                DiscountCal2.CalculateDiscountedPrice(discountPercentage));

            Assert.Equal("Price service not set", exception.Message);
        }
    }
}
