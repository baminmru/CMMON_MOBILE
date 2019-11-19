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
		private Account account;
		UIViewController myTableViewController;
		private static string db_file = "FeedUrl.db3";


		public SettingsIpadViewController (IntPtr handle) : base (handle)
		{
		}

		public override void AwakeFromNib ()
		{
			// Called when loaded from xib or storyboard.

			this.Initialize ();
		}

		public void Initialize(){

			var myStoryboard = AppDelegate.Storyboard;
			myTableViewController = myStoryboard.InstantiateViewController ("myTableViewCtrl") as myTableViewCtrl;
		}
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Console.WriteLine ("didload1");
			var dbPath = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), db_file);

			if (File.Exists (dbPath)) {
				this.NavigationController.PushViewController (myTableViewController, true);
			} /*else {
				netadress = FeedURlRepository.GetNetAdress (1); 
				account = LoginRepository.GetAccount (1);



				EnterButton.TouchUpInside += (object sender, EventArgs e) => {

					netadress.Url = TextUrl.Text;
					FeedURlRepository.SaveNetAdress (netadress);

					account.Login=TextLogin.Text;
					account.Password=TextPassword.Text;
					LoginRepository.SaveAccount(account);

				};
			}*/	


			// Perform any additional setup after loading the view, typically from a nib.
//			ButtonWithoutSaving.TouchUpInside += (object sender, EventArgs e) => {
//				this.NavigationController.PushViewController (myTableViewController, true);
//			};
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

			netadress = FeedURlRepository.GetNetAdress (1); 
			account = LoginRepository.GetAccount (1);

			TextPassword.Text = account.Password;
			TextLogin.Text = account.Login;
			TextUrl.Text = netadress.Url;

			EnterButton.TouchUpInside += (object sender, EventArgs e) => {

				netadress.Url = TextUrl.Text;
				FeedURlRepository.SaveNetAdress (netadress);

				account.Login=TextLogin.Text;
				account.Password=TextPassword.Text;
				LoginRepository.SaveAccount(account);

			};

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

		#endregion


	}
}
