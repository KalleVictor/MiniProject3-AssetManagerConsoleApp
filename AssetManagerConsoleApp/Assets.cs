namespace AssetManagerConsoleApp
{
    // Main Class AssetManagerConsoleApp
    public class Assets
    {
        public string Office { get; set; } = string.Empty;
        public string AssetTag { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal LocalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

    // Child Class - Computer
    class Computer : Assets
    {
        public Computer(string brand, string model, string office, DateTime purchaseDate, decimal price, string currency, decimal localPrice)
        {
            Office = office;
            AssetTag = this.GetType().Name;
            Brand = brand;
            Model = model;
            Price = price;
            Currency = currency;
            LocalPrice = localPrice;
            PurchaseDate = purchaseDate;
        }
    }

    // Child Class - Smartphone
    class Smartphone : Assets
    {
        public Smartphone(string brand, string model, string office, DateTime purchaseDate, decimal price, string currency, decimal localPrice)
        {
            Office = office;
            Brand = brand;
            AssetTag = this.GetType().Name;
            Model = model;
            Price = price;
            Currency = currency;
            LocalPrice = localPrice;
            PurchaseDate = purchaseDate;
        }
    }
}