using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Industrious.Redux.Internals;

namespace Industrious.Redux
{
	/// <summary>
	///  A mock version of the store to assist with testing. Captures, but does
	///  not dispatch, incoming actions, and enables simulation of store changes.
	/// </summary>
	public class MockStore<TState> : StoreBase<TState>
	{
		readonly List<Object> _dispatchedActions = new List<Object>();


		public MockStore(TState initialState = default(TState))
		{
			CurrentState = initialState;
			ReceivedActions = new ReadOnlyCollection<Object>(_dispatchedActions);
		}


		/// <summary>
		///  Replaces the store state with a new value. Does not fire any projections,
		///  use <see cref="Publish{T}(ValueProjector{TState, T}, T)" /> to do so.
		/// </summary>
		public void SetState(TState state)
		{
			CurrentState = state;
		}


		public override void Dispatch(Object action)
		{
			_dispatchedActions.Add(action);
		}


		/// <summary>
		///  Returns a list of all actions that have been received by the store,
		///  in the order in which they were received.
		/// </summary>
		public ReadOnlyCollection<Object> ReceivedActions { get; }


		/// <summary>
		///  Send a value to all subscribers of a projection, simulating a change
		///  to the store state.
		/// </summary>
		/// <param name="projector">
		///  The projection function being simulated. The function is not called.
		/// </param>
		/// <param name="value">
		///  The value to be sent to the project subscribers.
		/// </param>
		public void Publish<T>(ValueProjector<TState, T> projector, T value)
		{
			if (projector == null)
				throw new ArgumentNullException(nameof(projector));

			var observable = GetObservableForProjection<T>(projector);
			observable?.PublishValue(value);
		}


		/// <summary>
		///  Send a value to all subscribers of a collection item projection, 
		///  simulating a change to the item state.
		/// </summary>
		/// <param name="projector">
		///  The projection function being simulated. The function is not called.
		/// </param>
		/// <param name="key">
		///  The collection key of the item being simulated.
		/// </param>
		/// <param name="value">
		///  The value to be sent to the project subscribers.
		/// </param>
		public void Publish<T, K>(ElementProjector<TState, T, K> projector, K key, T value)
		{
			if (projector == null)
				throw new ArgumentNullException(nameof(projector));

			var observable = GetObservableForProjection<T>(Tuple.Create(projector, key));
			observable?.PublishValue(value);
		}
	}
}
