using System;
using System.Collections.Generic;

namespace MVP.Pools
{
    public class Pool<TModel, TView, TPresenter> : IDisposable
        where TPresenter : Presenter<TModel, TView>
    {
        #region Fields
        private readonly Queue<MvpPack<TModel, TView, TPresenter>> _queue;

        private readonly Func<MvpPack<TModel, TView, TPresenter>> _create;
        private readonly Action<MvpPack<TModel, TView, TPresenter>> _actionOnGet;
        private readonly Action<MvpPack<TModel, TView, TPresenter>> _actionOnRelease;
        private readonly Action<MvpPack<TModel, TView, TPresenter>> _actionOnDestroy;

        private readonly int _maxSize;
        private readonly bool _collectionCheck;
        #endregion
        
        public int Count => _queue.Count;

        public Pool(
            Func<MvpPack<TModel, TView, TPresenter>> create,
            Action<MvpPack<TModel, TView, TPresenter>> actionOnGet = null,
            Action<MvpPack<TModel, TView, TPresenter>> actionOnRelease = null,
            Action<MvpPack<TModel, TView, TPresenter>> actionOnDestroy = null,
            bool collectionCheck = false, int defaultCapacity = 10, int maxSize = 10000)
        {
            if (maxSize <= 0) throw new ArgumentException($"MaxSize can't be less than 1. MaxSize = {maxSize}");
            
            _queue = new Queue<MvpPack<TModel, TView, TPresenter>>(defaultCapacity);
            _create = create;
            _actionOnGet = actionOnGet;
            _actionOnRelease = actionOnRelease;
            _actionOnDestroy = actionOnDestroy;
            _collectionCheck = collectionCheck;
            _maxSize = maxSize;
        }

        public MvpPack<TModel, TView, TPresenter> Get()
        {
            if (Count == 0) return _create();
            
            var mvp = _queue.Dequeue();
            _actionOnGet?.Invoke(mvp);
            return mvp;
        }

        public void Release(MvpPack<TModel, TView, TPresenter> mvp)
        {
            if (_collectionCheck && _queue.Count > 0 && _queue.Contains(mvp))
                throw new InvalidOperationException("Trying to release an object that has already been released to the pool.");

            if (Count < _maxSize)
            {
                _queue.Enqueue(mvp);
                _actionOnRelease?.Invoke(mvp);
            }
            else _actionOnDestroy?.Invoke(mvp);
        }

        public void Clear()
        {
            if (_actionOnDestroy != null)
                foreach (var mvp in _queue)
                    _actionOnDestroy?.Invoke(mvp);
            
            _queue.Clear();
        }

        public void Dispose() => Clear();
    }
}
