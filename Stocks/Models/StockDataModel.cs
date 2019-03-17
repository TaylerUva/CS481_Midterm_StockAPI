using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;

namespace Stocks.Models {
    public static class StockDataModel {

        const string API_KEY = "&apikey=7W0XKA610P4NM7MQ";
        const string END_POINT = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=";

        static public string lastSymbol;

        static Dictionary<string, TimeSeriesDaily> dailyData;

        /// <summary>
        /// Gets the symbol data.
        /// </summary>
        /// <returns>The symbol data.</returns>
        /// <param name="symbol">Symbol.</param>
        public static async Task<Dictionary<string, TimeSeriesDaily>> GetSymbolData(string symbol) {
            if (symbol != lastSymbol) {
                // PULL DATA
                Uri stockApiUri = new Uri(END_POINT + symbol + API_KEY);

                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(stockApiUri);

                if (response.IsSuccessStatusCode) {
                    // PULL DATA
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    dailyData = StocksJson.FromJson(jsonContent).TimeSeriesDaily;

                    if (dailyData == null) {
                        return null;
                    }
                }
                lastSymbol = symbol;
            }
            return dailyData;
        }

        /// <summary>
        /// Gets the highest stock prices.
        /// </summary>
        /// <returns>String</returns>
        public static string GetHighestString() {
            if (dailyData != null) {
                return "Highest: " + GetHighestNum().ToString("c2");
            }
            return "";
        }

        /// <summary>
        /// Gets the highest stock prices.
        /// </summary>
        /// <returns>String</returns>
        public static double GetHighestNum() {
            double stockHigh = double.MinValue;
            if (dailyData != null) {
                foreach (KeyValuePair<string, TimeSeriesDaily> item in dailyData) {
                    double dailyHigh = item.Value.The2High;
                    if (dailyHigh > stockHigh) stockHigh = dailyHigh;
                }
            }
            return stockHigh;
        }

        /// <summary>
        /// Gets the highest stock prices.
        /// </summary>
        /// <returns>String</returns>
        public static string GetLowestString() {
            if (dailyData != null) {
                return "Lowest: " + GetLowestNum().ToString("c2");
            }
            return "";
        }

        /// <summary>
        /// Gets the highest stock prices.
        /// </summary>
        /// <returns>String</returns>
        public static double GetLowestNum() {
            double stockLow = double.MaxValue;
            if (dailyData != null) {
                foreach (KeyValuePair<string, TimeSeriesDaily> item in dailyData) {
                    double dailyLow = item.Value.The3Low;
                    if (dailyLow < stockLow) stockLow = dailyLow;
                }
            }
            return stockLow;
        }

        /// <summary>
        /// Gets the past days as far back as range.
        /// </summary>
        /// <returns>The past day range.</returns>
        /// <param name="range">Range.</param>
        public static List<Entry> GetPastDayRange(int range) {
            var subList = GetAsEntries();
            if (subList.Count > range) return subList.GetRange(0, range);
            return subList;
        }

        private static List<Entry> GetAsEntries() {
            var entryList = new List<Entry>();
            if (dailyData != null) {
                int i = 0;
                foreach (KeyValuePair<string, TimeSeriesDaily> item in dailyData) {
                    var newEntry = new Entry((float)item.Value.The2High) {
                        TextColor = SKColors.White,
                        Color = SKColors.White,
                    };

                    // Adds label to every 5th item
                    if (i++ % 5 == 0) {
                        newEntry.ValueLabel = "$" + item.Value.The2High.ToString();
                        newEntry.Label = i.ToString();
                    }

                    entryList.Add(newEntry);
                }
            }
            return entryList;
        }
    }
}
