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
            int exitCode = await StockDataModel.GetSymbolData(StockSearch);

            switch (exitCode) {
            case 1: await DisplayAlert("Empty Search", "Cannot leave stock search empty!", "Close"); break;
            case 2: await DisplayAlert("Stock Not Found", "No stock matching symbol:\n\"" + StockSearch.Text.ToUpper() + "\"", "Close"); break;
            }
            StocksListView.ItemsSource = StockDataModel.GetDailyDataList();
            HighestLabel.Text = StockDataModel.getHighest();
            LowestLabel.Text = StockDataModel.getLowest();
            StocksListView.IsRefreshing = false;
        }
    }
}
