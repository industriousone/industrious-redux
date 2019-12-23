using System;
using System.Collections.Immutable;

namespace Industrious.Redux.Tests.Sample
{
	/// <summary>
	///  Reducers produce new application states in response to actions.
	/// </summary>
	public static class Reducers
	{
		/// <summary>
		///  The "entry point" of the reducer. The store will call here when it has a
		///  new action to be processed.1
		/// </summary>
		public static SampleState Root(SampleState state, Object action)
		{
			var name = Reducers.Name(state.Name, action);
			var number = Reducers.Number(state.Number, action);
			var items = Reducers.Items(state.Items, action);

			return (new SampleState(name, number, items));
		}


		/// <summary>
		///  Process an action and return the new value of <c>Name</c>.
		/// </summary>
		public static String Name(String state, Object action)
		{
			switch (action)
			{
			case Actions.SetName a:
				return (a.Value);
			}

			return (state);
		}


		/// <summary>
		///  Process an action and return the new value of <c>Number</c>.
		/// </summary>
		public static Int32 Number(Int32 state, Object action)
		{
			switch (action)
			{
			case Actions.SetNumber a:
				return (a.Value);
			}

			return (state);
		}


		/// <summary>
		///  Process an action and return the new list of items.
		/// </summary>
		public static ImmutableList<String> Items(ImmutableList<String> state, Object action)
		{
			switch (action)
			{
			case Actions.UpdateItem a:
				if (a.Index >= 0 && a.Index < state.Count)
					return state.SetItem(a.Index, a.Value);
				break;

			case Actions.DeleteItem a:
				if (a.Index >= 0 && a.Index < state.Count)
					return (state.RemoveAt(a.Index));
				break;
			}

			return (state);
		}
	}
}
