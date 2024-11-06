using System;

namespace EasyFramework
{
    public interface IEDelegate
    {
        Action OnceAction { get; set; }
    
        Action GetAndClearOnceAction()
        {
            var onceAction = OnceAction;
            OnceAction = null;
            return onceAction;
        }
        void Clear();
        bool IsNull();
        internal void UnRegister(Delegate @delegate);
    }
    public interface IUnRegisterHandle
    {
        IEDelegate Delegate { get; }
        void UnRegister();
    }
    public readonly struct UnRegisterHandle: IUnRegisterHandle
    {
        public UnRegisterHandle(IEDelegate del,Delegate registerDelegate)
        {
            Delegate = del;
            _registerDelegate = registerDelegate;
        }

        private readonly Delegate _registerDelegate;
        public IEDelegate Delegate { get;}

        public void UnRegister()
        {
            Delegate.UnRegister(_registerDelegate);
        }
    }
    #region Event

    public interface IEasyEvent: IEDelegate
    {
        Action BaseAction { get; set; }

        IUnRegisterHandle Register(Action action)
        {
            BaseAction += action;
            return new UnRegisterHandle(this,action);
        }
        void UnRegister(Action action)=> BaseAction -= action;
        void BaseInvoke()
        {
            var onceAction = GetAndClearOnceAction();
            BaseAction?.Invoke();
            onceAction?.Invoke();
        }
    }

    public class EasyEvent : IEasyEvent
    {
        private Action _baseAction;
        private Action _onceAction;
        Action IEasyEvent.BaseAction { get => _baseAction; set => _baseAction = value; }
        Action IEDelegate.OnceAction { get => _onceAction; set => _onceAction = value; }

        public void Invoke()
        {
            var onceAction = _onceAction;
            _onceAction = null;
            _baseAction?.Invoke();
            onceAction?.Invoke();
        }

        public IUnRegisterHandle Register(Action action)
        {
            _baseAction += action;
            return new UnRegisterHandle(this,action);
        }

        public void UnRegister(Action action) => _baseAction -= action;


        public void Clear()
        {
            _baseAction = null;
            _onceAction = null;
        }

        public bool IsNull()
        {
            return _baseAction == null;
        }

        void IEDelegate.UnRegister(Delegate @delegate)
        {
            if (@delegate is Action action)
                _baseAction -= action;
        }
    }

    public class EasyEvent<A> : IEasyEvent
    {
        private Action<A> _action;
        private Action _baseAction;
        private Action _onceAction;
        Action IEDelegate.OnceAction { get => _onceAction; set => _onceAction = value; }
        Action IEasyEvent.BaseAction { get => _baseAction; set => _baseAction = value; }
        public void Invoke(A a)
        {
            var onceAction = _onceAction;
            _onceAction = null;
            _baseAction?.Invoke();
            _action?.Invoke(a);
            onceAction?.Invoke();
        }
        
        public IUnRegisterHandle Register(Action<A> action)
        {
            _action += action;
            return new UnRegisterHandle(this,action);
        }

        public void UnRegister(Action<A> action) => _action -= action;


        public void Clear()
        {
            _action = null;
            _baseAction = null;
            _onceAction = null;
        }
        public bool IsNull()
        {
            return _action == null&&_baseAction == null;
        }

        void IEDelegate.UnRegister(Delegate @delegate)
        {
            if (@delegate is Action<A> action)
                _action -= action;
            else if (@delegate is Action baseAction)
                _baseAction -= baseAction;
        }
    }

    public class EasyEvent<A, B> : IEasyEvent
    {
        private Action<A, B> _action;
        private Action _baseAction;
        private Action _onceAction;
        Action IEDelegate.OnceAction { get => _onceAction; set => _onceAction = value; }
        Action IEasyEvent.BaseAction { get => _baseAction; set => _baseAction = value; }

        public void Invoke(A a, B b)
        {
            var onceAction = _onceAction;
            _onceAction = null;
            _baseAction?.Invoke();
            _action?.Invoke(a, b);
            onceAction?.Invoke();
        }
        public IUnRegisterHandle Register(Action<A, B> action)
        {
            _action += action;
            return new UnRegisterHandle(this,action);
        }
        public void UnRegister(Action<A, B> act) => _action -= act;
        public void Clear()
        {
            _action = null;
            _baseAction = null;
            _onceAction = null;
        }
        public bool IsNull()
        {
            return _action == null&&_baseAction == null;
        }

        void IEDelegate.UnRegister(Delegate @delegate)
        {
            if (@delegate is Action<A, B> action)
                _action -= action;
            else if (@delegate is Action baseAction)
                _baseAction -= baseAction;
        }
    }

    public class EasyEvent<A, B, C> : IEasyEvent
    {
        private Action<A, B, C> _action;
        private Action _baseAction;
        private Action _onceAction;
        Action IEDelegate.OnceAction { get => _onceAction; set => _onceAction = value; }
        Action IEasyEvent.BaseAction { get => _baseAction; set => _baseAction = value; }

        public void Invoke(A a, B b, C c)
        {
            var onceAction = _onceAction;
            _onceAction = null;
            _baseAction?.Invoke();
            _action?.Invoke(a, b, c);
            onceAction?.Invoke();
        }
        public IUnRegisterHandle Register(Action<A, B, C> action)
        {
            _action += action;
            return new UnRegisterHandle(this,action);
        }
        public void UnRegister(Action<A, B, C> act) => _action -= act;
        public void Clear()
        {
            _action = null;
            _baseAction = null;
            _onceAction = null;
        }
        public bool IsNull()
        {
            return _action == null && _baseAction == null;
        }
        void IEDelegate.UnRegister(Delegate @delegate)
        {
            if (@delegate is Action<A, B, C> action)
                _action -= action;
            else if (@delegate is Action baseAction)
                _baseAction -= baseAction;
        }
    }

    public class EasyEvent<A, B, C, D> : IEasyEvent
    {
        private Action<A, B, C, D> _action;
        private Action _baseAction;
        private Action _onceAction;
        Action IEDelegate.OnceAction { get => _onceAction; set => _onceAction = value; }
        Action IEasyEvent.BaseAction { get => _baseAction; set => _baseAction = value; }

        public void Invoke(A a, B b, C c, D d)
        {
            var onceAction = _onceAction;
            _onceAction = null;
            _baseAction?.Invoke();
            _action?.Invoke(a, b, c, d);
            onceAction?.Invoke();
        }
        public IUnRegisterHandle Register(Action<A, B, C, D> action)
        {
            _action += action;
            return new UnRegisterHandle(this,action);
        }
        public void UnRegister(Action<A, B, C, D> act) => _action -= act;
        public void Clear()
        {
            _action = null;
            _baseAction = null;
            _onceAction = null;
        }
        public bool IsNull()
        {
            return _action == null && _baseAction == null;
        }

        void IEDelegate.UnRegister(Delegate @delegate)
        {
            if (@delegate is Action<A, B, C, D> action)
                _action -= action;
            else if (@delegate is Action baseAction)
                _baseAction -= baseAction;
        }
    }

    public class EasyEvent<A, B, C, D, E> : IEasyEvent
    {
        private Action<A, B, C, D, E> _action;
        private Action _baseAction;
        private Action _onceAction;
        Action IEDelegate.OnceAction { get => _onceAction; set => _onceAction = value; }
        Action IEasyEvent.BaseAction { get => _baseAction; set => _baseAction = value; }

        public void Invoke(A a, B b, C c, D d, E e)
        {
            var onceAction = _onceAction;
            _onceAction = null;
            _baseAction?.Invoke();
            _action?.Invoke(a, b, c, d, e);
            onceAction?.Invoke();
        }
        public IUnRegisterHandle Register(Action<A, B, C, D, E> action)
        {            
            _action += action;
            return new UnRegisterHandle(this,action);
        }
        public void UnRegister(Action<A, B, C, D, E> act) => _action -= act;
        public void Clear()
        {
            _action = null;
            _baseAction = null;
            _onceAction = null;
        }
        public bool IsNull()
        {
            if (_action == null && _baseAction == null)
                return true;
            return false;
        }

        void IEDelegate.UnRegister(Delegate @delegate)
        {
            if (@delegate is Action<A, B, C, D, E> action)
                _action -= action;
            else if (@delegate is Action baseAction)
                _baseAction -= baseAction;
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
        Func<Return> BaseFunc { get; protected set; }
        Delegate[] BaseDelegates { get; set; }
        Delegate[] GetBaseFuncInvocationList()=>BaseFunc == null ? Array.Empty<Delegate>() : BaseFunc.GetInvocationList();
        IUnRegisterHandle Register(Func<Return> func)
        {
            BaseDelegates = null;
            BaseFunc += func;
            return new UnRegisterHandle(this,func);
        }
        void UnRegister(Func<Return> func)
        {
            BaseDelegates = null;
            BaseFunc -= func;
        }

        Return BaseInvoke()
        {
            var onceAction = GetAndClearOnceAction();
            Return result = BaseFunc == null ? default : BaseFunc();
            onceAction?.Invoke();
            return result;
        }
        Return[] BaseInvokeAndReturnAll()
        {
            BaseDelegates ??= GetBaseFuncInvocationList();

            var onceAction = GetAndClearOnceAction();
            var delegateArray = BaseDelegates;
            var results = new Return[delegateArray.Length];
            for (int i = 0; i < results.Length; i++)
                    results[i] = ((Func<Return>)delegateArray[i])();
            
            onceAction?.Invoke();
            return results;
        }
        public int BaseInvokeAndReturnAllNonAlloc(Return[] results)
        {
            BaseDelegates ??= GetBaseFuncInvocationList();

            var onceAction = GetAndClearOnceAction();
            var delegateArray = BaseDelegates;
            var count = MathF.Max(results.Length, delegateArray.Length);
            for (int i = 0; i < count; i++)
            {
                Return result = default;
                if (delegateArray.Length > i)
                    result = ((Func<Return>) delegateArray[i])();
                if (results.Length > i)
                    results[i] = result;
            }
            
            onceAction?.Invoke();
            return delegateArray.Length;
        }
    }
    public class EasyFunc<Return> : IEasyFunc<Return>
    {
        private Func<Return> _baseFunc;
        private Action _onceAction;
        Action IEDelegate.OnceAction { get => _onceAction; set => _onceAction = value; }
        Func<Return> IEasyFunc<Return>.BaseFunc { get => _baseFunc; set => _baseFunc = value; }
        Delegate[] IEasyFunc<Return>.BaseDelegates { get; set; }

        public Return Invoke()
        {
            var onceAction = _onceAction;
            _onceAction = null;
            Return result = _baseFunc != null ? _baseFunc() : default;
            onceAction?.Invoke();
            return result;
        }

        public Return[] InvokeAndReturnAll()
        {
            ((IEasyFunc<Return>)this).BaseDelegates ??= _baseFunc == null ? Array.Empty<Delegate>() : _baseFunc.GetInvocationList();

            var onceAction = _onceAction;
            _onceAction = null;
            var delegateArray = ((IEasyFunc<Return>)this).BaseDelegates;
            var results = new Return[delegateArray.Length];
            for (int i = 0; i < results.Length; i++)
                    results[i] = ((Func<Return>)delegateArray[i])();
            
            onceAction?.Invoke();
            return results;
        }
        public int InvokeAndReturnAllNonAlloc(Return[] results)
        {
            ((IEasyFunc<Return>)this).BaseDelegates ??= _baseFunc == null ? Array.Empty<Delegate>() : _baseFunc.GetInvocationList();
            
            var onceAction = _onceAction;
            _onceAction = null;
            var delegateArray = ((IEasyFunc<Return>)this).BaseDelegates;
            var count = MathF.Max(results.Length, delegateArray.Length);
            for (int i = 0; i < count; i++)
            {
                Return result = default;
                if (delegateArray.Length > i)
                    result = ((Func<Return>) delegateArray[i])();
                if (results.Length > i)
                    results[i] = result;
            }
            
            onceAction?.Invoke();
            return delegateArray.Length;
        }

        public IUnRegisterHandle Register(Func<Return> func)
        {
            ((IEasyFunc<Return>)this).BaseDelegates = null;
            _baseFunc += func;
            return new UnRegisterHandle(this,func);
        }

        public void UnRegister(Func<Return> act)
        {
            ((IEasyFunc<Return>)this).BaseDelegates = null;
            _baseFunc -= act;
        }
        public void Clear()
        {
            ((IEasyFunc<Return>)this).BaseDelegates = null;
            _baseFunc = null;
            _onceAction = null;
        }
        public bool IsNull()
        {
            return _baseFunc == null;
        }

        void IEDelegate.UnRegister(Delegate @delegate)
        {
            if (@delegate is Func<Return> func)
                _baseFunc -= func;
        }
    }
    public class EasyFunc<A,Return> : IEasyFunc<Return>
    {
        private Func<A, Return> _func;
        private Func<Return> _baseFunc;
        private Action _onceAction;
        private Delegate[] _delegates;
        Action IEDelegate.OnceAction { get => _onceAction; set => _onceAction = value; }
        Func<Return> IEasyFunc<Return>.BaseFunc { get => _baseFunc; set => _baseFunc = value; }
        Delegate[] IEasyFunc<Return>.BaseDelegates { get; set; }

        public Return[] InvokeAndReturnAll(A a)
        {
            var self = (IEasyFunc<Return>) this;
            self.BaseDelegates ??= _baseFunc == null ? Array.Empty<Delegate>() : _baseFunc.GetInvocationList();
            _delegates ??= _func == null ? Array.Empty<Delegate>() : _func.GetInvocationList();

            var onceAction = _onceAction;
            _onceAction = null;
            var baseFuncs = self.BaseDelegates;
            var funcs = _delegates;
            var results = new Return[baseFuncs.Length + funcs.Length];
            for (int i = 0; i < baseFuncs.Length; i++)
                results[i] = ((Func<Return>) baseFuncs[i])();
            for (int i = 0; i < funcs.Length; i++)
                results[baseFuncs.Length + i] = ((Func<A, Return>) funcs[i])(a);

            onceAction?.Invoke();
            return results;
        }
        public int InvokeAndReturnAllNonAlloc(A a, Return[] results)
        {
            var self = (IEasyFunc<Return>) this;
            self.BaseDelegates ??= _baseFunc == null ? Array.Empty<Delegate>() : _baseFunc.GetInvocationList();
            _delegates ??= _func == null ? Array.Empty<Delegate>() : _func.GetInvocationList();

            var onceAction = _onceAction;
            _onceAction = null;
            var baseFuncs = self.BaseDelegates;
            var funcs = _delegates;
            var count = (int)MathF.Max(results.Length, baseFuncs.Length+funcs.Length);
            for (int i = 0; i < count; i++)
            {
                var result=default(Return);
                if (baseFuncs.Length > i)
                    result = ((Func<Return>) baseFuncs[i])();
                else if (baseFuncs.Length + funcs.Length > i)
                    result = ((Func<A, Return>) funcs[i-baseFuncs.Length])(a);
                if (results.Length > i)
                    results[i] = result;
            }

            onceAction?.Invoke();
            return count;
        }
        public Return Invoke(A a)
        {
            var onceAction = _onceAction;
            _onceAction = null;
            Return result =default;
            if ( _baseFunc != null )
                result = _baseFunc();
            if ( _func != null )
                result = _func(a);
            onceAction?.Invoke();
            return result;
        }
        public IUnRegisterHandle Register(Func<A,Return> func)
        {
            _delegates = null;
            _func += func;
            return new UnRegisterHandle(this,func);
        }

        public void UnRegister(Func<A,Return> act)
        {
            _delegates = null;
            _func -= act;
        }
        public void Clear()
        {
            _delegates = null;
            _func = null;
            _baseFunc = null;
            _onceAction = null;
        }

        public bool IsNull()
        {
            return _func == null && _baseFunc == null;
        }

        void IEDelegate.UnRegister(Delegate @delegate)
        {
            if (@delegate is Func<A, Return> func)
                _func -= func;
            else if (@delegate is Func<Return> baseFunc)
                _baseFunc -= baseFunc;
        }
    }
    public class EasyFunc<A, B,Return> : IEasyFunc<Return>
    {
        private Func<A, B, Return> _func;       
        private Func<Return> _baseFunc;
        private Action _onceAction;
        private Delegate[] _delegates;
        Action IEDelegate.OnceAction { get => _onceAction; set => _onceAction = value; }
        Func<Return> IEasyFunc<Return>.BaseFunc { get => _baseFunc; set => _baseFunc = value; }
        Delegate[] IEasyFunc<Return>.BaseDelegates { get; set; }

        public Return[] InvokeAndReturnAll(A a, B b)
        {
            var self = ((IEasyFunc<Return>) this);
            self.BaseDelegates ??= _baseFunc == null ? Array.Empty<Delegate>() : _baseFunc.GetInvocationList();
            _delegates ??= _func == null ? Array.Empty<Delegate>() : _func.GetInvocationList();

            var onceAction = _onceAction;
            _onceAction = null;
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
        public int InvokeAndReturnAllNonAlloc(A a,B b, Return[] results)
        {
            var self = (IEasyFunc<Return>) this;
            self.BaseDelegates ??= _baseFunc == null ? Array.Empty<Delegate>() : _baseFunc.GetInvocationList();
            _delegates ??= _func == null ? Array.Empty<Delegate>() : _func.GetInvocationList();

            var onceAction = _onceAction;
            _onceAction = null;
            var baseFuncs = self.BaseDelegates;
            var funcs = _delegates;
            var count = (int)MathF.Max(results.Length, baseFuncs.Length+funcs.Length);
            for (int i = 0; i < count; i++)
            {
                var result=default(Return);
                if (baseFuncs.Length > i)
                    result = ((Func<Return>) baseFuncs[i])();
                else if (baseFuncs.Length + funcs.Length > i)
                    result = ((Func<A,B, Return>) funcs[i-baseFuncs.Length])(a,b);
                if (results.Length > i)
                    results[i] = result;
            }

            onceAction?.Invoke();
            return count;
        }
        public Return Invoke(A a, B b)
        {
            var onceAction = _onceAction;
            _onceAction = null;
            Return result =default;
            if ( _baseFunc != null )
                result = _baseFunc();
            if ( _func != null )
                result = _func(a,b);
            onceAction?.Invoke();
            return result;
        }

        public IUnRegisterHandle Register(Func<A,B,Return> func)
        {
            _delegates = null;
            _func += func;
            return new UnRegisterHandle(this,func);
        }

        public void UnRegister(Func<A,B,Return> act)
        {
            _delegates = null;
            _func -= act;
        }
        public void Clear()
        {
            _delegates = null;
            _func = null;
            _baseFunc = null;
            _onceAction = null;
        }
        public bool IsNull()
        {
            return _func == null && _baseFunc == null;
        }

        void IEDelegate.UnRegister(Delegate @delegate)
        {
            if (@delegate is Func<A, B, Return> func)
                _func -= func;
            else if (@delegate is Func<Return> baseFunc)
                _baseFunc -= baseFunc;
        }
    }
    
    public class EasyFunc<A, B, C,Return> : IEasyFunc<Return>
    {
        private Func<A, B, C, Return> _func;
        private Func<Return> _baseFunc;
        private Action _onceAction;
        private Delegate[] _delegates;
        Action IEDelegate.OnceAction { get => _onceAction; set => _onceAction = value; }
        Func<Return> IEasyFunc<Return>.BaseFunc { get => _baseFunc; set => _baseFunc = value; }
        Delegate[] IEasyFunc<Return>.BaseDelegates { get; set; }

        public Return[] InvokeAndReturnAll(A a, B b, C c)
        {
            var self = ((IEasyFunc<Return>) this);
            self.BaseDelegates ??= _baseFunc == null ? Array.Empty<Delegate>() : _baseFunc.GetInvocationList();
            _delegates ??= _func == null ? Array.Empty<Delegate>() : _func.GetInvocationList();

            var onceAction = _onceAction;
            _onceAction = null;
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
        public int InvokeAndReturnAllNonAlloc(A a,B b,C c, Return[] results)
        {
            var self = (IEasyFunc<Return>) this;
            self.BaseDelegates ??= _baseFunc == null ? Array.Empty<Delegate>() : _baseFunc.GetInvocationList();
            _delegates ??= _func == null ? Array.Empty<Delegate>() : _func.GetInvocationList();

            var onceAction = _onceAction;
            _onceAction = null;
            var baseFuncs = self.BaseDelegates;
            var funcs = _delegates;
            var count = (int)MathF.Max(results.Length, baseFuncs.Length+funcs.Length);
            for (int i = 0; i < count; i++)
            {
                var result=default(Return);
                if (baseFuncs.Length > i)
                    result = ((Func<Return>) baseFuncs[i])();
                else if (baseFuncs.Length + funcs.Length > i)
                    result = ((Func<A,B,C, Return>) funcs[i-baseFuncs.Length])(a,b,c);
                if (results.Length > i)
                    results[i] = result;
            }

            onceAction?.Invoke();
            return count;
        }
        public Return Invoke(A a, B b, C c)
        {
            var onceAction = _onceAction;
            _onceAction = null;
            Return result =default;
            if ( _baseFunc != null )
                result = _baseFunc();
            if ( _func != null )
                result = _func(a,b,c);
            onceAction?.Invoke();
            return result;
        }

        public IUnRegisterHandle Register(Func<A,B,C,Return> func)
        {
            _delegates = null;
            _func += func;
            return new UnRegisterHandle(this,func);
        }

        public void UnRegister(Func<A,B,C,Return> act)
        {
            _delegates = null;
            _func -= act;
        }
        public void Clear()
        {
            _delegates = null;
            _func = null;
            _baseFunc = null;
            _onceAction = null;
        }

        public bool IsNull()
        {
            return _func == null && _baseFunc == null;
        }

        void IEDelegate.UnRegister(Delegate @delegate)
        {
            if (@delegate is Func<A, B, C, Return> func)
                _func -= func;
            else if (@delegate is Func<Return> baseFunc)
                _baseFunc -= baseFunc;
        }
    }
    
    public class EasyFunc<A, B, C, D,Return> : IEasyFunc<Return>
    {
        private Func<A, B, C, D, Return> _func;
        private Func<Return> _baseFunc;
        private Action _onceAction;
        private Delegate[] _delegates;
        Action IEDelegate.OnceAction { get => _onceAction; set => _onceAction = value; }
        Func<Return> IEasyFunc<Return>.BaseFunc { get => _baseFunc; set => _baseFunc = value; }
        Delegate[] IEasyFunc<Return>.BaseDelegates { get; set; }

        public Return[] InvokeAndReturnAll(A a, B b, C c, D d)
        {
            var self = ((IEasyFunc<Return>) this);
            self.BaseDelegates ??= _baseFunc == null ? Array.Empty<Delegate>() : _baseFunc.GetInvocationList();
            _delegates ??= _func == null ? Array.Empty<Delegate>() : _func.GetInvocationList();

            var onceAction = _onceAction;
            _onceAction = null;
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
        public int InvokeAndReturnAllNonAlloc(A a,B b,C c,D d, Return[] results)
        {
            var self = (IEasyFunc<Return>) this;
            self.BaseDelegates ??= _baseFunc == null ? Array.Empty<Delegate>() : _baseFunc.GetInvocationList();
            _delegates ??= _func == null ? Array.Empty<Delegate>() : _func.GetInvocationList();

            var onceAction = _onceAction;
            _onceAction = null;
            var baseFuncs = self.BaseDelegates;
            var funcs = _delegates;
            var count = (int)MathF.Max(results.Length, baseFuncs.Length+funcs.Length);
            for (int i = 0; i < count; i++)
            {
                var result=default(Return);
                if (baseFuncs.Length > i)
                    result = ((Func<Return>) baseFuncs[i])();
                else if (baseFuncs.Length + funcs.Length > i)
                    result = ((Func<A,B,C,D, Return>) funcs[i-baseFuncs.Length])(a,b,c,d);
                if (results.Length > i)
                    results[i] = result;
            }

            onceAction?.Invoke();
            return count;
        }
        public Return Invoke(A a, B b, C c, D d)
        {
            var onceAction = _onceAction;
            _onceAction = null;
            Return result =default;
            if ( _baseFunc != null )
                result = _baseFunc();
            if ( _func != null )
                result = _func(a,b,c,d);
            onceAction?.Invoke();
            return result;
        }

        public IUnRegisterHandle Register(Func<A,B,C,D,Return> func)
        {
            _delegates = null;
            _func += func;
            return new UnRegisterHandle(this,func);
        }

        public void UnRegister(Func<A,B,C,D,Return> act)
        {
            _delegates = null;
            _func -= act;
        }
        public void Clear()
        {
            _delegates = null;
            _func = null;
            _baseFunc = null;
            _onceAction = null;
        }
        public bool IsNull()
        {
            return _func == null && _baseFunc == null;
        }

        void IEDelegate.UnRegister(Delegate @delegate)
        {
            if (@delegate is Func<A, B, C, D, Return> func)
                _func -= func;
            else if (@delegate is Func<Return> baseFunc)
                _baseFunc -= baseFunc;
        }
    }
    
    public class EasyFunc<A, B, C, D, E,Return> : IEasyFunc<Return>
    {
        private Func<A, B, C, D, E, Return> _func;
        private Func<Return> _baseFunc;
        private Action _onceAction;
        private Delegate[] _delegates;
        Action IEDelegate.OnceAction { get => _onceAction; set => _onceAction = value; }
        Func<Return> IEasyFunc<Return>.BaseFunc { get => _baseFunc; set => _baseFunc = value; }
        Delegate[] IEasyFunc<Return>.BaseDelegates { get; set; }

        public Return[] InvokeAndReturnAll(A a, B b, C c, D d, E e)
        {
            var self = ((IEasyFunc<Return>) this);
            self.BaseDelegates ??= _baseFunc == null ? Array.Empty<Delegate>() : _baseFunc.GetInvocationList();
            _delegates ??= _func == null ? Array.Empty<Delegate>() : _func.GetInvocationList();

            var onceAction = _onceAction;
            _onceAction = null;
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
        public int InvokeAndReturnAllNonAlloc(A a,B b,C c,D d,E e, Return[] results)
        {
            var self = (IEasyFunc<Return>) this;
            self.BaseDelegates ??= _baseFunc == null ? Array.Empty<Delegate>() : _baseFunc.GetInvocationList();
            _delegates ??= _func == null ? Array.Empty<Delegate>() : _func.GetInvocationList();

            var onceAction = _onceAction;
            _onceAction = null;
            var baseFuncs = self.BaseDelegates;
            var funcs = _delegates;
            var count = (int)MathF.Max(results.Length, baseFuncs.Length+funcs.Length);
            for (int i = 0; i < count; i++)
            {
                var result=default(Return);
                if (baseFuncs.Length > i)
                    result = ((Func<Return>) baseFuncs[i])();
                else if (baseFuncs.Length + funcs.Length > i)
                    result = ((Func<A,B,C,D,E, Return>) funcs[i-baseFuncs.Length])(a,b,c,d,e);
                if (results.Length > i)
                    results[i] = result;
            }

            onceAction?.Invoke();
            return count;
        }
        public Return Invoke(A a, B b, C c, D d, E e)
        {
            var onceAction = _onceAction;
            _onceAction = null;
            Return result =default;
            if ( _baseFunc != null )
                result = _baseFunc();
            if ( _func != null )
                result = _func(a,b,c,d,e);
            onceAction?.Invoke();
            return result;
        }

        public IUnRegisterHandle Register(Func<A,B,C,D,E,Return> func)
        {
            _delegates = null;
            _func += func;
            return new UnRegisterHandle(this,func);
        }

        public void UnRegister(Func<A,B,C,D,E,Return> act)
        {
            _delegates = null;
            _func -= act;
        }
        public void Clear()
        {
            _delegates = null;
            _func = null;
            _baseFunc = null;
            _onceAction = null;
        }

        public bool IsNull()
        {
            return _func == null && _baseFunc == null;
        }

        void IEDelegate.UnRegister(Delegate @delegate)
        {
            if (@delegate is Func<A, B, C, D, E, Return> func)
                _func -= func;
            else if (@delegate is Func<Return> baseFunc)
                _baseFunc -= baseFunc;
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
        public static int BaseInvokeAndReturnAllNonAlloc<TResult>(this IEasyFunc<TResult> self, TResult[] results)=>self.BaseInvokeAndReturnAllNonAlloc(results);
        
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