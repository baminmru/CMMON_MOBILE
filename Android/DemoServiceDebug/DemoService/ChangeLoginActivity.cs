
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
using Android.Views.InputMethods;

namespace DemoService
{
	[Activity (Label = "Регистрация пользователя")]			
	public class ChangeLoginActivity : Activity
	{
		private Account account;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ChangeLogin);

			var SignInbutton = FindViewById<Button> (Resource.Id.buttonSignIn);
			var txtLogin = FindViewById<EditText>(Resource.Id.editTextLogin);
			var txtPassword = FindViewById<EditText> (Resource.Id.editTextPassword);
			var txtStatus = FindViewById<TextView> (Resource.Id.textViewStatus);

			account = LoginRepository.GetAccount(1); 
			SignInbutton.Click += delegate {

				InputMethodManager manager = (InputMethodManager) GetSystemService(InputMethodService);
				manager.HideSoftInputFromWindow(txtPassword.WindowToken, 0);

				if (string.IsNullOrWhiteSpace (txtLogin.Text)) {
					txtStatus.Text = "Введите имя пользователя";
				} else {

					account.Login = txtLogin.Text;
					account.Password = txtPassword.Text;
					LoginRepository.SaveAccount (account);

					/*Intent _intent = new Intent (this, typeof(FeedItemOverviewActivity));
					_intent.PutExtra ("Login", account.Login);
					_intent.PutExtra ("Password", account.Password);
					SetResult (Result.Ok, _intent);*/
					StopService (new Intent ("com.xamarin.DemoService"));
					StartService (new Intent ("com.xamarin.DemoService"));
					Finish ();
				}
			};
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			var item = menu.Add(0, 1, 0, new Java.Lang.String("Изменить URl")); 
			item = menu.Add(0, 0, 0, new Java.Lang.String("Посмотреть параметры"));

			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.TitleFormatted.ToString())
			{
			case "Изменить URl":
				var __intent = new Intent (this, typeof(ChangeURlActivity));
				//StartActivityForResult (__intent, 0);
				StartActivity (__intent);
				Finish ();
				return true;
			case "Посмотреть параметры":
				var ___intent = new Intent (this, typeof(ParametrsActivity));
				StartActivity (___intent);
				//StartActivityForResult (__intent, 0);


				return true;


			default:
				return base.OnOptionsItemSelected(item);
			}
		}	
	}
}

