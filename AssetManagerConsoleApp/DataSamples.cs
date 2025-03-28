using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagerConsoleApp
{
    internal class DataSamples
    {
        //Function to add sample assets to the list
        public static void AddSampleAssets(List<Assets> assets)
        {
            assets.Add(new Computer("Dell", "Latitude", "USA", new DateTime(2024, 1, 1), 1000, "USD", 1000));
            assets.Add(new Computer("HP", "Elitebook", "Spain", new DateTime(2022, 7, 1), 900, "EUR", 765));
            assets.Add(new Computer("Lenovo", "ThinkPad", "Sweden", new DateTime(2022, 3, 1), 800, "SEK", 8000));
            assets.Add(new Smartphone("Apple", "iPhone", "USA", new DateTime(2024, 4, 1), 1200, "USD", 1200));
        }

        //Add more sample assets to the list using a different method
        public static void AddSampleAssets2(List<Assets> assets)
        {
            List<Assets> sampleAssets = new()
            {
                new Smartphone("Apple", "iPhone 11", "Spain", new DateTime(2022, 8, 29), 970, "EUR", 801.65m),
                new Computer("HP", "Elitebook 2", "Spain", new DateTime(2024, 6, 1), 1423, "EUR", 1176.03m),
                new Smartphone("Apple", "iPhone 11", "Spain", new DateTime(2023, 9, 25), 990, "EUR", 818.18m),
                new Smartphone("Apple", "iPhone X", "Sweden", new DateTime(2024, 7, 15), 1245, "SEK", 10375),
            };
            assets.AddRange(sampleAssets);
        }
    }
}