using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Object = UnityEngine.Object;

namespace EasyFramework
{
    public class CoroutineHandle: ICoroutineHandle<CoroutineHandle>
    {
        private CoroutineHandle()
        {

        }
        private static readonly EasyPool<CoroutineHandle> Pool = new EasyPool<CoroutineHandle>(() => new CoroutineHandle(), null, null, 32);

        public static CoroutineHandle Fetch()
        {
            var handle = Pool.Fetch();
            handle._tcs = new TaskCompletionSource<bool>();
            handle._tokenSource = new CancellationTokenSource();
            handle.Canceled = null;
            handle._completed = null;
            return handle;
        }
        
        private TaskCompletionSource<bool> _tcs;
        private CancellationTokenSource _tokenSource;
        
        public Task Task =>_tcs.Task;
        public CancellationToken Token=> _tokenSource.Token;
        public bool IsRecycled => ((IRecycleable) this).IsRecycled;
        public event Action<CoroutineHandle> Canceled;
        private event Action<CoroutineHandle> _completed;
        public event Action<CoroutineHandle> Completed
        {
            add
            {
                _completed += value;
                if(_tcs.Task.IsCompleted)
                {
                    value(this);
                }
            }
            remove => _completed -= value;
        }
        
        void IEnumerator.Reset(){}
        bool IEnumerator.MoveNext() => !Task.IsCompleted;
        object IEnumerator.Current => null;

        public void Cancel()
        {
            if(!_tcs.Task.IsCompleted&&_tcs.TrySetCanceled(_tokenSource.Token))
            {
                Canceled?.Invoke(this);
                EasyCoroutine.StopCoroutine(this);
                Pool.Recycle(this);
            }
        
        }
        void ICoroutineHandle.Complete()
        {
            if(_tcs.TrySetResult(true))
            {
                _completed?.Invoke(this);
                Pool.Recycle(this);
            }
        }

        bool IRecycleable.IsRecycled { get; set; }
        void IRecycleable.Recycle(){}
    }
    public class CoroutineHandle<TResult>: ICoroutineHandle<CoroutineHandle<TResult>,TResult>
    {
        private CoroutineHandle()
        {

        }
        private static readonly EasyPool<CoroutineHandle<TResult>> Pool = new EasyPool<CoroutineHandle<TResult>>(() => new CoroutineHandle<TResult>(), null, null, 32);

        public static CoroutineHandle<TResult> Fetch()
        {
            var handle = Pool.Fetch();
            handle._tcs = new TaskCompletionSource<TResult>();
            handle._tokenSource = new CancellationTokenSource();
            handle._result = default;
            handle.Canceled = null;
            handle._completed = null;
            return handle;
        }
        
        
        private TaskCompletionSource<TResult> _tcs;
        private TResult _result;
        private CancellationTokenSource _tokenSource;
        
        public Task<TResult> Task =>_tcs.Task;
        public CancellationToken Token=> _tokenSource.Token;
        public TResult Result => _result;
        public bool IsRecycled => ((IRecycleable) this).IsRecycled;

        public event Action<CoroutineHandle<TResult>> Canceled;
        private event Action<CoroutineHandle<TResult>> _completed;
        public event Action<CoroutineHandle<TResult>> Completed
        {
            add
            {
                _completed += value;
                if(_tcs.Task.IsCompleted)
                {
                    value(this);
                }
            }
            remove => _completed -= value;
        }
        
        void IEnumerator.Reset(){}
        bool IEnumerator.MoveNext() => !Task.IsCompleted;
        object IEnumerator.Current => null;

        public void Cancel()
        {
            if(!_tcs.Task.IsCompleted&&_tcs.TrySetCanceled(_tokenSource.Token))
            {
                Canceled?.Invoke(this);
                EasyCoroutine.StopCoroutine(this);
                Pool.Recycle(this);
            }
        
        }

        void ICoroutineHandle.Complete()
        {
            if(_tcs.TrySetResult(_result))
            {
                _completed?.Invoke(this);
                Pool.Recycle(this);
            }
        }

        void ICoroutineHandle<CoroutineHandle<TResult>,TResult>.Complete(TResult result)
        {
            _result = result;
            ((ICoroutineHandle)this).Complete();
        }

        bool IRecycleable.IsRecycled { get; set; }
        void IRecycleable.Recycle(){}
        
    }
}