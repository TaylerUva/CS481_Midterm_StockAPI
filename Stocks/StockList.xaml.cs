using System;
using System.Collections.Generic;
using System.Net.Http;
using Stocks.Models;
using Xamarin.Forms;

namespace Stocks {
    public partial class StockList : ContentPage {

        public StockList() {
            InitializeComponent();
            StockSearch.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);
        }

        void Handle_Clicked(object sender, System.EventArgs e) {
            PullData();
        }

        void Handle_Completed(object sender, System.EventArgs e) {
            PullData();
        }

        async void PullData() {
            StocksListView.IsRefreshing = true;

            string symbol = StockSearch.Text;
            var symbolData = await StockDataModel.GetSymbolData(symbol);
            if (symbolData != null) {
                StocksListView.ItemsSource = symbolData;
                HighestLabel.Text = StockDataModel.getHighest();
                LowestLabel.Text = StockDataModel.getLowest();
            } else {
                if (string.IsNullOrEmpty(symbol)) {
                    await DisplayAlert("Empty Search", "Cannot leave stock search empty!", "Close");
                } else await DisplayAlert("Stock Not Found", "No stock matching symbol:\n\"" + symbol + "\"", "Close");
            }
            StocksListView.IsRefreshing = false;
        }
    }
}
