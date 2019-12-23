using System;
using System.Collections.Immutable;

namespace Industrious.Redux.Tests.Sample
{
	/// <summary>
	///  An example application state object. State objects must be immutable.
	/// </summary>
	/// <remarks>
	///  I've chosen to use a <c>struct</c> for state as it is a little more efficient
	///  than classes, but it isn't a requirement.
	/// </remarks>
	public struct SampleState
	{
		/// <summary>
		///  An empty state object.
		/// </summary>
		/// <remarks>
		///  Since it's immutable this can be used anywhere we need a new state object,
		///  avoiding an object allocation.
		/// </remarks>
		public static readonly SampleState Empty = new SampleState(null, 0, ImmutableList<String>.Empty);


		public SampleState(String name, Int32 number, ImmutableList<String> items)
		{
			Name = name;
			Number = number;
			Items = items;
		}


		// For testing use of nullable types
		public String Name { get; }


		// For testing use of non-nullable types
		public Int32 Number { get; }
				

		// For testing use of list indices
		public ImmutableList<String> Items { get; }


		/// <summary>
		///  Returns a new state object, setting <c>Name</c>.
		/// </summary>
		public SampleState WithName(String value)
		{
			return new SampleState(value, this.Number, this.Items);
		}


		/// <summary>
		///  Returns a new state object, setting <c>Number</c>.
		/// </summary>
		public SampleState WithNumber(Int32 value)
		{
			return new SampleState(this.Name, value, this.Items);
		}


		/// <summary>
		///  Returns a new state object, replacing the list of items.
		/// </summary>
		public SampleState WithItems(params String[] values)
		{
			var builder = ImmutableList.CreateBuilder<String>();
			builder.AddRange(values);
			return new SampleState(this.Name, this.Number, builder.ToImmutable());
		}
	}
}
