using System;

namespace Examples.Counter
{
	public static class Actions
	{
		public struct DecrementCounter
		{
			public readonly Int32 Amount;

			public DecrementCounter(Int32 amount)
			{
				Amount = amount;
			}
		}


		public struct IncrementCounter
		{
			public readonly Int32 Amount;

			public IncrementCounter(Int32 amount)
			{
				Amount = amount;
			}
		}


		public struct ResetCounter
		{
		}
	}
}
