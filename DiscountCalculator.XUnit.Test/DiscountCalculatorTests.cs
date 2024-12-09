using System;
using DiscountCalculator;
using Xunit;

namespace DiscountCalculator.XUnit.Test
{
        public class DiscountCalculatorTests
        {
            // Test for valid price and discount percentage
            [Fact]
            public void CalculateDiscountedPrice_ValidInput_ReturnsCorrectPrice()
            {
                // Arrange
                double originalPrice = 200.00;
                double discountPercentage = 20.0;

                // Act
                double result = DiscountCalculator.CalculateDiscountedPrice(originalPrice, discountPercentage);

                // Assert
                Assert.Equal(160.00, result);
            }

            // Test for discount percentage of 0%
            [Fact]
            public void CalculateDiscountedPrice_ZeroDiscount_ReturnsOriginalPrice()
            {
                // Arrange
                double originalPrice = 150.00;
                double discountPercentage = 0.0;

                // Act
                double result = DiscountCalculator.CalculateDiscountedPrice(originalPrice, discountPercentage);

                // Assert
                Assert.Equal(150.00, result);
            }

            // Test for discount percentage of 100%
            [Fact]
            public void CalculateDiscountedPrice_OneHundredPercentDiscount_ReturnsZero()
            {
                // Arrange
                double originalPrice = 150.00;
                double discountPercentage = 100.0;

                // Act
                double result = DiscountCalculator.CalculateDiscountedPrice(originalPrice, discountPercentage);

                // Assert
                Assert.Equal(0.00, result);
            }

            // Test for negative price (should throw exception)
            [Fact]
            public void CalculateDiscountedPrice_NegativePrice_ThrowsArgumentOutOfRangeException()
            {
                // Arrange
                double originalPrice = -50.00;
                double discountPercentage = 10.0;

                // Act & Assert
                var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                    DiscountCalculator.CalculateDiscountedPrice(originalPrice, discountPercentage));
                Assert.Equal("Original price should be equal or greater than 0", ex.Message);
            }

            // Test for discount percentage less than 0 (should throw exception)
            [Fact]
            public void CalculateDiscountedPrice_NegativeDiscount_ThrowsArgumentOutOfRangeException()
            {
                // Arrange
                double originalPrice = 100.00;
                double discountPercentage = -10.0;

                // Act & Assert
                var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                    DiscountCalculator.CalculateDiscountedPrice(originalPrice, discountPercentage));
                Assert.Equal("Discount percentage should be between 0 and 100", ex.Message);
            }

            // Test for discount percentage greater than 100 (should throw exception)
            [Fact]
            public void CalculateDiscountedPrice_DiscountGreaterThan100_ThrowsArgumentOutOfRangeException()
            {
                // Arrange
                double originalPrice = 100.00;
                double discountPercentage = 110.0;

                // Act & Assert
                var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                    DiscountCalculator.CalculateDiscountedPrice(originalPrice, discountPercentage));
                Assert.Equal("Discount percentage should be between 0 and 100", ex.Message);
            }

            // Test for zero price (should return 0 regardless of discount percentage)
            [Fact]
            public void CalculateDiscountedPrice_ZeroPrice_ReturnsZero()
            {
                // Arrange
                double originalPrice = 0.00;
                double discountPercentage = 50.0;

                // Act
                double result = DiscountCalculator.CalculateDiscountedPrice(originalPrice, discountPercentage);

                // Assert
                Assert.Equal(0.00, result);
            }

            // Test for zero discount with zero price (should return 0)
            [Fact]
            public void CalculateDiscountedPrice_ZeroPriceWithZeroDiscount_ReturnsZero()
            {
                // Arrange
                double originalPrice = 0.00;
                double discountPercentage = 0.0;

                // Act
                double result = DiscountCalculator.CalculateDiscountedPrice(originalPrice, discountPercentage);

                // Assert
                Assert.Equal(0.00, result);
            }
        }
    }
