using System;
using Xamarin.Forms;

using Examples.Counter.ViewModels;

namespace Examples.Counter.Forms
{
	public partial class App : Application
	{
		private readonly Store _store;


		public App()
		{
			InitializeComponent();

			_store = new Store();

			MainPage = new NavigationPage(new MainPage()
			{
				BindingContext = new MainPageModel(_store)
			});
		}
	}
}
