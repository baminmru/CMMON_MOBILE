using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DemoService
{
	[Activity (Label = "Настройки")]			
	public class ParametrsActivity : Activity
	{
		private Note note ;
		private Note addnetaddress ;
		private Note availableaddress;
		private Account account;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

				note = FeedURlRepository.GetNote (1);
				addnetaddress = FeedURlRepository.GetNote (2);
				availableaddress = FeedURlRepository.GetNote (3);
				account = LoginRepository.GetAccount (1);

				SetContentView (Resource.Layout.Parametrs);
				
				var txtPassword = FindViewById<TextView> (Resource.Id.editTextPasswordPrm);
				var txtLogin = FindViewById<TextView> (Resource.Id.editTextLoginPrm);
				var txtUrl = FindViewById<TextView> (Resource.Id.editTextUrlPrm);
				var txtAddUrl = FindViewById<TextView> (Resource.Id.editTextAddUrlPrm);

				var editButton = FindViewById<Button> (Resource.Id.buttonEditPrm);

				txtUrl.Text = note.Url;
				txtAddUrl.Text = addnetaddress.Url;
				txtLogin.Text = account.Login;
				txtPassword.Text = account.Password;

				editButton.Click += (object sender, EventArgs e) => {
					
					note.Url = txtUrl.Text;
					FeedURlRepository.SaveNote (note);

					addnetaddress.Url = txtAddUrl.Text;
					FeedURlRepository.SaveNote (addnetaddress);

					account.Login = txtLogin.Text;
					account.Password = txtPassword.Text;
					LoginRepository.SaveAccount (account);

					availableaddress.Url = txtUrl.Text;
					FeedURlRepository.SaveNote(availableaddress);

					Finish ();
				};
								
		}
	}
}

