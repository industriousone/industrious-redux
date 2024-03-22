namespace Industrious.Redux.Tests;

public class ReduxObservableTests
{
	[Fact]
	public void Constructor_InitializesCurrent ()
	{
		var observable = new ReduxObservable<Int32> (8);
		Assert.Equal (8, observable.Current);
	}


	[Fact]
	public void Publish_UpdatesCurrent ()
	{
		var observable = new ReduxObservable<Int32> (0);
		observable.Publish (9);
		Assert.Equal (9, observable.Current);
	}


	[Fact]
	public void Publish_DoesNotNotify_WhenValueIsUnchanged()
	{
		var observable = new ReduxObservable<Int32> (0);

		var numberOfCalls = 0;
		observable.Subscribe (value => ++numberOfCalls);
		observable.Publish (0);

		Assert.Equal (1, numberOfCalls);
	}


	[Fact]
	public void Publish_NotifiesActions_WhenValueChanges ()
	{
		var observable = new ReduxObservable<Int32> (0);

		var lastObservedValue = 0;
		observable.Subscribe (value => lastObservedValue = value);

		observable.Publish (7);
		Assert.Equal (7, lastObservedValue);
	}


	[Fact]
	public void Publish_NotifiesObservers_WhenValueChanges ()
	{
		var observable = new ReduxObservable<Int32> (8);

		var observer = new MockObserver<Int32> ();
		observable.Subscribe (observer);

		observable.Publish (7);
		Assert.Equal (7, observer.Value);
	}


	[Fact]
	public void Publish_NotifiesSelectors_WhenValueChanges ()
	{
		var observable = new ReduxObservable<Int32> (0);

		var lastObservedValue = String.Empty;
		observable
			.Select (value => value.ToString())
			.Subscribe (value => lastObservedValue = value);

		observable.Publish (7);
		Assert.Equal ("7", lastObservedValue);
	}


	[Fact]
	public void Select_ReturnsNewObservableWithInitialValue ()
	{
		var observable = new ReduxObservable<Int32> (3);
		var selected = observable.Select (value => value.ToString ());
		Assert.Equal ("3", selected.Current);
	}


	[Fact]
	public void Subscribe_ImmediatelyPublishesCurrentValue ()
	{
		var observable = new ReduxObservable<Int32> (3);

		var lastObservedValue = 0;
		observable.Subscribe (value => lastObservedValue = value);

		Assert.Equal (3, lastObservedValue);
	}


	[Fact]
	public void Subscribe_InitializesObserver ()
	{
		var observable = new ReduxObservable<Int32> (3);

		var observer = new MockObserver<Int32> ();
		observable.Subscribe (observer);

		Assert.Equal (3, observer.Value);
	}


	[Fact]
	public void Unsubscribe_RemovesAction ()
	{
		var observable = new ReduxObservable<Int32> (0);

		var lastObservedValue = 0;
		var subscription = observable.Subscribe (value => lastObservedValue = value);
		observable.Publish (11);
		subscription.Dispose();
		observable.Publish (13);

		Assert.Equal (11, lastObservedValue);
	}


	[Fact]
	public void Unsubscribe_RemovesObserver ()
	{
		var observable = new ReduxObservable<Int32> (0);

		var observer = new MockObserver<Int32> ();
		var subscription = observable.Subscribe (observer);
		observable.Publish (11);
		subscription.Dispose();
		observable.Publish (13);

		Assert.Equal (11, observer.Value);
	}
}
