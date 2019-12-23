using System;
using Xunit;

namespace Examples.Counter.Tests
{
	public class AppStateTests
	{
		[Fact]
		public void Constructor_DefaultsCounterToZero()
		{
			var sut = new AppState();
			Assert.Equal(0, sut.CounterValue);
		}


		[Fact]
		public void ConstructorWithParams_SetsCounter()
		{
			var sut = new AppState(2);
			Assert.Equal(2, sut.CounterValue);
		}
	}
}
