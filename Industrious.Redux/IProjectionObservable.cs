using System;

namespace Industrious.Redux
{
	/// <summary>
	///  Extend <see cref="IObservable{T}"/> to support subscribing with a simple 
	/// <see cref="Action{T}"/>, instead of a full blown <see cref="IObserver{T}"/>.
	/// </summary>
	public interface IProjectionObservable<T> : IObservable<T>
	{
		IDisposable Subscribe(Action<T> onNext);

		IDisposable Subscribe(Action<T> onNext, Action onComplete);
	}
}
