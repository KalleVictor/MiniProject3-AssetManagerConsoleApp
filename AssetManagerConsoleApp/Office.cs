using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagerConsoleApp
{
    internal static class OfficeClass
    {
        // Dictionary to map user input to office names and currencies
        public static Dictionary<string, (string OfficeName, string Currency)> officeData = new()
            {
                { "1", ("USA", "USD") },
                { "2", ("Spain", "EUR") },
                { "3", ("Sweden", "SEK") },
            };
    }
}
