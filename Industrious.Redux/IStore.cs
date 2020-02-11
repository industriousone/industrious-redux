using System;

namespace Industrious.Redux
{
	public interface IStore<TState>
	{
		TState CurrentState { get; }

		void Dispatch(Object action);

		IProjectionObservable<T> Observe<T>(ValueProjector<TState, T> projector);

		IProjectionObservable<T> ObserveElement<T, K>(ElementProjector<TState, T, K> projector, K key);
	}
}
