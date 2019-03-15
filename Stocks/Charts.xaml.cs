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
            if (string.IsNullOrEmpty(StockSearch.Text)) {
                StockSearch.Text = oldSymbol;
                await DisplayAlert("Empty Search", "Cannot leave stock search empty!", "Close");
                return;
            }
            await PullData();
        }

        async Task PullData() {
            LoadingIcon.IsRunning = true;


            string newSymbol = StockSearch.Text;

            stockData = await StockDataModel.GetSymbolData(newSymbol);

            Chart1.Chart = new LineChart() { Entries = StockDataModel.GetPastDayRange(30) };
            Chart2.Chart = new LineChart() { Entries = StockDataModel.GetPastDayRange(100) };

            oldSymbol = newSymbol;

            LoadingIcon.IsRunning = false;

            if (stockData == null) await DisplayAlert("Stock Not Found", "No stock matching symbol:\n\"" + newSymbol + "\"", "Close");
        }
    }
}
