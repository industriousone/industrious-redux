using System;

using Foundation;
using UIKit;

namespace Examples.Counter.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
		{
			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new Examples.Counter.Forms.App());

			return base.FinishedLaunching(uiApplication, launchOptions);
		}
	}
}
