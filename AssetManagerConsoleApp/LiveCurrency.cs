using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace AssetManagerConsoleApp
{
    public class LiveCurrency
    {
        public static Dictionary<string, decimal> exchangeRates = new();
        private static List<CurrencyObj> currencyList = new();

        public static void FetchRates()
        {
            string url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml"; // ECB Exchange Rate XML

            try
            {
                using HttpClient client = new();
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                string xmlData = response.Content.ReadAsStringAsync().Result;

                using XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlData));

                exchangeRates.Clear();
                currencyList.Clear();

                exchangeRates["EUR"] = 1.0m; // Base currency
                currencyList.Add(new CurrencyObj("EUR", 1.0));

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Cube" && reader.HasAttributes)
                    {
                        string? currencyCode = reader.GetAttribute("currency");
                        string? rateValue = reader.GetAttribute("rate");

                        if (!string.IsNullOrEmpty(currencyCode) && !string.IsNullOrEmpty(rateValue))
                        {
                            if (currencyCode == "USD" || currencyCode == "SEK")
                            {
                                decimal rate = decimal.Parse(rateValue, CultureInfo.InvariantCulture);
                                exchangeRates[currencyCode] = rate;
                                currencyList.Add(new CurrencyObj(currencyCode, (double)rate));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching exchange rates: {ex.Message}");
            }
        }

        public static decimal Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            if (!exchangeRates.ContainsKey(fromCurrency) || !exchangeRates.ContainsKey(toCurrency))
            {
                throw new ArgumentException("Invalid currency code.");
            }

            decimal fromRate = exchangeRates[fromCurrency];
            decimal toRate = exchangeRates[toCurrency];
            return amount * (toRate / fromRate);
        }
    }
}

public class CurrencyObj
{
    public string CurrencyCode { get; set; }
    public double Rate { get; set; }

    public CurrencyObj(string currencyCode, double rate)
    {
        CurrencyCode = currencyCode;
        Rate = rate;
    }
}
