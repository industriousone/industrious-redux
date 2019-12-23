using System;

namespace Industrious.Redux.Internals
{
	/// <summary>
	///  Provide a way to notify the store observables when the underlying state changes
	///  without having to know the exact type of the observable.
	/// </summary>
	internal interface IStateListener<TState>
	{
		void OnStateChanged(TState newState);
	}
}
