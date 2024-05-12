using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyFramework.EventKit;
using UnityEngine;

namespace EasyFramework.EasyTaskKit
{
    public static partial class EasyTask
    {
        public static CoroutineHandle Delay(float second, bool isIgnoreTimeScale = false)
        {
            return EasyCoroutine.Delay(second, isIgnoreTimeScale);
        }

        public static CoroutineHandle Delay(CoroutineHandle handle, float second, bool isIgnoreTimeScale = false)
        {
            return EasyCoroutine.Delay(handle, second, isIgnoreTimeScale);
        }

        public static CoroutineHandle Seconds(float interval, float second, Action<float> action, bool isIgnoreTimeScale = false)
        {
            return EasyCoroutine.Seconds(interval, second, action, isIgnoreTimeScale);
        }

        public static CoroutineHandle Seconds(CoroutineHandle handle, float interval, float second, Action<float> action,
            bool isIgnoreTimeScale = false)
        {
            return EasyCoroutine.Seconds(handle, interval, second, action, isIgnoreTimeScale);
        }

        public static CoroutineHandle TimeTask(float second, Action<float> action, bool isIgnoreTimeScale = false)
        {
            return EasyCoroutine.TimeTask(second, action, isIgnoreTimeScale);
        }

        public static CoroutineHandle TimeTask(CoroutineHandle handle, float second, Action<float> action,
            bool isIgnoreTimeScale = false)
        {
            return EasyCoroutine.TimeTask(handle, second, action, isIgnoreTimeScale);
        }
        
        public static CoroutineHandle Loop(IEnumerator enumerator, int loopCount)
        {
            return EasyCoroutine.Loop(enumerator, loopCount);
        }

        public static CoroutineHandle Loop(CoroutineHandle handle, IEnumerator enumerator, int loopCount)
        {
            return EasyCoroutine.Loop(handle, enumerator, loopCount);
        }


        public static async void ViewError<T>(this T self) where T : Task
        {
            try { await self; }
            catch (Exception e)
            {
                if (e.Message != "A task was canceled.")
                    throw;
            }
        }
        public static async void ViewError<T>(this T self, Action<T> onCompleted, Action<Exception> onError=null) where T : Task
        {
            try
            {
                await self;
                onCompleted(self);
            }
            catch (Exception e)
            {
                if (e.Message != "A task was canceled.")
                {
                    if (onError != null)
                        onError(e);
                    else
                        throw;
                }
            }
        }

        public static async Task RunTask(this Task self, ICoroutineHandle handle)
        {
            await self;
            handle.Complete();
        }
        public static async Task RunTaskResult<T, TResult>(this Task<TResult> self, ICoroutineHandle<T,TResult> handle) where T : ICoroutineHandle<T, TResult>
        {
            handle.Complete(await self);
        }

        public static CancellationTokenSource CancelOnDestroy(this MonoBehaviour self)
        {
            CancellationTokenSource tokenSource = new();
            self.gameObject.RegisterOnDestroy(tokenSource.Cancel);
            return tokenSource;
        }

        public static CancellationTokenSource CancelOnDestroy(this CancellationTokenSource self, MonoBehaviour mono)
        {
            mono.gameObject.RegisterOnDestroy(self.Cancel);
            return self;
        }

        public static void WhenAll<T>(Action allComplete, params T[] handles)
            where T : class,ICoroutineHandle<T>
        {
            void AllComplete(T handle)
            {
                if (handles.All(h => h.Task.IsCompleted))
                {
                    allComplete();
                    foreach (var h in handles)
                    {
                        if (h != handle)
                        {
                            h.Completed -= AllComplete;
                            h.Canceled -= AllComplete;
                        }
                    }
                }
            }
            foreach (var handle in handles)
            {

                handle.Completed += AllComplete;
                handle.Canceled += AllComplete;
            }
        }
    }
}