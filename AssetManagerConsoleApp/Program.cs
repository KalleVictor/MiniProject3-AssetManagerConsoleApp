using AssetManagerConsoleApp;

while (true)
{
    LiveCurrency.FetchRates(); // Live Exchange rate
    AssetManager.Start();
}