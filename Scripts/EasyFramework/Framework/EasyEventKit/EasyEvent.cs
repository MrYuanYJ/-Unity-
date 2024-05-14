using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyFramework.EventKit
{
    public interface IEDelegate
    {
        event Action OnceAction;
        void Clear();
        bool IsNull();
    }
    
    public interface IUnRegisterHandle
    {
        IEDelegate Delegate { get; }
        void UnRegister();
    }
    public struct UnRegisterHandle: IUnRegisterHandle
    {
        public UnRegisterHandle(IEDelegate del,Action action)
        {
            Delegate = del;
            UnRegisterAction = action;
        }

        public Action UnRegisterAction { get; private set; }

        public IEDelegate Delegate { get; private set; }

        public void UnRegister()
        {
            UnRegisterAction?.Invoke();
            UnRegisterAction = null;
        }
    }
    #region Event

    public interface IEasyEvent: IEDelegate
    {
        event Action BaseAction;

        IUnRegisterHandle Register(Action action)
        {
            BaseAction += action;
            return new UnRegisterHandle(this,() => BaseAction -= action);
        }
        void UnRegister(Action action)=> BaseAction -= action;
        void Invoke();
    }

    public class EasyEvent : IEasyEvent
    {
        public event Action BaseAction;
        public event Action OnceAction;

        public void Invoke()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            BaseAction?.Invoke();
            onceAction?.Invoke();
        }

        public IUnRegisterHandle Register(Action action)
        {
            BaseAction += action;
            return new UnRegisterHandle(this,() => BaseAction -= action);
        }

        public void UnRegister(Action action) => BaseAction -= action;

        public void Clear()
        {
            BaseAction = null;
            OnceAction = null;
        }

        public bool IsNull()
        {
            if (BaseAction == null)
                return true;
            return false;
        }
    }

    public class EasyEvent<A> : IEasyEvent
    {
        public event Action BaseAction;
        public event Action<A> Action;
        public event Action OnceAction;

        public void Invoke(A a)
        {
            var onceAction = OnceAction;
            OnceAction = null;
            BaseAction?.Invoke();
            Action?.Invoke(a);
            onceAction?.Invoke();
        }
        void IEasyEvent.Invoke()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            BaseAction?.Invoke();
            onceAction?.Invoke();
        }
        
        public IUnRegisterHandle Register(Action<A> action)
        {
            Action += action;
            return new UnRegisterHandle(this,() => Action -= action);
        }

        public void UnRegister(Action<A> action) => Action -= action;

        public void Clear()
        {
            Action = null;
            BaseAction = null;
            OnceAction = null;
        }
        public bool IsNull()
        {
            if (Action == null&&BaseAction == null)
                return true;
            return false;
        }
    }

    public class EasyEvent<A, B> : IEasyEvent
    {
        public event Action BaseAction;
        public event Action<A, B> Action;
        public event Action OnceAction;

        public void Invoke(A a, B b)
        {
            var onceAction = OnceAction;
            OnceAction = null;
            BaseAction?.Invoke();
            Action?.Invoke(a, b);
            onceAction?.Invoke();
        }
        void IEasyEvent.Invoke()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            BaseAction?.Invoke();
            onceAction?.Invoke();
        }
        public IUnRegisterHandle Register(Action<A, B> action)
        {
            Action += action;
            return new UnRegisterHandle(this,() => Action -= action);
        }
        public void UnRegister(Action<A, B> act) => Action -= act;

        public void Clear()
        {
            Action = null;
            BaseAction = null;
            OnceAction = null;
        }
        public bool IsNull()
        {
            if (Action == null&&BaseAction == null)
                return true;
            return false;
        }
    }

    public class EasyEvent<A, B, C> : IEasyEvent
    {
        public event Action BaseAction;
        public event Action<A, B, C> Action;
        public event Action OnceAction;

        public void Invoke(A a, B b, C c)
        {
            var onceAction = OnceAction;
            OnceAction = null;
            BaseAction?.Invoke();
            Action?.Invoke(a, b, c);
            onceAction?.Invoke();
        }
        void IEasyEvent.Invoke()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            BaseAction?.Invoke();
            onceAction?.Invoke();
        }
        public IUnRegisterHandle Register(Action<A, B, C> action)
        {
            Action += action;
            return new UnRegisterHandle(this,() => Action -= action);
        }
        public void UnRegister(Action<A, B, C> act) => Action -= act;

        public void Clear()
        {
            Action = null;
            BaseAction = null;
            OnceAction = null;
        }
        public bool IsNull()
        {
            if (Action == null && BaseAction == null)
                return true;
            return false;
        }
    }
    

    public class EasyEvent<A, B, C, D> : IEasyEvent
    {
        public event Action BaseAction;
        public event Action<A, B, C, D> Action;
        public event Action OnceAction;

        public void Invoke(A a, B b, C c, D d)
        {
            var onceAction = OnceAction;
            OnceAction = null;
            BaseAction?.Invoke();
            Action?.Invoke(a, b, c, d);
            onceAction?.Invoke();
        }
        void IEasyEvent.Invoke()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            BaseAction?.Invoke();
            onceAction?.Invoke();
        }
        public IUnRegisterHandle Register(Action<A, B, C, D> action)
        {
            Action += action;
            return new UnRegisterHandle(this,() => Action -= action);
        }
        public void UnRegister(Action<A, B, C, D> act) => Action -= act;

        public void Clear()
        {
            Action = null;
            BaseAction = null;
            OnceAction = null;
        }
        public bool IsNull()
        {
            if (Action == null && BaseAction == null)
                return true;
            return false;
        }
    }
    

    public class EasyEvent<A, B, C, D, E> : IEasyEvent
    {
        public event Action BaseAction;
        public event Action<A, B, C, D, E> Action;
        public event Action OnceAction;

        public void Invoke(A a, B b, C c, D d, E e)
        {
            var onceAction = OnceAction;
            OnceAction = null;
            BaseAction?.Invoke();
            Action?.Invoke(a, b, c, d, e);
            onceAction?.Invoke();
        }
        void IEasyEvent.Invoke()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            BaseAction?.Invoke();
            onceAction?.Invoke();
        }
        public IUnRegisterHandle Register(Action<A, B, C, D, E> action)
        {            
            Action += action;
            return new UnRegisterHandle(this,() => Action -= action);
        }
        public void UnRegister(Action<A, B, C, D, E> act) => Action -= act;

        public void Clear()
        {
            Action = null;
            BaseAction = null;
            OnceAction = null;
        }
        public bool IsNull()
        {
            if (Action == null && BaseAction == null)
                return true;
            return false;
        }
    }

    #endregion
    #region Funcs
   
    public interface IResult
    {
        public Type Type { get; }
        public T Value<T>();
    }
    public struct Result<T> : IResult
    {
        public Result(T value)
        {
            _value = value;
        }
        private T _value;
        public T Value => _value;

        public Type Type => typeof(T);

        TResult IResult.Value<TResult>()
        {
            if (_value is TResult result)
                return result;
            throw new InvalidCastException();
        }
        public static implicit operator T(Result<T> result) => result._value;
        public static implicit operator Result<T>(T value) => new Result<T> { _value = value };
    }

    public static class ResultExtensions
    {
        public static Result<T> AsResult<T>(this T value) => new Result<T>(value);
    }
    public interface IResults : ICollection
    {
        public Results<TResult> Values<TResult>();
    }
    
    public readonly struct Results<T>: IResults
    {
        public Results(int length)
        {
            _results = new T[length];
        }

        public Results(params Results<T>[] results)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < results.Length; i++)
            {
                list.AddRange(results[i]._results);
            }
            _results = list.ToArray();
        }
        private readonly T[] _results;

        public T GetResult(Func<T, bool> judgeFunc = null, T defaultReturn = default)
        {
            if (judgeFunc != null)
                foreach (var value in _results)
                {
                    if (judgeFunc(value))
                        return value;
                }
            else
            {
                if (_results.Length > 0)
                    return _results[^1];
            }

            return defaultReturn;
        }
        public void CopyTo(Array array, int index)=>_results.CopyTo(array, index);
        public int Count => _results?.Length ?? 0;
        public bool IsSynchronized => _results.IsSynchronized;
        public object SyncRoot => _results.SyncRoot;
        public IEnumerator GetEnumerator()=>_results.GetEnumerator();
        public T this[int index]
        {
            get => _results[index]; 
            set => _results[index] = value;
        }

        Results<TResult> IResults.Values<TResult>()
        {
            if(this is Results<TResult> results)
                return results;
            throw new InvalidCastException();
        }
    }

    public interface IEasyFunc: IEDelegate
    {
        public Type ReturnType { get; }
    }

    public interface IEasyFunc<Return> : IEasyFunc
    {
        Type IEasyFunc.ReturnType => typeof(Return);
        event Func<Return> BaseFunc;
        IUnRegisterHandle Register(Func<Return> func)
        {
            BaseFunc += func;
            return new UnRegisterHandle(this,() => BaseFunc -= func);
        }
        void UnRegister(Func<Return> func)=> BaseFunc -= func;
        Return Invoke();
        Results<Return> InvokeAndReturnAll();
    }
    public class EasyFunc<Return> : IEasyFunc<Return>
    {
        public event Func<Return> BaseFunc;
        public event Action OnceAction;

        public Return Invoke()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Return result = default;
            if (BaseFunc != null)
                result = BaseFunc();
            onceAction?.Invoke();
            return result;
        }
        public Results<Return> InvokeAndReturnAll()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Delegate[] funcs = Array.Empty<Delegate>();
            if (BaseFunc != null)
                funcs = BaseFunc.GetInvocationList();
            var results = new Results<Return>(funcs.Length);

            for (int i = 0; i < funcs.Length; i++)
                results[i] = ((Func<Return>) funcs[i])();

            onceAction?.Invoke();
            return results;
        }

        public IUnRegisterHandle Register(Func<Return> func)
        {
            BaseFunc += func;
            return new UnRegisterHandle(this,() => BaseFunc -= func);
        }

        public void UnRegister(Func<Return> act) => BaseFunc -= act;

        public void Clear()
        {
            BaseFunc = null;
            OnceAction = null;
        }

        public bool IsNull()
        {
            if (BaseFunc == null)
                return true;
            return false;
        }
    }
    public class EasyFunc<Return, A> : IEasyFunc<Return>
    {
        public event Func<Return> BaseFunc;
        public event Func<A, Return> Func;
        public event Action OnceAction;

        public Results<Return> InvokeAndReturnAll(A a)
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Delegate[] baseFuncs = Array.Empty<Delegate>();
            Delegate[] funcs = Array.Empty<Delegate>();
            if ( BaseFunc != null )
                baseFuncs = BaseFunc.GetInvocationList();
            if ( Func != null )
                funcs = Func.GetInvocationList();
            var results = new Results<Return>(baseFuncs.Length+funcs.Length);
            
            for ( int i = 0; i < baseFuncs.Length; i++ )
                results[i] = ( (Func<Return>) baseFuncs[i] )();
            for ( int i = 0; i < funcs.Length; i++ )
                results[baseFuncs.Length] = ( (Func<A,Return>) funcs[i] )(a);

            onceAction?.Invoke();
            return results;
        }
        public Return Invoke(A a)
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Return result =default;
            if ( BaseFunc != null )
                result = BaseFunc();
            if ( Func != null )
                result = Func(a);
            onceAction?.Invoke();
            return result;
        }
        Return IEasyFunc<Return>.Invoke()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Return result = default;
            if (BaseFunc != null)
                result = BaseFunc();
            onceAction?.Invoke();
            return result;
        }
        Results<Return> IEasyFunc<Return>.InvokeAndReturnAll()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Delegate[] funcs = Array.Empty<Delegate>();
            if (BaseFunc != null)
                funcs = BaseFunc.GetInvocationList();
            var results = new Results<Return>(funcs.Length);

            for (int i = 0; i < funcs.Length; i++)
                results[i] = ((Func<Return>) funcs[i])();

            onceAction?.Invoke();
            return results;
        }
        public IUnRegisterHandle Register(Func<A,Return> func)
        {
            Func += func;
            return new UnRegisterHandle(this,() => Func -= func);
        }

        public void UnRegister(Func<A,Return> act) => Func -= act;

        public void Clear()
        {
            Func = null;
            BaseFunc = null;
            OnceAction = null;
        }

        public bool IsNull()
        {
            if (Func == null && BaseFunc == null)
                return true;
            return false;
        }
    }
    public class EasyFunc<Return, A, B> : IEasyFunc<Return>
    {
        public event Func<Return> BaseFunc;
        public event Func<A, B, Return> Func;
        public event Action OnceAction;

        public Results<Return> InvokeAndReturnAll(A a, B b)
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Delegate[] baseFuncs = Array.Empty<Delegate>();
            Delegate[] funcs = Array.Empty<Delegate>();
            if ( BaseFunc != null )
                baseFuncs = BaseFunc.GetInvocationList();
            if ( Func != null )
                funcs = Func.GetInvocationList();
            var results = new Results<Return>(baseFuncs.Length+funcs.Length);
            
            for ( int i = 0; i < baseFuncs.Length; i++ )
                results[i] = ( (Func<Return>) baseFuncs[i] )();
            for ( int i = 0; i < funcs.Length; i++ )
                results[baseFuncs.Length] = ( (Func<A,B,Return>) funcs[i] )(a,b);

            onceAction?.Invoke();
            return results;
        }
        public Return Invoke(A a, B b)
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Return result =default;
            if ( BaseFunc != null )
                result = BaseFunc();
            if ( Func != null )
                result = Func(a,b);
            onceAction?.Invoke();
            return result;
        }
        Return IEasyFunc<Return>.Invoke()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Return result = default;
            if (BaseFunc != null)
                result = BaseFunc();
            onceAction?.Invoke();
            return result;
        }
        Results<Return> IEasyFunc<Return>.InvokeAndReturnAll()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Delegate[] funcs = Array.Empty<Delegate>();
            if (BaseFunc != null)
                funcs = BaseFunc.GetInvocationList();
            var results = new Results<Return>(funcs.Length);

            for (int i = 0; i < funcs.Length; i++)
                results[i] = ((Func<Return>) funcs[i])();

            onceAction?.Invoke();
            return results;
        }

        public IUnRegisterHandle Register(Func<A,B,Return> func)
        {
            Func += func;
            return new UnRegisterHandle(this,() => Func -= func);
        }

        public void UnRegister(Func<A,B,Return> act) => Func -= act;

        public void Clear()
        {
            Func = null;
            BaseFunc = null;
            OnceAction = null;
        }

        public bool IsNull()
        {
            if (Func == null && BaseFunc == null)
                return true;
            return false;
        }
    }
    public class EasyFunc<Return, A, B, C> : IEasyFunc<Return>
    {
        public event Func<Return> BaseFunc;
        public event Func<A, B, C, Return> Func;
        public event Action OnceAction;

        public Results<Return> InvokeAndReturnAll(A a, B b, C c)
        {
            var onceAction = OnceAction;            
            OnceAction = null;
            Delegate[] baseFuncs = Array.Empty<Delegate>();
            Delegate[] funcs = Array.Empty<Delegate>();
            if ( BaseFunc != null )
                baseFuncs = BaseFunc.GetInvocationList();
            if ( Func != null )
                funcs = Func.GetInvocationList();
            var results = new Results<Return>(baseFuncs.Length+funcs.Length);
            
            for ( int i = 0; i < baseFuncs.Length; i++ )
                results[i] = ( (Func<Return>) baseFuncs[i] )();
            for ( int i = 0; i < funcs.Length; i++ )
                results[baseFuncs.Length] = ( (Func<A,B,C,Return>) funcs[i] )(a,b,c);

            onceAction?.Invoke();
            return results;
        }
        public Return Invoke(A a, B b, C c)
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Return result =default;
            if ( BaseFunc != null )
                result = BaseFunc();
            if ( Func != null )
                result = Func(a,b,c);
            onceAction?.Invoke();
            return result;
        }
        Return IEasyFunc<Return>.Invoke()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Return result = default;
            if (BaseFunc != null)
                result = BaseFunc();
            onceAction?.Invoke();
            return result;
        }
        Results<Return> IEasyFunc<Return>.InvokeAndReturnAll()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Delegate[] funcs = Array.Empty<Delegate>();
            if (BaseFunc != null)
                funcs = BaseFunc.GetInvocationList();
            var results = new Results<Return>(funcs.Length);

            for (int i = 0; i < funcs.Length; i++)
                results[i] = ((Func<Return>) funcs[i])();

            onceAction?.Invoke();
            return results;
        }

        public IUnRegisterHandle Register(Func<A,B,C,Return> func)
        {
            Func += func;
            return new UnRegisterHandle(this,() => Func -= func);
        }

        public void UnRegister(Func<A,B,C,Return> act) => Func -= act;

        public void Clear()
        {
            Func = null;
            BaseFunc = null;
            OnceAction = null;
        }

        public bool IsNull()
        {
            if (Func == null && BaseFunc == null)
                return true;
            return false;
        }
    }
    public class EasyFunc<Return, A, B, C, D> : IEasyFunc<Return>
    {
       public event Func<Return> BaseFunc;
        public event Func<A, B, C, D, Return> Func;
        public event Action OnceAction;

        public Results<Return> InvokeAndReturnAll(A a, B b, C c, D d)
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Delegate[] baseFuncs = Array.Empty<Delegate>();
            Delegate[] funcs = Array.Empty<Delegate>();            
            if ( BaseFunc != null )
                baseFuncs = BaseFunc.GetInvocationList();
            if ( Func != null )
                funcs = Func.GetInvocationList();
            var results = new Results<Return>(baseFuncs.Length+funcs.Length);
            
            for ( int i = 0; i < baseFuncs.Length; i++ )
                results[i] = ( (Func<Return>) baseFuncs[i] )();
            for ( int i = 0; i < funcs.Length; i++ )
                results[baseFuncs.Length] = ( (Func<A,B,C,D,Return>) funcs[i] )(a,b,c,d);

            onceAction?.Invoke();
            return results;
        }
        public Return Invoke(A a, B b, C c, D d)
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Return result =default;
            if ( BaseFunc != null )
                result = BaseFunc();
            if ( Func != null )
                result = Func(a,b,c,d);
            onceAction?.Invoke();
            return result;
        }
        Return IEasyFunc<Return>.Invoke()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Return result = default;
            if (BaseFunc != null)
                result = BaseFunc();
            onceAction?.Invoke();
            return result;
        }
        Results<Return> IEasyFunc<Return>.InvokeAndReturnAll()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Delegate[] funcs = Array.Empty<Delegate>();
            if (BaseFunc != null)
                funcs = BaseFunc.GetInvocationList();
            var results = new Results<Return>(funcs.Length);

            for (int i = 0; i < funcs.Length; i++)
                results[i] = ((Func<Return>) funcs[i])();

            onceAction?.Invoke();
            return results;
        }

        public IUnRegisterHandle Register(Func<A,B,C,D,Return> func)
        {
            Func += func;
            return new UnRegisterHandle(this,() => Func -= func);
        }

        public void UnRegister(Func<A,B,C,D,Return> act) => Func -= act;

        public void Clear()
        {
            Func = null;
            BaseFunc = null;
            OnceAction = null;
        }

        public bool IsNull()
        {
            if (Func == null && BaseFunc == null)
                return true;
            return false;
        }
    }
    public class EasyFunc<Return, A, B, C, D, E> : IEasyFunc<Return>
    {
        public event Func<Return> BaseFunc;
        public event Func<A, B, C, D, E, Return> Func;
        public event Action OnceAction;

        public Results<Return> InvokeAndReturnAll(A a, B b, C c, D d, E e)
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Delegate[] baseFuncs = Array.Empty<Delegate>();
            Delegate[] funcs = Array.Empty<Delegate>();
            if ( BaseFunc != null )
                baseFuncs = BaseFunc.GetInvocationList();
            if ( Func != null )
                funcs = Func.GetInvocationList();
            var results = new Results<Return>(baseFuncs.Length+funcs.Length);
            
            for ( int i = 0; i < baseFuncs.Length; i++ )
                results[i] = ( (Func<Return>) baseFuncs[i] )();
            for ( int i = 0; i < funcs.Length; i++ )
                results[baseFuncs.Length] = ( (Func<A,B,C,D,E,Return>) funcs[i] )(a,b,c,d,e);

            onceAction?.Invoke();
            return results;
        }
        public Return Invoke(A a, B b, C c, D d, E e)
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Return result =default;
            if ( BaseFunc != null )
                result = BaseFunc();
            if ( Func != null )
                result = Func(a,b,c,d,e);
            onceAction?.Invoke();
            return result;
        }
        Return IEasyFunc<Return>.Invoke()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Return result = default;
            if (BaseFunc != null)
                result = BaseFunc();
            onceAction?.Invoke();
            return result;
        }
        Results<Return> IEasyFunc<Return>.InvokeAndReturnAll()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Delegate[] funcs = Array.Empty<Delegate>();
            if (BaseFunc != null)
                funcs = BaseFunc.GetInvocationList();
            var results = new Results<Return>(funcs.Length);

            for (int i = 0; i < funcs.Length; i++)
                results[i] = ((Func<Return>) funcs[i])();

            onceAction?.Invoke();
            return results;
        }

        public IUnRegisterHandle Register(Func<A,B,C,D,E,Return> func)
        {
            Func += func;
            return new UnRegisterHandle(this,() => Func -= func);
        }

        public void UnRegister(Func<A,B,C,D,E,Return> act) => Func -= act;

        public void Clear()
        {
            Func = null;
            BaseFunc = null;
            OnceAction = null;
        }

        public bool IsNull()
        {
            if (Func == null && BaseFunc == null)
                return true;
            return false;
        }
    }

    #endregion

    public static class EEventExtension
    {
        public static T OnlyPlayOnce<T>(this T self) where T : IUnRegisterHandle
        {
            self.Delegate.OnceAction += self.UnRegister;
            return self;
        }
        public static IUnRegisterHandle Register(this IEasyEvent self,Action action) => self.Register(action);
        public static IUnRegisterHandle Register<Return>(this IEasyFunc<Return> self,Func<Return> func)=> self.Register(func);
        public static void UnRegister(this IEasyEvent self,Action action) => self.UnRegister(action);
        public static void UnRegister<Return>(this IEasyFunc<Return> self,Func<Return> func)=> self.UnRegister(func);


        public static void RegisterAfterInvoke(this EasyEvent self, Action action, Action<IUnRegisterHandle> set = null)=>self.RegisterAfterInvoke(self, action, set);
        public static void RegisterAfterInvoke<A>(this EasyEvent<A> self, Action<A> action, Action<IUnRegisterHandle> set=null)=>self.RegisterAfterInvoke(self, action, set);
        public static void RegisterAfterInvoke<A, B>(this EasyEvent<A, B> self, Action<A, B> action, Action<IUnRegisterHandle> set=null)=>self.RegisterAfterInvoke(self, action, set);
        public static void RegisterAfterInvoke<A, B, C>(this EasyEvent<A, B, C> self, Action<A, B, C> action, Action<IUnRegisterHandle> set=null)=>self.RegisterAfterInvoke(self, action, set);
        public static void RegisterAfterInvoke<A, B, C, D>(this EasyEvent<A, B, C, D> self, Action<A, B, C, D> action, Action<IUnRegisterHandle> set=null)=>self.RegisterAfterInvoke(self, action, set);
        public static void RegisterAfterInvoke<A, B, C, D, E>(this EasyEvent<A, B, C, D, E> self, Action<A, B, C, D, E> action, Action<IUnRegisterHandle> set=null)=>self.RegisterAfterInvoke(self, action, set);
        

        public static void RegisterAfterInvoke(this IEDelegate self,EasyEvent easyEvent, Action action,Action<IUnRegisterHandle> set=null)
        {
            self.OnceAction += () =>
            {
                var handle = easyEvent.Register(action);
                set?.Invoke(handle);
            };
        }
        public static void RegisterAfterInvoke<A>(this IEDelegate self, EasyEvent<A> easyEvent, Action<A> action,Action<IUnRegisterHandle> set=null)
        {
            self.OnceAction += () =>
            {
                var handle = easyEvent.Register(action);
                set?.Invoke(handle);
            };
        }
        public static void RegisterAfterInvoke<A, B>(this IEDelegate self, EasyEvent<A, B> easyEvent, Action<A, B> action,Action<IUnRegisterHandle> set=null)
        {
            self.OnceAction += () =>
            {
                var handle = easyEvent.Register(action);
                set?.Invoke(handle);
            };
        }
        public static void RegisterAfterInvoke<A, B, C>(this IEDelegate self, EasyEvent<A, B, C> easyEvent, Action<A, B, C> action, Action<IUnRegisterHandle> set=null)
        {
            self.OnceAction += () =>
            {
                var handle = easyEvent.Register(action);
                set?.Invoke(handle);
            };
        }
        public static void RegisterAfterInvoke<A, B, C, D>(this IEDelegate self, EasyEvent<A, B, C, D> easyEvent, Action<A, B, C, D> action, Action<IUnRegisterHandle> set=null)
        {
            self.OnceAction += () =>
            {
                var handle = easyEvent.Register(action);
                set?.Invoke(handle);
            };
        }
        public static void RegisterAfterInvoke<A, B, C, D, E>(this IEDelegate self, EasyEvent<A, B, C, D, E> easyEvent, Action<A, B, C, D, E> action, Action<IUnRegisterHandle> set=null)
        {
            self.OnceAction += () =>
            {
                var handle = easyEvent.Register(action);
                set?.Invoke(handle);
            };
        }

        
        public static IUnRegisterHandle RegisterAfterInvoke(this IUnRegisterHandle self, EasyEvent easyEvent, Action action, Action<IUnRegisterHandle> set=null)
        {
            self.Delegate.OnceAction += () =>
            {
                var handle = easyEvent.Register(action);
                set?.Invoke(handle);
            };
            return self;
        }
        public static IUnRegisterHandle RegisterAfterInvoke<A>(this IUnRegisterHandle self, EasyEvent<A> easyEvent, Action<A> action, Action<IUnRegisterHandle> set=null)
        {
            self.Delegate.OnceAction += () =>
            {
                var handle = easyEvent.Register(action);
                set?.Invoke(handle);
            };
            return self;
        }
        public static IUnRegisterHandle RegisterAfterInvoke<A, B>(this IUnRegisterHandle self, EasyEvent<A, B> easyEvent, Action<A, B> action, Action<IUnRegisterHandle> set=null)
        {
            self.Delegate.OnceAction += () =>
            {
                var handle = easyEvent.Register(action);
                set?.Invoke(handle);
            };
            return self;
        }
        public static IUnRegisterHandle RegisterAfterInvoke<A, B, C>(this IUnRegisterHandle self, EasyEvent<A, B, C> easyEvent, Action<A, B, C> action, Action<IUnRegisterHandle> set=null)
        {
            self.Delegate.OnceAction += () =>
            {
                var handle = easyEvent.Register(action);
                set?.Invoke(handle);
            };
            return self;
        }
        public static IUnRegisterHandle RegisterAfterInvoke<A, B, C, D>(this IUnRegisterHandle self, EasyEvent<A, B, C, D> easyEvent, Action<A, B, C, D> action, Action<IUnRegisterHandle> set=null)
        {
            self.Delegate.OnceAction += () =>
            {
                var handle = easyEvent.Register(action);
                set?.Invoke(handle);
            };
            return self;
        }
        public static IUnRegisterHandle RegisterAfterInvoke<A, B, C, D, E>(this IUnRegisterHandle self, EasyEvent<A, B, C, D, E> easyEvent, Action<A, B, C, D, E> action, Action<IUnRegisterHandle> set=null)
        {
            self.Delegate.OnceAction += () =>
            {
                var handle = easyEvent.Register(action);
                set?.Invoke(handle);
            };
            return self;
        }

        
        public static void RegisterAfterInvoke<R>(this EasyFunc<R> self, Func<R> action, Action<IUnRegisterHandle> set=null)=>self.RegisterAfterInvoke(self, action, set);
        public static void RegisterAfterInvoke<R, A>(this EasyFunc<R,A> self, Func<A, R> action, Action<IUnRegisterHandle> set=null)=>self.RegisterAfterInvoke(self, action, set);
        public static void RegisterAfterInvoke<R, A, B>(this EasyFunc<R,A,B> self, Func<A, B, R> action, Action<IUnRegisterHandle> set=null)=>self.RegisterAfterInvoke(self, action, set);
        public static void RegisterAfterInvoke<R, A, B, C>(this EasyFunc<R,A,B,C> self, Func<A, B, C, R> action, Action<IUnRegisterHandle> set=null)=>self.RegisterAfterInvoke(self, action, set);
        public static void RegisterAfterInvoke<R, A, B, C, D>(this EasyFunc<R,A,B,C,D> self, Func<A, B, C, D, R> action, Action<IUnRegisterHandle> set=null)=>self.RegisterAfterInvoke(self, action, set);
        public static void RegisterAfterInvoke<R, A, B, C, D, E>(this EasyFunc<R,A,B,C,D,E> self, Func<A, B, C, D, E, R> action, Action<IUnRegisterHandle> set=null)=>self.RegisterAfterInvoke(self, action, set);
        
        
        public static void RegisterAfterInvoke<R>(this IEDelegate self, EasyFunc<R> easyFunc, Func<R> action, Action<IUnRegisterHandle> set=null)
        {
            self.OnceAction += () =>
            {
                var handle = easyFunc.Register(action);
                set?.Invoke(handle);
            };
        }
        public static void RegisterAfterInvoke<R, A>(this IEDelegate self, EasyFunc<R,A> easyFunc, Func<A, R> action, Action<IUnRegisterHandle> set=null)
        {
            self.OnceAction += () =>
            {
                var handle = easyFunc.Register(action);
                set?.Invoke(handle);
            };
        }
        public static void RegisterAfterInvoke<R, A, B>(this IEDelegate self, EasyFunc<R,A,B> easyFunc, Func<A, B, R> action, Action<IUnRegisterHandle> set=null)
        {
            self.OnceAction += () =>
            {
                var handle = easyFunc.Register(action);
                set?.Invoke(handle);
            };
        }
        public static void RegisterAfterInvoke<R, A, B, C>(this IEDelegate self, EasyFunc<R,A,B,C> easyFunc, Func<A, B, C, R> action, Action<IUnRegisterHandle> set=null)
        {
            self.OnceAction += () =>
            {
                var handle = easyFunc.Register(action);
                set?.Invoke(handle);
            };
        }
        public static void RegisterAfterInvoke<R, A, B, C, D>(this IEDelegate self, EasyFunc<R,A,B,C,D> easyFunc, Func<A, B, C, D, R> action, Action<IUnRegisterHandle> set=null)
        {
            self.OnceAction += () =>
            {
                var handle = easyFunc.Register(action);
                set?.Invoke(handle);
            };
        }
        public static void RegisterAfterInvoke<R, A, B, C, D, E>(this IEDelegate self, EasyFunc<R,A,B,C,D,E> easyFunc, Func<A, B, C, D, E, R> action, Action<IUnRegisterHandle> set=null)
        {
            self.OnceAction += () =>
            {
                var handle = easyFunc.Register(action);
                set?.Invoke(handle);
            };
        }
        
        
        public static IUnRegisterHandle RegisterAfterInvoke<R>(this IUnRegisterHandle self, EasyFunc<R> easyFunc, Func<R> action, Action<IUnRegisterHandle> set=null)
        {
            self.Delegate.OnceAction += () =>
            {
                var handle = easyFunc.Register(action);
                set?.Invoke(handle);
            };
            return self;
        }
        public static IUnRegisterHandle RegisterAfterInvoke<R, A>(this IUnRegisterHandle self, EasyFunc<R,A> easyFunc, Func<A, R> action, Action<IUnRegisterHandle> set=null)
        {
            self.Delegate.OnceAction += () =>
            {
                var handle = easyFunc.Register(action);
                set?.Invoke(handle);
            };
            return self;
        }
        public static IUnRegisterHandle RegisterAfterInvoke<R, A, B>(this IUnRegisterHandle self, EasyFunc<R,A,B> easyFunc, Func<A, B, R> action, Action<IUnRegisterHandle> set=null)
        {
            self.Delegate.OnceAction += () =>
            {
                var handle = easyFunc.Register(action);
                set?.Invoke(handle);
            };
            return self;
        }
        public static IUnRegisterHandle RegisterAfterInvoke<R, A, B, C>(this IUnRegisterHandle self, EasyFunc<R,A,B,C> easyFunc, Func<A, B, C, R> action, Action<IUnRegisterHandle> set=null)
        {
            self.Delegate.OnceAction += () =>
            {
                var handle = easyFunc.Register(action);
                set?.Invoke(handle);
            };
            return self;
        }
        public static IUnRegisterHandle RegisterAfterInvoke<R, A, B, C, D>(this IUnRegisterHandle self, EasyFunc<R,A,B,C,D> easyFunc, Func<A, B, C, D, R> action, Action<IUnRegisterHandle> set=null)
        {
            self.Delegate.OnceAction += () =>
            {
                var handle = easyFunc.Register(action);
                set?.Invoke(handle);
            };
            return self;
        }
        public static IUnRegisterHandle RegisterAfterInvoke<R, A, B, C, D, E>(this IUnRegisterHandle self, EasyFunc<R,A,B,C,D,E> easyFunc, Func<A, B, C, D, E, R> action, Action<IUnRegisterHandle> set=null)
        {
            self.Delegate.OnceAction += () =>
            {
                var handle = easyFunc.Register(action);
                set?.Invoke(handle);
            };
            return self;
        }

        
        public static IUnRegisterHandle InvokeAndRegister(this IEasyEvent self, Action action)
        {
            action();
            return self.Register(action);
        }
        public static IUnRegisterHandle InvokeAndRegister<A>(this EasyEvent<A> self, Action<A> action, A a)
        {
            action(a);
            return self.Register(action);
        }
        public static IUnRegisterHandle InvokeAndRegister<A, B>(this EasyEvent<A, B> self, Action<A, B> action, A a, B b)
        {
            action(a, b);
            return self.Register(action);
        }
        public static IUnRegisterHandle InvokeAndRegister<A, B, C>(this EasyEvent<A, B, C> self, Action<A, B, C> action, A a, B b, C c)
        {
            action(a, b, c);
            return self.Register(action);
        }
        public static IUnRegisterHandle InvokeAndRegister<A, B, C, D>(this EasyEvent<A, B, C, D> self, Action<A, B, C, D> action, A a, B b, C c, D d)
        {
            action(a, b, c, d);
            return self.Register(action);
        }
        public static IUnRegisterHandle InvokeAndRegister<A, B, C, D, E>(this EasyEvent<A, B, C, D, E> self, Action<A, B, C, D, E> action, A a, B b, C c, D d, E e)
        {
            action(a, b, c, d, e);
            return self.Register(action);
        }


        public static IUnRegisterHandle InvokeAndRegister<R>(this IEasyFunc<R> self, Func<R> func, out R result)
        {
            result = func();
            return self.Register(func);
        }
        public static IUnRegisterHandle InvokeAndRegister<R, A>(this EasyFunc<R,A> self, Func<A, R> func, A a, out R result)
        {
            result = func(a);
            return self.Register(func);
        }
        public static IUnRegisterHandle InvokeAndRegister<R, A, B>(this EasyFunc<R,A,B> self, Func<A, B, R> func, A a, B b, out R result)
        {
            result = func(a, b);
            return self.Register(func);
        }
        public static IUnRegisterHandle InvokeAndRegister<R, A, B, C>(this EasyFunc<R,A,B,C> self, Func<A, B, C, R> func, A a, B b, C c, out R result)
        {
            result = func(a, b, c);
            return self.Register(func);
        }
        public static IUnRegisterHandle InvokeAndRegister<R, A, B, C, D>(this EasyFunc<R,A,B,C,D> self, Func<A, B, C, D, R> func, A a, B b, C c, D d, out R result)
        {
            result = func(a, b, c, d);
            return self.Register(func);
        }
        public static IUnRegisterHandle InvokeAndRegister<R, A, B, C, D, E>(this EasyFunc<R,A,B,C,D,E> self, Func<A, B, C, D, E, R> func, A a, B b, C c, D d, E e, out R result)
        {
            result = func(a, b, c, d, e);
            return self.Register(func);
        }
    }
}