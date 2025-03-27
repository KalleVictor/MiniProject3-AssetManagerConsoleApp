using AssetManagerConsoleApp;

namespace AssetManagerConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Welcome to Asset Manager Console App");

            //List of assets for debugging
            List<Assets> assets =
            [
            new Smartphone("iPhone", "11", "Spain", new DateTime(2021, 12, 29), 970, "EUR", 801.65m),
            new Computer("HP", "Elitebook 2", "Spain", new DateTime(2024, 6, 1), 1423, "EUR", 1176.03m),
            new Smartphone("iPhone", "11", "Spain", new DateTime(2023, 9, 25), 990, "EUR", 818.18m),
            new Smartphone("iPhone", "X", "Sweden", new DateTime(2024, 7, 15), 1245, "SEK", 10375),
            new Smartphone("Motorola", "Razr", "Sweden", new DateTime(2024, 3, 16), 970, "SEK", 8083.33m),
            new Computer("HP", "Elitebook 2", "Sweden", new DateTime(2023, 10, 2), 588, "SEK", 4900),
            new Computer("ASUS", "W234", "USA", new DateTime(2022, 2, 21), 1200, "USD", 1200),
            new Computer("Lenovo", "Yoga 730", "USA", new DateTime(2022, 8, 28), 835, "USD", 835),
            new Computer("Lenovo", "Yoga 530", "USA", new DateTime(2019, 5, 21), 1030, "USD", 1030),
            ];

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


            static void MainMenu(List<Assets> assets)
            {
                Console.WriteLine("Press (1) to add an asset, Sort the list by (2) Purchase Date, (3) Office or (4) by Class. Press (Q) to Exit the app.");
                string? option = Console.ReadLine();
                if (option == "1")
                {
                    AddAsset(assets);
                    MainMenu(assets);
                }
                else if (option == "2")
                {
                    MainHeader();
                    DisplayAssetsByPurchaseDate(assets);
                    MainMenu(assets);
                }
                else if (option == "3")
                {
                    MainHeader();
                    DisplayAssetsByOffice(assets);
                    MainFooter();
                    MainMenu(assets);
                }
                else if (option == "4")
                {
                    MainHeader();
                    DisplayAssetsByClass(assets);
                    MainFooter();
                    MainMenu(assets);
                }
                else if (option == "Q")
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

            //Function to add an asset to the list
            static void AddAsset(List<Assets> assets)
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
                if (office == "1")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("USA selected.");
                    Console.ResetColor();
                    office = "USA";
                }
                else if (office == "2")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Spain selected.");
                    Console.ResetColor();
                    office = "Spain";
                }
                else if (office == "3")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Sweden selected.");
                    Console.ResetColor();
                    office = "Sweden";
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid office location. Please choose one of the three locations available.");
                    Console.ResetColor();
                    return;
                }

                // **Step 1: Assign Currency Based on Office Location**
                Dictionary<string, string> officeToCurrency = new()
        {
            { "sweden", "SEK" },
            { "usa", "USD" },
            { "spain", "EUR" },
        };
                string? currency;
                if (officeToCurrency.TryGetValue(office, out string? foundCurrency))
                {
                    currency = foundCurrency;
                }
                else
                {
                    currency = "EUR"; // Default to EUR
                }

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
                    assets.Add(new Computer(brand, model, office, purchaseDate, price, currency, localPrice));
                    return;
                }
                else if (assetType == "Smartphone")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Smartphone added as an asset!");
                    Console.ResetColor();
                    assets.Add(new Smartphone(brand, model, office, purchaseDate, price, currency, localPrice));
                    return;
                }
            }


            //Header for the table
            static void MainHeader()
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
            static void TableColor()
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            //List all assets   
            static void DisplayAssets(List<Assets> assets)
            {
                TableColor();
                // Get current date
                DateTime currentDate = DateTime.Now;
                foreach (var asset in assets)
                {
                    AgeOfAsset(asset);
                    Console.WriteLine(
                        asset.Office.PadRight(15) +
                        asset.AssetTag.PadRight(20) +
                        asset.Brand.PadRight(10) +
                        asset.Model.PadRight(15) +
                        asset.Price.ToString("F2").PadRight(15) +  //Ensure two decimal places 
                        asset.Currency + " " + asset.LocalPrice.ToString("F2").PadRight(15) +
                        asset.PurchaseDate.ToString("yyyy-MM-dd") + " ".PadRight(13)
                    );
                }
                Console.ResetColor();
            }

            //List all assets sorted by Office
            static void DisplayAssetsByOffice(List<Assets> assets)
            {
                Console.Clear();
                Console.WriteLine("Assets sorted by Office (Country)");
                MainHeader();
                TableColor();
                // Get current date
                DateTime currentDate = DateTime.Now;
                foreach (var asset in assets.OrderBy(x => x.Office))
                {
                    AgeOfAsset(asset);
                    Console.WriteLine(
                        asset.Office.PadRight(15) +
                        asset.AssetTag.PadRight(20) +
                        asset.Brand.PadRight(10) +
                        asset.Model.PadRight(15) +
                        asset.Price.ToString("F2").PadRight(15) +  //Ensure two decimal places 
                        asset.Currency + " " + asset.LocalPrice.ToString("F2").PadRight(15) +
                        asset.PurchaseDate.ToString("yyyy-MM-dd") + " ".PadRight(13)
                    );
                }
                Console.ResetColor();
            }

            //List all assets sorted by Purchase Date
            static void DisplayAssetsByPurchaseDate(List<Assets> assets)
            {
                Console.Clear();
                Console.WriteLine("Assets sorted by Purchase Date");
                MainHeader();
                TableColor();
                // Get current date
                DateTime currentDate = DateTime.Now;
                foreach (var asset in assets.OrderBy(x => x.PurchaseDate))
                {
                    AgeOfAsset(asset);
                    Console.WriteLine(
                        asset.Office.PadRight(15) +
                        asset.AssetTag.PadRight(20) +
                        asset.Brand.PadRight(10) +
                        asset.Model.PadRight(15) +
                        asset.Price.ToString("F2").PadRight(15) +  //Ensure two decimal places 
                        asset.Currency + " " + asset.LocalPrice.ToString("F2").PadRight(15) +
                        asset.PurchaseDate.ToString("yyyy-MM-dd") + " ".PadRight(13)
                    );
                }
                Console.ResetColor();
            }
            //List all assets sorted by class
            static void DisplayAssetsByClass(List<Assets> assets)
            {
                Console.Clear();
                Console.WriteLine("Assets sorted by Class.");
                MainHeader();
                TableColor();
                foreach (var asset in assets.OrderBy(x => x.AssetTag))
                {
                    AgeOfAsset(asset);
                    Console.WriteLine(
                        asset.Office.PadRight(15) +
                        asset.AssetTag.PadRight(20) +
                        asset.Brand.PadRight(10) +
                        asset.Model.PadRight(15) +
                        asset.Price.ToString("F2").PadRight(15) +  //Ensure two decimal places 
                        asset.Currency + " " + asset.LocalPrice.ToString("F2").PadRight(15) +
                        asset.PurchaseDate.ToString("yyyy-MM-dd") + " ".PadRight(13)
                    );
                }
                Console.ResetColor();
            }

            //Add function to calculate asset age
            static void AgeOfAsset(Assets asset)
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
            //Footer for the table
            static void MainFooter()
            {

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Blue;
                //Present to total amount of assets 

                Console.ResetColor();
            }
        }
    }
}


