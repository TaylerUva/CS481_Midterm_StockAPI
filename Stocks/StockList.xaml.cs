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
            if (string.IsNullOrEmpty(StockSearch.Text)) {
                StockSearch.Text = oldSymbol;
                await DisplayAlert("Empty Search", "Cannot leave stock search empty!", "Close");
                return;
            }
            await PullData();
        }

        async Task PullData() {
            StocksListView.IsRefreshing = true;

            string newSymbol = StockSearch.Text;

            stockData = await StockDataModel.GetSymbolData(newSymbol);

            StocksListView.ItemsSource = stockData;
            HighestLabel.Text = StockDataModel.GetHighest();
            LowestLabel.Text = StockDataModel.GetLowest();

            oldSymbol = newSymbol;

            StocksListView.IsRefreshing = false;

            if (stockData == null) await DisplayAlert("Stock Not Found", "No stock matching symbol:\n\"" + newSymbol + "\"", "Close");
        }
    }
}

