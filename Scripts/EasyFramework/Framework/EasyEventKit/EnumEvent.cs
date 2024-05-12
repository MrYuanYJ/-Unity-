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
        public Dictionary<State, EasyEvent> ActionDic = new();

        public EnumEvent() => IEnumDelegate.Fill(ActionDic);
        public void Invoke(State state)=>ActionDic[state].Invoke();
        public void InvokeAll()
        {
            foreach (var easyEvent in ActionDic.Values)
            {
                easyEvent.Invoke();
            }
        } 
        public IUnRegisterHandle Register(State state, Action action)=> ActionDic[state].Register(action);
        public void UnRegister(State state, Action action)=>ActionDic[state].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(ActionDic);
    }

    public class EnumEvent<State, A>: IEnumDelegate where State : Enum
    {
        public Dictionary<State, EasyEvent<A>> ActionDic = new();

        public EnumEvent() => IEnumDelegate.Fill(ActionDic);
        public void Invoke(State state, A a)=> ActionDic[state]?.Invoke(a);
        public void InvokeAll(A a)
        {
            foreach (var easyEvent in ActionDic.Values)
            {
                easyEvent?.Invoke(a);
            }
        }
        public IUnRegisterHandle Register(State state, Action action)=> ActionDic[state].Register(action);
        public void UnRegister(State state, Action action)=>ActionDic[state].UnRegister(action);
        public IUnRegisterHandle Register(State state, Action<A> action)=>ActionDic[state].Register(action);
        public void UnRegister(State state, Action<A> action)=>ActionDic[state].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(ActionDic);
    }

    public class EnumEvent<State, A, B>: IEnumDelegate where State : Enum
    {
        public Dictionary<State, EasyEvent<A, B>> ActionDic = new();

        public EnumEvent() => IEnumDelegate.Fill(ActionDic);
        public void Invoke(State state, A a, B b)=>ActionDic[state]?.Invoke(a, b);
        public void InvokeAll(A a,B b)
        {
            foreach (var easyEvent in ActionDic.Values)
            {
                easyEvent?.Invoke(a,b);
            }
        }
        public IUnRegisterHandle Register(State state, Action action)=> ActionDic[state].Register(action);
        public void UnRegister(State state, Action action)=>ActionDic[state].UnRegister(action);
        public IUnRegisterHandle Register(State state, Action<A, B> action)=>ActionDic[state].Register(action);
        public void UnRegister(State state, Action<A, B> action)=>ActionDic[state].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(ActionDic);
    }

    public class EnumEvent<State, A, B, C>: IEnumDelegate where State : Enum
    {
        public Dictionary<State, EasyEvent<A, B, C>> ActionDic = new();

        public EnumEvent() => IEnumDelegate.Fill(ActionDic);
        public void Invoke(State state, A a, B b, C c)=>ActionDic[state]?.Invoke(a, b, c);
        public void InvokeAll(A a,B b,C c)
        {
            foreach (var easyEvent in ActionDic.Values)
            {
                easyEvent?.Invoke(a,b,c);
            }
        }
        public IUnRegisterHandle Register(State state, Action action)=> ActionDic[state].Register(action);
        public void UnRegister(State state, Action action)=>ActionDic[state].UnRegister(action);
        public IUnRegisterHandle Register(State state, Action<A, B, C> action)=>ActionDic[state].Register(action);
        public void UnRegister(State state, Action<A, B, C> action)=>ActionDic[state].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(ActionDic);
    }

    public class EnumEvent<State, A, B, C, D>: IEnumDelegate where State : Enum
    {
        public Dictionary<State, EasyEvent<A, B, C, D>> ActionDic = new();

        public EnumEvent() => IEnumDelegate.Fill(ActionDic);
        public void Invoke(State state, A a, B b, C c, D d)=>ActionDic[state]?.Invoke(a, b, c, d);
        public void InvokeAll(A a,B b,C c,D d)
        {
            foreach (var easyEvent in ActionDic.Values)
            {
                easyEvent?.Invoke(a,b,c,d);
            }
        }
        public IUnRegisterHandle Register(State state, Action action)=> ActionDic[state].Register(action);
        public void UnRegister(State state, Action action)=>ActionDic[state].UnRegister(action);
        public IUnRegisterHandle Register(State state, Action<A, B, C, D> action)=>ActionDic[state].Register(action);
        public void UnRegister(State state, Action<A, B, C, D> action)=>ActionDic[state].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(ActionDic);
    }

    public class EnumEvent<State, A, B, C, D, E>: IEnumDelegate where State : Enum
    {
        public Dictionary<State, EasyEvent<A, B, C, D, E>> ActionDic = new();

        public EnumEvent() => IEnumDelegate.Fill(ActionDic);
        public void Invoke(State state, A a, B b, C c, D d, E e)=>ActionDic[state]?.Invoke(a, b, c, d, e);
        public void InvokeAll(A a,B b,C c,D d,E e)
        {
            foreach (var easyEvent in ActionDic.Values)
            {
                easyEvent?.Invoke(a,b,c,d,e);
            }
        }
        public IUnRegisterHandle Register(State state, Action action)=> ActionDic[state].Register(action);
        public void UnRegister(State state, Action action)=>ActionDic[state].UnRegister(action);
        public IUnRegisterHandle Register(State state, Action<A, B, C, D, E> action)=>ActionDic[state].Register(action);
        public void UnRegister(State state, Action<A, B, C, D, E> action)=>ActionDic[state].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(ActionDic);
    }


    public class EnumFunc<State, Return>: IEnumDelegate where State : Enum
    {
        public Dictionary<State, EasyFunc<Return>> FuncDic = new();

        public EnumFunc() => IEnumDelegate.Fill(FuncDic);
        public Results<Return> InvokeAndReturnAll(State state)=>FuncDic[state].InvokeAndReturnAll();
        public Results<Return> InvokeAndReturnAll()
        {
            var results = new List<Results<Return>>();
            foreach (var easyEvent in FuncDic.Values)
            {
                results.Add(easyEvent.InvokeAndReturnAll());
            }

            return new Results<Return>(results.ToArray());
        }
        public Return Invoke(State state)=> FuncDic[state].Invoke();
        public IUnRegisterHandle Register(State state, Func<Return> action)=>FuncDic[state].Register(action);
        public void UnRegister(State state, Func<Return> action)=>FuncDic[state].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(FuncDic);
    }

    public class EnumFunc<State, Return, A>: IEnumDelegate where State : Enum
    {
        public Dictionary<State, EasyFunc<Return, A>> FuncDic = new();

        public EnumFunc() => IEnumDelegate.Fill(FuncDic);
        public Results<Return> InvokeAndReturnAll(State state, A a)=> FuncDic[state].InvokeAndReturnAll(a);
        public Results<Return> InvokeAndReturnAll(A a)
        {
            var results = new List<Results<Return>>();
            foreach (var easyEvent in FuncDic.Values)
            {
                results.Add(easyEvent.InvokeAndReturnAll(a));
            }

            return new Results<Return>(results.ToArray());
        }
        public Return Invoke(State state, A a)=> FuncDic[state].Invoke(a);
        public IUnRegisterHandle Register(State state, Func<Return> action)=>FuncDic[state].Register(action);
        public void UnRegister(State state, Func<Return> action)=>FuncDic[state].UnRegister(action);
        public IUnRegisterHandle Register(State state, Func<A, Return> action)=>FuncDic[state].Register(action);
        public void UnRegister(State state, Func<A, Return> action) { FuncDic[state].UnRegister(action); }
        public void Clear() => IEnumDelegate.Clear(FuncDic);
    }

    public class EnumFunc<State, Return, A, B>: IEnumDelegate where State : Enum
    {
        public Dictionary<State, EasyFunc<Return, A, B>> FuncDic = new();

        public EnumFunc() => IEnumDelegate.Fill(FuncDic);
        public Results<Return> InvokeAndReturnAll(State state, A a, B b)=> FuncDic[state].InvokeAndReturnAll(a, b);
        public Results<Return> InvokeAndReturnAll(A a, B b)
        {
            var results = new List<Results<Return>>();
            foreach (var easyEvent in FuncDic.Values)
            {
                results.Add(easyEvent.InvokeAndReturnAll(a,b));
            }

            return new Results<Return>(results.ToArray());
        }
        public Return Invoke(State state, A a, B b)=> FuncDic[state].Invoke(a, b);
        public IUnRegisterHandle Register(State state, Func<Return> action)=>FuncDic[state].Register(action);
        public void UnRegister(State state, Func<Return> action)=>FuncDic[state].UnRegister(action);
        public IUnRegisterHandle Register(State state, Func<A, B, Return> action)=>FuncDic[state].Register(action);
        public void UnRegister(State state, Func<A, B, Return> action) { FuncDic[state].UnRegister(action); }
        public void Clear() => IEnumDelegate.Clear(FuncDic);
    }

    public class EnumFunc<State, Return, A, B, C>: IEnumDelegate where State : Enum
    {
        public Dictionary<State, EasyFunc<Return, A, B, C>> FuncDic = new();
        
        public EnumFunc() => IEnumDelegate.Fill(FuncDic);
        public Results<Return> InvokeAndReturnAll(State state, A a, B b, C c)=> FuncDic[state].InvokeAndReturnAll(a, b, c);
        public Results<Return> InvokeAndReturnAll(A a, B b, C c)
        {
            var results = new List<Results<Return>>();
            foreach (var easyEvent in FuncDic.Values)
            {
                results.Add(easyEvent.InvokeAndReturnAll(a,b,c));
            }

            return new Results<Return>(results.ToArray());
        }
        public Return Invoke(State state, A a, B b, C c)=> FuncDic[state].Invoke(a, b, c);
        public IUnRegisterHandle Register(State state, Func<Return> action)=>FuncDic[state].Register(action);
        public void UnRegister(State state, Func<Return> action)=>FuncDic[state].UnRegister(action);
        public IUnRegisterHandle Register(State state, Func<A, B, C, Return> action)=>FuncDic[state].Register(action);
        public void UnRegister(State state, Func<A, B, C, Return> action) { FuncDic[state].UnRegister(action); }
        public void Clear() => IEnumDelegate.Clear(FuncDic);
    }

    public class EnumFunc<State, Return, A, B, C, D>: IEnumDelegate where State : Enum
    {
        public Dictionary<State, EasyFunc<Return, A, B, C, D>> FuncDic = new();
        
        public EnumFunc() => IEnumDelegate.Fill(FuncDic);
        public Results<Return> InvokeAndReturnAll(State state, A a, B b, C c, D d)=> FuncDic[state].InvokeAndReturnAll(a, b, c, d);
        public Results<Return> InvokeAndReturnAll(A a, B b, C c, D d)
        {
            var results = new List<Results<Return>>();
            foreach (var easyEvent in FuncDic.Values)
            {
                results.Add(easyEvent.InvokeAndReturnAll(a,b,c,d));
            }

            return new Results<Return>(results.ToArray());
        }
        public Return Invoke(State state, A a, B b, C c, D d)=> FuncDic[state].Invoke(a, b, c, d);
        public IUnRegisterHandle Register(State state, Func<Return> action)=>FuncDic[state].Register(action);
        public void UnRegister(State state, Func<Return> action)=>FuncDic[state].UnRegister(action);
        public IUnRegisterHandle Register(State state, Func<A, B, C, D, Return> action)=>FuncDic[state].Register(action);
        public void UnRegister(State state, Func<A, B, C, D, Return> action) { FuncDic[state].UnRegister(action); }
        public void Clear() => IEnumDelegate.Clear(FuncDic);
    }

    public class EnumFunc<State, Return, A, B, C, D, E>: IEnumDelegate where State : Enum
    {
        public Dictionary<State, EasyFunc<Return, A, B, C, D, E>> FuncDic = new();
        
        public EnumFunc() => IEnumDelegate.Fill(FuncDic);
        public Results<Return> InvokeAndReturnAll(State state, A a, B b, C c, D d, E e)=> FuncDic[state].InvokeAndReturnAll(a, b, c, d, e);
        public Results<Return> InvokeAndReturnAll(A a, B b, C c, D d, E e)
        {
            var results = new List<Results<Return>>();
            foreach (var easyEvent in FuncDic.Values)
            {
                results.Add(easyEvent.InvokeAndReturnAll(a,b,c,d,e));
            }

            return new Results<Return>(results.ToArray());
        }
        public Return Invoke(State state, A a, B b, C c, D d, E e)=> FuncDic[state].Invoke(a, b, c, d, e);
        public IUnRegisterHandle Register(State state, Func<Return> action)=>FuncDic[state].Register(action);
        public void UnRegister(State state, Func<Return> action)=>FuncDic[state].UnRegister(action);
        public IUnRegisterHandle Register(State state, Func<A, B, C, D, E, Return> action)=>FuncDic[state].Register(action);
        public void UnRegister(State state, Func<A, B, C, D, E, Return> action) { FuncDic[state].UnRegister(action); }
        public void Clear() => IEnumDelegate.Clear(FuncDic);
    }


    public class DEnumEvent<State, LifeType>: IEnumDelegate where State : Enum where LifeType : Enum
    {
        public Dictionary<State, Dictionary<LifeType, EasyEvent>> ActionDic = new();

        public DEnumEvent()
        {
            IEnumDelegate.Fill(ActionDic);
            foreach (var value in ActionDic.Values)
                IEnumDelegate.Fill(value);
        }
        public void Invoke(State state, LifeType lifeType)=>ActionDic[state][lifeType].Invoke();

        public void InvokeAll()
        {
            foreach (var dic in ActionDic.Values)
            {
                foreach (var easyEvent in dic.Values) { easyEvent.Invoke(); }
            }
        }

        public IUnRegisterHandle Register(State state, LifeType lifeType, Action action)=>ActionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action action)=>ActionDic[state][lifeType].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(ActionDic);
    }

    public class DEnumEvent<State, LifeType, A>: IEnumDelegate where State : Enum where LifeType : Enum
    {
        public Dictionary<State, Dictionary<LifeType, EasyEvent<A>>> ActionDic = new();

        public DEnumEvent()
        {
            IEnumDelegate.Fill(ActionDic);
            foreach (var value in ActionDic.Values)
                IEnumDelegate.Fill(value);
        }
        public void Invoke(State state, LifeType lifeType, A a)=>ActionDic[state][lifeType]?.Invoke(a);

        public void InvokeAll(A a)
        {
            foreach (var dic in ActionDic.Values)
            {
                foreach (var easyEvent in dic.Values) { easyEvent?.Invoke(a); }
            }
        }
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action action)=>ActionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action action)=>ActionDic[state][lifeType].UnRegister(action);
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action<A> action)=>ActionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action<A> action)=>ActionDic[state][lifeType].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(ActionDic);
    }

    public class DEnumEvent<State, LifeType, A, B>: IEnumDelegate where State : Enum where LifeType : Enum
    {
        public Dictionary<State, Dictionary<LifeType, EasyEvent<A, B>>> ActionDic = new();

        public DEnumEvent()
        {
            IEnumDelegate.Fill(ActionDic);
            foreach (var value in ActionDic.Values)
                IEnumDelegate.Fill(value);
        }
        public void Invoke(State state, LifeType lifeType, A a, B b)=>ActionDic[state][lifeType]?.Invoke(a, b);

        public void InvokeAll(A a, B b)
        {
            foreach (var dic in ActionDic.Values)
            {
                foreach (var easyEvent in dic.Values) { easyEvent?.Invoke(a,b); }
            }
        }
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action action)=>ActionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action action)=>ActionDic[state][lifeType].UnRegister(action);
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action<A, B> action)=>ActionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action<A, B> action)=>ActionDic[state][lifeType].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(ActionDic);
    }

    public class DEnumEvent<State, LifeType, A, B, C>: IEnumDelegate where State : Enum where LifeType : Enum
    {
        public Dictionary<State, Dictionary<LifeType, EasyEvent<A, B, C>>> ActionDic = new();

        public DEnumEvent()
        {
            IEnumDelegate.Fill(ActionDic);
            foreach (var value in ActionDic.Values)
                IEnumDelegate.Fill(value);
        }
        public void Invoke(State state, LifeType lifeType, A a, B b, C c)=>ActionDic[state][lifeType]?.Invoke(a, b, c);

        public void InvokeAll(A a, B b, C c)
        {
            foreach (var dic in ActionDic.Values)
            {
                foreach (var easyEvent in dic.Values) { easyEvent?.Invoke(a,b,c); }
            }
        }
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action action)=>ActionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action action)=>ActionDic[state][lifeType].UnRegister(action);
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action<A, B, C> action)=>ActionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action<A, B, C> action)=>ActionDic[state][lifeType].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(ActionDic);
    }

    public class DEnumEvent<State, LifeType, A, B, C, D>: IEnumDelegate where State : Enum where LifeType : Enum
    {
        public Dictionary<State, Dictionary<LifeType, EasyEvent<A, B, C, D>>> ActionDic = new();
        
        public DEnumEvent()
        {
            IEnumDelegate.Fill(ActionDic);
            foreach (var value in ActionDic.Values)
                IEnumDelegate.Fill(value);
        }
        public void Invoke(State state, LifeType lifeType, A a, B b, C c, D d)=>ActionDic[state][lifeType]?.Invoke(a, b, c, d);

        public void InvokeAll(A a, B b, C c, D d)
        {
            foreach (var dic in ActionDic.Values)
            {
                foreach (var easyEvent in dic.Values) { easyEvent?.Invoke(a,b,c,d); }
            }
        }
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action action)=>ActionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action action)=>ActionDic[state][lifeType].UnRegister(action);
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action<A, B, C, D> action)=>ActionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action<A, B, C, D> action)=>ActionDic[state][lifeType].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(ActionDic);
    }

    public class DEnumEvent<State, LifeType, A, B, C, D, E>: IEnumDelegate where State : Enum where LifeType : Enum
    {
        public Dictionary<State, Dictionary<LifeType, EasyEvent<A, B, C, D, E>>> ActionDic = new();
        
        public DEnumEvent() 
        {
            IEnumDelegate.Fill(ActionDic);
            foreach (var value in ActionDic.Values)
                IEnumDelegate.Fill(value);
        }
        public void Invoke(State state, LifeType lifeType, A a, B b, C c, D d, E e)=>ActionDic[state][lifeType]?.Invoke(a, b, c, d, e);

        public void InvokeAll(A a, B b, C c, D d, E e)
        {
            foreach (var dic in ActionDic.Values)
            {
                foreach (var easyEvent in dic.Values) { easyEvent?.Invoke(a,b,c,d,e); }
            }
        }
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action action)=>ActionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action action)=>ActionDic[state][lifeType].UnRegister(action);
        public IUnRegisterHandle Register(State state, LifeType lifeType, Action<A, B, C, D, E> action)=>ActionDic[state][lifeType].Register(action);
        public void UnRegister(State state, LifeType lifeType, Action<A, B, C, D, E> action)=>ActionDic[state][lifeType].UnRegister(action);
        public void Clear() => IEnumDelegate.Clear(ActionDic);
    }
}
