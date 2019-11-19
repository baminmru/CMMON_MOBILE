using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DemoService
{
	[Activity(Label = "Мониторинг")]            
	public class FeedItemOverviewActivity : Activity
	{

		QDFeedParser.IFeed feed;

		private Note note;
		private Note addnetaddress;
		private Note availableaddress;
		private Account account;

		private string AvailableUrl;
		bool CanFeel;
		bool CanShowErorr;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView (Resource.Layout.FeedItemOwerview);

			var menubutton = FindViewById<Button> (Resource.Id.buttonPopMenu);
			var listview = FindViewById<ListView> (Resource.Id.listViewOwerview);

			note = FeedURlRepository.GetNote(1);
			addnetaddress = FeedURlRepository.GetNote(2);
			availableaddress = FeedURlRepository.GetNote (3);
			account = LoginRepository.GetAccount (1);

			AvailableUrl = availableaddress.Url;

			CanFeel = false;
			CanShowErorr = true;

			for (int k=0; k <= 2; k++) {
				try {
					feed = new QDFeedParser.HttpFeedFactory ().CreateFeed (new Uri (AvailableUrl + "?username=" + account.Login + "&password=" + account.Password));
					CanFeel=true;

							if(k!=0)
							{
							availableaddress.Url=AvailableUrl;
							FeedURlRepository.SaveNote(availableaddress);
							}
							else
							{
							//Toast.MakeText (this, "Не поменялся доступный Url", ToastLength.Long).Show ();
							}
					break;

				} catch {

					switch(k)
					{
					case 0:
						AvailableUrl = addnetaddress.Url;
						//Toast.MakeText (this, "первая смена адреса", ToastLength.Long).Show ();
						break;
					case 1:
						AvailableUrl = note.Url;
						//Toast.MakeText (this, "вторая смена адреса", ToastLength.Long).Show ();
						break;
					case 2:
						new AlertDialog.Builder(this)
							.SetPositiveButton("Ок", (sender, args) =>
								{
								})
							.SetMessage("Проверьте настройки и перезагрузите приложение")
							.SetTitle("Недоступны оба Url адреса")
							.Show();	
						break;
					}
				}
			}

			menubutton.Click += (s, arg) => {
				PopupMenu menu = new PopupMenu (this, menubutton);
				menu.Inflate (Resource.Menu.popup_menu);

				menu.MenuItemClick += (s1, arg1) => {
					Console.WriteLine ("{0} selected", arg1.Item.TitleFormatted);
					switch(arg1.Item.TitleFormatted.ToString())
					{
					case "Обновить":
						FillListView(availableaddress.Url + "?username=" + account.Login + "&password=" + account.Password);
						break;
					case "Настройки":
						var ___intent = new Intent (this, typeof(ParametrsActivity));
						StartActivity (___intent);
						//Finish ();
						break;
					case "Запретить уведомления":
						StopService (new Intent (this, typeof(DemoService)));
						break;
					}
				};

				menu.DismissEvent += (s2, arg2) => {
					Console.WriteLine ("menu dismissed"); 
				};
				menu.Show ();
			};

			//FillListView(FeedUrl);

			listview.ItemClick += delegate(object sender, AdapterView.ItemClickEventArgs e)
			{
				var intent = new Intent(this, typeof(FeedItemContentActivity));
				intent.PutExtra("ItemUrl", feed.Items[e.Position].Link);
				StartActivity(intent);
			};
	}

		void FillListView(string url)
		{
			try
			{
				var listview = FindViewById<ListView> (Resource.Id.listViewOwerview);
				feed = new QDFeedParser.HttpFeedFactory().CreateFeed(new Uri(url));
				var feedTitleList = feed.Items.Select(i=>i.Title).ToList();
				listview.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, feedTitleList);
			}
			catch
			{
				if (CanShowErorr) {
					new AlertDialog.Builder (this)
					.SetPositiveButton ("Ок", (sender, args) => {
							Finish ();
					})
					.SetMessage ("Проверьте настройки и перезагрузите приложение")
					.SetTitle ("Потеряна связь")
					.Show ();		
					CanShowErorr = false;

				}
			}
		}
	
		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			var item = menu.Add(0, 0, 0, new Java.Lang.String("Обновить"));
			item = menu.Add(0, 0, 1, new Java.Lang.String("Запретить уведомления")); 
			item = menu.Add(1, 0, 0, new Java.Lang.String("Настройки")); 

			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			availableaddress = FeedURlRepository.GetNote (3);
			account = LoginRepository.GetAccount (1);

			switch (item.TitleFormatted.ToString())
			{
			case "Обновить":
			FillListView(availableaddress.Url + "?username=" + account.Login + "&password=" + account.Password);
			return true;

			case "Запретить уведомления":
			StopService (new Intent (this, typeof(DemoService)));
			//Finish ();
			return true;

			case "Настройки":
			var ___intent = new Intent (this, typeof(ParametrsActivity));
			StartActivity (___intent);
			//Finish ();
			return true;

			default:
			return base.OnOptionsItemSelected(item);
			}
		}	
			
		protected override void OnStart ()
		{
			base.OnStart ();
			Console.WriteLine ("Start");
			if (CanFeel) {
				availableaddress = FeedURlRepository.GetNote (3);
				account = LoginRepository.GetAccount (1);
				FillListView (availableaddress.Url + "?username=" + account.Login + "&password=" + account.Password);
			}
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();
			Console.WriteLine ("destroy");
		}

	}
}