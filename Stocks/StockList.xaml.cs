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

        public StockList() {
            InitializeComponent();
            StockSearch.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);
        }

        async void GetSymbolData() {
            string symbol = StockSearch.Text;
            Uri stockApiUri = new Uri(END_POINT + symbol + API_KEY);

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(stockApiUri);

            if (response.IsSuccessStatusCode) {
                string jsonContent = await response.Content.ReadAsStringAsync();
                var stocksData = StocksData.FromJson(jsonContent);
                StocksListView.ItemsSource = stocksData.TimeSeriesDaily;
            }
        }

        void Handle_Clicked(object sender, System.EventArgs e) {
            GetSymbolData();
        }

        void Handle_Completed(object sender, System.EventArgs e) {
            GetSymbolData();
        }
    }
}
