using System;
using System.Collections.Immutable;

namespace Industrious.Redux.Internals
{
	/// <summary>
	///  Track a list of subscribers to a specific projection of the store's stage and notify
	///  them when the projected value changes.
	/// </summary>
	internal abstract class ProjectionObservable<T> : IProjectionObservable<T>
	{
		private ImmutableList<IObserver<T>> _observers = ImmutableList<IObserver<T>>.Empty;
		private readonly Action _onEmpty;


		/// <summary>
		///  Create a new observable.
		/// </summary>
		/// <param name="onEmpty">
		///  An action to call when the last observer unsubscribes.
		/// </param>
		protected ProjectionObservable(Action onEmpty)
		{
			_onEmpty = onEmpty;
		}


		protected abstract void OnObserverAdded(IObserver<T> observer);


		/// <summary>
		///  Immediately send a value to all subscribers.
		/// </summary>
		public void PublishValue(T value)
		{
			_observers.ForEach(observer => observer.OnNext(value));
		}


		/// <summary>
		///  Subscribe for notifications from this observable.
		/// </summary>
		/// <param name="onNext">
		///  An action to be called when the observable changes.
		/// </param>
		public IDisposable Subscribe(Action<T> onNext)
		{
			return Subscribe(onNext, null);
		}


		/// <summary>
		///  Subscribe for notifications from this observable.
		/// </summary>
		/// <param name="onNext">
		///  An action to be called when the observable changes.
		/// </param>
		/// <param name="onComplete">
		///  An action to be called when the observable completes.
		/// </param>
		public IDisposable Subscribe(Action<T> onNext, Action onComplete)
		{
			if (onNext == null)
				throw new ArgumentNullException(nameof(onNext));

			return Subscribe(new ProjectionObserver<T>(onNext, onComplete));
		}


		/// <summary>
		///  Subscribe for notifications from this observable.
		/// </summary>
		/// <param name="observer">
		///  The observer to be notified when the observable changes.
		/// </param>
		public IDisposable Subscribe(IObserver<T> observer)
		{
			if (observer == null)
				throw new ArgumentNullException(nameof(observer));

			if (_observers.Contains(observer) == false)
			{
				_observers = _observers.Add(observer);
				OnObserverAdded(observer);
			}

			return new Unsubscriber(() => Unsubscribe(observer));
		}


		protected void Complete()
		{
			_observers.ForEach(observer =>
			{
				// completing one observer may cause others to unsubscribe
				if (_observers.Contains(observer))
					observer.OnCompleted();
			});

			_observers = _observers.Clear();
			_onEmpty.Invoke();
		}


		private void Unsubscribe(IObserver<T> observer)
		{
			if (_observers.Contains(observer))
				_observers = _observers.Remove(observer);

			if (_observers.Count == 0)
				_onEmpty.Invoke();
		}


		/// <summary>
		///  An <see cref="IDisposable"/> to be returned from the <c>Subscribe</c> calls.
		///  Wraps a call to <see cref="ProjectionObservable{T}.Unsubscribe(IObserver{T})"/>.
		/// </summary>
		private class Unsubscriber : IDisposable
		{
			private readonly Action _unsubscriber;

			public Unsubscriber(Action unsubscriber)
			{
				_unsubscriber = unsubscriber;
			}

			public void Dispose()
			{
				_unsubscriber.Invoke();
			}
		}
	}
}
