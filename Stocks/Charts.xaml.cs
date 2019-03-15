using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microcharts;
using Stocks.Models;
using Xamarin.Forms;
using SkiaSharp;

namespace Stocks {
    public partial class Charts : ContentPage {

        Dictionary<string, TimeSeriesDaily> stockData;

        string oldSymbol;

        public Charts() {
            InitializeComponent();
            StockSearch.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);
        }

        async void Handle_Appearing(object sender, System.EventArgs e) {
            StockSearch.Text = StockDataModel.lastSymbol;
            if (StockSearch.Text != oldSymbol) {
                await PullData();
            }
        }

        async void RequestStockData(object sender, System.EventArgs e) {
            if (string.IsNullOrEmpty(StockSearch.Text)) {
                StockSearch.Text = oldSymbol;
                StockDataModel.lastSymbol = oldSymbol;
                await DisplayAlert("Empty Search", "Cannot leave stock search empty!", "Close");
                return;
            }
            await PullData();
        }

        async Task PullData() {
            LoadingIcon.IsRunning = true;

            string newSymbol = StockSearch.Text;

            stockData = await StockDataModel.GetSymbolData(newSymbol);

            if (stockData == null) {
                StockSearch.Text = oldSymbol;
                StockDataModel.lastSymbol = oldSymbol;
                await DisplayAlert("Stock Not Found", "No stock matching symbol:\n\"" + newSymbol + "\"", "Close");
            } else {
                Label30Days.IsVisible = true;
                Label100Days.IsVisible = true;


                Chart30Days.Chart = new LineChart() { Entries = StockDataModel.GetPastDayRange(30) };
                Chart100Days.Chart = new LineChart() { Entries = StockDataModel.GetPastDayRange(100) };

                Chart30Days.Chart.BackgroundColor = SKColors.Transparent;
                Chart100Days.Chart.BackgroundColor = SKColors.Transparent;

                HighestLabel.Text = StockDataModel.GetHighest();
                LowestLabel.Text = StockDataModel.GetLowest();
                oldSymbol = newSymbol;
            }

            LoadingIcon.IsRunning = false;

        }
    }
}
