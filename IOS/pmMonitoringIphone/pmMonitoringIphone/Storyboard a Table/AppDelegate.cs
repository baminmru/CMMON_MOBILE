using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using System.Threading.Tasks;
//using Newtonsoft.Json;
using System.IO;
//using System.Net.Http;

using System.Xml;
using System.Xml.XPath;

namespace Storyboard
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		string strtek;
		string strprev;
		private string FeedUrl;
		private NetAdress availableAdress;
		private Account account;

		UIWindow window;
		public static UIStoryboard Storyboard = UIStoryboard.FromName ("MainStoryboardIphone", null);
		public static UIViewController initialViewController;
		public myTableViewCtrl myTableViewController;


		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			initialViewController = Storyboard.InstantiateInitialViewController () as UIViewController;

			window.RootViewController = initialViewController;
			window.MakeKeyAndVisible ();

			var settings = UIUserNotificationSettings.GetSettingsForTypes(
				UIUserNotificationType.Alert
				| UIUserNotificationType.Badge
				| UIUserNotificationType.Sound,
				new NSSet());
			UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);

			UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval (UIApplication.BackgroundFetchIntervalMinimum);

			return true;
		}


		public override void PerformFetch (UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
		//public override async void PerformFetch (UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
		{
			Console.WriteLine ("PerformFetch called...");

			//Return no new data by default
			var result = UIBackgroundFetchResult.NoData;

			/*var fetchnotification = new UILocalNotification();
			fetchnotification.FireDate = null;
			fetchnotification.AlertAction = "View Alert";
			fetchnotification.AlertBody = "PerformFetch called..!";
			fetchnotification.ApplicationIconBadgeNumber = 1;
			fetchnotification.SoundName = UILocalNotification.DefaultSoundName;

			UIApplication.SharedApplication.ScheduleLocalNotification(fetchnotification);*/
			availableAdress = FeedURlRepository.GetNetAdress (3);
			account = LoginRepository.GetAccount (1);
			FeedUrl = availableAdress.Url+"?username="+account.Login+"&password="+account.Password;

			try
			{
				string w1 = GetRSSAsync(FeedUrl);
				if (w1 != null)
				{
					strprev = GetCachedRSS();
					Console.WriteLine(strprev);

					if (strprev != w1) {
						Console.WriteLine("Произошли изменения");

						var notifice = new UILocalNotification();
						notifice.AlertAction = "View Alert";
						notifice.AlertBody = "Произошли изменения!";
						notifice.ApplicationIconBadgeNumber = 1;
						notifice.SoundName = UILocalNotification.DefaultSoundName;
						UIApplication.SharedApplication.ScheduleLocalNotification(notifice);
						result = UIBackgroundFetchResult.NewData;
					}

					CacheRSSAsync(FeedUrl);
				}
			}
			catch 
			{
				//Indicate a failed fetch if there was an exception
				result = UIBackgroundFetchResult.Failed;
				Console.WriteLine (result);
			}
			finally
			{
				//We really should call the completion handler with our result
				completionHandler (result);
			}
		}

		void CacheRSSAsync(string feedurl)
		{
			var file = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "mnstrprev.txt");
			XPathDocument document = new XPathDocument(feedurl);
			XPathNavigator navigator = document.CreateNavigator();
			strtek = navigator.OuterXml;
			File.WriteAllText(file, strtek);	
		}

		/*async Task<string> GetRSSAsync()
		{
			XPathDocument document =  new XPathDocument (FeedUrl);
			XPathNavigator navigator = document.CreateNavigator ();
			//navigator.MoveToChild ("channel", FeedUrl);
			string strRSS = navigator.OuterXml;
			return strRSS;
		}*/

		string GetRSSAsync(string feedurl)
		{
			XPathDocument document =  new XPathDocument (feedurl);
			XPathNavigator navigator = document.CreateNavigator ();
			//navigator.MoveToChild ("channel", FeedUrl);
			string strRSS = navigator.OuterXml;
			return strRSS;
		}
		public static string GetCachedRSS()
		{
			var file = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "mnstrprev.txt");

			try { 
				return File.ReadAllText (file); 
			}
			catch { }

			return null;
		}

		public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		{
//			initialViewController = Storyboard.InstantiateInitialViewController () as UIViewController;
//			window.RootViewController = initialViewController;
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		}

		public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
		{

		}


	}
}


