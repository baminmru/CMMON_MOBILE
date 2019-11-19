using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;

using System;
using System.IO;

using Java.Lang;

namespace DemoService
{
	[Activity (Label = "Мониторинг Просмотр", Theme = "@android:style/Theme.NoTitleBar", ConfigurationChanges=Android.Content.PM.ConfigChanges.Orientation)]			
	public class FeedItemContentActivity : Activity
	{


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			this.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;

			SetContentView (Resource.Layout.FeedItemContent);
			var webView = FindViewById<WebView> (Resource.Id.FeedItemContent_WebView);
			webView.Settings.JavaScriptEnabled = true;
			webView.Settings.SetRenderPriority (WebSettings.RenderPriority.High);
			webView.Settings.CacheMode = CacheModes.NoCache;
			Console.WriteLine (Intent.GetStringExtra ("ItemUrl"));
			webView.LoadUrl (Intent.GetStringExtra("ItemUrl"));

		}
	}
}