using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;

namespace SwiperNoSwiping.iOS
{

	public partial class SwiperTableViewController : UITableViewController
	{
		static List<string> SwiperStrings = new List<string> { "Apple iPhone 3", "Apple iPhone 3G", "Apple iPhone 3Gs", "Apple iPhone 4", "Apple iPhone 4s", "Apple iPhone 5", "Apple iPhone 5s", "Apple iPhone 6", "Apple iPhone 6 Plus" };

		UITapGestureRecognizer SwiperTapGesture;


		public SwiperTableViewController (IntPtr handle) : base (handle)
		{

		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			SwiperTapGesture = new UITapGestureRecognizer(HandleSwiperTapGesture);
		}


		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			TableView.AddGestureRecognizer (SwiperTapGesture);
		}


		public override void ViewDidDisappear (bool animated)
		{
			TableView.RemoveGestureRecognizer (SwiperTapGesture);

			base.ViewDidDisappear (animated);
		}


		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}


		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return SwiperStrings.Count;
		}


		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (SwiperTableViewCell.CellId) as SwiperTableViewCell;

			cell.SetData (SwiperStrings[indexPath.Row]);

			cell.LeftAvailable = true;

			return cell;
		}


		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 62;
		}


		public void HandleSwiperTapGesture (UITapGestureRecognizer press)
		{
			if (press.State == UIGestureRecognizerState.Recognized) {

				// Where did the user tap?
				CGPoint point = press.LocationInView (press.View);

				// Get the NSIndexPath for that cell
				NSIndexPath indexPath = TableView.IndexPathForRowAtPoint (point);

				if (indexPath == null) {
					return;
				}
					
				// Get the UITableViewCell the user tapped
				var cell = TableView.CellAt (indexPath) as SwiperTableViewCell;

				// If the cell is 'open' then check if the user is tapping the right or left button, otherwise close the drawer
				if (cell != null && cell.Open) {

					if (cell.LeftOpen && point.X < cell.LeftButtonOffset) { // Left Button

						var leftTitle = "Left Button";
						var leftMessage = SwiperStrings[indexPath.Row];

						PresentAlert (leftTitle, leftMessage);

						return;
					}

					if (cell.RightOpen && point.X > cell.RightButtonOffset) { // Right Button

						var rightTitle = "Right Button";
						var rightMessage = SwiperStrings[indexPath.Row];

						PresentAlert (rightTitle, rightMessage);

						return;
					} 


					cell.CloseSlideButtons ();

					return;
				}

				// Business as usual
				RowSelected (TableView, indexPath);	

				return;
			}
		}


		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var normalTitle = "Normal Behavior";
			var normalMessage = "Business as usual my Dear";
			PresentAlert (normalTitle, normalMessage);
		}

		void PresentAlert (string title, string message)
		{
			var alert = UIAlertController.Create (title, message, UIAlertControllerStyle.Alert);

			alert.AddAction (UIAlertAction.Create ("Thanks c0lby!", UIAlertActionStyle.Cancel, null));

			PresentViewController (alert, true, null);
		}
	}
}