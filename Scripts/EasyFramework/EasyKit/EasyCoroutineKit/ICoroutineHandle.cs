using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace EasyFramework
{
    public interface INonImmediate
    {
        bool IsDone { get; }
    }
    public interface ICancelAble
    {
        event Action Cancelled;
        bool IsCanceled { get; }
        void Cancel();
    }

    public interface ICompleteAble
    {
        event Action Completed;
        bool IsCompleted { get; }
    }

    public interface IDoneAble
    {
        event Action Done;
        bool IsDone { get; }
    }
    public interface IYieldAble: INonImmediate,IEnumerator
    {
        
    }

    public interface IAwaitAble: INonImmediate,INotifyCompletion
    {

    }
    public interface IAwaitAble<T>: IAwaitAble
    {
        T Result { get; }
        T GetResult();
    }

    public static class CoroutineHandleExtensions
    {
        public static void Cancel(this ICancelAble handle)=>handle?.Cancel();
        public static T OnCanceled<T>(this T handle, Action action) where T : ICancelAble
        {
            if (handle.IsCanceled)
                action?.Invoke();
            handle.Cancelled += action;
            return handle;
        }
        public static T OnCompleted<T>(this T handle, Action action) where T : ICompleteAble
        {
            if (handle.IsCompleted)
                action?.Invoke();
            handle.Completed += action;
            return handle;
        }
        public static T OnDone<T>(this T handle, Action action) where T : IDoneAble
        {
            if (handle.IsDone)
                action?.Invoke();
            handle.Done += action;
            return handle;
        }

        public static CoroutineHandle<T> OnReceivedResult<T>(this CoroutineHandle<T> handle, Action<T> action)
        {
            handle.OnCompleted(() =>
            {
                if (handle.Result != null)
                    action?.Invoke(handle.Result);
            });
            return handle;
        }
    }
}