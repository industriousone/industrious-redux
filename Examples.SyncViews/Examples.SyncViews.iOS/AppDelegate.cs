using System;

using Foundation;
using UIKit;

namespace Examples.SyncViews.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new Examples.SyncViews.Forms.App());

			return base.FinishedLaunching(app, options);
		}
	}
}
