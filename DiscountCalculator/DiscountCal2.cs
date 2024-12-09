using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountCalculator
{
    public interface IPriceService
    {
        double GetOriginalPrice();
    }

    public static class DiscountCal2
    {
        private static IPriceService _priceService;

        // Dependency Injection for the PriceService
        public static void SetPriceService(IPriceService priceService)
        {
            _priceService = priceService;
        }

        /// <summary> 
        /// Calculate the total cost after applying a discount. 
        /// </summary> 
        /// <param name="discountPercentage">The discount percentage (0 to 100).</param> 
        /// <returns>The total cost after discount.</returns> 
        public static double CalculateDiscountedPrice(double discountPercentage)
        {
            if (_priceService == null)
            {
                throw new InvalidOperationException("Price service not set");
            }

            double originalPrice = _priceService.GetOriginalPrice();

            if (discountPercentage < 0 || discountPercentage > 100)
            {
                throw new ArgumentOutOfRangeException("Discount percentage should be between 0 and 100");
            }

            if (originalPrice < 0)
            {
                throw new ArgumentOutOfRangeException("Original price should be equal or greater than 0");
            }

            double discount = originalPrice * (discountPercentage / 100);
            return originalPrice - discount;
        }
    }
}
