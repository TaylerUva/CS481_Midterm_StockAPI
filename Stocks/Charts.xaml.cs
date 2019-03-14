using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using Stocks.Models;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

namespace Stocks {

    public partial class Charts : ContentPage {

        Dictionary<string, TimeSeriesDaily> stockData;

        string oldSymbol;

        public Charts() {
            InitializeComponent();
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
            LoadingIcon.IsRunning = true;
            await PullData(StockSearch.Text);
            await ErrorMessages();
            LoadingIcon.IsRunning = false;
        }

        async Task PullData(string symbol) {
            stockData = await StockDataModel.GetSymbolData(symbol);

            Chart1.Chart = new LineChart() { Entries = StockDataModel.GetPastDayRange(30) };
            Chart2.Chart = new LineChart() { Entries = StockDataModel.GetPastDayRange(100) };
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
