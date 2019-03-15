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
                System.Diagnostics.Debug.WriteLine("CHANGED!"); //*************
                await PullData();
            }
        }

        async void RequestStockData(object sender, System.EventArgs e) {
            await PullData();
            await ErrorMessages();
        }

        async Task PullData() {
            StocksListView.IsRefreshing = true;

            stockData = await StockDataModel.GetSymbolData(StockSearch.Text);

            StocksListView.ItemsSource = stockData;
            HighestLabel.Text = StockDataModel.GetHighest();
            LowestLabel.Text = StockDataModel.GetLowest();

            oldSymbol = StockSearch.Text;

            StocksListView.IsRefreshing = false;
        }

        async Task ErrorMessages() {
            string symbol = StockSearch.Text;
            if (stockData == null) {
                if (string.IsNullOrEmpty(symbol)) {
                    await DisplayAlert("Empty Search", "Cannot leave stock search empty!", "Close");
                } else await DisplayAlert("Stock Not Found", "No stock matching symbol:\n\"" + symbol + "\"", "Close");
            }
        }
    }
}
