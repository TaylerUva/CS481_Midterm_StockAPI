using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Stocks.Models {
    public static class StockDataModel {

        const string API_KEY = "&apikey=7W0XKA610P4NM7MQ";
        const string END_POINT = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=";

        static string symbol;

        static private Dictionary<string, TimeSeriesDaily> dailyData;

        public static async Task<int> GetSymbolData(Entry StockSearch) {
            if (StockSearch.Text == null) {
                return 1;
            }

            // PULL DATA
            symbol = StockSearch.Text.ToUpper();

            Uri stockApiUri = new Uri(END_POINT + symbol + API_KEY);

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(stockApiUri);

            if (response.IsSuccessStatusCode) {
                // PULL DATA
                string jsonContent = await response.Content.ReadAsStringAsync();
                dailyData = StocksData.FromJson(jsonContent).TimeSeriesDaily;

                if (dailyData == null) {
                    return 2;
                }

            }
            return 0;
        }

        public static Dictionary<string, TimeSeriesDaily> GetDailyDataList() {
            return dailyData;
        }

        public static string getHighest() {
            double stockHigh = double.MinValue;
            if (dailyData != null) {
                foreach (KeyValuePair<string, TimeSeriesDaily> item in dailyData) {
                    double dailyHigh = item.Value.The2High;
                    if (dailyHigh > stockHigh) stockHigh = dailyHigh;
                }
            }
            return "Highest: " + stockHigh.ToString("c2");
        }

        public static string getLowest() {
            double stockLow = double.MaxValue;
            if (dailyData != null) {
                foreach (KeyValuePair<string, TimeSeriesDaily> item in dailyData) {
                    double dailyLow = item.Value.The2High;
                    if (dailyLow < stockLow) stockLow = dailyLow;
                }
            }
            return "Lowest: " + stockLow.ToString("c2");
        }
    }
}
