using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System;
using Xunit;
using DiscountCalculator;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Intrinsics.X86;

namespace DiscountCal2.Tests
{
    /// <summary>
    /// Key Changes with Moq:
//    Mocking IPriceService:
//We use Moq to create a mock of the IPriceService interface, which is then used in the DiscountCalculator static method via SetPriceService().
//The method Setup(service => service.GetOriginalPrice()).Returns(mockOriginalPrice) is used to specify the behavior of the mocked service.
//Setting the Mocked Service:

//DiscountCalculator.SetPriceService(mockPriceService.Object) is called to inject the mocked service into the DiscountCalculator.
//Test Assertions:

//The assertions are similar to those in the previous examples, but now we're mocking the behavior of the price service.
//How to Use Moq:
//Moq allows you to create mock objects for interfaces or classes.
//You can specify behaviors using Setup() (for methods like GetOriginalPrice()) and assertions are made using XUnit's Assert.Equal().
//This setup can be particularly useful when the method depends on external services(like a database or web API), which we want to mock in our tests.
//By using Moq, you ensure that the tests are isolated and that you can test the logic in DiscountCalculator without being dependent on external services.
    /// </summary>
    public class DiscountCal2Tests
    {
        // Test for valid price and discount percentage with mocked IPriceService
        [Fact]
        public void CalculateDiscountedPrice_ValidInput_ReturnsCorrectPrice()
        {
            // Arrange
            double mockOriginalPrice = 200.00;
            var mockPriceService = new Mock<IPriceService>();
            mockPriceService.Setup(service => service.GetOriginalPrice()).Returns(mockOriginalPrice);

            // Set the mocked price service
            DiscountCalculator.DiscountCal2.SetPriceService(mockPriceService.Object);

            double discountPercentage = 20.0;

            // Act
            double result = DiscountCalculator.DiscountCal2.CalculateDiscountedPrice(discountPercentage);

            // Assert
            Assert.Equal(160.00, result);
        }

        // Test for zero discount with mocked IPriceService
        [Fact]
        public void CalculateDiscountedPrice_ZeroDiscount_ReturnsOriginalPrice()
        {
            // Arrange
            double mockOriginalPrice = 150.00;
            var mockPriceService = new Mock<IPriceService>();
            mockPriceService.Setup(service => service.GetOriginalPrice()).Returns(mockOriginalPrice);

            // Set the mocked price service
            DiscountCalculator.DiscountCal2.SetPriceService(mockPriceService.Object);

            double discountPercentage = 0.0;

            // Act
            double result = DiscountCalculator.DiscountCal2.CalculateDiscountedPrice(discountPercentage);

            // Assert
            Assert.Equal(150.00, result);
        }

        // Test for negative original price (should throw exception)
        [Fact] // failed
        public void CalculateDiscountedPrice_NegativePrice_ThrowsArgumentOutOfRangeException() 
        {
            // Arrange
            double mockOriginalPrice = -50.00;
            var mockPriceService = new Mock<IPriceService>();
            mockPriceService.Setup(service => service.GetOriginalPrice()).Returns(mockOriginalPrice);

            // Set the mocked price service
            DiscountCalculator.DiscountCal2.SetPriceService(mockPriceService.Object);

            double discountPercentage = 10.0;

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                DiscountCalculator.DiscountCal2.CalculateDiscountedPrice(discountPercentage));
            Assert.Equal("Original price should be equal or greater than 0", ex.Message);
        }

        // Test for discount percentage greater than 100 (should throw exception)
        [Fact]  //failed
        public void CalculateDiscountedPrice_DiscountGreaterThan100_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            double mockOriginalPrice = 100.00;
            var mockPriceService = new Mock<IPriceService>();
            mockPriceService.Setup(service => service.GetOriginalPrice()).Returns(mockOriginalPrice);

            // Set the mocked price service
            DiscountCalculator.DiscountCal2.SetPriceService(mockPriceService.Object);

            double discountPercentage = 110.0;

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                DiscountCalculator.DiscountCal2.CalculateDiscountedPrice(discountPercentage));
            Assert.Equal("Discount percentage should be between 0 and 100", ex.Message);
        }

        // Test for zero price (should return 0 regardless of discount percentage)
        [Fact]
        public void CalculateDiscountedPrice_ZeroPrice_ReturnsZero()
        {
            // Arrange
            double mockOriginalPrice = 0.00;
            var mockPriceService = new Mock<IPriceService>();
            mockPriceService.Setup(service => service.GetOriginalPrice()).Returns(mockOriginalPrice);

            // Set the mocked price service
            DiscountCalculator.DiscountCal2.SetPriceService(mockPriceService.Object);

            double discountPercentage = 50.0;

            // Act
            double result = DiscountCalculator.DiscountCal2.CalculateDiscountedPrice(discountPercentage);

            // Assert
            Assert.Equal(0.00, result);
        }
    }
}

