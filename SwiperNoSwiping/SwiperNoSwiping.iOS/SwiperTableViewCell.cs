using System;
using CoreGraphics;
using UIKit;

namespace SwiperNoSwiping.iOS
{

	public partial class SwiperTableViewCell : UITableViewCell
	{
		public static readonly string CellId = "SwiperTableViewCell";

		// Set this to true to prevent the cell from revealing the button on the right
		public bool LockRight { get; set; }

		// Set this to true to allow the cell to reveal the button on the left
		public bool LeftAvailable { get; set; }


		// Either the LeftButton or RightButton is 'open'
		public bool Open { 
			get {
				return LeftOpen || RightOpen;
			}
		}


		// Use the ContentOffset of the ScrollView to determine whether or not the LeftButton is 'open'
		public bool LeftOpen {
			get {
				return ContentScrollView.ContentOffset != CGPoint.Empty && ContentScrollView.ContentOffset.X < 0f;
			}
		}


		// Use the ContentOffset of the ScrollView to determine whether or not the RightButton is 'open'
		public bool RightOpen {
			get {
				return ContentScrollView.ContentOffset != CGPoint.Empty && ContentScrollView.ContentOffset.X > 0f;
			}
		}


		// Used when determining if the user tapped on the LeftButton when it is 'open'
		public nfloat LeftButtonOffset {
			get {
				return LeftButton.Frame.Width;
			}
		}

		// Used when determining if the user tapped on the RightButton when it is 'open'
		public nfloat RightButtonOffset {
			get {
				return Frame.Width - RightButton.Frame.Width;
			}
		}


		public SwiperTableViewCell (IntPtr handle) : base (handle)
		{

		}


		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			ContentScrollView.Delegate = new SlidingCellScrollDelegate(this);

			ContentScrollView.AlwaysBounceVertical = false;
			ContentScrollView.DirectionalLockEnabled = true;
		}


		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ContentScrollView.ContentInset = new UIEdgeInsets(0.0f, LeftAvailable ? LeftButton.Frame.Width : 0.0f, 0.0f, LockRight ? 0.0f : RightButton.Frame.Width);
			ContentScrollView.SetContentOffset (CGPoint.Empty, true);

			LeftButton.Hidden = !LeftAvailable;
		}


		public void SetData (string title)
		{
			SliderTitleLabel.Text = title;
		}


		public void CloseSlideButtons ()
		{
			ContentScrollView.SetContentOffset (CGPoint.Empty, true);
		}


		public class SlidingCellScrollDelegate : UIScrollViewDelegate
		{
			readonly SwiperTableViewCell cell;


			public SlidingCellScrollDelegate (SwiperTableViewCell cell)
			{
				this.cell = cell;
			}


			public override void Scrolled (UIScrollView scrollView)
			{
				if (scrollView.ContentOffset.X > 0.0f && cell.LockRight) {
					scrollView.ContentOffset = CGPoint.Empty;
				}

				if (scrollView.ContentOffset.X < 0 && !cell.LeftAvailable) {
					scrollView.ContentOffset = CGPoint.Empty;
				}
			}


			public override void WillEndDragging (UIScrollView scrollView, CGPoint velocity, ref CGPoint targetContentOffset)
			{
				if (scrollView.ContentOffset.X > cell.RightButton.Frame.Width - 10) {
					targetContentOffset.X = cell.RightButton.Frame.Width;
				} else if (scrollView.ContentOffset.X < -cell.LeftButton.Frame.Width + 10) {
					targetContentOffset.X = -cell.LeftButton.Frame.Width;
				} else {
					targetContentOffset = CGPoint.Empty;
				}
			}
		}
	}
}