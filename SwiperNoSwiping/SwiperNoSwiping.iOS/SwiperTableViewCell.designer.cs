// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SwiperNoSwiping.iOS
{
	[Register ("SwiperTableViewCell")]
	partial class SwiperTableViewCell
	{
		[Outlet]
		UIKit.UIView ContainerViewMain { get; set; }

		[Outlet]
		UIKit.UIView ContainerViewSlider { get; set; }

		[Outlet]
		UIKit.UIScrollView ContentScrollView { get; set; }

		[Outlet]
		UIKit.UIButton LeftButton { get; set; }

		[Outlet]
		UIKit.UIButton RightButton { get; set; }

		[Outlet]
		UIKit.UILabel SliderTitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContainerViewMain != null) {
				ContainerViewMain.Dispose ();
				ContainerViewMain = null;
			}

			if (LeftButton != null) {
				LeftButton.Dispose ();
				LeftButton = null;
			}

			if (RightButton != null) {
				RightButton.Dispose ();
				RightButton = null;
			}

			if (ContentScrollView != null) {
				ContentScrollView.Dispose ();
				ContentScrollView = null;
			}

			if (ContainerViewSlider != null) {
				ContainerViewSlider.Dispose ();
				ContainerViewSlider = null;
			}

			if (SliderTitleLabel != null) {
				SliderTitleLabel.Dispose ();
				SliderTitleLabel = null;
			}
		}
	}
}
