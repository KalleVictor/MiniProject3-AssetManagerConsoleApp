namespace AssetManagerConsoleApp
{
    public class LiveCurrency
    {
        private static Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>();

        public static void FetchRates()
        {
            // Simulate fetching exchange rates from an external service
            exchangeRates["USD"] = 1.0m;
            exchangeRates["EUR"] = 0.85m;
            exchangeRates["SEK"] = 8.5m;
        }

        public static decimal Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            if (exchangeRates.ContainsKey(fromCurrency) && exchangeRates.ContainsKey(toCurrency))
            {
                decimal fromRate = exchangeRates[fromCurrency];
                decimal toRate = exchangeRates[toCurrency];
                return amount * (toRate / fromRate);
            }
            throw new ArgumentException("Invalid currency code.");
        }
    }
}
