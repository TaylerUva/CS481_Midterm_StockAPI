using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

namespace Stocks {
    public partial class MainPage : TabbedPage {
        public MainPage() {
            InitializeComponent();
            UITabBar.Appearance.TintColor = UIColor.FromRGB(0, 128, 128);
        }
    }
}
