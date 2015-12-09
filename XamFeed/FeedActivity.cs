using System.Linq;
using System.Net.Http;
using System.Xml.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace XamFeed
{
	[Activity (Label = "XamFeed", MainLauncher = true, Icon = "@mipmap/icon")]
	public class FeedActivity : ListActivity
	{
		private RssItem[] _items;

		protected async override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			using (var client = new HttpClient())
			{
				var xmlFeed = await client.GetStringAsync("http://eluniversal.com.mx/rss.xml");
				var doc = XDocument.Parse(xmlFeed);
				XNamespace dc = "http://purl.org/dc/elements/1.1/";

				_items = (from item in doc.Descendants("item")
					select new RssItem
					{
						Title = item.Element("title").Value,
						PubDate = item.Element("pubDate").Value,
						Creator = item.Element(dc + "creator").Value,
						Link = item.Element("link").Value
					}).ToArray();
				
				ListAdapter = new FeedAdapter(this, _items);
			}
		}

		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			base.OnListItemClick(l, v, position, id);

			var second = new Intent(this, typeof(WebActivity));
			second.PutExtra("link", _items[position].Link);
			StartActivity(second);
		}
	}
}
