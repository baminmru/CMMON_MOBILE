using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using pmMonitoring.Resources;
using System.Xml.Linq;
using Microsoft.Phone.Tasks;

using Microsoft.Phone.Scheduler;

namespace pmMonitoring
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        string RSS;
        string RSS2;
        string RSS3;
        string TxtPassword;
        string TxtLogin;
       
        string RSSString = "";
        string RSSString12 = "";
        bool CanChangeUrl;
        bool StartUrl2;
        bool StartUrl1;

        int counterror;

        const string ToastAgentName = "Agent-Toast";

        PeriodicTask toastPeriodicTask;

        // Конструктор
        public MainPage()
        {
            InitializeComponent();

            CanChangeUrl = true;
            StartUrl2 = false;
            StartUrl1 = false;
            counterror = 0;
            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains("Url1") == true)
            {
                RSS = (string)System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["Url1"];
            }
            else
            {
                RSS = "";
            }
            
            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains("Url2") == true)
            {
                RSS2 = (string)System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["Url2"];
            }
            else
            {
                RSS2 = "";
            }

            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains("Url3") == true)
            {
                RSS3 = (string)System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["Url3"];
            }
            else
            {
                RSS3 = "";
            }
            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains("login") == true)
            {
                TxtLogin = (string)System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["login"];
            }
            else
            {
                TxtLogin = "";
            }

            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains("password") == true)
            {
                TxtPassword = (string)System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["password"];
            }
            else
            {
                TxtPassword = "";
            }

            LoadRSS(RSS3);          
           


            // Пример кода для локализации ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Пример кода для сборки локализованной панели ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Установка в качестве ApplicationBar страницы нового экземпляра ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Создание новой кнопки и установка текстового значения равным локализованной строке из AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Создание нового пункта меню с локализованной строкой из AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        private void LoadRSS(string url)
        {
            try
            {
                RequestRSS(url);
            }
            catch
            {
                MessageBox.Show("Введите Url адрес");
            }
        }
       
        private void RequestRSS(string feedurl)
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(new Uri(feedurl+"?username="+TxtLogin+"&password="+TxtPassword+"&disablecache=" + Environment.TickCount));

            System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["Url3"] = feedurl;
            System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Save();
        }
       
        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    RSSString = e.Result;

                    XElement twitterElements = XElement.Parse(e.Result);

                    var postList =
                        from tweet in twitterElements.Descendants("item")
                        select new PostMessage
                        {
                            title = tweet.Element("title").Value,
                            pubDate = tweet.Element("pubDate").Value,
                            link = tweet.Element("link").Value
                        };
                  
                    RssList.ItemsSource = postList;


                   /* if ((!CanChangeUrl)&&(!StartUrl2))
                    {
                        StartShedukeAgentWithRestart();
                        MessageBox.Show("Старт агента с перезагрузкой");
                       ] StartUrl2=true;
                    }*/
                     PeriodicTask myPeriodicTask = ScheduledActionService.Find(ToastAgentName) as PeriodicTask;

                     if (myPeriodicTask == null)
                     {
                         StartShedukeAgent();
                         MessageBox.Show("Старт агента без перезагрузки");
                     }
                }

                catch
                {
                    MessageBox.Show("Не удаёться разобрать XML из указанного адреса");
                }
            }
            else {

                if (counterror <= 2)
                {
                    if (StartUrl1)
                    {
                        RSS3 = RSS;
                        LoadRSS(RSS3);
                        StartUrl2 = true;
                        MessageBox.Show("смена адреса 2");
                        counterror++;

                    }
                    else
                    {
                        RSS3 = RSS2;
                        LoadRSS(RSS3);
                        StartUrl1 = true;
                        MessageBox.Show("смена адреса");
                        counterror++;
                    }
                
               if((StartUrl1)&&(StartUrl2))
                {
                    MessageBox.Show("Недоступны оба адреса");
                    counterror++;
                    StopAgent();
                }
                }
            }
        }
       
        private void RssList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //WebBrowserTask webTask = new WebBrowserTask();
             
            //string parametrs = "menubar=yes,location=yes,resizable=yes,scrollbars=yes,status=yes";
            //window.open("http://ya.ru/", "Yandex", params)
           // webTask.Uri = new Uri(((PostMessage)(RssList.SelectedItem)).link);
            //webTask.Uri = new Uri("http://ya.ru");
            //webTask.Show(parametrs);
           string Link = ((PostMessage)(RssList.SelectedItem)).link;
           NavigationService.Navigate(new Uri("/Webview.xaml?text=" + Link, UriKind.Relative));


        }

        private void RssList_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void SettingsButtonS_Click(object sender, System.EventArgs e)
        {
        	 NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative)); 
        }

        private void RefreshButtonS_Click(object sender, System.EventArgs e)
        {
        	RssList.ItemsSource = null;
            LoadRSS(RSS3);
            CanChangeUrl = true;



        }

        private void CloseButtonS_Click(object sender, System.EventArgs e)
        {
            StopAgent();
                          
        }
        
        

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void StartShedukeAgent()
        {
            PeriodicTask myPeriodicTask = ScheduledActionService.Find(ToastAgentName) as PeriodicTask;

             myPeriodicTask = new PeriodicTask(ToastAgentName);
            myPeriodicTask.Description = "Agent-Toast";
            
            try
            {
                ScheduledActionService.Add(myPeriodicTask);



#if DEBUG
                ScheduledActionService.LaunchForTest(ToastAgentName, TimeSpan.FromSeconds(60));
#endif
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно создать сервис:" + ex.Message);
            }

        }

        private void StartShedukeAgentWithRestart()
        {
            PeriodicTask myPeriodicTask = ScheduledActionService.Find(ToastAgentName) as PeriodicTask;

            if (myPeriodicTask != null)
            {
                try
                {
                    ScheduledActionService.Remove(ToastAgentName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно удалить ранее созданный сервис:" + ex.Message);
                }
            }

            myPeriodicTask = new PeriodicTask(ToastAgentName);
            myPeriodicTask.Description = "Agent-Toast";

            try
            {
                ScheduledActionService.Add(myPeriodicTask);



#if DEBUG
                ScheduledActionService.LaunchForTest(ToastAgentName, TimeSpan.FromSeconds(60));
#endif
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно создать сервис:" + ex.Message);
            }

        }

        private void RemoveAgent(string name)
        {
            try
            {
                ScheduledActionService.Remove(name);
            }
            catch (Exception)
            {
                // Handle exception code here.
            }

            
        }

        public void StopAgent()
        {
            toastPeriodicTask = ScheduledActionService.Find(ToastAgentName) as PeriodicTask;


            // If either periodic agent is running, end it.
            // Otherwise, run the periodic agent based on the radio
            // button selection.
            if (toastPeriodicTask != null)
            {
                RemoveAgent(ToastAgentName);
                MessageBox.Show("Уведомления остановлены");
            }
        }
    }
}