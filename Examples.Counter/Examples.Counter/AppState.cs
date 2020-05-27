using System;

namespace Examples.Counter
{
	/// <summary>
	///  The application state. Pretty simple for this example: just the counter
	///  value. For more complex projects your state will likely be composed of
	///  multiple objects, each related to a specific feature area.
	/// </summary>
	/// <remarks>
	///  State objects must be immutable.
	/// </remarks>
	public struct AppState
	{
		public AppState(Int32 counterValue = 0)
		{
			CounterValue = counterValue;
		}


		public Int32 CounterValue { get; }
	}
}
