using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace EasyFramework
{
    public class EasyCoroutine : MonoSingleton<EasyCoroutine>
    {
        private readonly Lazy<Dictionary<CoroutineHandle, Coroutine>> _coroutineDic = new(new Dictionary<CoroutineHandle, Coroutine>());
        private readonly Lazy<Dictionary<float,WaitForSeconds>> _secondsDic = new(new Dictionary<float, WaitForSeconds>());
        private readonly Lazy<Dictionary<float,WaitForSecondsRealtime>> _secondsRealTimeDic = new(new Dictionary<float, WaitForSecondsRealtime>());
        private static Action _waitEndOfFrameAction;
        private static Action _everySecondAction;
        private static Queue<CoroutineHandle> _coroutineHandleRecycleQueue = new();
        
        private void OnDestroy()
        {
            Dispose();
            _waitEndOfFrameAction = null;
            _everySecondAction = null;
        }

        private void Update()
        {
            if (_waitEndOfFrameAction != null)
            {
                StartCoroutine(WaitEndOfFrameCoroutine());
            }

            if (_coroutineHandleRecycleQueue.Count > 0)
                StartCoroutine(CoroutineHandleRecycle());
        }

        public static void RecycleCoroutineHandle(CoroutineHandle handle)
        {
            _coroutineHandleRecycleQueue.Enqueue(handle);
        }
        public static CoroutineHandle RegisterCoroutine(CoroutineHandle handle, IEnumerator enumerator)
        {
            if (Application.isPlaying && TryRegister() && Instance.enabled && Instance.gameObject.activeSelf)
            {
                Instance._coroutineDic.Value.Add(handle, Instance.StartCoroutine(enumerator));
            }
            return handle;
        }
        public static void StopCoroutine(CoroutineHandle handle)
        {
            if(!Instance) return;
            if (GetInstance()._coroutineDic.Value.TryGetValue(handle, out var coroutine))
            {
                GetInstance().StopCoroutine(coroutine);
                GetInstance()._coroutineDic.Value.Remove(handle);
            }
        }

        public static void StopAllCoroutine()
        {
            if(!Instance) return;
            var handles = Instance._coroutineDic.Value.Keys.ToArray();
            foreach (var handle in handles)
            {
                handle.Cancel();
            }
            Instance._coroutineDic.Value.Clear();
        }

        public static void CoroutineCompleted(CoroutineHandle handle)
        {
            if(!Instance) return;
            Instance._coroutineDic.Value.Remove(handle);
            handle.Complete();
        }
        public static void ReturnResult<T>(CoroutineHandle<T> handle, T result)
        {
            Instance._coroutineDic.Value.Remove(handle);
            handle.SetResult(result);
        }

        public static CoroutineHandle DelayFrame(int frameCount)
        {
            var handle = CoroutineHandle.Fetch();
            RegisterCoroutine(handle, DelayFrameCoroutine(()=>CoroutineCompleted(handle), frameCount));
            return handle;
        }
        public static CoroutineHandle Delay(float second, bool isIgnoreTimescale)
       {
           var handle = CoroutineHandle.Fetch();
           RegisterCoroutine(handle, DelayCoroutine(()=>CoroutineCompleted(handle), second, isIgnoreTimescale));
           return handle;
       }
        public static CoroutineHandle Seconds(float interval, float duration, Action<float> action, bool isIgnoreTimescale)
        {
            var handle = CoroutineHandle.Fetch();
            RegisterCoroutine(handle, SecondsCoroutine(()=>CoroutineCompleted(handle), interval, duration, action, isIgnoreTimescale));
            return handle;
        }
        public static CoroutineHandle TimeTask(float second,Action<float> action,bool isIgnoreTimescale)
        {
            var handle = CoroutineHandle.Fetch();
            RegisterCoroutine(handle, TimeTaskCoroutine(()=>CoroutineCompleted(handle), second,action,isIgnoreTimescale));
            return handle;
        }
        public static CoroutineHandle Loop(Func<IEnumerator> enumerator, int loopCount)
        {
            var handle = CoroutineHandle.Fetch();
            RegisterCoroutine(handle, LoopCoroutine(() => CoroutineCompleted(handle), enumerator, loopCount));
            return handle;
        }
        public static CoroutineHandle Condition(Func<bool> condition)
        {
            var handle = CoroutineHandle.Fetch();
            RegisterCoroutine(handle, ConditionCoroutine(() => CoroutineCompleted(handle), condition));
            return handle;
        }

        public static void WaitEndOfFrame(Action completed)
        {
            _waitEndOfFrameAction += completed;
        }
        public static void RegisterEverySecond(Action action)
        {
            _everySecondAction += action;
        }
        public static void UnRegisterEverySecond(Action action)
        {
            _everySecondAction -= action;
        }
        
        public static CoroutineHandle<TResult> Await<T, TResult>(CoroutineHandle<T> handle, Func<T, TResult> result)
        {
            var resultHandle = CoroutineHandle<TResult>.Fetch();
            handle
                .OnCompleted(() => resultHandle.SetResult(result(handle.Result)))
                .OnCanceled(resultHandle.Cancel);
            return resultHandle;
        }


        
        internal static IEnumerator CustomCoroutine(Action completed, IEnumerator enumerator)
        {
            yield return enumerator;
            completed?.Invoke();
        }
        private static IEnumerator LoopCoroutine(Action completed, Func<IEnumerator> enumerator, int loopCount)
        {
            bool loop = loopCount <= 0;
            while (loopCount-- > 0||loop)
            {
                yield return enumerator();
            }
            completed?.Invoke();
        }
        private static IEnumerator ConditionCoroutine(Action completed,Func<bool> condition)
        {
            if(condition!=null)
                while (!condition())
                {
                    yield return null;
                }

            yield return null;
            completed?.Invoke();
        }
        private static IEnumerator WaitEndOfFrameCoroutine()
        {
            yield return new WaitForEndOfFrame();
            _waitEndOfFrameAction?.Invoke();
            _waitEndOfFrameAction = null;
        }

        private static IEnumerator DelayFrameCoroutine(Action completed, int frameCount)
        {
            for (int i = 0; i < frameCount; i++)
                yield return null;
            completed?.Invoke();
        }
        private static IEnumerator DelayCoroutine(Action completed, float seconds, bool isIgnoreTimescale)
        {
            if (isIgnoreTimescale)
            {
                if (!TryRegister()._secondsRealTimeDic.Value.TryGetValue(seconds, out var wait))
                {
                    wait = new WaitForSecondsRealtime(seconds);
                    Instance._secondsRealTimeDic.Value[seconds] = wait;
                }

                yield return wait;
            }
            else
            {
                if (!Instance._secondsDic.Value.TryGetValue(seconds, out var wait))
                {
                    wait = new WaitForSeconds(seconds);
                    Instance._secondsDic.Value[seconds] = wait;
                }

                yield return wait;
            }

            completed?.Invoke();
        }

        private static IEnumerator SecondsCoroutine(Action completed, float interval, float duration, Action<float> action, bool isIgnoreTimescale)
        {
            float time = 0;
            interval = Mathf.Abs(interval);
            while (true)
            {
                if (time < duration)
                {
                    if (isIgnoreTimescale)
                    {
                        if (!TryRegister()._secondsRealTimeDic.Value.TryGetValue(interval, out var wait))
                        {
                            wait = new WaitForSecondsRealtime(interval);
                            Instance._secondsRealTimeDic.Value[interval] = wait;
                        }

                        yield return wait;
                    }
                    else
                    {
                        if (!Instance._secondsDic.Value.TryGetValue(interval, out var wait))
                        {
                            wait = new WaitForSeconds(interval);
                            Instance._secondsDic.Value[interval] = wait;
                        }

                        yield return wait;
                    }

                    time += interval;
                    if (time >= duration)
                        break;
                    action?.Invoke(time / duration);
                }
                else
                    break;
            }

            action?.Invoke(1);
            completed?.Invoke();
        }

        private static IEnumerator TimeTaskCoroutine(Action completed, float duration, Action<float> action, bool isIgnoreTimescale)
        {
            float time = 0;
            while (true)
            {
                yield return null;
                if (time < duration)
                {
                    time = isIgnoreTimescale
                        ? Math.Clamp(time + EasyTime.UnscaledDeltaTime, 0, duration)
                        : Math.Clamp(time + EasyTime.DeltaTime, 0, duration);
                    if (time >= duration)
                        break;
                    action?.Invoke(time / duration);
                }
                else
                    break;
            }

            action?.Invoke(1);
            completed?.Invoke();
        }

        private IEnumerator CoroutineHandleRecycle()
        {
            yield return new WaitForEndOfFrame();
            for (int i = _coroutineHandleRecycleQueue.Count; i >0; i--)
            {
                _coroutineHandleRecycleQueue.Dequeue().Recycle();
            }
        }

        protected override void OnInit()
        {
            Loop(()=>DelayCoroutine(_everySecondAction, 1,false),0);
        }

        protected override void OnDispose(bool usePool)
        {
            StopAllCoroutine();
        }
    }
}