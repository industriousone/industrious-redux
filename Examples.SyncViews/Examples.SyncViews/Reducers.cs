using System;

namespace Examples.SyncViews
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
			case Actions.AddItem a:
				return state.AddItem(new Item());

			case Actions.DeleteItem a:
				return state.DeleteItem(a.ItemId);

			case Actions.ResetItems a:
				return AppState.Empty;

			case Actions.ToggleItem a:
				return state.ToggleItem(a.ItemId, a.ToggleValue);
			}

			return (state);
		}
	}
}
