using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Stocks;
using Xamarin.Forms;

namespace Stocks {
    public partial class StockList : ContentPage {

        const string API_KEY = "&apikey=7W0XKA610P4NM7MQ";
        const string END_POINT = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=";

        Dictionary<string, TimeSeriesDaily> dailyData;

        public StockList() {
            InitializeComponent();
            StockSearch.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);
        }

        async void GetSymbolData() {
            if (StockSearch.Text == null) {
                await DisplayAlert("Empty Search", "Cannot leave stock search empty!", "Close");
                return;
            }

            LoadingIcon.IsRunning = true;
            // PULL DATA
            string symbol = StockSearch.Text.ToUpper();


            Uri stockApiUri = new Uri(END_POINT + symbol + API_KEY);

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(stockApiUri);

            if (response.IsSuccessStatusCode) {
                // PULL DATA
                string jsonContent = await response.Content.ReadAsStringAsync();
                dailyData = StocksData.FromJson(jsonContent).TimeSeriesDaily;

                if (dailyData == null) {
                    LoadingIcon.IsRunning = false;
                    await DisplayAlert("Stock Not Found", "No stock matching symbol:\n\"" + symbol + "\"", "Close");
                    return;
                }

                // USE DATA
                StocksListView.ItemsSource = dailyData;
                HighestLabel.Text = "Highest: " + getHighest().ToString("c2");
                LowestLabel.Text = "Lowest: " + getLowest().ToString("c2");
                LoadingIcon.IsRunning = false;
            }
        }

        private double getHighest() {
            double stockHigh = double.MinValue;
            foreach (KeyValuePair<string, TimeSeriesDaily> item in dailyData) {
                double dailyHigh = item.Value.The2High;
                if (dailyHigh > stockHigh) stockHigh = dailyHigh;
            }
            return stockHigh;
        }

        private double getLowest() {
            double stockLow = double.MaxValue;
            foreach (KeyValuePair<string, TimeSeriesDaily> item in dailyData) {
                double dailyLow = item.Value.The2High;
                if (dailyLow < stockLow) stockLow = dailyLow;
            }
            return stockLow;
        }

        void Handle_Clicked(object sender, System.EventArgs e) {
            GetSymbolData();
        }

        void Handle_Completed(object sender, System.EventArgs e) {
            GetSymbolData();
        }
    }
}
