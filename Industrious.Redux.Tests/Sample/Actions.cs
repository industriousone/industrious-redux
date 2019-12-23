using System;

namespace Industrious.Redux.Tests.Sample
{
	/// <summary>
	///  Actions describe "things to do" with the application state. They provide
	///  a library of all the application state changes that can be requested, along
	///  with any local state required to make the change.
	/// </summary>
	public static class Actions
	{
		/// <summary>
		///  Update the name.
		/// </summary>
		public class SetName
		{
			public SetName(String value)
			{
				Value = value;
			}

			public String Value { get; }
		}


		/// <summary>
		///  Update the number.
		/// </summary>
		public class SetNumber
		{
			public SetNumber(Int32 value)
			{
				Value = value;
			}

			public Int32 Value { get; }
		}


		/// <summary>
		///  Updates a value in the list of items.
		/// </summary>
		public class UpdateItem
		{
			public UpdateItem(Int32 index, String value)
			{
				Index = index;
				Value = value;
			}

			public Int32 Index { get; }

			public String Value { get; }
		}


		/// <summary>
		///  Remove a value from the list of items.
		/// </summary>
		public class DeleteItem
		{
			public DeleteItem(Int32 index)
			{
				Index = index;
			}

			public Int32 Index { get; }
		}
	}
}
