using Interopability;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var connector = new InteroperabilityConnector())
            {
                var sum = connector.CalculateSum(1, 2);
                Console.WriteLine(sum);
            }

            Console.ReadKey();
        }
    }
}
