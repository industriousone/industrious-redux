using System;
using Xunit;

using Industrious.Redux.Tests.Sample;

namespace Industrious.Redux.Tests
{
	public class StoreTests
	{
		/// <summary>
		///  When a new Store is created, <c>CurrentState</c> should be immediately
		///  set to the provided initial state.
		/// </summary>
		[Fact]
		public void Constructor_InitializesCurrentState()
		{
			var store = new Store<SampleState>(Reducers.Root, SampleState.Empty);
			Assert.Equal(SampleState.Empty, store.CurrentState);
		}


		/// <summary>
		///  When an action is received, it should be passed along to the reducer
		///  assigned to the store, producing a new state.
		/// </summary>
		[Fact]
		public void Dispatch_UpdatesStateViaReducer_OnActionRecieved()
		{
			var sut = new Store<SampleState>(Reducers.Root, SampleState.Empty);
			sut.Dispatch(new Actions.SetNumber(8));
			Assert.Equal(8, sut.CurrentState.Number);
		}


		/// <summary>
		///  New value observers should be called immediately with the current state value.
		/// </summary>
		[Fact]
		public void Observe_CallsSubscriber_ImmediatelyOnSubscribe()
		{
			var sut = new Store<SampleState>(Reducers.Root, SampleState.Empty.WithNumber(2));

			Int32? receivedValue = null;
			sut.Observe(Projections.Number).Subscribe(value => { receivedValue = value; });

			Assert.Equal(2, receivedValue);
		}


		/// <summary>
		///  Observers should be called again if the projected value they're observing changes.
		/// </summary>
		[Fact]
		public void Observe_CallsSubsciber_WhenProjectedValueChanges()
		{
			var sut = new Store<SampleState>(Reducers.Root, SampleState.Empty);

			Int32? receivedValue = null;
			sut.Observe(Projections.Number).Subscribe(value => { receivedValue = value; });

			receivedValue = null;
			sut.Dispatch(new Actions.SetNumber(7));

			Assert.Equal(7, receivedValue);
		}


		/// <summary>
		///  Observers should *not* be called if the projected value they're watching doesn't change.
		/// </summary>
		[Fact]
		public void Observe_DoesNotCallSubscriber_WhenProjectionIsUnchanged()
		{
			var sut = new Store<SampleState>(Reducers.Root, SampleState.Empty.WithNumber(7));

			Int32? receivedValue;
			sut.Observe(Projections.Number).Subscribe(value => { receivedValue = value; });

			receivedValue = null;
			sut.Dispatch(new Actions.SetNumber(7));

			Assert.Null(receivedValue);
		}


		/// <summary>
		///  Observers should stop calling subscribers that have been disposed.
		/// </summary>
		[Fact]
		public void Observe_StopsCallingSubscriber_AfterUnsubscribe()
		{
			var sut = new Store<SampleState>(Reducers.Root, SampleState.Empty);

			Int32? receivedValue;
			var subscription = sut.Observe(Projections.Number).Subscribe(value => { receivedValue = value; });

			subscription.Dispose();

			receivedValue = null;
			sut.Dispatch(new Actions.SetNumber(7));

			Assert.Null(receivedValue);
		}


		/// <summary>
		///  New element observers should be called immediately with the current element value.
		/// </summary>
		[Fact]
		public void ObserveElement_CallsSubscriber_ImmediatelyOnSubscribe()
		{
			var initialState = SampleState.Empty.WithItems("A", "B", "C");
			var sut = new Store<SampleState>(Reducers.Root, initialState);

			String receivedValue = null;
			sut.ObserveElement(Projections.Item, 1).Subscribe(value => { receivedValue = value; });

			Assert.Equal("B", receivedValue);
		}


		/// <summary>
		///  Observers should be called again if the projected value they're observing changes.
		/// </summary>
		[Fact]
		public void ObserveElement_CallsSubsciber_WhenProjectedValueChanges()
		{
			var initialState = SampleState.Empty.WithItems("A", "B", "C");
			var sut = new Store<SampleState>(Reducers.Root, initialState);

			String receivedValue;
			sut.ObserveElement(Projections.Item, 1).Subscribe(value => { receivedValue = value; });

			receivedValue = null;
			sut.Dispatch(new Actions.UpdateItem(1, "Spanish Inquisition"));

			Assert.Equal("Spanish Inquisition", receivedValue);
		}


		/// <summary>
		///  Observers should *not* be called if the projected value they're watching doesn't change.
		/// </summary>
		[Fact]
		public void ObserveElement_DoesNotCallSubscriber_WhenProjectionIsUnchanged()
		{
			var initialState = SampleState.Empty.WithItems("A", "B", "C");
			var sut = new Store<SampleState>(Reducers.Root, initialState);

			String receivedValue;
			sut.ObserveElement(Projections.Item, 1).Subscribe(value => { receivedValue = value; });

			receivedValue = null;
			sut.Dispatch(new Actions.UpdateItem(1, "B"));

			Assert.Null(receivedValue);
		}


		/// <summary>
		///  Observers should stop calling subscribers that have been disposed.
		/// </summary>
		[Fact]
		public void ObserveElement_StopsCallingSubscriber_AfterUnsubscribe()
		{
			var initialState = SampleState.Empty.WithItems("A", "B", "C");
			var sut = new Store<SampleState>(Reducers.Root, initialState);

			String receivedValue;
			var subscription = sut.ObserveElement(Projections.Item, 1).Subscribe(value => { receivedValue = value; });

			subscription.Dispose();

			receivedValue = null;
			sut.Dispatch(new Actions.UpdateItem(1, "Spanish Inquisition"));

			Assert.Null(receivedValue);
		}


		/// <summary>
		///  Element observers should complete their subscribers if the element they
		///  are observing is removed from the underlying collection. (This makes more
		///  sense when observing a value in a dictionary.)
		/// </summary>
		[Fact]
		public void ObserveElement_CompletesSubscriber_WhenItemIsRemovedFromCollection()
		{
			var initialState = SampleState.Empty.WithItems("A", "B", "C");
			var sut = new Store<SampleState>(Reducers.Root, initialState);

			Boolean completed = false;
			sut.ObserveElement(Projections.Item, 2).Subscribe(value => { }, () => { completed = true; });

			sut.Dispatch(new Actions.DeleteItem(2));

			Assert.True(completed);
		}


		/// <summary>
		///  Project immediately returns the state's current projected value.
		/// </summary>
		[Fact]
		public void Project_ReturnsCurrentValue()
		{
			var sut = new Store<SampleState>(Reducers.Root, SampleState.Empty.WithNumber(2));
			Assert.Equal(2, sut.Project(Projections.Number));
		}


		/// <summary>
		///  Project immediately returns the state's current projected value.
		/// </summary>
		[Fact]
		public void Project_WithElementKey_ReturnsCurrentValue()
		{
			var initialState = SampleState.Empty.WithItems("A", "B", "C");
			var sut = new Store<SampleState>(Reducers.Root, initialState);
			Assert.Equal("B", sut.Project(Projections.Item, 1));
		}
	}
}
