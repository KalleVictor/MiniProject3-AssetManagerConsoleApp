using System.Reflection.Metadata.Ecma335;

namespace AssetManagerConsoleApp
{
    internal class AssetManager
    {

        //Function to start the Asset Manager Console App
        static public void Start()
        {
            Console.WriteLine("Welcome to Asset Manager Console App");
            var assets = new List<Assets>();
            DataSamples.AddSampleAssets(assets);
            DataSamples.AddSampleAssets2(assets);
            while (true)
            {
                //Use MainHeader function to display header 
                MainHeader();
                //Use DisplayAssets function to display all current assets
                DisplayAssets(assets);
                //Use MainMenu function to display the main menu
                MainMenu(assets);
                Console.ReadLine();
            }
        }

        //Table appearance - Header
        public static void MainHeader()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(
                "Office".PadRight(15) +
                "Asset".PadRight(20) +
                "Brand".PadRight(10) +
                "Model".PadRight(15) +
                "Price(USD)".PadRight(15) +
                "Price(Local)".PadRight(19) +
                "Purchase Date" + " ".PadLeft(10)
            );
            Console.ResetColor();
        }
        //Table appearance 
        public static void TableColor()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        //Table appearance - Footer - For additional functions, such as include the total amount of assets and the total cost of all assets - Not used at the moment
        public static void MainFooter()
        {

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            //Present total amount etc. 
            Console.ResetColor();
        }
        public static void MainMenu(List<Assets> assets)
        {
            Console.WriteLine("Press (1) to add an asset, Sort the list by (2) Purchase Date, (3) Office or (4) by Class. Press (Q) to Exit the app.");
            string? option = Console.ReadLine();
            if (option == "1")
            {
                AssetManager.AddAsset(assets);
                MainMenu(assets);
            }
            else if (option == "2")
            {
                DisplayAssets(assets, x => x.PurchaseDate, "Purchase Date");
                MainFooter();
                MainMenu(assets);
            }
            else if (option == "3")
            {
                DisplayAssets(assets, x => x.Office, "Office (Country)");
                MainFooter();
                MainMenu(assets);
            }
            else if (option == "4")
            {
                DisplayAssets(assets, x => x.AssetTag, "Class");
                MainFooter();
                MainMenu(assets);
            }
            else if (option != null && option.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting the app...");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid option.");
                MainMenu(assets);
            }
        }
        
        //Function to display all assets and sort. 

        public static void DisplayAssets(List<Assets> assets, Func<Assets, object>? orderBy = null, string title = "All Assets")
        {
            Console.Clear();
            Console.WriteLine($"Assets sorted by {title}");
            MainHeader();
            TableColor();

            var sortedAssets = orderBy != null ? assets.OrderBy(orderBy).ToList() : assets;

            foreach (var asset in sortedAssets)
            {
                AgeOfAsset(asset);
                Console.WriteLine(
                    asset.Office.PadRight(15) +
                    asset.AssetTag.PadRight(20) +
                    asset.Brand.PadRight(10) +
                    asset.Model.PadRight(15) +
                    asset.Price.ToString("F2").PadRight(15) +  // Ensure two decimal places 
                    asset.Currency + " " + asset.LocalPrice.ToString("F2").PadRight(15) +
                    asset.PurchaseDate.ToString("yyyy-MM-dd") + " ".PadRight(13)
                );
            }
    
            Console.ResetColor();
        }

        //Function to calculate the age of the asset
        public static void AgeOfAsset(Assets asset)
        {
            // Get current date
            DateTime currentDate = DateTime.Now;
            // Calculate asset age in months
            int assetAgeMonths = (currentDate.Year - asset.PurchaseDate.Year) * 12 + currentDate.Month - asset.PurchaseDate.Month;
            // Apply conditional formatting based on asset age
            if (assetAgeMonths >= 33) // 3 months away from 3 years (i.e., 33+ months old)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (assetAgeMonths >= 30) // 6 months away from 3 years (i.e., 30+ months old)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }

        //Function to add a new asset
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

            if (OfficeClass.officeData.TryGetValue(office ?? "", out var officeInfo))
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
            Dictionary<string, decimal> currencyToExchangeRate = new Dictionary<string, decimal>
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
            Console.WriteLine($"Purchase Date Set: {purchaseDate:yyyy-MM-dd}");
            Console.ResetColor();

            if (assetType == "Computer")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Computer added as an asset!");
                Console.ResetColor();
                assets.Add(new Computer(brand, model, officeName, purchaseDate, price, currency, localPrice));
                return;
            }
            else if (assetType == "Smartphone")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Smartphone added as an asset!");
                Console.ResetColor();
                assets.Add(new Smartphone(brand, model, officeName, purchaseDate, price, currency, localPrice));
                return;
            }
        }
    }
}