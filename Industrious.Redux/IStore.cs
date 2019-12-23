using System;

namespace Industrious.Redux
{
	public interface IStore<TState>
	{
		TState CurrentState { get; }

		void Dispatch(Object action);

		IProjectionObservable<T> Observe<T>(ValueProjector<TState, T> projector);
	}
}
