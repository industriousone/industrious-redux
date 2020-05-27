using System;
using Xunit;

using Industrious.Redux;

namespace Examples.Counter.ViewModels.Tests
{
	public class MainPageModelTests
	{
		private MockStore<AppState> _store = new MockStore<AppState>(new AppState());


		[Fact]
		public void Constructor_InitializesCounterValueToStoreValue()
		{
			_store.SetState(new AppState(99));
			var sut = new MainPageModel(_store);
			Assert.Equal(99, sut.CounterValue);
		}


		[Fact]
		public void CounterValue_Updates_WhenStoreValueChanges()
		{
			var sut = new MainPageModel(_store);
			_store.Publish(Projections.CounterValue, 5);
			Assert.Equal(5, sut.CounterValue);
		}


		[Fact]
		public void CounterValue_RaisesPropertyChanged_WhenValueChanges()
		{
			Boolean eventWasRaised = false;

			var sut = new MainPageModel(_store);
			sut.PropertyChanged += (sender, e) => { eventWasRaised = true; };

			sut.CounterValue = 7;
			Assert.True(eventWasRaised);
		}


		[Fact]
		public void DecrementCounterCommand_SendsDecrementAction()
		{
			var sut = new MainPageModel(_store);
			sut.DecrementCounterCommand.Execute("1");
			Assert.Equal(new Actions.DecrementCounter(1), _store.ReceivedActions[0]);
		}


		[Fact]
		public void IncrementCounterCommand_SendsIncrementAction()
		{
			var sut = new MainPageModel(_store);
			sut.IncrementCounterCommand.Execute("1");
			Assert.Equal(new Actions.IncrementCounter(1), _store.ReceivedActions[0]);
		}


		[Fact]
		public void ResetCounterCommand_SendsResetAction()
		{
			var sut = new MainPageModel(_store);
			sut.ResetCounterCommand.Execute(null);
			Assert.Equal(new Actions.ResetCounter(), _store.ReceivedActions[0]);
		}
	}
}
