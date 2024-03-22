using System.Collections.Immutable;

namespace Industrious.Redux;

public class ReduxObservable<T> : IObservable<T>
{
	private class Subscription : IDisposable
	{
		private readonly Action _onUnsubscribe;

		public Subscription(Action onUnsubscribe)
		{
			_onUnsubscribe = onUnsubscribe;
		}

		public void Dispose()
		{
			_onUnsubscribe.Invoke();
		}
	}


	private ImmutableList<Action<T>> _subscribedActions = ImmutableList<Action<T>>.Empty;
	private ImmutableList<IObserver<T>> _subscribedObservers = ImmutableList<IObserver<T>>.Empty;
	private ImmutableDictionary<Object, IReduxSelector<T>> _selections = ImmutableDictionary<Object, IReduxSelector<T>>.Empty;


	public ReduxObservable (T initialValue)
	{
		Current = initialValue;
	}


	public T Current { get; private set; }


	public void Publish (T value)
	{
		if (EqualityComparer<T>.Default.Equals (Current, value))
			return;

		Current = value;

		foreach (var action in _subscribedActions)
			action.Invoke (Current);

		foreach (var observer in _subscribedObservers)
			observer.OnNext (Current);

		foreach (var selection in _selections.Values)
			selection.Update (Current);
	}


	public ReduxObservable<TValue> Select<TValue> (Func<T, TValue> selector)
	{
		if (!_selections.TryGetValue (selector, out var selection))
		{
			selection = new ReduxSelector<T, TValue> (Current, selector);
			_selections = _selections.Add (selector, selection);
		}

		return ((ReduxSelector<T, TValue>)selection).Observable;
	}


	public IDisposable Subscribe (Action<T> action)
	{
		ArgumentNullException.ThrowIfNull (action);

		if (!_subscribedActions.Contains (action))
		{
			_subscribedActions = _subscribedActions.Add (action);
			action.Invoke (Current);
		}

		return new Subscription(() => Unsubscribe(action));
	}


	public IDisposable Subscribe (IObserver<T> observer)
	{
		ArgumentNullException.ThrowIfNull (observer);

		_subscribedObservers = _subscribedObservers.Add (observer);
		observer.OnNext (Current);

		return new Subscription(() => Unsubscribe(observer));
	}


	private void Unsubscribe (Action<T> action)
	{
		_subscribedActions = _subscribedActions.Remove (action);
	}


	private void Unsubscribe (IObserver<T> observer)
	{
		_subscribedObservers = _subscribedObservers.Remove (observer);
	}
}
