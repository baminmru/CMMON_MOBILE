using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Storyboard
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		string strtek;
		string strprev;
		private string FeedUrl;
		private NetAdress availableAdress;
		private Account account;

		UIWindow window;
		public static UIStoryboard Storyboard = UIStoryboard.FromName ("MainStoryboardIpad", null);
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
		{
			Console.WriteLine ("PerformFetch called...");

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
					//Console.WriteLine(strprev);

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
				result = UIBackgroundFetchResult.Failed;
				Console.WriteLine (result);
			}
			finally
			{
				completionHandler (result);
			}
		}

		public void CacheRSSAsync(string feedurl)
		{
			var file = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "mnstrprev.txt");
			XPathDocument document = new XPathDocument(feedurl);
			XPathNavigator navigator = document.CreateNavigator();
			strtek = navigator.OuterXml;
			File.WriteAllText(file, strtek);	

		}
		string GetRSSAsync(string feedurl)
		{
			XPathDocument document =  new XPathDocument (feedurl);
			XPathNavigator navigator = document.CreateNavigator ();
			string strRSS = navigator.OuterXml;
			return strRSS;
		}
		public static string GetCachedRSS()
		{
			var file = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "mnstrprev.txt");

			try { 
				Console.WriteLine(File.ReadAllText (file));
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
			/*var notification = new UILocalNotification();
			notification.AlertAction = "View Alert";
			notification.AlertBody = "Произошли изменения!";
			notification.ApplicationIconBadgeNumber = 1;
			notification.SoundName = UILocalNotification.DefaultSoundName;
			UIApplication.SharedApplication.ScheduleLocalNotification(notification);*/
		}

	}
}