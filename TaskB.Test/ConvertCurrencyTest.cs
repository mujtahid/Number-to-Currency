using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskB.Libs;

namespace TaskB.Test
{
    [TestClass]
    public class ConvertCurrencyTest
    {
        [TestMethod]
        public void CurrencyTestSuccess()
        {
            var convertCurrency = new ConvertCurrency();

            var result = convertCurrency.ConvertCurrencyToEnglish("$127.45");

            result.Should().Be("One hundred and twenty-seven dollars and forty-five cents");
        }

        [TestMethod]
        public void InvalidInput_Throws()
        {
            var convertCurrency = new ConvertCurrency();

            Action act = () => convertCurrency.ConvertCurrencyToEnglish("AD45");

            act.ShouldThrow<ApplicationException>();
        }

        [TestMethod]
        public void InvalidInputStartWithZero_Throws()
        {
            var convertCurrency = new ConvertCurrency();

            Action act = () => convertCurrency.ConvertCurrencyToEnglish("$0");

            act.ShouldThrow<ApplicationException>();
        }
    }
}