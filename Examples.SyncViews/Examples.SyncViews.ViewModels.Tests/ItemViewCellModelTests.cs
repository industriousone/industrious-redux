using System;
using Xunit;

using Industrious.Redux;

namespace Examples.SyncViews.ViewModels.Tests
{
	public class ItemViewCellModelTests
	{
		private readonly MockStore<AppState> _store;
		private readonly Item _testItem;
		private readonly ItemViewCellModel _sut;

		public ItemViewCellModelTests()
		{
			_store = new MockStore<AppState>(AppState.SampleState);

			Guid itemId = AppState.SampleState.Index[1];
			_testItem = _store.CurrentState.Items[itemId];
			_sut = new ItemViewCellModel(_store, itemId);
		}


		[Fact]
		public void Constructor_SetsText_ToStoreValue()
		{
			Assert.Equal(_testItem.Text, _sut.Text);
		}


		[Fact]
		public void Constructor_SetsToggleValue_ToStoreValue()
		{
			Assert.Equal(_testItem.ToggleValue, _sut.ToggleValue);
		}


		[Fact]
		public void ToggleValue_Updates_WhenStoreValueChanges()
		{
			var newItem = _testItem.WithToggleValue(true);
			_store.Publish(Projections.Item, newItem.ID, newItem);
			Assert.Equal(newItem.ToggleValue, _sut.ToggleValue);
		}


		[Fact]
		public void ToggleValue_RaisesPropertyChanged_WhenValueChanges()
		{
			Boolean eventWasRaised = false;
			_sut.PropertyChanged += (sender, e) => { eventWasRaised = true; };
			_sut.ToggleValue = !_sut.ToggleValue;
			Assert.True(eventWasRaised);

		}


		[Fact]
		public void DeleteItemCommand_SendsDeleteItemAction()
		{
			_sut.DeleteItemCommand.Execute(null);
			Assert.Equal(new Actions.DeleteItem(_testItem.ID), _store.ReceivedActions[0]);
		}


		[Fact]
		public void ToggleItemCommand_SendsToggleItemAction()
		{
			_sut.ToggleItemCommand.Execute(true);
			Assert.Equal(new Actions.ToggleItem(_testItem.ID, true), _store.ReceivedActions[0]);
		}
	}
}
