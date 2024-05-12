using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace EasyFramework
{
    public interface ICoroutineHandle: IEnumerator,IRecycleable
    {
        Task Task { get; }
        CancellationToken Token { get; }
        new bool IsRecycled { get; }
        
        void Complete();
        void Cancel();
    }
    public interface ICoroutineHandle<T> : ICoroutineHandle where T : ICoroutineHandle<T>
    {
        event Action<T> Canceled;
        event Action<T> Completed;
    }

    public interface ICoroutineHandle<T,TResult>:ICoroutineHandle<T> where T : ICoroutineHandle<T, TResult>
    {
        new Task<TResult> Task { get; }
        Task ICoroutineHandle.Task => Task;
        TResult Result { get; }
        void Complete(TResult result);
    }

    public static class CoroutineHandleExtensions
    {
        public static T OnCompleted<T>(this T handle, Action<T> callback) where T : ICoroutineHandle<T>
        {
            handle.Completed += callback;
            return handle;
        }
        public static T OnCanceled<T>(this T handle, Action<T> callback) where T : ICoroutineHandle<T>
        {
            handle.Canceled += callback;
            return handle;
        }
    }
}