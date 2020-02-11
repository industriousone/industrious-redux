using System;

using Industrious.Redux;
using Industrious.Mvvm;

namespace Examples.Counter.ViewModels
{
	public class MainPageModel : NotifyPropertyChanged
	{
		public MainPageModel(IStore<AppState> store)
		{
			store.Observe(Projections.CounterValue).Subscribe(value => { CounterValue = value; });

			DecrementCounterCommand = new Command<String>((value) =>
			{
				var decrementBy = Int32.Parse(value);
				store.Dispatch(new Actions.DecrementCounter(decrementBy));
			});

			IncrementCounterCommand = new Command<String>((value) =>
			{
				var incrementBy = Int32.Parse(value);
				store.Dispatch(new Actions.IncrementCounter(incrementBy));
			});

			ResetCounterCommand = new Command(() =>
			{
				store.Dispatch(new Actions.ResetCounter());
			});
		}


		Int32 _counterValue;

		public Int32 CounterValue
		{
			get => _counterValue;
			set => SetAndRaiseIfChanged(ref _counterValue, value);
		}


		public Command<String> DecrementCounterCommand { get; }


		public Command<String> IncrementCounterCommand { get; }


		public Command ResetCounterCommand { get; }
	}
}
