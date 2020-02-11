using System;
using System.Collections.Generic;
using System.Linq;

using Industrious.Mvvm;
using Industrious.Redux;

namespace Examples.SyncViews.ViewModels
{
	public class ItemListViewModel : NotifyPropertyChanged
	{
		public ItemListViewModel(IStore<AppState> store)
		{
			store.Observe(Projections.Index).Subscribe(index =>
			{
				// Convert the list of item IDs from the store into a list of item
				// view-models which can be bound to the ListView. This approach of
				// rebinding the entire list each time loses the usual add/delete
				// list item UI animations; I'll be fixes this in the next iteration.
				if (Items != null)
				{
					foreach (var item in Items)
						item.Dispose();
				}

				Items = index
					.Select(itemId => new ItemViewCellModel(store, itemId))
					.ToList();
			});
		}


		private IList<ItemViewCellModel> _items;

		public IList<ItemViewCellModel> Items
		{
			get => _items;
			set => SetAndRaiseIfChanged(ref _items, value);
		}
	}
}
