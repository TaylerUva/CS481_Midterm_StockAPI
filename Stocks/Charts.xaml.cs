using System;
using System.Collections.Generic;
using Microcharts;
using SkiaSharp;
using Stocks.Models;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

namespace Stocks {
    public partial class Charts : ContentPage {

        public Charts() {
            InitializeComponent();
        }

        void Handle_Appearing(object sender, System.EventArgs e) {
            CreateCharts();
        }

        private void CreateCharts() {
            var past30Days = new LineChart() { Entries = StockDataModel.GetAsEntries() };
            //var past100Days = new LineChart() { Entries = entryList };

            Chart1.Chart = past30Days;
            //Chart2.Chart = past100Days;
        }
    }
}
