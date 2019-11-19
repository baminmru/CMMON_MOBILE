using System;
using System.Collections.Generic;
using System.IO;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using System.Linq;

namespace Storyboard 
{
	public class TableSource : UITableViewSource {

		List<string> tableItems;
		List<string> feedTitleList11;
		string cellIdentifier = "TableCell";
		QDFeedParser.IFeed feed;
		UIViewController viewController;
		private string FeedUrlContent;

		public TableSource (UIViewController ctrl, List<string> items,List<string> titlelist, string feedurl)
		{
			tableItems = items;
			viewController = ctrl;
			feedTitleList11 = titlelist;
			FeedUrlContent = feedurl;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return tableItems.Count;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			try{
			feed = new QDFeedParser.HttpFeedFactory().CreateFeed(new Uri(FeedUrlContent));
			var feedTitleList = feed.Items.Select(i=>i.Title).ToList();
			tableView.DeselectRow (indexPath, true);

			// Specially for Storyboard !!
			var detail = viewController.Storyboard.InstantiateViewController("vegeIdentifier") as myVegeViewCtrl;
			detail.Title = feedTitleList[indexPath.Row];
			detail.LoadUrl(feed.Items [indexPath.Row].Link);
			viewController.NavigationController.PushViewController (detail, true);
			}
				catch{
				UIAlertView alert = new UIAlertView ("Не открыть из-за отсутствия связи", "Проверьте настройки", null, "OK", null);
				alert.Show();
			}
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
		UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
		
			var cellStyle = UITableViewCellStyle.Default;
			//			var cellStyle = UITableViewCellStyle.Subtitle;
			//			var cellStyle = UITableViewCellStyle.Value1;
			//			var cellStyle = UITableViewCellStyle.Value2;

			if (cell == null) {
				cell = new UITableViewCell (cellStyle, cellIdentifier);
						}

			cell.TextLabel.Text =  feedTitleList11[indexPath.Row];
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

			return cell;
		}
	}
}