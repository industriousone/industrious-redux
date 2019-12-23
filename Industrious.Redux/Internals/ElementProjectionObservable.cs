using System;
using System.Collections.Generic;

namespace Industrious.Redux.Internals
{
	internal class ElementProjectionObservable<TState, T, K> : ProjectionObservable<T>, IStateListener<TState>
	{
		private readonly StoreBase<TState> _store;
		private readonly ElementProjector<TState, T, K> _projector;
		private readonly K _elementKey;
		private T _currentValue;


		public ElementProjectionObservable(StoreBase<TState> store, ElementProjector<TState, T, K> projector, K elementKey, Action onEmpty)
			: base(onEmpty)
		{
			_store = store;
			_projector = projector;
			_elementKey = elementKey;
			_currentValue = projector(_store.CurrentState, elementKey);
		}


		public void OnStateChanged(TState newState)
		{
			T newValue = _projector(newState, _elementKey);

			if (!typeof(T).IsValueType && EqualityComparer<T>.Default.Equals(newValue, default(T)))
			{
				// value is a reference type and has become null
				Complete();
			}
			else if (!EqualityComparer<T>.Default.Equals(_currentValue, newValue))
			{
				// value has changed
				_currentValue = newValue;
				PublishValue(newValue);
			}
		}


		protected override void OnObserverAdded(IObserver<T> observer)
		{
			var currentValue = _projector(_store.CurrentState, _elementKey);
			observer.OnNext(currentValue);
		}
	}
}
