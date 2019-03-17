using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Stocks.iOS {
    public class Application {
        // This is the main entry point of the application.
        static void Main(string[] args) {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UITabBar.Appearance.TintColor = UIColor.FromRGB(0, 128, 128);
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
