using System;
using System.Collections.Immutable;

namespace Industrious.Redux.Internals
{
	public abstract class StoreBase<TState> : IStore<TState>
	{
		// Map "raw" projector functions to an Observable, which tracks the list of subscribers
		private ImmutableDictionary<Object, IStateListener<TState>> _observables = ImmutableDictionary<Object, IStateListener<TState>>.Empty;


		/// <summary>
		///  Return the current application state.
		/// </summary>
		public TState CurrentState { get; protected set; }


		/// <summary>
		///  Dispatches an action to the store's reducer.
		/// </summary>
		public abstract void Dispatch(Object action);


		/// <summary>
		///  Observe a projection of the application state for changes.
		/// </summary>
		/// <typeparam name="T">
		///  The projector's return type.
		/// </typeparam>
		/// <param name="projector">
		///  The projection function. Projections are pure static functions which receive
		///  the current state and return a value based on that state.
		/// </param>
		/// <returns>
		///  An <see cref="IObservable{T}"/> which can be subscribed for changes.
		/// </returns>
		public IProjectionObservable<T> Observe<T>(ValueProjector<TState, T> projector)
		{
			if (projector == null)
				throw new ArgumentNullException(nameof(projector));

			// if we've seen this projector before, reuse the observable we created for it, else create one
			var observable = GetObservableForProjection<T>(projector);
			if (observable == null)
			{
				observable = new ValueProjectionObservable<TState, T>(this, projector, () => ForgetProjection(projector));
				_observables = _observables.Add(projector, (IStateListener<TState>)observable);
			}

			return (observable);
		}


		/// <summary>
		///  Observe an item contained by a collection within the application state.
		/// </summary>
		/// <typeparam name="T">
		///  The collection item's type.
		/// </typeparam>
		/// <typeparam name="K">
		///  The collection's key or index type.
		/// </typeparam>
		/// <param name="projector">
		///  The projection function. Projections are pure static functions which receive
		///  the current state and return a value based on that state.
		/// </param>
		/// <param name="elementKey">
		///  The collection key or index value to be observed.
		/// </param>
		/// <returns>
		///  An <see cref="IObservable{T}"/> which can be subscribed for changes.
		/// </returns>
		public IProjectionObservable<T> ObserveElement<T, K>(ElementProjector<TState, T, K> projector, K elementKey)
		{
			if (projector == null)
				throw new ArgumentNullException(nameof(projector));

			// if we've seen this projector before, reuse the observable we created for it, else create one
			var observableCollectionKey = Tuple.Create(projector, elementKey);
			var observable = GetObservableForProjection<T>(observableCollectionKey);
			if (observable == null)
			{
				observable = new ElementProjectionObservable<TState, T, K>(this, projector, elementKey, () => ForgetProjection(observableCollectionKey));
				_observables = _observables.Add(observableCollectionKey, (IStateListener<TState>)observable);
			}

			return (observable);
		}


		/// <summary>
		///  Apply a projection function to the store and return its value immediately.
		/// </summary>
		/// <typeparam name="T">
		///  The projector's return type.
		/// </typeparam>
		/// <param name="projector">
		///  The projection function. Projections are pure static functions which receive
		///  the current state and return a value based on that state.
		/// </param>
		/// <returns>
		///  The value returned by the projection function.
		/// </returns>
		public T Project<T>(ValueProjector<TState, T> projector)
		{
			var value = projector(CurrentState);
			return (value);
		}


		/// <summary>
		///  Apply a projection function to the store and return its value immediately.
		/// </summary>
		/// <typeparam name="T">
		///  The projector's return type.
		/// </typeparam>
		/// <typeparam name="K">
		///  The collection's key or index type.
		/// </typeparam>
		/// <param name="projector">
		///  The projection function. Projections are pure static functions which receive
		///  the current state and return a value based on that state.
		/// </param>
		/// <param name="elementKey">
		///  The collection key or index value to be observed.
		/// </param>
		/// <returns>
		///  The value returned by the projection function.
		/// </returns>
		public T Project<T, K>(ElementProjector<TState, T, K> projector, K elementKey)
		{
			var value = projector(CurrentState, elementKey);
			return (value);
		}


		internal ProjectionObservable<T> GetObservableForProjection<T>(Object key)
		{
			ProjectionObservable<T> observable = null;

			if (_observables.ContainsKey(key))
				observable = _observables[key] as ProjectionObservable<T>;

			return (observable);
		}


		protected void UpdateAllObservables(TState newState)
		{
			foreach (var observable in _observables.Values)
			{
				observable.OnStateChanged(newState);
			}
		}


		private void ForgetProjection(Object key)
		{
			_observables = _observables.Remove(key);
		}
	}
}
