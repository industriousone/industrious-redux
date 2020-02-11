using System;

using Industrious.Mvvm;
using Industrious.Redux;

namespace Examples.SyncViews.ViewModels
{
	public class MainPageModel
	{
		public MainPageModel(IStore<AppState> store)
		{
			LeftListViewModel = new ItemListViewModel(store);
			RightListViewModel = new ItemListViewModel(store);

			AddItemCommand = new Command(() =>
			{
				store.Dispatch(new Actions.AddItem());
			});

			ResetItemsCommand = new Command(() =>
			{
				store.Dispatch(new Actions.ResetItems());
			});
		}


		public ItemListViewModel LeftListViewModel { get; }

		public ItemListViewModel RightListViewModel { get; }

		public Command AddItemCommand { get; }

		public Command ResetItemsCommand { get; }
	}
}
