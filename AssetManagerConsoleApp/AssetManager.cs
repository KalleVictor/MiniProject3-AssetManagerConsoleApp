//Function to add an asset to the list
namespace AssetManagerConsoleApp
{
    public class AssetManager
    {
        public static void AddAsset(List<Assets> assets)
        {
            Console.Write("Enter the type of asset; 1.Computer or 2.Smartphone: ");
            string? assetType = Console.ReadLine();
            if (assetType == "1")
            {
                assetType = "Computer";
            }
            else if (assetType == "2")
            {
                assetType = "Smartphone";
            }
            else if (assetType != "1" || assetType != "2")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid asset type. Please choose one of the two assets.");
                Console.ResetColor();
                return;
            }

            Console.Write("Enter the office location: 1.USA 2.Spain 3.Sweden ");
            string? office = Console.ReadLine();

            // Dictionary to map user input to office names and currencies
            Dictionary<string, (string OfficeName, string Currency)> officeData = new()
        {
            { "1", ("USA", "USD") },
            { "2", ("Spain", "EUR") },
            { "3", ("Sweden", "SEK") },
        };

            if (officeData.TryGetValue(office ?? "", out var officeInfo))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{officeInfo.OfficeName} selected. Local Currency set to {officeInfo.Currency}.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid office location. Please choose one of the three locations available.");
                Console.ResetColor();
                return;
            }

            string currency = officeInfo.Currency;
            string officeName = officeInfo.OfficeName; // Extract office name from officeInfo

            Console.Write("Enter the brand: ");
            string? brand = Console.ReadLine();
            if (string.IsNullOrEmpty(brand))
            {
                brand = "Missing";
            }

            Console.Write("Enter the model: ");
            string? model = Console.ReadLine();
            if (string.IsNullOrEmpty(model))
            {
                model = "Missing";
            }

            Console.Write("Enter the price (USD): ");
            string? priceInput = Console.ReadLine();
            if (!decimal.TryParse(priceInput, out decimal price))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid price. Please enter a valid number.");
                Console.ResetColor();
                return;
            }


            // **Step 2: Convert Price to Local Currency** using the LiveCurrency Class
            // Exchange rates from USD to EUR and SEK from
            LiveCurrency.FetchRates();
            Dictionary<string, decimal> currencyToExchangeRate = new()
                    {
                        { "SEK", (decimal)LiveCurrency.Convert(1, "USD", "SEK") },
                        { "USD", 1 },
                        { "EUR", (decimal)LiveCurrency.Convert(1, "USD", "SEK") }
                    };

            decimal localPrice = price * currencyToExchangeRate[currency];

            if (price <= 0 || localPrice <= 0)  // Ensure price and local price are positive
            {
                Console.WriteLine("Invalid input. Price and local price must be positive.");
                return;
            }

            // Enter Date of Purchase, for functionally purposes, enter "T" or "t" if the date is today
            Console.Write("Enter the purchase date (YYYY-MM-DD), if today enter (T): ");
            string? purchaseToday = Console.ReadLine()?.Trim();

            DateTime purchaseDate;

            if (!string.IsNullOrEmpty(purchaseToday) && purchaseToday.Equals("T", StringComparison.OrdinalIgnoreCase))
            {
                purchaseDate = DateTime.Today; // Assign today's date
            }
            else if (!DateTime.TryParseExact(purchaseToday, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out purchaseDate))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid date! Please enter the date in yyyy-MM-dd format or 'T' for today.");
                Console.ResetColor();
                return;
            }

            // Validate that the date is not in the future and not before 2000
            if (purchaseDate > DateTime.Today)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Purchase date cannot be in the future.");
                Console.ResetColor();
                return;
            }
            else if (purchaseDate < new DateTime(2000, 1, 1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Purchase date cannot be before the year 2000.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" Purchase Date Set: {purchaseDate:yyyy-MM-dd}");
            Console.ResetColor();

            if (assetType == "Computer")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Computer added as an asset!");
                Console.ResetColor();
                assets.Add(new Computer(brand, model, officeName, purchaseDate, price, currency, localPrice));
                return;
            }
            else if (assetType == "Smartphone")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Smartphone added as an asset!");
                Console.ResetColor();
                assets.Add(new Smartphone(brand, model, officeName, purchaseDate, price, currency, localPrice));
                return;
            }
        }
    }
}