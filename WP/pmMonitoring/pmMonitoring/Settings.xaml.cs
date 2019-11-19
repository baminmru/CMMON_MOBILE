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
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();

            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains("Url1") == true)
            {
                Url1.Text = (string)System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["Url1"];    
            }
            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains("login") == true)
            {
                Login.Text = (string)System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["login"];
            }
            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains("password") == true)
            {
                Password.Text = (string)System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["password"];
            }
            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains("Url2") == true)
            {
                Url2.Text = (string)System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["Url2"];
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Сохр 
            System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["login"] = Login.Text;

            System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["password"] = Password.Text;

            System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["Url1"] = Url1.Text;

            System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["Url2"] = Url2.Text;
            System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["Url3"] = Url1.Text;

            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative)); 
        
        
        }

        
    }
}