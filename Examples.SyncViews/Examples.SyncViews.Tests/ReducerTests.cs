using System;
using System.Collections.Generic;
using Xunit;

namespace Examples.SyncViews.Tests
{
	public class ReducerTests
	{
		[Fact]
		public void Root_PassesThroughPreviousState_OnUnknownAction()
		{
			var newState = Reducers.Root(AppState.Empty, "Unknown action");
			Assert.Equal(AppState.Empty, newState);
		}


		[Fact]
		public void Root_AddsItem_OnAddItem()
		{
			var newState = Reducers.Root(AppState.Empty, new Actions.AddItem());
			Assert.Single(newState.Items);
		}


		[Fact]
		public void Root_DeletesItem_OnDeleteItem()
		{
			var itemId = AppState.SampleState.Index[1];
			var newState = Reducers.Root(AppState.SampleState, new Actions.DeleteItem(itemId));
			Assert.DoesNotContain(itemId, (IDictionary<Guid, Item>)newState.Items);
		}


		[Fact]
		public void Root_EmptiesItemList_OnResetItems()
		{
			var newState = Reducers.Root(AppState.SampleState, new Actions.ResetItems());
			Assert.Empty(newState.Items);
		}


		[Fact]
		public void Root_TogglesItemValue_OnToggleItem()
		{
			var item = new Item();
			var state = AppState.FromItems(new Item[] { item });
			var newState = Reducers.Root(state, new Actions.ToggleItem(item.ID, true));
			Assert.True(newState.Items[item.ID].ToggleValue);
		}
	}
}
