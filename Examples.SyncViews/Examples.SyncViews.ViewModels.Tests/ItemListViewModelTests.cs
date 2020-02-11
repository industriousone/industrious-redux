using System;
using Xunit;

using Industrious.Redux;
using System.Collections.Immutable;

namespace Examples.SyncViews.ViewModels.Tests
{
	public class ItemListViewModelTests
	{
		private readonly MockStore<AppState> _store = new MockStore<AppState>();


		[Fact]
		public void Constructor_InitializesItems_ToStoreValue()
		{
			_store.SetState(AppState.SampleState);
			var sut = new ItemListViewModel(_store);
			Assert.Equal(AppState.SampleState.Items.Count, sut.Items.Count);
		}


		[Fact]
		public void Items_Updates_WhenStoreValueChanges()
		{
			_store.SetState(AppState.SampleState);
			var sut = new ItemListViewModel(_store);

			// have to use ID of existing item or lookup in ItemViewCellModel will break
			var newIndex = ImmutableList<Guid>.Empty.Add(AppState.SampleState.Index[1]);
			_store.Publish(Projections.Index, newIndex);

			Assert.Equal(newIndex.Count, sut.Items.Count);
		}


		[Fact]
		public void Items_RaisesPropertyChanged_WhenValueChanges()
		{
			_store.SetState(AppState.SampleState);
			var sut = new ItemListViewModel(_store);

			Boolean eventWasRaised = false;
			sut.PropertyChanged += (sender, e) => { eventWasRaised = (e.PropertyName == nameof(sut.Items)); };

			_store.Publish(Projections.Index, ImmutableList<Guid>.Empty.Add(AppState.SampleState.Index[1]));

			Assert.True(eventWasRaised);
		}
	}
}
