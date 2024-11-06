using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace EasyFramework
{
    public static partial class EasyTask
    {
        public static CoroutineHandle DelayFrame(int frameCount)
        {
            return EasyCoroutine.DelayFrame(frameCount);
        }
        public static CoroutineHandle Delay(float second, bool isIgnoreTimeScale = false)
        {
            return EasyCoroutine.Delay(second, isIgnoreTimeScale);
        }
        public static CoroutineHandle Seconds(float interval, float second, Action<float> action, bool isIgnoreTimeScale = false)
        {
            return EasyCoroutine.Seconds(interval, second, action, isIgnoreTimeScale);
        }
        public static CoroutineHandle TimeTask(float second, Action<float> action, bool isIgnoreTimeScale = false)
        {
            return EasyCoroutine.TimeTask(second, action, isIgnoreTimeScale);
        }
        public static CoroutineHandle Loop(Func<IEnumerator> enumerator, int loopCount)
        {
            return EasyCoroutine.Loop(enumerator, loopCount);
        }

        public static CoroutineHandle Condition(Func<bool> condition)
        {
            return EasyCoroutine.Condition(condition);
        }
        public static void WaitEndOfFrame(Action action)
        { 
            EasyCoroutine.WaitEndOfFrame(action);
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
        public static async void ViewError<T>(this T self, Action<T> onCompleted, Action<Exception> onError = null)
            where T : Task
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
        public static void WhenAll(Action allComplete, params CoroutineHandle[] handles)
        {
            void AllComplete()
            {
                if (handles.All(h => h.IsDone))
                {
                    allComplete();
                    foreach (var h in handles)
                    {
                        h.Completed -= AllComplete;
                        h.Cancelled -= AllComplete;
                    }
                }
            }
            foreach (var handle in handles)
            {

                handle.Completed += AllComplete;
                handle.Cancelled += AllComplete;
            }
        }
    }
}