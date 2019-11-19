using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace pmMonitoring
{
    public partial class WebView : PhoneApplicationPage
    {
        string Url;

        public WebView()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.Portrait | SupportedPageOrientation.Landscape;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);

            if (NavigationContext.QueryString.ContainsKey("text"))
            {
                Url = NavigationContext.QueryString["text"].ToString();
                Console.WriteLine(Url);
            }

        }

        private void webview_Loaded(object sender, RoutedEventArgs e)
        {
            webview.Navigate(new Uri(Url, UriKind.Absolute));
          
        }
    }
}