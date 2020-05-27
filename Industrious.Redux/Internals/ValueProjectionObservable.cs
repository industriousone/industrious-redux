using System;
using System.Collections.Generic;

namespace Industrious.Redux.Internals
{
	internal class ValueProjectionObservable<TState, T> : ProjectionObservable<T>, IStateListener<TState>
	{
		private readonly StoreBase<TState> _store;
		private readonly ValueProjector<TState, T> _projector;
		private T _currentValue;


		public ValueProjectionObservable(StoreBase<TState> store, ValueProjector<TState, T> projector, Action onEmpty)
			: base(onEmpty)
		{
			_store = store;
			_projector = projector;
			_currentValue = projector(_store.CurrentState);
		}


		public void OnStateChanged(TState newState)
		{
			T newValue = _projector(newState);
			if (!EqualityComparer<T>.Default.Equals(_currentValue, newValue))
			{
				_currentValue = newValue;
				PublishValue(newValue);
			}
		}


		protected override void OnObserverAdded(IObserver<T> observer)
		{
			var currentValue = _projector(_store.CurrentState);
			observer.OnNext(currentValue);
		}
	}
}
