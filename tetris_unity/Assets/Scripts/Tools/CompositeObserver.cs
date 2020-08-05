using System;

namespace Tools
{
    public class CompositeObserver<T> : IObserver<T>
    {
        private readonly IObserver<T>[] _observers;
        
        public CompositeObserver(params IObserver<T>[] observers)
        {
            _observers = observers;
        }

        public void OnCompleted()
        {
            foreach (var observer in _observers)
            {
                observer.OnCompleted();
            }
        }

        public void OnError(Exception error)
        {
            foreach (var observer in _observers)
            {
                observer.OnError(error);
            }
        }

        public void OnNext(T value)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(value);
            }
        }
    }
}