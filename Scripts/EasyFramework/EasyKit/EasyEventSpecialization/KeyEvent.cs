using System;
using System.Collections.Generic;

namespace EasyFramework
{
    public interface IkeyDelegate<TKey,TDelegate> where TDelegate : IEDelegate
    {
        Dictionary<TKey, TDelegate> Dic { get; }
    }

    public interface IKeyEvent<TKey, TEvent> : IkeyDelegate<TKey, TEvent> where TEvent : IEasyEvent
    {
        
    }
    public class KeyEvent<TKey>:IKeyEvent<TKey,EasyEvent>
    {
        public Dictionary<TKey, EasyEvent> Dic { get; } = new ();

        public IUnRegisterHandle Register(TKey key, Action action)
        {
            if (!Dic.TryGetValue(key, out var e))
            {
                e = new EasyEvent();
                Dic[key] = e;
            }
            return e.Register(action);
        }

        public void UnRegister(TKey key, Action action)
        {
            if (Dic.TryGetValue(key,out var e))
                e.UnRegister(action);
        }
        public void Invoke(TKey key)
        {
            if (Dic.TryGetValue(key,out var e))
                e.Invoke();
        }
        public void InvokeAll()
        {
            foreach (var e in Dic.Values)
            {
                e.Invoke();
            }
        }
    }
    public class KeyEvent<TKey,T1>:IKeyEvent<TKey,EasyEvent<T1>>
    {
        public Dictionary<TKey, EasyEvent<T1>> Dic { get; } = new();

        public IUnRegisterHandle Register(TKey key, Action<T1> action)
        {
            if (!Dic.TryGetValue(key, out var e))
            {
                e = new EasyEvent<T1>();
                Dic[key] = e;
            }
            return e.Register(action);
        }

        public void UnRegister(TKey key, Action<T1> action)
        {
            if (Dic.TryGetValue(key, out var e))
                e.UnRegister(action);
        }
        public void Invoke(TKey key, T1 arg1)
        {
            if (Dic.TryGetValue(key, out var e))
                e.Invoke(arg1);
        }
        public void InvokeAll(T1 arg1)
        {
            foreach (var e in Dic.Values)
            {
                e.Invoke(arg1);
            }
        }
    }
    public class KeyEvent<TKey, T1, T2>:IKeyEvent<TKey, EasyEvent<T1, T2>>
    {
        public Dictionary<TKey, EasyEvent<T1, T2>> Dic { get; } = new();

        public IUnRegisterHandle Register(TKey key, Action<T1, T2> action)
        {
            if (!Dic.TryGetValue(key, out var e))
            {
                e = new EasyEvent<T1, T2>();
                Dic[key] = e;
            }
            return e.Register(action);
        }

        public void UnRegister(TKey key, Action<T1, T2> action)
        {
            if (Dic.TryGetValue(key, out var e))
                e.UnRegister(action);
        }
        public void Invoke(TKey key, T1 arg1, T2 arg2)
        {
            if (Dic.TryGetValue(key, out var e))
                e.Invoke(arg1, arg2);
        }
        public void InvokeAll(T1 arg1, T2 arg2)
        {
            foreach (var e in Dic.Values)
            {
                e.Invoke(arg1, arg2);
            }
        }
    }
    
    public class KeyEvent<TKey, T1, T2, T3>:IKeyEvent<TKey, EasyEvent<T1, T2, T3>>
    {
        public Dictionary<TKey, EasyEvent<T1, T2, T3>> Dic { get; } = new();

        public IUnRegisterHandle Register(TKey key, Action<T1, T2, T3> action)
        {
            if (!Dic.TryGetValue(key, out var e))
            {
                e = new EasyEvent<T1, T2, T3>();
                Dic[key] = e;
            }
            return e.Register(action);
        }

        public void UnRegister(TKey key, Action<T1, T2, T3> action)
        {
            if (Dic.TryGetValue(key, out var e))
                e.UnRegister(action);
        }
        public void Invoke(TKey key, T1 arg1, T2 arg2, T3 arg3)
        {
            if (Dic.TryGetValue(key, out var e))
                e.Invoke(arg1, arg2, arg3);
        }
        public void InvokeAll(T1 arg1, T2 arg2, T3 arg3)
        {
            foreach (var e in Dic.Values)
            {
                e.Invoke(arg1, arg2, arg3);
            }
        }
    }
    
    public class KeyEvent<TKey, T1, T2, T3, T4>:IKeyEvent<TKey, EasyEvent<T1, T2, T3, T4>>
    {
        public Dictionary<TKey, EasyEvent<T1, T2, T3, T4>> Dic { get; } = new();

        public IUnRegisterHandle Register(TKey key, Action<T1, T2, T3, T4> action)
        {
            if (!Dic.TryGetValue(key, out var e))
            {
                e = new EasyEvent<T1, T2, T3, T4>();
                Dic[key] = e;
            }
            return e.Register(action);
        }

        public void UnRegister(TKey key, Action<T1, T2, T3, T4> action)
        {
            if (Dic.TryGetValue(key, out var e))
                e.UnRegister(action);
        }
        public void Invoke(TKey key, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (Dic.TryGetValue(key, out var e))
                e.Invoke(arg1, arg2, arg3, arg4);
        }
        public void InvokeAll(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            foreach (var e in Dic.Values)
            {
                e.Invoke(arg1, arg2, arg3, arg4);
            }
        }
    }
    
    public class KeyEvent<TKey, T1, T2, T3, T4, T5>:IKeyEvent<TKey, EasyEvent<T1, T2, T3, T4, T5>>
    {
        public Dictionary<TKey, EasyEvent<T1, T2, T3, T4, T5>> Dic { get; } = new();

        public IUnRegisterHandle Register(TKey key, Action<T1, T2, T3, T4, T5> action)
        {
            if (!Dic.TryGetValue(key, out var e))
            {
                e = new EasyEvent<T1, T2, T3, T4, T5>();
                Dic[key] = e;
            }
            return e.Register(action);
        }

        public void UnRegister(TKey key, Action<T1, T2, T3, T4, T5> action)
        {
            if (Dic.TryGetValue(key, out var e))
                e.UnRegister(action);
        }
        public void Invoke(TKey key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (Dic.TryGetValue(key, out var e))
                e.Invoke(arg1, arg2, arg3, arg4, arg5);
        }
        public void InvokeAll(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            foreach (var e in Dic.Values)
            {
                e.Invoke(arg1, arg2, arg3, arg4, arg5);
            }
        }
    }
    
    public interface IKeyFunc<TKey, TFunc,TResult> : IkeyDelegate<TKey, TFunc> where TFunc : IEasyFunc<TResult>
    {
        
    }
    public class KeyFunc<TKey, TResult>:IKeyFunc<TKey, EasyFunc<TResult>, TResult>
    {
        public Dictionary<TKey, EasyFunc<TResult>> Dic { get; } = new();

        public IUnRegisterHandle Register(TKey key, Func<TResult> func)
        {
            if (!Dic.TryGetValue(key, out var f))
            {
                f = new EasyFunc<TResult>();
                Dic[key] = f;
            }
            return f.Register(func);
        }

        public void UnRegister(TKey key, Func<TResult> func)
        {
            if (Dic.TryGetValue(key, out var f))
                f.UnRegister(func);
        }
        public TResult Invoke(TKey key)
        {
            if (Dic.TryGetValue(key, out var f))
                return f.Invoke();
            return default;
        }
        public TResult[] InvokeAndReturnAll(TKey key)
        {
            if (Dic.TryGetValue(key, out var f))
                return f.InvokeAndReturnAll();
            return default;
        }
        public TResult[] InvokeAll()
        {
            TResult[] result = new TResult[Dic.Count];
            var i = 0;
            foreach (var f in Dic.Values)
            {
                result[i] = f.Invoke();
                i++;
            }
            return result;
        }
        public TResult[][] InvokeAllAndReturnAll(Action<TResult> action)
        {
            TResult[][] result = new TResult[Dic.Count][];
            var i = 0;
            foreach (var f in Dic.Values)
            {
                result[i] = f.InvokeAndReturnAll();
                i++;
            }
            return result;
        }
    }
    
    public class KeyFunc<TKey, T1, TResult>:IKeyFunc<TKey, EasyFunc<T1, TResult>, TResult>
    {
        public Dictionary<TKey, EasyFunc<T1, TResult>> Dic { get; } = new();

        public IUnRegisterHandle Register(TKey key, Func<T1, TResult> func)
        {
            if (!Dic.TryGetValue(key, out var f))
            {
                f = new EasyFunc<T1, TResult>();
                Dic[key] = f;
            }
            return f.Register(func);
        }

        public void UnRegister(TKey key, Func<T1, TResult> func)
        {
            if (Dic.TryGetValue(key, out var f))
                f.UnRegister(func);
        }
        public TResult Invoke(TKey key, T1 arg1)
        {
            if (Dic.TryGetValue(key, out var f))
                return f.Invoke(arg1);
            return default;
        }
        public TResult[] InvokeAndReturnAll(TKey key, T1 arg1)
        {
            if (Dic.TryGetValue(key, out var f))
                return f.InvokeAndReturnAll(arg1);
            return default;
        }
        public TResult[] InvokeAll(T1 arg1)
        {
            TResult[] result = new TResult[Dic.Count];
            var i = 0;
            foreach (var f in Dic.Values)
            {
                result[i] = f.Invoke(arg1);
                i++;
            }
            return result;
        }
        public TResult[][] InvokeAllAndReturnAll(T1 arg1, Action<TResult> action)
        {
            TResult[][] result = new TResult[Dic.Count][];
            var i = 0;
            foreach (var f in Dic.Values)
            {
                result[i] = f.InvokeAndReturnAll(arg1);
                i++;
            }
            return result;
        }
    }
    
    public class KeyFunc<TKey, T1, T2, TResult>:IKeyFunc<TKey, EasyFunc<T1, T2, TResult>, TResult>
    {
        public Dictionary<TKey, EasyFunc<T1, T2, TResult>> Dic { get; } = new();

        public IUnRegisterHandle Register(TKey key, Func<T1, T2, TResult> func)
        {
            if (!Dic.TryGetValue(key, out var f))
            {
                f = new EasyFunc<T1, T2, TResult>();
                Dic[key] = f;
            }
            return f.Register(func);
        }

        public void UnRegister(TKey key, Func<T1, T2, TResult> func)
        {
            if (Dic.TryGetValue(key, out var f))
                f.UnRegister(func);
        }
        public TResult Invoke(TKey key, T1 arg1, T2 arg2)
        {
            if (Dic.TryGetValue(key, out var f))
                return f.Invoke(arg1, arg2);
            return default;
        }
        public TResult[] InvokeAndReturnAll(TKey key, T1 arg1, T2 arg2)
        {
            if (Dic.TryGetValue(key, out var f))
                return f.InvokeAndReturnAll(arg1, arg2);
            return default;
        }
        public TResult[] InvokeAll(T1 arg1, T2 arg2)
        {
            TResult[] result = new TResult[Dic.Count];
            var i = 0;
            foreach (var f in Dic.Values)
            {
                result[i] = f.Invoke(arg1, arg2);
                i++;
            }
            return result;
        }
        public TResult[][] InvokeAllAndReturnAll(T1 arg1, T2 arg2, Action<TResult> action)
        {
            TResult[][] result = new TResult[Dic.Count][];
            var i = 0;
            foreach (var f in Dic.Values)
            {
                result[i] = f.InvokeAndReturnAll(arg1, arg2);
                i++;
            }
            return result;
        }
    }
    
    public class KeyFunc<TKey, T1, T2, T3, TResult>:IKeyFunc<TKey, EasyFunc<T1, T2, T3, TResult>, TResult>
    {
        public Dictionary<TKey, EasyFunc<T1, T2, T3, TResult>> Dic { get; } = new();

        public IUnRegisterHandle Register(TKey key, Func<T1, T2, T3, TResult> func)
        {
            if (!Dic.TryGetValue(key, out var f))
            {
                f = new EasyFunc<T1, T2, T3, TResult>();
                Dic[key] = f;
            }
            return f.Register(func);
        }

        public void UnRegister(TKey key, Func<T1, T2, T3, TResult> func)
        {
            if (Dic.TryGetValue(key, out var f))
                f.UnRegister(func);
        }
        public TResult Invoke(TKey key, T1 arg1, T2 arg2, T3 arg3)
        {
            if (Dic.TryGetValue(key, out var f))
                return f.Invoke(arg1, arg2, arg3);
            return default;
        }
        public TResult[] InvokeAndReturnAll(TKey key, T1 arg1, T2 arg2, T3 arg3)
        {
            if (Dic.TryGetValue(key, out var f))
                return f.InvokeAndReturnAll(arg1, arg2, arg3);
            return default;
        }
        public TResult[] InvokeAll(T1 arg1, T2 arg2, T3 arg3)
        {
            TResult[] result = new TResult[Dic.Count];
            var i = 0;
            foreach (var f in Dic.Values)
            {
                result[i] = f.Invoke(arg1, arg2, arg3);
                i++;
            }
            return result;
        }
        public TResult[][] InvokeAllAndReturnAll(T1 arg1, T2 arg2, T3 arg3, Action<TResult> action)
        {
            TResult[][] result = new TResult[Dic.Count][];
            var i = 0;
            foreach (var f in Dic.Values)
            {
                result[i] = f.InvokeAndReturnAll(arg1, arg2, arg3);
                i++;
            }
            return result;
        }
    }
    
    public class KeyFunc<TKey, T1, T2, T3, T4, TResult>:IKeyFunc<TKey, EasyFunc<T1, T2, T3, T4, TResult>, TResult>
    {
        public Dictionary<TKey, EasyFunc<T1, T2, T3, T4, TResult>> Dic { get; } = new();

        public IUnRegisterHandle Register(TKey key, Func<T1, T2, T3, T4, TResult> func)
        {
            if (!Dic.TryGetValue(key, out var f))
            {
                f = new EasyFunc<T1, T2, T3, T4, TResult>();
                Dic[key] = f;
            }
            return f.Register(func);
        }

        public void UnRegister(TKey key, Func<T1, T2, T3, T4, TResult> func)
        {
            if (Dic.TryGetValue(key, out var f))
                f.UnRegister(func);
        }
        public TResult Invoke(TKey key, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (Dic.TryGetValue(key, out var f))
                return f.Invoke(arg1, arg2, arg3, arg4);
            return default;
        }
        public TResult[] InvokeAndReturnAll(TKey key, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (Dic.TryGetValue(key, out var f))
                return f.InvokeAndReturnAll(arg1, arg2, arg3, arg4);
            return default;
        }
        public TResult[] InvokeAll(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            TResult[] result = new TResult[Dic.Count];
            var i = 0;
            foreach (var f in Dic.Values)
            {
                result[i] = f.Invoke(arg1, arg2, arg3, arg4);
                i++;
            }
            return result;
        }
        public TResult[][] InvokeAllAndReturnAll(T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<TResult> action)
        {
            TResult[][] result = new TResult[Dic.Count][];
            var i = 0;
            foreach (var f in Dic.Values)
            {
                result[i] = f.InvokeAndReturnAll(arg1, arg2, arg3, arg4);
                i++;
            }
            return result;
        }
    }
    
    public class KeyFunc<TKey, T1, T2, T3, T4, T5, TResult>:IKeyFunc<TKey, EasyFunc<T1, T2, T3, T4, T5, TResult>, TResult>
    {
        public Dictionary<TKey, EasyFunc<T1, T2, T3, T4, T5, TResult>> Dic { get; } = new();

        public IUnRegisterHandle Register(TKey key, Func<T1, T2, T3, T4, T5, TResult> func)
        {
            if (!Dic.TryGetValue(key, out var f))
            {
                f = new EasyFunc<T1, T2, T3, T4, T5, TResult>();
                Dic[key] = f;
            }
            return f.Register(func);
        }

        public void UnRegister(TKey key, Func<T1, T2, T3, T4, T5, TResult> func)
        {
            if (Dic.TryGetValue(key, out var f))
                f.UnRegister(func);
        }
        public TResult Invoke(TKey key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (Dic.TryGetValue(key, out var f))
                return f.Invoke(arg1, arg2, arg3, arg4, arg5);
            return default;
        }
        public TResult[] InvokeAndReturnAll(TKey key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (Dic.TryGetValue(key, out var f))
                return f.InvokeAndReturnAll(arg1, arg2, arg3, arg4, arg5);
            return default;
        }
        public TResult[] InvokeAll(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            TResult[] result = new TResult[Dic.Count];
            var i = 0;
            foreach (var f in Dic.Values)
            {
                result[i] = f.Invoke(arg1, arg2, arg3, arg4, arg5);
                i++;
            }
            return result;
        }
        public TResult[][] InvokeAllAndReturnAll(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<TResult> action)
        {
            TResult[][] result = new TResult[Dic.Count][];
            var i = 0;
            foreach (var f in Dic.Values)
            {
                result[i] = f.InvokeAndReturnAll(arg1, arg2, arg3, arg4, arg5);
                i++;
            }
            return result;
        }
    }



    public static class KeyEventExtension
    {
        public static IUnRegisterHandle Register<TKey, TEvent>(this IKeyEvent<TKey, TEvent> keyEvent, TKey key, Action action) where TEvent: IEasyEvent, new()
        {
            if (!keyEvent.Dic.TryGetValue(key, out var e))
            {
                e = new TEvent();
                keyEvent.Dic[key] = e;
            }
            return e.Register(action);
        }
        public static void UnRegister<TKey, TEvent>(this IKeyEvent<TKey, TEvent> keyEvent,TKey key, Action action) where TEvent: IEasyEvent
        {
            if (keyEvent.Dic.TryGetValue(key,out var e))
                e.UnRegister(action);
        }
        public static void BaseInvoke<TKey, TEvent>(this IKeyEvent<TKey, TEvent> keyEvent,TKey key) where TEvent: IEasyEvent
        {
            if (keyEvent.Dic.TryGetValue(key,out var e))
                e.BaseInvoke();
        }
        public static void InvokeAll<TKey, TEvent>(this IKeyEvent<TKey, TEvent> keyEvent) where TEvent: IEasyEvent
        {
            foreach (var e in keyEvent.Dic.Values)
            {
                e.BaseInvoke();
            }
        }
        
        
        public static IUnRegisterHandle Register<TKey,TFunc,TResult>(this IKeyFunc<TKey, TFunc, TResult> keyFunc, TKey key, Func<TResult> func) where TFunc: IEasyFunc<TResult>, new()
        {
            if (!keyFunc.Dic.TryGetValue(key, out var f))
            {
                f = new TFunc();
                keyFunc.Dic[key] = f;
            }
            return f.Register(func);
        }
        public static void UnRegister<TKey, TFunc, TResult>(this IKeyFunc<TKey, TFunc, TResult> keyFunc, TKey key, Func<TResult> func) where TFunc: IEasyFunc<TResult>
        {
            if (keyFunc.Dic.TryGetValue(key, out var f))
                f.UnRegister(func);
        }
        public static TResult Invoke<TKey, TFunc, TResult>(this IKeyFunc<TKey, TFunc, TResult> keyFunc, TKey key) where TFunc: IEasyFunc<TResult>
        {
            if (keyFunc.Dic.TryGetValue(key, out var f))
                return f.BaseInvoke();
            return default;
        }
        public static TResult[] InvokeAndReturnAll<TKey, TFunc, TResult>(this IKeyFunc<TKey, TFunc, TResult> keyFunc, TKey key) where TFunc: IEasyFunc<TResult>
        {
            if (keyFunc.Dic.TryGetValue(key, out var f))
                return f.BaseInvokeAndReturnAll();
            return default;
        }
        public static TResult[] InvokeAll<TKey, TFunc, TResult>(this IKeyFunc<TKey, TFunc, TResult> keyFunc) where TFunc: IEasyFunc<TResult>
        {
            TResult[] result = new TResult[keyFunc.Dic.Count];
            var i = 0;
            foreach (var f in keyFunc.Dic.Values)
            {
                result[i] = f.BaseInvoke();
                i++;
            }
            return result;
        }
        public static TResult[][] InvokeAllAndReturnAll<TKey, TFunc, TResult>(this IKeyFunc<TKey, TFunc, TResult> keyFunc, Action<TResult> action) where TFunc: IEasyFunc<TResult>
        {
            TResult[][] result = new TResult[keyFunc.Dic.Count][];
            var i = 0;
            foreach (var f in keyFunc.Dic.Values)
            {
                result[i] = f.BaseInvokeAndReturnAll();
                i++;
            }
            return result;
        }


        public static void Clear<TKey, TDelegate>(this IkeyDelegate<TKey, TDelegate> keyEvent, TKey key) where TDelegate : IEDelegate
        {
            if (keyEvent.Dic.TryGetValue(key, out var e))
                e.Clear();
        }
        public static void ClearAll<TKey, TDelegate>(this IkeyDelegate<TKey, TDelegate> keyEvent) where TDelegate : IEDelegate
        {
            foreach (var e in keyEvent.Dic.Values)
                e.Clear();  
        }
    }
}