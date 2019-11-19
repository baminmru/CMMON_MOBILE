using System;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using System.Linq;
namespace Storyboard
{
	public partial class myTableViewCtrl : UITableViewController
	{
		private string FeedUrl;
		string AvailableUrl;
		private NetAdress netAdress;
		private NetAdress netAdress2;
		private NetAdress availabaleAdress;
		private Account account;
		bool CanUpdate;
		QDFeedParser.IFeed feed;
		public myTableViewCtrl () : base ("myTableViewCtrl", null)
		{
		}

		public myTableViewCtrl (IntPtr handle) : base (handle)
		{
			Console.WriteLine("handle");

		}

		public void Update (List<string> titlelist,string feedurl)
		{
			TableView.Source = new TableSource(this, titlelist,titlelist,feedurl);


		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Console.WriteLine ("didload");

			CanUpdate = false;

			var RefreshButton = new UIBarButtonItem (UIBarButtonSystemItem.Refresh, RefreshList);

			NavigationItem.RightBarButtonItem = RefreshButton;

			netAdress = FeedURlRepository.GetNetAdress (1);
			netAdress2 = FeedURlRepository.GetNetAdress (2);
			availabaleAdress = FeedURlRepository.GetNetAdress (3);

			account = LoginRepository.GetAccount(1);
			AvailableUrl = availabaleAdress.Url;


			for (int j = 0; j <= 2; j++) {

				try {
					feed = new QDFeedParser.HttpFeedFactory ().CreateFeed (new Uri (AvailableUrl+"?username="+account.Login+"&password="+account.Password));
					CanUpdate =true;
					if (j != 0) {
						availabaleAdress.Url=AvailableUrl;
						FeedURlRepository.SaveNetAdress(availabaleAdress);
					}
					break;
				}
				catch{
					switch (j) {
					case 0:
						AvailableUrl = netAdress2.Url;
//						UIAlertView alert0 = new UIAlertView ("pervaya smena", "Проверьте настройки", null, "OK", null);
//						alert0.Show();
						break;
					case 1:
						AvailableUrl = netAdress.Url;
//						UIAlertView alert1 = new UIAlertView ("vtoraya smena", "Проверьте настройки", null, "OK", null);
//						alert1.Show();
						break;
					case 2:
						UIAlertView alert2 = new UIAlertView ("Невозможно подключиться", "Проверьте настройки", null, "OK", null);
						alert2.Show ();
						break;
					}

				}
			}
		}

		

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			Console.WriteLine ("didappear");
			if (CanUpdate) {
				availabaleAdress = FeedURlRepository.GetNetAdress (3);
				account = LoginRepository.GetAccount (1);
				AvailableUrl = availabaleAdress.Url;
				FeedUrl = AvailableUrl + "?username=" + account.Login + "&password=" + account.Password;
				try{
				feed = new QDFeedParser.HttpFeedFactory().CreateFeed(new Uri(FeedUrl));

				var feedTitleList = feed.Items.Select (i => i.Title).ToList ();
				Update (feedTitleList, FeedUrl);
					TableView.ReloadData();
				}
				catch{
					UIAlertView alert = new UIAlertView ("Обрыв связи", "Проверьте настройки", null, "OK", null);
					alert.Show();
				
				}
			}
		}


		void RefreshList (object sender, EventArgs args)
		{
			Console.WriteLine ("refresh");
			try
			{
				availabaleAdress = FeedURlRepository.GetNetAdress (3);
				account = LoginRepository.GetAccount(1);
				FeedUrl = availabaleAdress.Url+"?username="+account.Login+"&password="+account.Password;


				feed = new QDFeedParser.HttpFeedFactory().CreateFeed(new Uri(FeedUrl));

				// need list outside cashe
				var feedTitleList = feed.Items.Select(i=>i.Title).ToList();
				Update (feedTitleList,FeedUrl);
				TableView.ReloadData();

			}
			catch
			{
				UIAlertView alert = new UIAlertView ("Невозможно обновить", "Проверьте настройки", null, "OK", null);
				alert.Show();
			}

		}

	}
}
