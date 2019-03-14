using System;
using System.Collections.Generic;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

namespace Stocks {
    public partial class Charts : ContentPage {

        public Charts() {
            InitializeComponent();
            CreateCharts();
        }

        void Handle_Appearing(object sender, System.EventArgs e) {
        }

        private void CreateCharts() {
            var entryList = new List<Entry>()
            {
                 new Entry(200)
                {
                    Label = "Grade A",
                    Color = SKColor.Parse("#234432"),
                },
                new Entry(400)
                {
                    Label = "Grade B",
                    Color = SKColor.Parse("#2FF234"),
                },
                new Entry(200)
                {
                    Label = "Grade C",
                    Color = SKColor.Parse("#23CCC2"),
                }
            };
            var past30Days = new LineChart() { Entries = entryList };
            var past100Days = new LineChart() { Entries = entryList };

            Chart1.Chart = past30Days;
            Chart2.Chart = past100Days;
        }
    }
}
