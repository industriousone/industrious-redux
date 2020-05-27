using System;

using Industrious.Mvvm;
using Industrious.Redux;

namespace Examples.SyncViews.ViewModels
{
	public class ItemViewCellModel : NotifyPropertyChanged, IDisposable
	{
		private readonly IDisposable _subscription;


		public ItemViewCellModel(IStore<AppState> store, Guid itemId)
		{
			_subscription = store.ObserveElement(Projections.Item, itemId).Subscribe(item =>
			{
				Text = item.Text;
				ToggleValue = item.ToggleValue;
			});

			DeleteItemCommand = new Command(() =>
			{
				store.Dispatch(new Actions.DeleteItem(itemId));
			});

			ToggleItemCommand = new Command<Boolean>(isToggled =>
			{
				store.Dispatch(new Actions.ToggleItem(itemId, isToggled));
			});
		}


		private String _text;

		public String Text
		{
			get { return (_text); }
			set { SetAndRaiseIfChanged(ref _text, value); }
		}


		private Boolean _toggleValue;

		public Boolean ToggleValue
		{
			get { return (_toggleValue); }
			set { SetAndRaiseIfChanged(ref _toggleValue, value); }
		}


		public Command DeleteItemCommand { get; }

		public Command<Boolean> ToggleItemCommand { get; }


		public void Dispose()
		{
			_subscription.Dispose();
		}
	}
}
