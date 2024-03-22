namespace Industrious.Redux;

public class ReduxSelector<TIn, TOut> : IReduxSelector<TIn>
{
	private readonly Func<TIn, TOut> _selector;


	public ReduxSelector (TIn initialValue, Func<TIn, TOut> selector)
	{
		_selector = selector;
		Observable = new ReduxObservable<TOut> (selector (initialValue));
	}


	public ReduxObservable<TOut> Observable { get; init; }


	public void Update (TIn newValue)
	{
		Observable.Publish (_selector (newValue));
	}
}
