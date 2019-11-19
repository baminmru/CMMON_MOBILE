using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using System.Net;
using System.IO.IsolatedStorage;
using System.IO;

namespace ScheduledTaskAgent1
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// Конструктор ScheduledAgent, инициализирует обработчик UnhandledException
        /// </remarks>


        //const string RSS = "http://abolsoft.ru/rss/rss.php";
        string RSS3;
        const string RSSFileName = "rss.xml";
        string RSSStringnow = "";
        string RSSStringprev = "";
        string RSSString = "";       
        
        string TxtPassword;
        string TxtLogin;


        static ScheduledAgent()
        {
            // Подпишитесь на обработчик управляемых исключений
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Код для выполнения на необработанных исключениях
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // Произошло необработанное исключение; перейти в отладчик
                Debugger.Break();
            }
        }

        /// <summary>
        /// Агент, запускающий назначенное задание
        /// </summary>
        /// <param name="task">
        /// Вызванная задача
        /// </param>
        /// <remarks>
        /// Этот метод вызывается при запуске периодических или ресурсоемких задач
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains("Url3") == true)
            {
                RSS3 = (string)System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["Url3"];
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
            LoadRSS();


            // Launch a toast to show that the agent is running.
            // The toast will not be shown if the foreground application is running.

            // If debugging is enabled, launch the agent again in one minute.
#if DEBUG
        ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(60));
#endif

            // Call NotifyComplete to let the system know the agent is done working.
       
        }

        private void LoadRSS()
        {
            if (IsRSSExist())
            {
                try
                {
                    RequestRSS();
                }
                catch {
                    MessageBox.Show("Нет Url адреса для агента...");
                }
            }
            else
            {
                try
                {
                    RequestRSSFirsttime();
                }
                catch
                {
                    MessageBox.Show("Нет Url адреса для агента...");
                }
            }
        }



        private void RequestRSS()
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(new Uri(RSS3 + "?username=" + TxtLogin + "&password=" + TxtPassword));

        }
        private void RequestRSSFirsttime()
        {
            WebClient clientFT = new WebClient();
            clientFT.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompletedFT);
            clientFT.DownloadStringAsync(new Uri(RSS3 + "?username=" + TxtLogin + "&password=" + TxtPassword));

        }

        void SaveRSSToIsolatedStorage(string RSSText)
        {
            IsolatedStorageFile rssFileStorage = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream rssFileStream = rssFileStorage.CreateFile(RSSFileName);

            StreamWriter sw = new StreamWriter(rssFileStream);
            sw.Write(RSSText);
            sw.Close();

            rssFileStream.Close();
        }

        string LoadRSSFromIsolatedStorage()
        {
            IsolatedStorageFile rssFileStorage = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream rssFileStream = rssFileStorage.OpenFile(RSSFileName, System.IO.FileMode.Open);

            StreamReader sr = new StreamReader(rssFileStream);
            string RSS = sr.ReadToEnd();
            sr.Close();
            rssFileStream.Close();

            return RSS;
        }

        bool IsRSSExist()
        {
            IsolatedStorageFile rssFileStorage = IsolatedStorageFile.GetUserStoreForApplication();
            return rssFileStorage.FileExists(RSSFileName);
        }


        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                RSSStringnow = e.Result;
                RSSStringprev = LoadRSSFromIsolatedStorage();
                if (RSSStringnow != RSSStringprev)
                {
                    SaveRSSToIsolatedStorage(RSSStringnow);
                    ShellToast toast = new ShellToast();
                    toast.Title = "Сообщение от мониторинга";
                    toast.Content = "Произошли изменения";
                    toast.Show();
                }
                else
                {
                    /*ShellToast toast = new ShellToast();
                    toast.Title = "Сообщение от мониторинга";
                    toast.Content = "Нет изменений";
                    toast.Show();*/
                }
            }
            NotifyComplete();
        }

        void client_DownloadStringCompletedFT(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                RSSString = e.Result;
                SaveRSSToIsolatedStorage(RSSString);
                /*ShellToast toast = new ShellToast();
                toast.Title = "Сообщение";
                toast.Content = "start";
                toast.Show();*/
            }
            NotifyComplete();
        }
    
    
    }
}