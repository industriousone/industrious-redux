using System;
using Xamarin.Forms;

using Industrious.Redux;

using Examples.SyncViews.ViewModels;

namespace Examples.SyncViews.Forms
{
	public partial class App : Application
	{
		private readonly IStore<AppState> _store;


		public App()
		{
			InitializeComponent();

			_store = new Store<AppState>(Reducers.Root, AppState.SampleState);

			MainPage = new NavigationPage(new MainPage()
			{
				BindingContext = new MainPageModel(_store)
			});
		}
	}
}
