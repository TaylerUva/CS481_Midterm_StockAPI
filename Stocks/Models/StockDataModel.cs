using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microcharts;

namespace Stocks.Models {
    public static class StockDataModel {

        const string API_KEY = "&apikey=7W0XKA610P4NM7MQ";
        const string END_POINT = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=";

        static string m_symbol;

        static Dictionary<string, TimeSeriesDaily> dailyData;

        public static async Task<Dictionary<string, TimeSeriesDaily>> GetSymbolData(string symbol) {
            // PULL DATA
            m_symbol = symbol;

            Uri stockApiUri = new Uri(END_POINT + m_symbol + API_KEY);

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
            return dailyData;
        }

        public static string getHighest() {
            double stockHigh = double.MinValue;
            if (dailyData != null) {
                foreach (KeyValuePair<string, TimeSeriesDaily> item in dailyData) {
                    double dailyHigh = item.Value.The2High;
                    if (dailyHigh > stockHigh) stockHigh = dailyHigh;
                }
                return "Highest: " + stockHigh.ToString("c2");
            }
            return "COULD NOT GET SYMBOL";
        }

        public static string getLowest() {
            double stockLow = double.MaxValue;
            if (dailyData != null) {
                foreach (KeyValuePair<string, TimeSeriesDaily> item in dailyData) {
                    double dailyLow = item.Value.The2High;
                    if (dailyLow < stockLow) stockLow = dailyLow;
                }
                return "Lowest: " + stockLow.ToString("c2");
            }
            return "COULD NOT GET SYMBOL";
        }

        private static List<Entry> GetAsEntries() {
            var entryList = new List<Entry>();
            if (dailyData != null) {
                foreach (KeyValuePair<string, TimeSeriesDaily> item in dailyData) {
                    var newEntry = new Entry((float)item.Value.The2High) {
                        ValueLabel = item.Value.The2High.ToString()
                    };
                    entryList.Add(newEntry);
                }
            }
            return entryList;
        }

        public static List<Entry> GetPastDayRange(int range) {
            var subList = GetAsEntries();
            if (subList.Count > range) return subList.GetRange(0, range);
            return subList;
        }
    }
}
