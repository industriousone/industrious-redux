using System;
using System.Threading;

using Industrious.Redux.Internals;

namespace Industrious.Redux
{
	/// <summary>
	///  The store holds and manages the application's state. Changes to the state
	///  are initiated by sending an action to <see cref="Dispatch(object)"/>, which
	///  is then forwarded to the <see cref="Reducer{TState}">reducers</see>.
	///  Application code can respond to changes in the state by subscribing observers,
	///  using a <see cref="ValueProjector{TState, T}">projector</see> to select the
	///  value(s) of interest.
	/// </summary>
	/// <typeparam name="TState">
	///  The type representing the application state.
	/// </typeparam>
	public class Store<TState> : StoreBase<TState>
	{
		private readonly Reducer<TState> _reducer;
		private readonly SynchronizationContext _syncContext;
		private readonly Object _stateLock = new Object();


		/// <summary>
		///  Create a new store instance. You should only have a single store in your application.
		/// </summary>
		/// <param name="reducer">
		///  The root reducer, which produces new application states in response to actions.
		/// </param>
		/// <param name="initialState">
		///  The initial application state.
		/// </param>
		/// <param name="syncContext">
		///  If provided, all observer callbacks will be done within this context. The most common
		///  use would be to pass in <see cref="SynchronizationContext.Current"/> from the UI thread;
		///  all observer callbacks will then also happen on the UI thread.
		/// </param>
		public Store(Reducer<TState> reducer, TState initialState, SynchronizationContext syncContext = null)
		{
			_reducer = reducer ?? throw new ArgumentNullException(nameof(reducer));
			_syncContext = syncContext;
			CurrentState = initialState;
		}


		/// <summary>
		///  Dispatches an action to the store's reducer, causing the application's state
		///  to be advanced. Dispatches are performed synchronously and run on the caller's
		///  thread.
		/// </summary>
		/// <param name="action">
		///  An object which represents the action to be taken. Any type of object may be
		///  used; it is up to the application's reducers to interpret.
		/// </param>
		public override void Dispatch(Object action)
		{
			TState newState;

			lock (_stateLock)
			{
				newState = _reducer(CurrentState, action);
				CurrentState = newState;
			}

			if (_syncContext != null)
			{
				_syncContext.Post(e => UpdateAllObservables(newState), null);
			}
			else
			{
				UpdateAllObservables(newState);
			}
		}
	}
}
