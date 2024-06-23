using System;
using System.Collections;
using System.Collections.Generic;
using EXFunctionKit;

namespace EasyFramework.EventKit
{
    public interface IEDelegate
    {
        event Action OnceAction;
        protected Action GetAndClearOnceAction();
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

        private Action UnRegisterAction { get; set; }
        public IEDelegate Delegate { get; private set; }

        public void UnRegister()
        {
            UnRegisterAction?.Invoke();
        }
    }
    #region Event

    public interface IEasyEvent: IEDelegate
    {
        event Action BaseAction;
        protected void InvokeBaseAction();

        IUnRegisterHandle Register(Action action)
        {
            BaseAction += action;
            return new UnRegisterHandle(this,() => BaseAction -= action);
        }
        void UnRegister(Action action)=> BaseAction -= action;
        void BaseInvoke()
        {
            var onceAction = GetAndClearOnceAction();
            InvokeBaseAction();
            onceAction?.Invoke();
        }
    }

    public class EasyEvent : IEasyEvent
    {
        public event Action BaseAction;
        void IEasyEvent.InvokeBaseAction()=> BaseAction?.Invoke();
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
        Action IEDelegate.GetAndClearOnceAction()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            return onceAction;
        }

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
        void IEasyEvent.InvokeBaseAction()=> BaseAction?.Invoke();
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
        
        public IUnRegisterHandle Register(Action<A> action)
        {
            Action += action;
            return new UnRegisterHandle(this,() => Action -= action);
        }

        public void UnRegister(Action<A> action) => Action -= action;
        Action IEDelegate.GetAndClearOnceAction()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            return onceAction;
        }
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
        void IEasyEvent.InvokeBaseAction()=> BaseAction?.Invoke();
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
        public IUnRegisterHandle Register(Action<A, B> action)
        {
            Action += action;
            return new UnRegisterHandle(this,() => Action -= action);
        }
        public void UnRegister(Action<A, B> act) => Action -= act;
        Action IEDelegate.GetAndClearOnceAction()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            return onceAction;
        }
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
        void IEasyEvent.InvokeBaseAction()=> BaseAction?.Invoke();
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
        public IUnRegisterHandle Register(Action<A, B, C> action)
        {
            Action += action;
            return new UnRegisterHandle(this,() => Action -= action);
        }
        public void UnRegister(Action<A, B, C> act) => Action -= act;
        Action IEDelegate.GetAndClearOnceAction()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            return onceAction;
        }
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
        void IEasyEvent.InvokeBaseAction()=> BaseAction?.Invoke();
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
        public IUnRegisterHandle Register(Action<A, B, C, D> action)
        {
            Action += action;
            return new UnRegisterHandle(this,() => Action -= action);
        }
        public void UnRegister(Action<A, B, C, D> act) => Action -= act;
        Action IEDelegate.GetAndClearOnceAction()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            return onceAction;
        }
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
        void IEasyEvent.InvokeBaseAction()=> BaseAction?.Invoke();
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
        public IUnRegisterHandle Register(Action<A, B, C, D, E> action)
        {            
            Action += action;
            return new UnRegisterHandle(this,() => Action -= action);
        }
        public void UnRegister(Action<A, B, C, D, E> act) => Action -= act;
        Action IEDelegate.GetAndClearOnceAction()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            return onceAction;
        }
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
    // public interface IResults : ICollection
    // {
    //     public Results<TResult> Values<TResult>();
    // }
    //
    // public readonly struct Results<T>: IResults
    // {
    //     public Results(int length)
    //     {
    //         _results = new T[length];
    //     }
    //
    //     public Results(params Results<T>[] results)
    //     {
    //         List<T> list = new List<T>();
    //         for (int i = 0; i < results.Length; i++)
    //         {
    //             list.AddRange(results[i]._results);
    //         }
    //         _results = list.ToArray();
    //     }
    //     private readonly T[] _results;
    //
    //     public T GetResult(Func<T, bool> judgeFunc = null, T defaultReturn = default)
    //     {
    //         if (judgeFunc != null)
    //             foreach (var value in _results)
    //             {
    //                 if (judgeFunc(value))
    //                     return value;
    //             }
    //         else
    //         {
    //             if (_results.Length > 0)
    //                 return _results[^1];
    //         }
    //
    //         return defaultReturn;
    //     }
    //     public void CopyTo(Array array, int index)=>_results.CopyTo(array, index);
    //     public int Count => _results?.Length ?? 0;
    //     public bool IsSynchronized => _results.IsSynchronized;
    //     public object SyncRoot => _results.SyncRoot;
    //     public IEnumerator GetEnumerator()=>_results.GetEnumerator();
    //     public T this[int index]
    //     {
    //         get => _results[index]; 
    //         set => _results[index] = value;
    //     }
    //
    //     Results<TResult> IResults.Values<TResult>()
    //     {
    //         if(this is Results<TResult> results)
    //             return results;
    //         throw new InvalidCastException();
    //     }
    // }

    public interface IEasyFunc: IEDelegate
    {
        public Type ReturnType { get; }
    }

    public interface IEasyFunc<Return> : IEasyFunc
    {
        Type IEasyFunc.ReturnType => typeof(Return);
        event Func<Return> BaseFunc;
        public Delegate[] BaseDelegates { get; set; }
        protected Return InvokeBaseFunc();
        protected Delegate[] GetBaseFuncInvocationList();
        IUnRegisterHandle Register(Func<Return> func)
        {
            BaseDelegates = null;
            BaseFunc += func;
            return new UnRegisterHandle(this,() => BaseFunc -= func);
        }
        void UnRegister(Func<Return> func)
        {
            BaseDelegates = null;
            BaseFunc -= func;
        }

        Return BaseInvoke()
        {
            var onceAction = GetAndClearOnceAction();
            Return result = InvokeBaseFunc();
            onceAction?.Invoke();
            return result;
        }
        Return[] BaseInvokeAndReturnAll()
        {
            BaseDelegates ??= GetBaseFuncInvocationList();

            var onceAction = GetAndClearOnceAction();
            var delegateArray = BaseDelegates;
            var results = new Return[delegateArray.Length];
            for (int i = 0; i < delegateArray.Length; i++)
                if (delegateArray[i] is Func<Return> func)
                    results[i] = func();
            
            onceAction?.Invoke();
            return results;
        }
    }
    public class EasyFunc<Return> : IEasyFunc<Return>
    {
        public event Func<Return> BaseFunc;
        Delegate[] IEasyFunc<Return>.BaseDelegates { get; set; }
        Return IEasyFunc<Return>.InvokeBaseFunc() => BaseFunc != null ? BaseFunc() : default;
        Delegate[] IEasyFunc<Return>.GetBaseFuncInvocationList()=>BaseFunc == null ? Array.Empty<Delegate>() : BaseFunc.GetInvocationList();
        
        public event Action OnceAction;

        public Return Invoke()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            Return result = BaseFunc != null ? BaseFunc() : default;
            onceAction?.Invoke();
            return result;
        }

        public Return[] InvokeAndReturnAll()
        {
            ((IEasyFunc<Return>)this).BaseDelegates ??= BaseFunc == null ? Array.Empty<Delegate>() : BaseFunc.GetInvocationList();

            var onceAction = OnceAction;
            OnceAction = null;
            var delegateArray = ((IEasyFunc<Return>)this).BaseDelegates;
            var results = new Return[delegateArray.Length];
            for (int i = 0; i < delegateArray.Length; i++)
                if (delegateArray[i] is Func<Return> func)
                    results[i] = func();
            
            onceAction?.Invoke();
            return results;
        }

        public IUnRegisterHandle Register(Func<Return> func)
        {
            ((IEasyFunc<Return>)this).BaseDelegates = null;
            BaseFunc += func;
            return new UnRegisterHandle(this,() => BaseFunc -= func);
        }

        public void UnRegister(Func<Return> act)
        {
            ((IEasyFunc<Return>)this).BaseDelegates = null;
            BaseFunc -= act;
        }
        Action IEDelegate.GetAndClearOnceAction()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            return onceAction;
        }
        public void Clear()
        {
            ((IEasyFunc<Return>)this).BaseDelegates = null;
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
    public class EasyFunc<A,Return> : IEasyFunc<Return>
    {
        public event Func<Return> BaseFunc;
        Delegate[] IEasyFunc<Return>.BaseDelegates { get; set; }
        Return IEasyFunc<Return>.InvokeBaseFunc() => BaseFunc != null ? BaseFunc() : default;
        Delegate[] IEasyFunc<Return>.GetBaseFuncInvocationList()=>BaseFunc == null ? Array.Empty<Delegate>() : BaseFunc.GetInvocationList();
        public event Func<A, Return> Func;
        private Delegate[] _delegates;
        public event Action OnceAction;

        public Return[] InvokeAndReturnAll(A a)
        {
            var self = ((IEasyFunc<Return>) this);
            self.BaseDelegates ??= BaseFunc == null ? Array.Empty<Delegate>() : BaseFunc.GetInvocationList();
            _delegates ??= Func == null ? Array.Empty<Delegate>() : Func.GetInvocationList();

            var onceAction = OnceAction;
            OnceAction = null;
            var baseFuncs = self.BaseDelegates;
            var funcs = _delegates;
            var results = new Return[baseFuncs.Length + funcs.Length];
            for ( int i = 0; i < baseFuncs.Length; i++ )
                results[i] = ( (Func<Return>) baseFuncs[i] )();
            for ( int i = 0; i < funcs.Length; i++ )
                results[baseFuncs.Length+i] = ( (Func<A,Return>) funcs[i] )(a);

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
        public IUnRegisterHandle Register(Func<A,Return> func)
        {
            _delegates = null;
            Func += func;
            return new UnRegisterHandle(this,() => Func -= func);
        }

        public void UnRegister(Func<A,Return> act)
        {
            _delegates = null;
            Func -= act;
        }

        Action IEDelegate.GetAndClearOnceAction()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            return onceAction;
        }
        public void Clear()
        {
            _delegates = null;
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
    public class EasyFunc<A, B,Return> : IEasyFunc<Return>
    {
        public event Func<Return> BaseFunc;     
        Delegate[] IEasyFunc<Return>.BaseDelegates { get; set; }
        Return IEasyFunc<Return>.InvokeBaseFunc() => BaseFunc != null ? BaseFunc() : default;
        Delegate[] IEasyFunc<Return>.GetBaseFuncInvocationList()=>BaseFunc == null ? Array.Empty<Delegate>() : BaseFunc.GetInvocationList();
        public event Func<A, B, Return> Func;
        private Delegate[] _delegates;
        public event Action OnceAction;

        public Return[] InvokeAndReturnAll(A a, B b)
        {
            var self = ((IEasyFunc<Return>) this);
            self.BaseDelegates ??= BaseFunc == null ? Array.Empty<Delegate>() : BaseFunc.GetInvocationList();
            _delegates ??= Func == null ? Array.Empty<Delegate>() : Func.GetInvocationList();

            var onceAction = OnceAction;
            OnceAction = null;
            var baseFuncs = self.BaseDelegates;
            var funcs = _delegates;
            var results = new Return[baseFuncs.Length+funcs.Length];
            for ( int i = 0; i < baseFuncs.Length; i++ )
                results[i] = ( (Func<Return>) baseFuncs[i] )();
            for ( int i = 0; i < funcs.Length; i++ )
                results[baseFuncs.Length+i] = ( (Func<A,B,Return>) funcs[i] )(a,b);

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

        public IUnRegisterHandle Register(Func<A,B,Return> func)
        {
            _delegates = null;
            Func += func;
            return new UnRegisterHandle(this,() => Func -= func);
        }

        public void UnRegister(Func<A,B,Return> act)
        {
            _delegates = null;
            Func -= act;
        }

        Action IEDelegate.GetAndClearOnceAction()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            return onceAction;
        }
        public void Clear()
        {
            _delegates = null;
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
    
    public class EasyFunc<A, B, C,Return> : IEasyFunc<Return>
    {
        public event Func<Return> BaseFunc;
        Delegate[] IEasyFunc<Return>.BaseDelegates { get; set; }
        Return IEasyFunc<Return>.InvokeBaseFunc() => BaseFunc != null ? BaseFunc() : default;
        Delegate[] IEasyFunc<Return>.GetBaseFuncInvocationList()=>BaseFunc == null ? Array.Empty<Delegate>() : BaseFunc.GetInvocationList();
        public event Func<A, B, C, Return> Func;
        private Delegate[] _delegates;
        public event Action OnceAction;

        public Return[] InvokeAndReturnAll(A a, B b, C c)
        {
            var self = ((IEasyFunc<Return>) this);
            self.BaseDelegates ??= BaseFunc == null ? Array.Empty<Delegate>() : BaseFunc.GetInvocationList();
            _delegates ??= Func == null ? Array.Empty<Delegate>() : Func.GetInvocationList();

            var onceAction = OnceAction;
            OnceAction = null;
            var baseFuncs = self.BaseDelegates;
            var funcs = _delegates;
            var results = new Return[baseFuncs.Length+funcs.Length];
            for ( int i = 0; i < baseFuncs.Length; i++ )
                results[i] = ( (Func<Return>) baseFuncs[i] )();
            for ( int i = 0; i < funcs.Length; i++ )
                results[baseFuncs.Length+i] = ( (Func<A,B,C,Return>) funcs[i] )(a,b,c);

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

        public IUnRegisterHandle Register(Func<A,B,C,Return> func)
        {
            _delegates = null;
            Func += func;
            return new UnRegisterHandle(this,() => Func -= func);
        }

        public void UnRegister(Func<A,B,C,Return> act)
        {
            _delegates = null;
            Func -= act;
        }

        Action IEDelegate.GetAndClearOnceAction()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            return onceAction;
        }
        public void Clear()
        {
            _delegates = null;
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
    
    public class EasyFunc<A, B, C, D,Return> : IEasyFunc<Return>
    {
        public event Func<Return> BaseFunc;
        Delegate[] IEasyFunc<Return>.BaseDelegates { get; set; }
        Return IEasyFunc<Return>.InvokeBaseFunc() => BaseFunc != null ? BaseFunc() : default;
        Delegate[] IEasyFunc<Return>.GetBaseFuncInvocationList()=>BaseFunc == null ? Array.Empty<Delegate>() : BaseFunc.GetInvocationList();
        public event Func<A, B, C, D, Return> Func;
        private Delegate[] _delegates;
        public event Action OnceAction;

        public Return[] InvokeAndReturnAll(A a, B b, C c, D d)
        {
            var self = ((IEasyFunc<Return>) this);
            self.BaseDelegates ??= BaseFunc == null ? Array.Empty<Delegate>() : BaseFunc.GetInvocationList();
            _delegates ??= Func == null ? Array.Empty<Delegate>() : Func.GetInvocationList();

            var onceAction = OnceAction;
            OnceAction = null;
            var baseFuncs = self.BaseDelegates;
            var funcs = _delegates;
            var results = new Return[baseFuncs.Length+funcs.Length];
            for ( int i = 0; i < baseFuncs.Length; i++ )
                results[i] = ( (Func<Return>) baseFuncs[i] )();
            for ( int i = 0; i < funcs.Length; i++ )
                results[baseFuncs.Length+i] = ( (Func<A,B,C,D,Return>) funcs[i] )(a,b,c,d);

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

        public IUnRegisterHandle Register(Func<A,B,C,D,Return> func)
        {
            _delegates = null;
            Func += func;
            return new UnRegisterHandle(this,() => Func -= func);
        }

        public void UnRegister(Func<A,B,C,D,Return> act)
        {
            _delegates = null;
            Func -= act;
        }

        Action IEDelegate.GetAndClearOnceAction()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            return onceAction;
        }
        public void Clear()
        {
            _delegates = null;
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
    
    public class EasyFunc<A, B, C, D, E,Return> : IEasyFunc<Return>
    {
        public event Func<Return> BaseFunc;
        Delegate[] IEasyFunc<Return>.BaseDelegates { get; set; }
        Return IEasyFunc<Return>.InvokeBaseFunc() => BaseFunc != null ? BaseFunc() : default;
        Delegate[] IEasyFunc<Return>.GetBaseFuncInvocationList()=>BaseFunc == null ? Array.Empty<Delegate>() : BaseFunc.GetInvocationList();
        public event Func<A, B, C, D, E, Return> Func;
        private Delegate[] _delegates;
        public event Action OnceAction;

        public Return[] InvokeAndReturnAll(A a, B b, C c, D d, E e)
        {
            var self = ((IEasyFunc<Return>) this);
            self.BaseDelegates ??= BaseFunc == null ? Array.Empty<Delegate>() : BaseFunc.GetInvocationList();
            _delegates ??= Func == null ? Array.Empty<Delegate>() : Func.GetInvocationList();

            var onceAction = OnceAction;
            OnceAction = null;
            var baseFuncs = self.BaseDelegates;
            var funcs = _delegates;
            var results = new Return[baseFuncs.Length+funcs.Length];
            for ( int i = 0; i < baseFuncs.Length; i++ )
                results[i] = ( (Func<Return>) baseFuncs[i] )();
            for ( int i = 0; i < funcs.Length; i++ )
                results[baseFuncs.Length+i] = ( (Func<A,B,C,D,E,Return>) funcs[i] )(a,b,c,d,e);

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

        public IUnRegisterHandle Register(Func<A,B,C,D,E,Return> func)
        {
            _delegates = null;
            Func += func;
            return new UnRegisterHandle(this,() => Func -= func);
        }

        public void UnRegister(Func<A,B,C,D,E,Return> act)
        {
            _delegates = null;
            Func -= act;
        }

        Action IEDelegate.GetAndClearOnceAction()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            return onceAction;
        }
        public void Clear()
        {
            _delegates = null;
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

    public static class EventExtension
    {
        public static IUnRegisterHandle OnlyPlayOnce(this IUnRegisterHandle self)
        {
            self.Delegate.OnceAction += self.UnRegister;
            return self;
        }

        public static IUnRegisterHandle UnRegisterOnInvoke(this IUnRegisterHandle self, IEasyEvent easyEvent)
        {
            easyEvent.Register(self.UnRegister).OnlyPlayOnce();
            return self;
        }

        public static IUnRegisterHandle Register(this IEasyEvent self, Action action)=>self.Register(action);
        public static IUnRegisterHandle Register<Return>(this IEasyFunc<Return> self,Func<Return> func)=> self.Register(func);

        public static void UnRegister(this IEasyEvent self, Action action)=>self.UnRegister(action);
        public static void UnRegister<Return>(this IEasyFunc<Return> self,Func<Return> func)=> self.UnRegister(func);
        public static void BaseInvoke(this IEasyEvent self)=>self.BaseInvoke();
        public static TResult BaseInvoke<TResult>(this IEasyFunc<TResult> self)=>self.BaseInvoke();
        public static TResult[] BaseInvokeAndReturnAll<TResult>(this IEasyFunc<TResult> self)=>self.BaseInvokeAndReturnAll();
        
        public static void AfterInvoke(this IEDelegate self, Action action)=>self.OnceAction += action;
        public static IUnRegisterHandle AfterInvoke(this IUnRegisterHandle self, Action action)
        {
            self.Delegate.OnceAction += action;
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
        public static IUnRegisterHandle InvokeAndRegister<A,R>(this EasyFunc<A,R> self, Func<A, R> func, A a, out R result)
        {
            result = func(a);
            return self.Register(func);
        }
        public static IUnRegisterHandle InvokeAndRegister<A, B,R>(this EasyFunc<A, B,R> self, Func<A, B, R> func, A a, B b, out R result)
        {
            result = func(a, b);
            return self.Register(func);
        }
        public static IUnRegisterHandle InvokeAndRegister<A, B, C,R>(this EasyFunc<A, B, C,R> self, Func<A, B, C, R> func, A a, B b, C c, out R result)
        {
            result = func(a, b, c);
            return self.Register(func);
        }
        public static IUnRegisterHandle InvokeAndRegister<A, B, C, D,R>(this EasyFunc<A, B, C, D,R> self, Func<A, B, C, D, R> func, A a, B b, C c, D d, out R result)
        {
            result = func(a, b, c, d);
            return self.Register(func);
        }
        public static IUnRegisterHandle InvokeAndRegister<A, B, C, D, E,R>(this EasyFunc<A, B, C, D, E,R> self, Func<A, B, C, D, E, R> func, A a, B b, C c, D d, E e, out R result)
        {
            result = func(a, b, c, d, e);
            return self.Register(func);
        }
    }
}