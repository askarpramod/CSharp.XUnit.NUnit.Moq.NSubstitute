using System.Diagnostics.CodeAnalysis;
namespace NewMethodCreation
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        protected Program() { }
        static void Main(string[] args)
        {
            Console.WriteLine("Discounted Price for 100 with discount 10% is : " +
                DiscountCalculator.DiscountCalculator.CalculateDiscountedPrice(100, 10));

            Console.ReadLine();
        }
    }
}