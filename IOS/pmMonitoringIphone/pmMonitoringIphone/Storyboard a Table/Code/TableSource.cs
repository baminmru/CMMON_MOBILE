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
		string FeedUrlContent;


		public TableSource (UIViewController ctrl, List<string> items,List<string> titlelist,string feedurl)
		{
			tableItems = items;
			viewController = ctrl;
			feedTitleList11=titlelist;
			FeedUrlContent = feedurl;
		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section.
		/// </summary>
		public override int RowsInSection (UITableView tableview, int section)
		{
			return tableItems.Count;
		}

		/// <summary>
		/// Called when a row is touched
		/// </summary>
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			try{
			feed = new QDFeedParser.HttpFeedFactory().CreateFeed(new Uri(FeedUrlContent));
			var feedTitleList = feed.Items.Select(i=>i.Title).ToList();
			//new UIAlertView("Row Selected", tableItems[indexPath.Row].Heading, null, "OK", null).Show();
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

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row
		/// </summary>
		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
		// request a recycled cell to save memory
		UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
		// UNCOMMENT one of these to use that style
			var cellStyle = UITableViewCellStyle.Default;
			//			var cellStyle = UITableViewCellStyle.Subtitle;
			//			var cellStyle = UITableViewCellStyle.Value1;
			//			var cellStyle = UITableViewCellStyle.Value2;

			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new UITableViewCell (cellStyle, cellIdentifier);
						}

			cell.TextLabel.Text =  feedTitleList11[indexPath.Row];
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

//			// Default style doesn't support Subtitle
//			if (cellStyle == UITableViewCellStyle.Subtitle 
//				|| cellStyle == UITableViewCellStyle.Value1
//				|| cellStyle == UITableViewCellStyle.Value2) {
//				//		cell.DetailTextLabel.Text = tableItems[indexPath.Row].SubHeading;
//			}

			// Value2 style doesn't support an image
			//	if (cellStyle != UITableViewCellStyle.Value2)
			//		cell.ImageView.Image = UIImage.FromFile ("Images/" +tableItems[indexPath.Row].ImageName);





			return cell;
		}
	}
}