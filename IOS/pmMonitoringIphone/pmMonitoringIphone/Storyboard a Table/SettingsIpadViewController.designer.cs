// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace Storyboard
{
	[Register ("SettingsIpadViewController")]
	partial class SettingsIpadViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonWithoutSaving { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton EnterButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextLogin { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextUrl { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonWithoutSaving != null) {
				ButtonWithoutSaving.Dispose ();
				ButtonWithoutSaving = null;
			}
			if (EnterButton != null) {
				EnterButton.Dispose ();
				EnterButton = null;
			}
			if (TextLogin != null) {
				TextLogin.Dispose ();
				TextLogin = null;
			}
			if (TextPassword != null) {
				TextPassword.Dispose ();
				TextPassword = null;
			}
			if (TextUrl != null) {
				TextUrl.Dispose ();
				TextUrl = null;
			}
		}
	}
}
