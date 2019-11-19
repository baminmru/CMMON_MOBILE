
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
	[Activity (Label = "Изменение URl")]			
	public class ChangeURlActivity : Activity
	{
		private Note note;
		protected override void OnCreate (Bundle bundle)
		{

			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ChangeURl);

			var changeURlbutton = FindViewById<Button> (Resource.Id.buttonChangeURl);


			var txtCurrentURl = FindViewById<EditText> (Resource.Id.editTextCurrentURl);
			var txtEditURl = FindViewById<EditText>(Resource.Id.editTextChangeURl);
			var txtAdminPass = FindViewById<EditText> (Resource.Id.editTextAdminPass);

			var txtStatus = FindViewById<TextView> (Resource.Id.textViewStatus);




			note = FeedURlRepository.GetNote(1); 

			txtCurrentURl.Text = note.Url;

			changeURlbutton.Click += delegate {
				InputMethodManager manager = (InputMethodManager) GetSystemService(InputMethodService);
				manager.HideSoftInputFromWindow(txtEditURl.WindowToken, 0);		
				if (txtAdminPass.Text == "1111"){
					//String.IsNullOrWhiteSpace()
					if (txtEditURl.Text != ""){
					note.Url=txtEditURl.Text;
					FeedURlRepository.SaveNote(note);

						/*Intent _intent = new Intent(this, typeof(FeedItemOverviewActivity));
						_intent.PutExtra ("FeedUrl", URl.Url);
						SetResult (Result.Ok, _intent);*/
						StopService (new Intent ("com.xamarin.DemoService"));
						StartService (new Intent ("com.xamarin.DemoService"));
						Finish();
					}
				else{
					txtStatus.Text = "Введите новый URl!";
					}}
				else{
					txtStatus.Text = "Неверный пароль администратора!";
				
				}
			};

		}




	}
}

