using System;

namespace Examples.Counter
{
	public static class Projections
	{
		public static Int32 CounterValue(AppState state)
		{
			return (state.CounterValue);
		}
	}
}
