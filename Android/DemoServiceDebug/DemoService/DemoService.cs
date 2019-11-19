using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Xml;
using System.Xml.XPath;
using System.Threading;

using Android.App;
using Android.Util;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Media;

using Java.Lang;
using System.IO;

namespace DemoService
{
	[Service]

	public class DemoService : Service
	{
		private Note availableaddress;

		private Account account;
		private string AvailableUrl;

		private Timer _timer;

		private static readonly int ButtonClickNotificationId = 1000;
		private static readonly int IconButtoClickNotificationId = 1001;

		string strtek;
		string strprev;

		public delegate void StrMod(object sender, EventArgs e);

		public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{
			StartServiceInForeground ();
			StartTimer ();

			return StartCommandResult.NotSticky;

		}

		void StartServiceInForeground ()
		{
			var ongoing = new Notification (Resource.Drawable.ic_action_star, "Служба мониторинга запущена");
			var pendingIntent = PendingIntent.GetActivity (this, 0, new Intent (this, typeof(FeedItemOverviewActivity)), 0);
			ongoing.SetLatestEventInfo (this, "Мониторинг", "Нажмите, чтобы запустить окно Мониторинга", pendingIntent);

			StartForeground ((int)NotificationFlags.ForegroundService, ongoing);
		}

		public override void OnDestroy ()
		{
			base.OnDestroy ();
			try{
			_timer.Dispose ();
			}
			catch {
				//Toast.MakeText(this, "таймер не запускался" , ToastLength.Long).Show();
			}
			Console.WriteLine ("Служба мониторинга остановлена");  
			//Toast.MakeText(this, "Мониторинг остановлен" , ToastLength.Long).Show();
			NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
			notificationManager.Cancel(IconButtoClickNotificationId);
			notificationManager.Cancel(ButtonClickNotificationId);
		}

		void SendNotification (object sender, EventArgs e)
		{
			Intent intent = new Intent(this, typeof(FeedItemOverviewActivity)); 


			TaskStackBuilder stackBuilder = TaskStackBuilder.Create (this);
			stackBuilder.AddParentStack(Class.FromType(typeof(FeedItemOverviewActivity)));
			stackBuilder.AddNextIntent(intent);

			PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

			NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
				.SetSound ( RingtoneManager.GetDefaultUri (RingtoneType.Notification))
				.SetAutoCancel(true) // dismiss the notification from the notification area when the user clicks on it
				.SetContentIntent(resultPendingIntent) // start up this activity when the user clicks the intent.
				.SetContentTitle("Мониторинг") // Set the title
				.SetSmallIcon(Resource.Drawable.ic_action_new_event) // This is the icon to display
				.SetContentText("Произошли изменения!"); // the message to display.

			NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
			notificationManager.Notify(ButtonClickNotificationId, builder.Build());
		}


		public void StartTimer ()
		{
			var t = new System.Threading.Thread (() => {
				try
				{
					Console.WriteLine ("Создаём таймер");
					TimerCallback tcb = IsChanged;
					_timer = new Timer(tcb, null, 5000, 300000);
				
				}
				catch
				{
					Console.WriteLine ("Таймер не создан");
					StopSelf ();
				}
			
			}
			);

			t.Start ();
		}
			
			private void IsChanged(object stateInfo)
		{
			Console.WriteLine ("Проверка изменений : "+DateTime.Now.ToString());
			account = LoginRepository.GetAccount (1);
			availableaddress = FeedURlRepository.GetNote (3);
			AvailableUrl = availableaddress.Url + "?username=" + account.Login + "&password=" + account.Password;

			try
			{			
				strtek = GetRSSAsync(AvailableUrl);
				if (strtek != null)
				{
					strprev = GetCachedRSS();
					//Console.WriteLine(strprev);
					if (strprev != null)
					{
					if (strprev != strtek) {
						Console.WriteLine("Произошли изменения");
						StrMod notice = SendNotification;
						notice (this, EventArgs.Empty);
					}
					else
					{
						Console.WriteLine ("Изменений нет");
						
					}
					}
					CacheRSSAsync(AvailableUrl);
					
				}


			}
			catch
			{
			Console.WriteLine ("URL недоступен");
			//SendNotificationUnavailable ();
			//StopSelf ();
			}
				
	    }

		public bool ConnectionHost (string feedurl)
		{
			try{
				Uri _url = new Uri (feedurl);
			string pingurl = string.Format ("{0}", _url.Host);
			string host = pingurl;
			Console.WriteLine (pingurl);
			Ping p = new Ping ();
			try 
			{
				PingReply reply = p.Send (host, 3000);
				if (reply.Status == IPStatus.Success)
				Console.WriteLine ("Подключение прошло успешно");
				return true;
			}
			catch 
			{ 
				/*if (IsLocalIp()) Console.WriteLine ("Узел недостуен");
				else */
				Console.WriteLine ("Проверьте подключение");
				return false;
			}

			}
		catch
		{
				return false;
		}
		}
		public bool Connection (string feedurl)
		{
			try{
				Uri _url = new Uri (feedurl);
				Console.WriteLine (feedurl);
				Ping p = new Ping ();
				try 
				{
					PingReply reply = p.Send (feedurl, 3000);
					if (reply.Status == IPStatus.Success)
						Console.WriteLine ("Подключение прошло успешно");
					return true;
				}
				catch 
				{ 
					Console.WriteLine ("Проверьте подключение");
					return false;
				}

			}
			catch
			{
				return false;
			}
		}

		void SendNotificationUnavailable ()
		{
			var nMgr = (NotificationManager)GetSystemService (NotificationService);
			var notification = new Notification (Resource.Drawable.ic_action_warning, "Сообщение от службы мониторинга");
			var pendingIntent = PendingIntent.GetActivity (this, 0, new Intent (this, typeof(DemoActivity)), 0);
			notification.SetLatestEventInfo (this, DateTime.Now.ToString(), "Нажмите, после подключения к сети!", pendingIntent);
			nMgr.Notify (0, notification);
		}

		void SendNotificationIcon (object sender, EventArgs e)
		{
			Intent intent = new Intent(this, typeof(FeedItemOverviewActivity)); 


			TaskStackBuilder stackBuilder = TaskStackBuilder.Create (this);
			stackBuilder.AddParentStack(Class.FromType(typeof(FeedItemOverviewActivity)));
			stackBuilder.AddNextIntent(intent);

			PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(1, (int)PendingIntentFlags.UpdateCurrent);

			NotificationCompat.Builder builder = new NotificationCompat.Builder (this)
				.SetOngoing(true)
				.SetContentIntent(resultPendingIntent) 
				.SetContentTitle("Служба мониторинга запущена")                       
				.SetSmallIcon (Resource.Drawable.ic_action_star)
				.SetContentText("Нажмите, чтобы запустить окно Мониторинга"); 

			NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
			notificationManager.CancelAll ();
			notificationManager.Notify(IconButtoClickNotificationId, builder.Build());

		}


		void CacheRSSAsync(string feedurl)
		{
			var file = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), "mnstrprev.txt");
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
			var file = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), "mnstrprev.txt");

			try { 
				return File.ReadAllText (file); 
			}
			catch { }

			return null;
		}


		public override Android.OS.IBinder OnBind (Android.Content.Intent intent)
		{
						//throw new NotImplementedException ();
			return null;
		}
	}
}