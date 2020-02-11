using System;
using Xunit;

namespace Examples.Counter.Tests
{
	public class ProjectionTests
	{
		[Fact]
		public void CounterValue_ReturnsCounter()
		{
			var value = Projections.CounterValue(new AppState(8));
			Assert.Equal(8, value);
		}
	}
}
