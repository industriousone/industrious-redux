namespace Industrious.Redux;

// Enable updating state selections without knowing the output type
public interface IReduxSelector<in T>
{
	void Update (T newValue);
}
