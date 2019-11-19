using System;
using System.IO;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Telephony;
using Android.Support.V4.App;
using Android.Media;
using Android.Views.InputMethods;

using Java.Lang;

namespace DemoService
{
	[Activity (Label = "Мониторинг", Theme = "@android:style/Theme.NoTitleBar", MainLauncher = true)]
	public class DemoActivity : Activity
	{
		private Note note ;
		private Note addnetaddress;
		private Note availableaddress;

		private Account account;

		private static string db_file = "feedurl.db3";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			var dbPath = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), db_file);

			if (File.Exists (dbPath)) {

				var intent = new Intent(this, typeof(FeedItemOverviewActivity));
				StartActivity(intent);

				StopService (new Intent (this, typeof(DemoService)));
				StartService (new Intent (this, typeof(DemoService)));

				Finish ();
			} 
			else
			{
				SetContentView (Resource.Layout.Main);

				var enterButton = FindViewById<Button> (Resource.Id.buttonEnter);

				var txtUser = FindViewById<EditText> (Resource.Id.editTextLogin);
				var txtPass = FindViewById<EditText> (Resource.Id.editTextPassword);
				var txtUrl = FindViewById<EditText> (Resource.Id.editTextUrlAddress);
				var txtAddUrl = FindViewById<EditText> (Resource.Id.editTextAddUrlAddress);

				var txtStatus = FindViewById<TextView> (Resource.Id.textViewStatus);

				note = FeedURlRepository.GetNote (1);
				addnetaddress = FeedURlRepository.GetNote (2);
				availableaddress = FeedURlRepository.GetNote (3);
				account = LoginRepository.GetAccount (1); 

				txtUser.Text = account.Login;
				txtPass.Text = account.Password;
				txtUrl.Text = note.Url;
				txtAddUrl.Text = addnetaddress.Url;


				enterButton.Click += delegate {

					InputMethodManager manager = (InputMethodManager) GetSystemService(InputMethodService);
					manager.HideSoftInputFromWindow(txtStatus.WindowToken, 0);

					if (string.IsNullOrWhiteSpace(txtUser.Text))
						txtStatus.Text = "Введите имя";
						else if (string.IsNullOrWhiteSpace(txtPass.Text))
						txtStatus.Text = "Введите пароль";
						else if (string.IsNullOrWhiteSpace(txtUrl.Text))
						txtStatus.Text = "Введите Url адрес";
					else
					{
					account.Login = txtUser.Text;
					account.Password = txtPass.Text;
					LoginRepository.SaveAccount (account);

					note.Url = txtUrl.Text;
					FeedURlRepository.SaveNote(note);

					addnetaddress.Url = txtAddUrl.Text;
					FeedURlRepository.SaveNote(addnetaddress);

					availableaddress.Url = txtUrl.Text;
					FeedURlRepository.SaveNote(availableaddress);

					var intent = new Intent(this, typeof(FeedItemOverviewActivity));
					StartActivity(intent);

					StartService (new Intent (this, typeof(DemoService)));
					
						Finish ();
					}
					};
		
			}									
		}
	}
}


