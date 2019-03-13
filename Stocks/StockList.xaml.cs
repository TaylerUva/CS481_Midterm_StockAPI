using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Stocks {
    public partial class StockList : ContentPage {

        List<int> test = new List<int>();

        const string API_KEY = "&apikey=7W0XKA610P4NM7MQ";
        const string END_POINT = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=";

        public StockList() {
            InitializeComponent();
            PopulateListView();
            StockSearch.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);
        }

        void PopulateListView() {
        }

        void Handle_Clicked(object sender, System.EventArgs e) {
            GetSymbolData();
        }

        async void GetSymbolData() {
            string symbol = StockSearch.Text;
            Uri stockApiUri = new Uri(END_POINT + symbol + API_KEY);

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(stockApiUri);

            StocksData stocksData = new StocksData();

            if (response.IsSuccessStatusCode) {
                string jsonContent = await response.Content.ReadAsStringAsync();
                stocksData = JsonConvert.DeserializeObject<StocksData>(jsonContent);
                StocksListView.ItemsSource = stocksData.TimeSeriesDaily;
            }
        }

        void Handle_Completed(object sender, System.EventArgs e) {
            GetSymbolData();
        }
    }
}
