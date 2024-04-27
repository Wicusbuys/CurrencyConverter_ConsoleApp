using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CurrencyConverter
{
    internal class Program
    {
        static string currentCurrencySelected = null; 
        static Dictionary<string, decimal> ExchangeRate = new Dictionary<string, decimal>();
        static void Main(string[] args)
        {
            Start();
        }

        static void Start()
        {
            FillDictionary();
            while (true)
            {
                PrintMenu();
            }
        }

        static void FillDictionary()
        {
            ExchangeRate.Add("USD", 1);
            ExchangeRate.Add("EUR", 0.92M);
            ExchangeRate.Add("GBP", 0.79M);
            ExchangeRate.Add("JPY", 152M);
            ExchangeRate.Add("CNY", 7.23M); 
            ExchangeRate.Add("CHF", 0.90M); 
            ExchangeRate.Add("CAD", 1.36M); 
            ExchangeRate.Add("AUD", 1.51M); 
            ExchangeRate.Add("NZD", 1.66M); 
            ExchangeRate.Add("KRW", 1354M);
            ExchangeRate.Add("ZAR", 18.63M);
        }
        static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Hello! Welcome to the Currency Converter app.");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Select currency");
            Console.WriteLine("2. Convert currency");
            Console.WriteLine("3. List current exhange rate");
            Console.WriteLine("4. List current exhange rate for currency");
            Console.WriteLine("5. Exit program");

            Console.Write("\nOption: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    SelectCurrency();
                    Console.ReadLine();
                    break;
                case "2":
                    ConvertValue();
                    Console.ReadLine();
                    break;
                case "3":
                    ListExchangeRate();
                    Console.ReadLine();
                    break;
                case "4":
                    ListExchangeRateForCurrentCurrency();
                    Console.ReadLine();
                    break;
                case "5":
                    Environment.Exit(0);
                    Console.ReadLine();
                    break;
                case "69":
                    Console.WriteLine("Just no.....");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("\nPlease enter a valid option!");
                    Console.ReadLine();
                    break;
            }
        }

        static void SelectCurrency()
        {
            string input = null;

            while (input == null)
            {
                Console.Clear();
                Console.WriteLine($"Current currency selected: {currentCurrencySelected}");
                Console.WriteLine("Please select a currency from the list to convert to:");

                DisplayCurrencyList();

                Console.Write("\nOption: ");
                input = Console.ReadLine().ToUpper();

                if (!ExchangeRate.ContainsKey(input))
                {
                    Console.WriteLine("Please enter a valid currency!");
                    Console.ReadLine();
                    input = null;
                    continue;
                }
                
                currentCurrencySelected = input;
                Console.WriteLine($"{currentCurrencySelected} has been selected!");
            }
        }

        static void DisplayCurrencyList()
        {
            foreach (string currencyISO in ExchangeRate.Keys)
            {
                Console.WriteLine(currencyISO);
            }
        }

        static void ConvertValue()
        {
            if (currentCurrencySelected == null)
            {
                Console.WriteLine("You need to select a currency first!");
                return;
            }

            Console.Clear();
            Console.WriteLine($"Current currency selected: {currentCurrencySelected}");
            Console.Write("Amount you would like to convert: ");
       
            try
            {
                Console.Write("\nAmount: ");
                var input = Console.ReadLine();
                decimal amount = decimal.Parse(input);

                if (amount < 0)
                {
                    Console.WriteLine("\nCannot convert a negative value!");
                    Console.ReadLine();
                    ConvertValue();
                    return;
                }

                Console.WriteLine($"\nA {amount} {currentCurrencySelected} is worth:");
                
                foreach (string currencyISO in ExchangeRate.Keys)
                {
                    string formattedValue = ((ExchangeRate[currencyISO] * amount) / ExchangeRate[currentCurrencySelected]).ToString("#,##0.00");
                    Console.WriteLine($"{currencyISO} : {formattedValue}");
                }

            }
            catch 
            {
                Console.WriteLine("\nInvalid input!");
                Console.ReadLine();
                ConvertValue();
            }

            Console.WriteLine();
        }

        static void ListExchangeRate()
        {
            Console.Clear();
            Console.WriteLine("Current exchange rate based on US Dollar (USD)");
            foreach (string currencyISO in ExchangeRate.Keys)
            {
                string formattedValue = ExchangeRate[currencyISO].ToString("#,##0.00");
                Console.WriteLine($"{currencyISO} : {formattedValue}");
            }
        }

        static void ListExchangeRateForCurrentCurrency()
        {
            if (currentCurrencySelected == null)
            {
                Console.WriteLine("You need to select a currency first!");
                return;
            }
            Console.Clear();
            Console.WriteLine($"Current currency selected: {currentCurrencySelected}");
            Console.WriteLine($"1 {currentCurrencySelected} is worth:");

            var currentCurrencyValue = ExchangeRate[currentCurrencySelected];

            foreach (string currencyISO in ExchangeRate.Keys)
            {
                string formattedValue = ((ExchangeRate[currencyISO] / currentCurrencyValue) * ExchangeRate["USD"]).ToString("#,##0.00");
                Console.WriteLine($"{currencyISO} : {formattedValue}");
            }
        }
    }
}
