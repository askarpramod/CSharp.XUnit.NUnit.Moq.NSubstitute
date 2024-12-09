
namespace DiscountCalculator
{
    public static class DiscountCalculator
    {
        /// <summary> 
        /// Calculate the total cost after applying a discount. 
        /// </summary> 
        /// <param name="originalPrice">The original price of the item.</param> 
        /// <param name="discountPercentage">The discount percentage (0 to 100).</param> 
        /// <returns>The total cost after discount.</returns> 
        public static double CalculateDiscountedPrice(double originalPrice, double discountPercentage)
        {
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
