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
                System.Diagnostics.Debug.WriteLine("CHANGED!"); //*************
                await PullData();
            }
        }

        async void RequestStockData(object sender, System.EventArgs e) {
            await PullData();
            await ErrorMessages();
        }

        async Task PullData() {
            LoadingIcon.IsRunning = true;

            stockData = await StockDataModel.GetSymbolData(StockSearch.Text);

            Chart1.Chart = new LineChart() { Entries = StockDataModel.GetPastDayRange(30) };
            Chart2.Chart = new LineChart() { Entries = StockDataModel.GetPastDayRange(100) };

            oldSymbol = StockSearch.Text;

            LoadingIcon.IsRunning = false;
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
