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
using Android.Webkit;

namespace XamFeed
{
	[Activity (Label = "WebActivity")]			
	public class WebActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.WebActivity);

			WebView view = FindViewById<WebView>(Resource.Id.detailView);

			view.LoadUrl(Intent.GetStringExtra("link"));
		}
	}
}
