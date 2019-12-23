using System;

namespace Industrious.Redux.Tests.Sample
{
	/// <summary>
	///  The collection of application state accessor methods.
	/// </summary>
	/// <remarks>
	///  Each projection is a pure function which extracts and returns a subset of
	///  the application state. Taken together, these projections provide a library
	///  of the "whats" that can be asked about the state, while encapsulating the
	///  "how". They work together with <c>Store.Observe()</c> to provide notifications
	///  when state values change.
	/// </remarks>
	public static class Projections
	{
		/// <summary>
		///  Retrieve the name.
		/// </summary>
		public static String Name(SampleState state)
		{
			return (state.Name);
		}


		/// <summary>
		///  Retrieve the number.
		/// </summary>
		public static Int32 Number(SampleState state)
		{
			return (state.Number);
		}


		/// <summary>
		///  Retrieve the item at the specified index of the item collection.
		/// </summary>
		/// <returns>
		///  The item at the specified index if one is available, or <c>null</c>.
		/// </returns>
		public static String Item(SampleState state, Int32 index)
		{
			return (index >= 0 && index < state.Items.Count)
				? state.Items[index]
				: null;
		}
	}
}
