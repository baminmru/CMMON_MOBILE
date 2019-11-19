using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Storyboard
{
	public partial class myVegeViewCtrl : UIViewController
	{
		string address;

		#region orientation



		public override bool ShouldAutorotate()

		{

			return false;

		}



		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()

		{

			return UIInterfaceOrientationMask.LandscapeLeft;

		}



		#endregion

		public myVegeViewCtrl (IntPtr handle) : base (handle)
		{
		}

		public void LoadUrl (string address) {
			Console.WriteLine ("Set url to " + address);
			this.address = address;

		}
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			Console.WriteLine ("load url"+ address);
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;

			try
			{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			var url = new NSUrl(address);
			var req = new NSUrlRequest(url);
			webview.LoadRequest (req);
			webview.LoadFinished += (sender, e) => {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			};
			}
			catch
			{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
				UIAlertView alert = new UIAlertView ("Ошибка", "Невозможно отоброзать страницу", null, "OK", null);
				alert.Show();
			}
		}
	}
}
