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

            //Table appearance - Header

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

            //Function to display all assets

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

            //Table Appearance - Footer
            //For additional functions, such as include the total amount of assets and the total cost of all assets
            static void MainFooter()
            {

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Blue;
                //Present to total amount of assets 

                Console.ResetColor();
            }

            //Main menu function

            static void MainMenu(List<Assets> assets)
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
        }
    }
}