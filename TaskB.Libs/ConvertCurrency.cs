using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TaskB.Libs
{
    public class ConvertCurrency
    {
        public string ConvertCurrencyToEnglish(string currencyNumber)
        {
            if (string.IsNullOrWhiteSpace(currencyNumber)) return string.Empty;

            var regEx =
                new Regex(@"^(\$?(0|[1-9]\d{0,2}(\d{3})?)(\.\d\d?)?|\(\$?(0|[1-9]\d{0,2}(\d{3})?)(\.\d\d?)?\))$");

            var isValidCurrencyNumber = regEx.Match(currencyNumber);

            if (!isValidCurrencyNumber.Success)
            {
                throw new ApplicationException(string.Format("{0} not valid input format. Example format $127.45", currencyNumber));
            }

            if (currencyNumber.StartsWith("$0") || currencyNumber.StartsWith("0"))
            {
                throw new ApplicationException(string.Format("{0} not valid input, number can't start with 0. Example format $127.45", currencyNumber));
            }

            var places = new Dictionary<int, string>
            {
                {1, ""},
                {2, " thousand "},
                {3, " million "},
                {4, " billion "},
                {5, " trillion "}
            };

            currencyNumber = currencyNumber.StartsWith("$") ? currencyNumber.Substring(1).Trim() : currencyNumber.Trim();

            var decimalPlace = currencyNumber.IndexOf(".", StringComparison.Ordinal);

            string temp;
            var cents = string.Empty;

            if (decimalPlace > 0)
            {
                temp = currencyNumber.Substring(decimalPlace + 1).PadRight(2, '0');
                cents = ConvertTens(temp);

                currencyNumber = currencyNumber.Substring(0, decimalPlace);
            }

            var dollars = string.Empty;
            var index = 1;
            do
            {
                var hundreads = currencyNumber.Length > 3 ? currencyNumber.Substring(currencyNumber.Length - 3) : currencyNumber;
                temp = ConvertHundreds(hundreads);
                if (!string.IsNullOrWhiteSpace(temp))
                {
                    dollars = string.Format("{0}{1}", temp, places[index]).CombineString(dollars, "and ");
                }

                currencyNumber = currencyNumber.Length > 3
                    ? currencyNumber.Substring(0, currencyNumber.Length - 3)
                    : string.Empty;

                index++;
            } while (!currencyNumber.Equals(string.Empty));

            dollars = dollars.Equals("one") ? "one dollar" : string.Format("{0} dollars", dollars.Trim());

            if (!string.IsNullOrWhiteSpace(cents))
            {
                cents = cents.Equals("one") ? " and one cent" : string.Format(" and {0} cents", cents.Trim());
            }
                
            return string.Format("{0}{1}", dollars, cents).ToUpperFirstChar();
        }

        private string ConvertHundreds(string number)
        {
            var result = string.Empty;

            if (string.IsNullOrWhiteSpace(number))
                return result;

            number = number.PadLeft(3, '0');

            var firstNumber = number.Substring(0, 1);
            var secondNumber = number.Substring(1, 2);
            var thirdNumber = number.Substring(2, 1);

            if (!firstNumber.Equals("0"))
            {
                result = string.Format("{0} hundred ", ConvertDigit(firstNumber));
            }

            result = CombineInBetween(result, !secondNumber.StartsWith("0") ? ConvertTens(secondNumber) : ConvertDigit(thirdNumber));

            return result;
        }

        private string ConvertTens(string tens)
        {
            var result = string.Empty;

            if (tens.StartsWith("1"))
            {
                switch (tens)
                {
                    case "10":
                        result = "ten";
                        break;
                    case "11":
                        result = "eleven";
                        break;
                    case "12":
                        result = "twelve";
                        break;
                    case "13":
                        result = "thirteen";
                        break;
                    case "14":
                        result = "fourteen";
                        break;
                    case "15":
                        result = "fifteen";
                        break;
                    case "16":
                        result = "sixteen";
                        break;
                    case "17":
                        result = "seventeen";
                        break;
                    case "18":
                        result = "eighteen";
                        break;
                    case "19":
                        result = "nineteen";
                        break;
                }
            }
            else
            {
                var firstNumber = tens.Substring(0, 1);
                var secondNumber = tens.Substring(1, 1);
                switch (firstNumber)
                {
                    case "2":
                        result = "twenty";
                        break;
                    case "3":
                        result = "thirty";
                        break;
                    case "4":
                        result = "forty";
                        break;
                    case "5":
                        result = "fifty";
                        break;
                    case "6":
                        result = "sixty";
                        break;
                    case "7":
                        result = "seventy";
                        break;
                    case "8":
                        result = "eighty";
                        break;
                    case "9":
                        result = "ninety";
                        break;
                }

                var secondNumberEnglish = ConvertDigit(secondNumber);
                result = CombineTens(result, secondNumberEnglish);
            }

            return result;
        }

        private string ConvertDigit(string digit)
        {
            var result = string.Empty;

            switch (digit)
            {
                case "1":
                    result = "one";
                    break;
                case "2":
                    result = "two";
                    break;
                case "3":
                    result = "three";
                    break;
                case "4":
                    result = "four";
                    break;
                case "5":
                    result = "five";
                    break;
                case "6":
                    result = "six";
                    break;
                case "7":
                    result = "seven";
                    break;
                case "8":
                    result = "eight";
                    break;
                case "9":
                    result = "nine";
                    break;
            }

            return result;
        }

        private string CombineTens(string firstString, string secondString)
        {
            return firstString.CombineString(secondString, "-");
        }

        private string CombineInBetween(string firstString, string secondString)
        {
            return firstString.CombineString(secondString, "and ");
        }
    }
}