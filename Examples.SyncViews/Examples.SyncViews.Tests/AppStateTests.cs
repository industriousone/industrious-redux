using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Examples.SyncViews.Tests
{
	public class AppStateTests
	{
		private readonly Item[] TestItems = new Item[]
		{
			new Item(),
			new Item()
		};


		[Fact]
		public void Constructor_SetsItemsProperty_WhenItemsProvided()
		{
			var newState = AppState.FromItems(TestItems);
			Assert.Equal(2, newState.Items.Count);
			Assert.Contains(TestItems[0].ID, (IDictionary<Guid, Item>)newState.Items);
			Assert.Contains(TestItems[1].ID, (IDictionary<Guid, Item>)newState.Items);
		}


		[Fact]
		public void Constructor_SetsIndexProperty_WhenItemsProvided()
		{
			var newState = AppState.FromItems(TestItems);
			Assert.Equal(TestItems.Select(Item => Item.ID), newState.Index);
		}


		[Fact]
		public void AddItem_AddsToItems()
		{
			var item = new Item();
			var newState = AppState.Empty.AddItem(item);
			Assert.Equal(item, newState.Items[item.ID]);
		}


		[Fact]
		public void AddItem_AddsToEndOfIndex()
		{
			var item = new Item();
			var newState = AppState.Empty.AddItem(item);
			Assert.Equal(item.ID, newState.Index[newState.Index.Count - 1]);
		}


		[Fact]
		public void DeleteItem_DeletesFromItems()
		{
			var itemId = AppState.SampleState.Index[1];
			var newState = AppState.Empty.DeleteItem(itemId);
			Assert.DoesNotContain(itemId, (IDictionary<Guid, Item>)newState.Items);
		}


		[Fact]
		public void DeleteItem_DeletesFromIndex()
		{
			var itemId = AppState.SampleState.Index[1];
			var newState = AppState.Empty.DeleteItem(itemId);
			Assert.DoesNotContain(itemId, newState.Index);
		}


		[Fact]
		public void ToggleItem_ReplacesItemInItems()
		{
			var itemId = AppState.SampleState.Index[1];
			var originalItem = AppState.SampleState.Items[itemId];
			var newState = AppState.SampleState.ToggleItem(itemId, !originalItem.ToggleValue);
			Assert.Equal(!originalItem.ToggleValue, newState.Items[itemId].ToggleValue);
		}
	}
}
