namespace Industrious.Redux.Tests;

// A simple observer for testing correctness of published values
public class MockObserver<T> : IObserver<T>
{
	public Boolean Completed { get; private set; } = false;
	public T? Value { get; private set; }

	public void OnCompleted ()
	{
		Completed = true;
	}

	public void OnError (Exception error)
	{
		throw new NotImplementedException ();
	}

	public void OnNext (T value)
	{
		Value = value;
	}
}
