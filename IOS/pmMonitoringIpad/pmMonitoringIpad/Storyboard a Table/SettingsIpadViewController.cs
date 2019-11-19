using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using System.IO;

namespace Storyboard
{
	partial class SettingsIpadViewController : UIViewController
	{
		private NetAdress netadress;
		private NetAdress extnetadress;
		private Account account;
		private NetAdress availabaleAdress;



		public SettingsIpadViewController (IntPtr handle) : base (handle)
		{
		}

	

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Console.WriteLine ("didload1");
			netadress = FeedURlRepository.GetNetAdress (1); 
			extnetadress = FeedURlRepository.GetNetAdress (2);
			availabaleAdress = FeedURlRepository.GetNetAdress (3);
			account = LoginRepository.GetAccount (1);

			TextPassword.Text = account.Password;
			TextLogin.Text = account.Login;
			TextUrl.Text = netadress.Url;
			TextExtUrl.Text = extnetadress.Url;

			EnterButton.TouchUpInside += (object sender, EventArgs e) => {

				netadress.Url = TextUrl.Text;
				FeedURlRepository.SaveNetAdress (netadress);

				extnetadress.Url = TextExtUrl.Text;
				FeedURlRepository.SaveNetAdress (extnetadress);

				availabaleAdress.Url = TextUrl.Text;
				FeedURlRepository.SaveNetAdress(availabaleAdress);

				account.Login=TextLogin.Text;
				account.Password=TextPassword.Text;
				LoginRepository.SaveAccount(account);

			};


		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			Console.WriteLine ("WillAppear1");
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			Console.WriteLine ("didlAppear1");
		}
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			Console.WriteLine ("WillDisappear1");
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
			Console.WriteLine ("didDisappear1");
		}

	}
}
