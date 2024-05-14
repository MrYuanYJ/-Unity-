using System;
using System.Collections.Generic;

namespace EasyFramework.EventKit
{ 
    public interface IEnumDelegate
    {
        public static void Fill<K, V>(Dictionary<K, V> self) where K : Enum where V : new()
        {
            foreach (K key in Enum.GetValues(typeof(K)))
                self[key] = new V();
        }
        public static void Clear<K,TDelegate>(Dictionary<K, TDelegate> self) where TDelegate: IEDelegate
        {
            foreach (var value in self.Values)
                value.Clear();
        }
        public static void Clear<K,K2,TDelegate>(Dictionary<K, Dictionary<K2,TDelegate>> self) where TDelegate: IEDelegate
        {
            foreach (var dic in self.Values)
            foreach (var value in dic.Values)
                value.Clear();
        }
    }
    public class EnumEvent<State>: IEnumDelegate where State : Enum
    {
        private readonly Dictionary<State, EasyEvent> _actionDic = new();

        public EnumEvent() => IEnumDelegate.Fill(_actionDic);
        public void Invoke(State state)=>_actionDic[state].Invoke();
        public void InvokeAll()
        {
            foreach (var easyEvent in _actionDic.Values)
            {
                easyEvent.Invoke();
            }
        } 
        public IUnRegisterHandle Register(State state, Action action)=> _actionDic[state].Register(action);
        public void UnRegister(State state, Action action)=>_actionDic[state].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(_actionDic);
    }

    public class EnumEvent<State, A>: IEnumDelegate where State : Enum
    {
        private readonly Dictionary<State, EasyEvent<A>> _actionDic = new();

        public EnumEvent() => IEnumDelegate.Fill(_actionDic);
        public void Invoke(State state, A a)=> _actionDic[state]?.Invoke(a);
        public void InvokeAll(A a)
        {
            foreach (var easyEvent in _actionDic.Values)
            {
                easyEvent?.Invoke(a);
            }
        }
        public IUnRegisterHandle Register(State state, Action action)=> _actionDic[state].Register(action);
        public IUnRegisterHandle Register(State state, Action<A> action)=>_actionDic[state].Register(action);
        public void UnRegister(State state, Action action)=>_actionDic[state].UnRegister(action);
        public void UnRegister(State state, Action<A> action)=>_actionDic[state].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(_actionDic);
    }

    public class EnumEvent<State, A, B>: IEnumDelegate where State : Enum
    {
        private readonly Dictionary<State, EasyEvent<A, B>> _actionDic = new();

        public EnumEvent() => IEnumDelegate.Fill(_actionDic);
        public void Invoke(State state, A a, B b)=>_actionDic[state]?.Invoke(a, b);
        public void InvokeAll(A a,B b)
        {
            foreach (var easyEvent in _actionDic.Values)
            {
                easyEvent?.Invoke(a,b);
            }
        }
        public IUnRegisterHandle Register(State state, Action action)=> _actionDic[state].Register(action);
        public IUnRegisterHandle Register(State state, Action<A, B> action)=>_actionDic[state].Register(action);
        public void UnRegister(State state, Action action)=>_actionDic[state].UnRegister(action);
        public void UnRegister(State state, Action<A, B> action)=>_actionDic[state].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(_actionDic);
    }

    public class EnumEvent<State, A, B, C>: IEnumDelegate where State : Enum
    {
        private readonly Dictionary<State, EasyEvent<A, B, C>> _actionDic = new();

        public EnumEvent() => IEnumDelegate.Fill(_actionDic);
        public void Invoke(State state, A a, B b, C c)=>_actionDic[state]?.Invoke(a, b, c);
        public void InvokeAll(A a,B b,C c)
        {
            foreach (var easyEvent in _actionDic.Values)
            {
                easyEvent?.Invoke(a,b,c);
            }
        }
        public IUnRegisterHandle Register(State state, Action action)=> _actionDic[state].Register(action);
        public IUnRegisterHandle Register(State state, Action<A, B, C> action)=>_actionDic[state].Register(action);
        public void UnRegister(State state, Action action)=>_actionDic[state].UnRegister(action);
        public void UnRegister(State state, Action<A, B, C> action)=>_actionDic[state].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(_actionDic);
    }

    public class EnumEvent<State, A, B, C, D>: IEnumDelegate where State : Enum
    {
        private readonly Dictionary<State, EasyEvent<A, B, C, D>> _actionDic = new();

        public EnumEvent() => IEnumDelegate.Fill(_actionDic);
        public void Invoke(State state, A a, B b, C c, D d)=>_actionDic[state]?.Invoke(a, b, c, d);
        public void InvokeAll(A a,B b,C c,D d)
        {
            foreach (var easyEvent in _actionDic.Values)
            {
                easyEvent?.Invoke(a,b,c,d);
            }
        }
        public IUnRegisterHandle Register(State state, Action action)=> _actionDic[state].Register(action);
        public IUnRegisterHandle Register(State state, Action<A, B, C, D> action)=>_actionDic[state].Register(action);
        public void UnRegister(State state, Action action)=>_actionDic[state].UnRegister(action);
        public void UnRegister(State state, Action<A, B, C, D> action)=>_actionDic[state].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(_actionDic);
    }

    public class EnumEvent<State, A, B, C, D, E>: IEnumDelegate where State : Enum
    {
        private readonly Dictionary<State, EasyEvent<A, B, C, D, E>> _actionDic = new();

        public EnumEvent() => IEnumDelegate.Fill(_actionDic);
        public void Invoke(State state, A a, B b, C c, D d, E e)=>_actionDic[state]?.Invoke(a, b, c, d, e);
        public void InvokeAll(A a,B b,C c,D d,E e)
        {
            foreach (var easyEvent in _actionDic.Values)
            {
                easyEvent?.Invoke(a,b,c,d,e);
            }
        }
        public IUnRegisterHandle Register(State state, Action action)=> _actionDic[state].Register(action);
        public IUnRegisterHandle Register(State state, Action<A, B, C, D, E> action)=>_actionDic[state].Register(action);
        public void UnRegister(State state, Action action)=>_actionDic[state].UnRegister(action);
        public void UnRegister(State state, Action<A, B, C, D, E> action)=>_actionDic[state].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(_actionDic);
    }


    public class EnumFunc<State, Return>: IEnumDelegate where State : Enum
    {
        private readonly Dictionary<State, EasyFunc<Return>> _funcDic = new();

        public EnumFunc() => IEnumDelegate.Fill(_funcDic);
        public Results<Return> InvokeAndReturnAll(State state)=>_funcDic[state].InvokeAndReturnAll();
        public Results<Return> InvokeAndReturnAll()
        {
            var results = new List<Results<Return>>();
            foreach (var easyEvent in _funcDic.Values)
            {
                results.Add(easyEvent.InvokeAndReturnAll());
            }

            return new Results<Return>(results.ToArray());
        }
        public Return Invoke(State state)=> _funcDic[state].Invoke();
        public IUnRegisterHandle Register(State state, Func<Return> action)=>_funcDic[state].Register(action);
        public void UnRegister(State state, Func<Return> action)=>_funcDic[state].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(_funcDic);
    }

    public class EnumFunc<State, Return, A>: IEnumDelegate where State : Enum
    {
        private readonly Dictionary<State, EasyFunc<Return, A>> _funcDic = new();

        public EnumFunc() => IEnumDelegate.Fill(_funcDic);
        public Results<Return> InvokeAndReturnAll(State state, A a)=> _funcDic[state].InvokeAndReturnAll(a);
        public Results<Return> InvokeAndReturnAll(A a)
        {
            var results = new List<Results<Return>>();
            foreach (var easyEvent in _funcDic.Values)
            {
                results.Add(easyEvent.InvokeAndReturnAll(a));
            }

            return new Results<Return>(results.ToArray());
        }
        public Return Invoke(State state, A a)=> _funcDic[state].Invoke(a);
        public IUnRegisterHandle Register(State state, Func<Return> action)=>_funcDic[state].Register(action);
        public IUnRegisterHandle Register(State state, Func<A, Return> action)=>_funcDic[state].Register(action);
        public void UnRegister(State state, Func<Return> action)=>_funcDic[state].UnRegister(action);
        public void UnRegister(State state, Func<A, Return> action) { _funcDic[state].UnRegister(action); }
        public void Clear() => IEnumDelegate.Clear(_funcDic);
    }

    public class EnumFunc<State, Return, A, B>: IEnumDelegate where State : Enum
    {
        private readonly Dictionary<State, EasyFunc<Return, A, B>> _funcDic = new();

        public EnumFunc() => IEnumDelegate.Fill(_funcDic);
        public Results<Return> InvokeAndReturnAll(State state, A a, B b)=> _funcDic[state].InvokeAndReturnAll(a, b);
        public Results<Return> InvokeAndReturnAll(A a, B b)
        {
            var results = new List<Results<Return>>();
            foreach (var easyEvent in _funcDic.Values)
            {
                results.Add(easyEvent.InvokeAndReturnAll(a,b));
            }

            return new Results<Return>(results.ToArray());
        }
        public Return Invoke(State state, A a, B b)=> _funcDic[state].Invoke(a, b);
        public IUnRegisterHandle Register(State state, Func<Return> action)=>_funcDic[state].Register(action);
        public IUnRegisterHandle Register(State state, Func<A, B, Return> action)=>_funcDic[state].Register(action);
        public void UnRegister(State state, Func<Return> action)=>_funcDic[state].UnRegister(action);
        public void UnRegister(State state, Func<A, B, Return> action) { _funcDic[state].UnRegister(action); }
        public void Clear() => IEnumDelegate.Clear(_funcDic);
    }

    public class EnumFunc<State, Return, A, B, C>: IEnumDelegate where State : Enum
    {
        private readonly Dictionary<State, EasyFunc<Return, A, B, C>> _funcDic = new();
        
        public EnumFunc() => IEnumDelegate.Fill(_funcDic);
        public Results<Return> InvokeAndReturnAll(State state, A a, B b, C c)=> _funcDic[state].InvokeAndReturnAll(a, b, c);
        public Results<Return> InvokeAndReturnAll(A a, B b, C c)
        {
            var results = new List<Results<Return>>();
            foreach (var easyEvent in _funcDic.Values)
            {
                results.Add(easyEvent.InvokeAndReturnAll(a,b,c));
            }

            return new Results<Return>(results.ToArray());
        }
        public Return Invoke(State state, A a, B b, C c)=> _funcDic[state].Invoke(a, b, c);
        public IUnRegisterHandle Register(State state, Func<Return> action)=>_funcDic[state].Register(action);
        public IUnRegisterHandle Register(State state, Func<A, B, C, Return> action)=>_funcDic[state].Register(action);
        public void UnRegister(State state, Func<Return> action)=>_funcDic[state].UnRegister(action);
        public void UnRegister(State state, Func<A, B, C, Return> action) { _funcDic[state].UnRegister(action); }
        public void Clear() => IEnumDelegate.Clear(_funcDic);
    }

    public class EnumFunc<State, Return, A, B, C, D>: IEnumDelegate where State : Enum
    {
        private readonly Dictionary<State, EasyFunc<Return, A, B, C, D>> _funcDic = new();
        
        public EnumFunc() => IEnumDelegate.Fill(_funcDic);
        public Results<Return> InvokeAndReturnAll(State state, A a, B b, C c, D d)=> _funcDic[state].InvokeAndReturnAll(a, b, c, d);
        public Results<Return> InvokeAndReturnAll(A a, B b, C c, D d)
        {
            var results = new List<Results<Return>>();
            foreach (var easyEvent in _funcDic.Values)
            {
                results.Add(easyEvent.InvokeAndReturnAll(a,b,c,d));
            }

            return new Results<Return>(results.ToArray());
        }
        public Return Invoke(State state, A a, B b, C c, D d)=> _funcDic[state].Invoke(a, b, c, d);
        public IUnRegisterHandle Register(State state, Func<Return> action)=>_funcDic[state].Register(action);
        public IUnRegisterHandle Register(State state, Func<A, B, C, D, Return> action)=>_funcDic[state].Register(action);
        public void UnRegister(State state, Func<Return> action)=>_funcDic[state].UnRegister(action);
        public void UnRegister(State state, Func<A, B, C, D, Return> action) { _funcDic[state].UnRegister(action); }
        public void Clear() => IEnumDelegate.Clear(_funcDic);
    }

    public class EnumFunc<State, Return, A, B, C, D, E>: IEnumDelegate where State : Enum
    {
        private readonly Dictionary<State, EasyFunc<Return, A, B, C, D, E>> _funcDic = new();
        
        public EnumFunc() => IEnumDelegate.Fill(_funcDic);
        public Results<Return> InvokeAndReturnAll(State state, A a, B b, C c, D d, E e)=> _funcDic[state].InvokeAndReturnAll(a, b, c, d, e);
        public Results<Return> InvokeAndReturnAll(A a, B b, C c, D d, E e)
        {
            var results = new List<Results<Return>>();
            foreach (var easyEvent in _funcDic.Values)
            {
                results.Add(easyEvent.InvokeAndReturnAll(a,b,c,d,e));
            }

            return new Results<Return>(results.ToArray());
        }
        public Return Invoke(State state, A a, B b, C c, D d, E e)=> _funcDic[state].Invoke(a, b, c, d, e);
        public IUnRegisterHandle Register(State state, Func<Return> action)=>_funcDic[state].Register(action);
        public IUnRegisterHandle Register(State state, Func<A, B, C, D, E, Return> action)=>_funcDic[state].Register(action);
        public void UnRegister(State state, Func<Return> action)=>_funcDic[state].UnRegister(action);
        public void UnRegister(State state, Func<A, B, C, D, E, Return> action) { _funcDic[state].UnRegister(action); }
        public void Clear() => IEnumDelegate.Clear(_funcDic);
    }


    public class DEnumEvent<State, LifeType>: IEnumDelegate where State : Enum where LifeType : Enum
    {
        private readonly Dictionary<State, Dictionary<LifeType, EasyEvent>> _actionDic = new();

        public DEnumEvent()
        {
            IEnumDelegate.Fill(_actionDic);
            foreach (var value in _actionDic.Values)
                IEnumDelegate.Fill(value);
        }
        public void Invoke(State state, LifeType lifeType)=>_actionDic[state][lifeType].Invoke();

        public void InvokeAll()
        {
            foreach (var dic in _actionDic.Values)
            {
                foreach (var easyEvent in dic.Values) { easyEvent.Invoke(); }
            }
        }

        public IUnRegisterHandle Register(State state, LifeType lifeType, Action action)=>_actionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action action)=>_actionDic[state][lifeType].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(_actionDic);
    }

    public class DEnumEvent<State, LifeType, A>: IEnumDelegate where State : Enum where LifeType : Enum
    {
        private readonly Dictionary<State, Dictionary<LifeType, EasyEvent<A>>> _actionDic = new();

        public DEnumEvent()
        {
            IEnumDelegate.Fill(_actionDic);
            foreach (var value in _actionDic.Values)
                IEnumDelegate.Fill(value);
        }
        public void Invoke(State state, LifeType lifeType, A a)=>_actionDic[state][lifeType]?.Invoke(a);

        public void InvokeAll(A a)
        {
            foreach (var dic in _actionDic.Values)
            {
                foreach (var easyEvent in dic.Values) { easyEvent?.Invoke(a); }
            }
        }
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action action)=>_actionDic[state][lifeType].Register(action);
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action<A> action)=>_actionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action action)=>_actionDic[state][lifeType].UnRegister(action);
        public void UnRegister(State state, LifeType lifeType, Action<A> action)=>_actionDic[state][lifeType].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(_actionDic);
    }

    public class DEnumEvent<State, LifeType, A, B>: IEnumDelegate where State : Enum where LifeType : Enum
    {
        private readonly Dictionary<State, Dictionary<LifeType, EasyEvent<A, B>>> _actionDic = new();

        public DEnumEvent()
        {
            IEnumDelegate.Fill(_actionDic);
            foreach (var value in _actionDic.Values)
                IEnumDelegate.Fill(value);
        }
        public void Invoke(State state, LifeType lifeType, A a, B b)=>_actionDic[state][lifeType]?.Invoke(a, b);

        public void InvokeAll(A a, B b)
        {
            foreach (var dic in _actionDic.Values)
            {
                foreach (var easyEvent in dic.Values) { easyEvent?.Invoke(a,b); }
            }
        }
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action action)=>_actionDic[state][lifeType].Register(action);
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action<A, B> action)=>_actionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action action)=>_actionDic[state][lifeType].UnRegister(action);
        public void UnRegister(State state, LifeType lifeType, Action<A, B> action)=>_actionDic[state][lifeType].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(_actionDic);
    }

    public class DEnumEvent<State, LifeType, A, B, C>: IEnumDelegate where State : Enum where LifeType : Enum
    {
        private readonly Dictionary<State, Dictionary<LifeType, EasyEvent<A, B, C>>> _actionDic = new();

        public DEnumEvent()
        {
            IEnumDelegate.Fill(_actionDic);
            foreach (var value in _actionDic.Values)
                IEnumDelegate.Fill(value);
        }
        public void Invoke(State state, LifeType lifeType, A a, B b, C c)=>_actionDic[state][lifeType]?.Invoke(a, b, c);

        public void InvokeAll(A a, B b, C c)
        {
            foreach (var dic in _actionDic.Values)
            {
                foreach (var easyEvent in dic.Values) { easyEvent?.Invoke(a,b,c); }
            }
        }
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action action)=>_actionDic[state][lifeType].Register(action);
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action<A, B, C> action)=>_actionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action action)=>_actionDic[state][lifeType].UnRegister(action);
        public void UnRegister(State state, LifeType lifeType, Action<A, B, C> action)=>_actionDic[state][lifeType].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(_actionDic);
    }

    public class DEnumEvent<State, LifeType, A, B, C, D>: IEnumDelegate where State : Enum where LifeType : Enum
    {
        private readonly Dictionary<State, Dictionary<LifeType, EasyEvent<A, B, C, D>>> _actionDic = new();
        
        public DEnumEvent()
        {
            IEnumDelegate.Fill(_actionDic);
            foreach (var value in _actionDic.Values)
                IEnumDelegate.Fill(value);
        }
        public void Invoke(State state, LifeType lifeType, A a, B b, C c, D d)=>_actionDic[state][lifeType]?.Invoke(a, b, c, d);

        public void InvokeAll(A a, B b, C c, D d)
        {
            foreach (var dic in _actionDic.Values)
            {
                foreach (var easyEvent in dic.Values) { easyEvent?.Invoke(a,b,c,d); }
            }
        }
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action action)=>_actionDic[state][lifeType].Register(action);
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action<A, B, C, D> action)=>_actionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action action)=>_actionDic[state][lifeType].UnRegister(action);
        public void UnRegister(State state, LifeType lifeType, Action<A, B, C, D> action)=>_actionDic[state][lifeType].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(_actionDic);
    }

    public class DEnumEvent<State, LifeType, A, B, C, D, E>: IEnumDelegate where State : Enum where LifeType : Enum
    {
        private readonly Dictionary<State, Dictionary<LifeType, EasyEvent<A, B, C, D, E>>> _actionDic = new();
        
        public DEnumEvent() 
        {
            IEnumDelegate.Fill(_actionDic);
            foreach (var value in _actionDic.Values)
                IEnumDelegate.Fill(value);
        }
        public void Invoke(State state, LifeType lifeType, A a, B b, C c, D d, E e)=>_actionDic[state][lifeType]?.Invoke(a, b, c, d, e);

        public void InvokeAll(A a, B b, C c, D d, E e)
        {
            foreach (var dic in _actionDic.Values)
            {
                foreach (var easyEvent in dic.Values) { easyEvent?.Invoke(a,b,c,d,e); }
            }
        }
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action action)=>_actionDic[state][lifeType].Register(action);
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action<A, B, C, D, E> action)=>_actionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action action)=>_actionDic[state][lifeType].UnRegister(action);
        public void UnRegister(State state, LifeType lifeType, Action<A, B, C, D, E> action)=>_actionDic[state][lifeType].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(_actionDic);
    }
}
