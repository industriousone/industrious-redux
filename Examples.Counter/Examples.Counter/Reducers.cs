using System;

namespace Examples.Counter
{
	/// <summary>
	///  The reducers produce new application states in response to actions. They are
	///  called by the Store when a new action is passed to <c>Dispatch()</c>. Reducers
	///  should always be pure static functions with no side effects.
	/// </summary>
	public static class Reducers
	{
		public static AppState Root(AppState state, Object action)
		{
			switch (action)
			{
			case Actions.DecrementCounter a:
				return (new AppState(state.CounterValue - a.Amount));

			case Actions.IncrementCounter a:
				return (new AppState(state.CounterValue + a.Amount));

			case Actions.ResetCounter _:
				return (new AppState(0));
			}

			// if I don't recognize the action, return the state unchanged
			return (state);
		}
	}
}
