using System;
using Xunit;

namespace Examples.SyncViews.Tests
{
	public class ProjectionTests
	{
		[Fact]
		public void Index_ReturnsIndex_WhenIndexIsSet()
		{
			var value = Projections.Index(AppState.SampleState);
			Assert.Equal(AppState.SampleState.Index, value);
		}


		[Fact]
		public void Index_ReturnsEmptyList_WhenIndexIsNotSet()
		{
			var value = Projections.Index(AppState.Empty);
			Assert.Empty(value);
		}


		[Fact]
		public void Item_ReturnsCorrectItem_WhenItemExists()
		{
			var itemId = AppState.SampleState.Index[0];
			var value = Projections.Item(AppState.SampleState, itemId);
			Assert.Equal(AppState.SampleState.Items[itemId], value);
		}


		[Fact]
		public void Item_ReturnsNull_WhenItemDoesNotExist()
		{
			var value = Projections.Item(AppState.SampleState, Guid.NewGuid());
			Assert.Null(value);
		}
	}
}
