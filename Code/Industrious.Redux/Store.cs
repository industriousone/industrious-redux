namespace Industrious.Redux;

public class Store<TState> : ReduxObservable<TState>
{
	private readonly Func<TState, Object, TState> _reducer;
	private readonly SynchronizationContext? _synchronizationContext;

	private readonly SemaphoreSlim _dispatchingSemaphore = new(1, 1);


	public Store (Func<TState, Object, TState> reducer, TState initialState, SynchronizationContext? syncContext = null)
		: base(initialState)
	{
		_reducer = reducer;
		_synchronizationContext = syncContext;
	}


	public async Task DispatchAsync (Object action)
	{
		if (_synchronizationContext is not null)
		{
			_synchronizationContext.Post (_ => ReduceAndNotify (action), null);
		}
		else
		{
			await _dispatchingSemaphore.WaitAsync ();
			try
			{
				ReduceAndNotify (action);
			}
			finally
			{
				_dispatchingSemaphore.Release ();
			}
		}
	}


	private void ReduceAndNotify (Object action)
	{
		var newValue = _reducer (Current, action);
		Publish (newValue);
	}
}
