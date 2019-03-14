using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Stocks.Models;
using Xamarin.Forms;

namespace Stocks {
    public partial class StockList : ContentPage {

        Dictionary<string, TimeSeriesDaily> stockData;

        string oldSymbol;

        public StockList() {
            InitializeComponent();
            StockSearch.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);
        }

        async void Handle_Appearing(object sender, System.EventArgs e) {
            StockSearch.Text = StockDataModel.GetSymbol();
            if (StockSearch.Text != oldSymbol) {
                System.Diagnostics.Debug.WriteLine("CHANGED!");
                await PullData(StockSearch.Text);
                oldSymbol = StockSearch.Text;
            }
        }

        async void RequestStockData(object sender, System.EventArgs e) {
            StocksListView.IsRefreshing = true;
            await PullData(StockSearch.Text);
            await ErrorMessages();
            StocksListView.IsRefreshing = false;
        }

        async Task PullData(string symbol) {
            stockData = await StockDataModel.GetSymbolData(symbol);

            StocksListView.ItemsSource = stockData;
            HighestLabel.Text = StockDataModel.GetHighest();
            LowestLabel.Text = StockDataModel.GetLowest();
        }

        async Task ErrorMessages() {
            string symbol = StockDataModel.GetSymbol();
            if (stockData == null) {
                if (string.IsNullOrEmpty(symbol)) {
                    await DisplayAlert("Empty Search", "Cannot leave stock search empty!", "Close");
                } else await DisplayAlert("Stock Not Found", "No stock matching symbol:\n\"" + symbol + "\"", "Close");
            }
        }
    }
}
