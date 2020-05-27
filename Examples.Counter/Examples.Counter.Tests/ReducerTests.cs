using System;
using Xunit;

namespace Examples.Counter.Tests
{
	public class ReducerTests
	{
		[Fact]
		public void Root_PassesThroughPreviousState_OnUnknownAction()
		{
			var initialState = new AppState();
			var newState = Reducers.Root(initialState, "Unknown action");
			Assert.Equal(initialState, newState);
		}


		[Fact]
		public void Root_DecrementsValue_OnDecrementAction()
		{
			var newState = Reducers.Root(new AppState(), new Actions.DecrementCounter(5));
			Assert.Equal(-5, newState.CounterValue);
		}


		[Fact]
		public void Root_IncrementsValue_OnIncrementAction()
		{
			var newState = Reducers.Root(new AppState(), new Actions.IncrementCounter(5));
			Assert.Equal(5, newState.CounterValue);
		}


		[Fact]
		public void Root_SetsValueToZero_OnResetAction()
		{
			var newState = Reducers.Root(new AppState(8), new Actions.ResetCounter());
			Assert.Equal(0, newState.CounterValue);
		}
	}
}
