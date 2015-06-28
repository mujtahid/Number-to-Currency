using System;
using TaskB.Libs;

namespace Task_B
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var convertCurrency = new ConvertCurrency();

            Console.WriteLine(convertCurrency.ConvertCurrencyToEnglish("$1.45"));

            Console.ReadLine();
        }
    }
}