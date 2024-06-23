using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EasyFramework.EventKit;
using UnityEngine;

namespace EasyFramework
{
    public class EasyCoroutineOnApplicationQuit : AutoClassEvent<GlobalEvent.ApplicationQuit,IStructure>
    {
        protected override void Run(IStructure a)
        {
            EasyCoroutine.StopAllCoroutine();
        }
    }

    public class EasyCoroutine : MonoSingleton<EasyCoroutine>
    {
        private readonly Lazy<Dictionary<ICoroutineHandle, Coroutine>> _coroutineDic = new(new Dictionary<ICoroutineHandle, Coroutine>());
        private readonly Lazy<Dictionary<float,WaitForSeconds>> _secondsDic = new(new Dictionary<float, WaitForSeconds>());
        private readonly Lazy<Dictionary<float,WaitForSecondsRealtime>> _secondsRealTimeDic = new(new Dictionary<float, WaitForSecondsRealtime>());

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        public static ICoroutineHandle RegisterCoroutine(ICoroutineHandle handle, IEnumerator enumerator)
        {
            TryRegister()._coroutineDic.Value.Add(handle, Instance.StartCoroutine(enumerator));
            return handle;
        }
        public static void StopCoroutine(ICoroutineHandle handle)
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

        public static void CoroutineCompleted(ICoroutineHandle handle)
        {
            if(!Instance) return;
            handle.Complete();
            Instance._coroutineDic.Value.Remove(handle);
        }
        public static void ReturnResult<T, TResult>(ICoroutineHandle<T, TResult> handle, TResult result) where T : ICoroutineHandle<T, TResult>
        {
            handle.Complete(result);
        }

        public static CoroutineHandle Delay(float second, bool isIgnoreTimescale)
       {
           var handle = CoroutineHandle.Fetch();
           RegisterCoroutine(handle, Delay(()=>CoroutineCompleted(handle), second, isIgnoreTimescale));
           return handle;
       }
        public static CoroutineHandle Seconds(float interval, float duration, Action<float> action, bool isIgnoreTimescale)
        {
            var handle = CoroutineHandle.Fetch();
            RegisterCoroutine(handle, Seconds(()=>CoroutineCompleted(handle), interval, duration, action, isIgnoreTimescale));
            return handle;
        }
        public static CoroutineHandle TimeTask(float second,Action<float> action,bool isIgnoreTimescale)
        {
            var handle = CoroutineHandle.Fetch();
            RegisterCoroutine(handle, TimeTask(()=>CoroutineCompleted(handle), second,action,isIgnoreTimescale));
            return handle;
        }
        public static CoroutineHandle Loop(IEnumerator enumerator, int loopCount)
        {
            var handle = CoroutineHandle.Fetch();
            RegisterCoroutine(handle, LoopCoroutine(() => CoroutineCompleted(handle), enumerator, loopCount));
            return handle;
        }

        public static CoroutineHandle Delay(CoroutineHandle handle,float second, bool isIgnoreTimescale)
        {
            RegisterCoroutine(handle, Delay(()=>CoroutineCompleted(handle), second, isIgnoreTimescale));
            return handle;
        }
        public static CoroutineHandle Seconds(CoroutineHandle handle,float interval, float duration, Action<float> action, bool isIgnoreTimescale)
        {
            RegisterCoroutine(handle, Seconds(()=>CoroutineCompleted(handle), interval, duration, action, isIgnoreTimescale));
            return handle;
        }
        public static CoroutineHandle TimeTask(CoroutineHandle handle,float second,Action<float> action,bool isIgnoreTimescale)
        {
            RegisterCoroutine(handle, TimeTask(()=>CoroutineCompleted(handle), second,action,isIgnoreTimescale));
            return handle;
        }
        public static CoroutineHandle Loop(CoroutineHandle handle, IEnumerator enumerator, int loopCount)
        {
            RegisterCoroutine(handle, LoopCoroutine(()=>CoroutineCompleted(handle), enumerator, loopCount));
            return handle;
        }


        public static IEnumerator CustomCoroutine(Action completed, IEnumerator enumerator)
        {
            yield return enumerator;
            completed?.Invoke();
        }

        public static IEnumerator LoopCoroutine(Action completed, IEnumerator enumerator, int loopCount)
        {
            bool loop = loopCount <= 0;
            while (loopCount-- > 0||loop)
            {
                yield return enumerator;
            }
            completed?.Invoke();
        }
        
        private static IEnumerator Delay(Action completed, float seconds, bool isIgnoreTimescale)
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
        private static IEnumerator Seconds(Action completed,float interval, float duration, Action<float> action,bool isIgnoreTimescale)
        {
            float time = 0;
            interval = Mathf.Abs(interval);
            action?.Invoke(time/duration);
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
                    action?.Invoke(time / duration);
                }
                else
                {
                    action?.Invoke(1);
                    completed?.Invoke();
                    yield break;
                }
            }
        }
        private static IEnumerator TimeTask(Action completed, float duration, Action<float> action,bool isIgnoreTimescale)
        {
            float time = 0;
            action?.Invoke(time/duration);
            while (true)
            {
                yield return null;
                if (time < duration)
                {
                    time = isIgnoreTimescale
                        ? Math.Clamp(time + Time.unscaledDeltaTime, 0, duration)
                        : Math.Clamp(time + Time.deltaTime, 0, duration);
                    action?.Invoke(time / duration);
                }
                else
                {
                    action?.Invoke(1);
                    completed?.Invoke();
                    yield break;
                }
            }
        }

        protected override void OnInit()
        {
            
        }

        protected override void OnDispose(bool usePool)
        {
            StopAllCoroutines();
        }
    }
}