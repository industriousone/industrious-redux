using System;
using System.Runtime.ExceptionServices;

namespace Industrious.Redux.Internals
{
	/// <summary>
	///  An <see cref="IObserver{T}"/> wrapper around an <see cref="Action{T}"/>.
	/// </summary>
	internal class ProjectionObserver<T> : IObserver<T>
	{
		private readonly Action<T> _onNext;
		private readonly Action _onComplete;


		public ProjectionObserver(Action<T> onNext, Action onComplete)
		{
			_onNext = onNext;
			_onComplete = onComplete;
		}


		public void OnCompleted()
		{
			_onComplete?.Invoke();
		}


		public void OnError(Exception error)
		{
			ExceptionDispatchInfo.Capture(error).Throw();
		}


		public void OnNext(T value)
		{
			_onNext.Invoke(value);
		}
	}
}
