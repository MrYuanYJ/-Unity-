using System;
using System.Collections.Generic;

namespace EasyFramework.EventKit
{
    #region AEventIndex
    
    public abstract class IEventIndex<T> where T : IEventIndex<T>
    {
        public static void ClearEvent() => ClassEvent.Global.Clear<T>();
        public static void ClearEvent(ClassEvent e) => e.Clear<T>();
    }
    public abstract class AEventIndex<T> : IEventIndex<T> where T : AEventIndex<T>
    {
        public static IUnRegisterHandle RegisterEvent(Action a) => ClassEvent.Global.Register<T>(a);
        public static IUnRegisterHandle RegisterEvent(ClassEvent e, Action a) => e.Register<T>(a);
        public static void UnRegisterEvent(Action a) => ClassEvent.Global.UnRegister<T>(a);
        public static void UnRegisterEvent(ClassEvent e, Action a) => e.UnRegister<T>(a);
        public static void InvokeEvent() => ClassEvent.Global.Invoke<T>();
        public static void InvokeEvent(ClassEvent e) => e.Invoke<T>();
    }

    public abstract class AEventIndex<T, T1>:IEventIndex<T> where T : AEventIndex<T, T1>
    {
        public static IUnRegisterHandle RegisterEvent(Action a) => ClassEvent.Global.Register<T>(a);
        public static IUnRegisterHandle RegisterEvent(ClassEvent e, Action a) => e.Register<T>(a);
        public static IUnRegisterHandle RegisterEvent(Action<T1> a) => ClassEvent.Global.Register<T, T1>(a);
        public static IUnRegisterHandle RegisterEvent(ClassEvent e, Action<T1> a) => e.Register<T, T1>(a);
        public static void UnRegisterEvent(Action<T1> a) => ClassEvent.Global.UnRegister<T, T1>(a);
        public static void UnRegisterEvent(ClassEvent e, Action<T1> a) => e.UnRegister<T, T1>(a);
        public static void InvokeEvent(T1 arg1) => ClassEvent.Global.Invoke<T, T1>(arg1);
        public static void InvokeEvent(ClassEvent e, T1 arg1) => e.Invoke<T, T1>(arg1);
    }
    public abstract class AEventIndex<T, T1, T2>:IEventIndex<T> where T : AEventIndex<T, T1, T2>
    {
        public static IUnRegisterHandle RegisterEvent(Action a) => ClassEvent.Global.Register<T>(a);
        public static IUnRegisterHandle RegisterEvent(ClassEvent e, Action a) => e.Register<T>(a);
        public static IUnRegisterHandle RegisterEvent(Action<T1, T2> a) => ClassEvent.Global.Register<T, T1, T2>(a);
        public static IUnRegisterHandle RegisterEvent(ClassEvent e, Action<T1, T2> a) => e.Register<T, T1, T2>(a);
        public static void UnRegisterEvent(Action<T1, T2> a) => ClassEvent.Global.UnRegister<T, T1, T2>(a);
        public static void UnRegisterEvent(ClassEvent e, Action<T1, T2> a) => e.UnRegister<T, T1, T2>(a);
        public static void InvokeEvent(T1 arg1, T2 arg2) => ClassEvent.Global.Invoke<T, T1, T2>(arg1, arg2);
        public static void InvokeEvent(ClassEvent e, T1 arg1, T2 arg2) => e.Invoke<T, T1, T2>(arg1, arg2);
    }
    public abstract class AEventIndex<T, T1, T2, T3>:IEventIndex<T> where T : AEventIndex<T, T1, T2, T3>
    {
        public static IUnRegisterHandle RegisterEvent(Action a) => ClassEvent.Global.Register<T>(a);
        public static IUnRegisterHandle RegisterEvent(ClassEvent e, Action a) => e.Register<T>(a);
        public static IUnRegisterHandle RegisterEvent(Action<T1, T2, T3> a) => ClassEvent.Global.Register<T, T1, T2, T3>(a);
        public static IUnRegisterHandle RegisterEvent(ClassEvent e, Action<T1, T2, T3> a) => e.Register<T, T1, T2, T3>(a);
        public static void UnRegisterEvent(Action<T1, T2, T3> a) => ClassEvent.Global.UnRegister<T, T1, T2, T3>(a);
        public static void UnRegisterEvent(ClassEvent e, Action<T1, T2, T3> a) => e.UnRegister<T, T1, T2, T3>(a);
        public static void InvokeEvent(T1 arg1, T2 arg2, T3 arg3) => ClassEvent.Global.Invoke<T, T1, T2, T3>(arg1, arg2, arg3);
        public static void InvokeEvent(ClassEvent e, T1 arg1, T2 arg2, T3 arg3) => e.Invoke<T, T1, T2, T3>(arg1, arg2, arg3);
    }
    public abstract class AEventIndex<T, T1, T2, T3, T4>:IEventIndex<T> where T : AEventIndex<T, T1, T2, T3, T4>
    {
        public static IUnRegisterHandle RegisterEvent(Action a) => ClassEvent.Global.Register<T>(a);
        public static IUnRegisterHandle RegisterEvent(ClassEvent e, Action a) => e.Register<T>(a);
        public static IUnRegisterHandle RegisterEvent(Action<T1, T2, T3, T4> a) => ClassEvent.Global.Register<T, T1, T2, T3, T4>(a);
        public static IUnRegisterHandle RegisterEvent(ClassEvent e, Action<T1, T2, T3, T4> a) => e.Register<T, T1, T2, T3, T4>(a);
        public static void UnRegisterEvent(Action<T1, T2, T3, T4> a) => ClassEvent.Global.UnRegister<T, T1, T2, T3, T4>(a);
        public static void UnRegisterEvent(ClassEvent e, Action<T1, T2, T3, T4> a) => e.UnRegister<T, T1, T2, T3, T4>(a);
        public static void InvokeEvent(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => ClassEvent.Global.Invoke<T, T1, T2, T3, T4>(arg1, arg2, arg3, arg4);
        public static void InvokeEvent(ClassEvent e, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => e.Invoke<T, T1, T2, T3, T4>(arg1, arg2, arg3, arg4);
    }
    public abstract class AEventIndex<T, T1, T2, T3, T4, T5>:IEventIndex<T> where T : AEventIndex<T, T1, T2, T3, T4, T5>
    {
        public static IUnRegisterHandle RegisterEvent(Action a) => ClassEvent.Global.Register<T>(a);
        public static IUnRegisterHandle RegisterEvent(ClassEvent e, Action a) => e.Register<T>(a);
        public static IUnRegisterHandle RegisterEvent(Action<T1, T2, T3, T4, T5> a) => ClassEvent.Global.Register<T, T1, T2, T3, T4, T5>(a);
        public static IUnRegisterHandle RegisterEvent(ClassEvent e, Action<T1, T2, T3, T4, T5> a) => e.Register<T, T1, T2, T3, T4, T5>(a);
        public static void UnRegisterEvent(Action<T1, T2, T3, T4, T5> a) => ClassEvent.Global.UnRegister<T, T1, T2, T3, T4, T5>(a);
        public static void UnRegisterEvent(ClassEvent e, Action<T1, T2, T3, T4, T5> a) => e.UnRegister<T, T1, T2, T3, T4, T5>(a);
        public static void InvokeEvent(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => ClassEvent.Global.Invoke<T, T1, T2, T3, T4, T5>(arg1, arg2, arg3, arg4, arg5);
        public static void InvokeEvent(ClassEvent e, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => e.Invoke<T, T1, T2, T3, T4, T5>(arg1, arg2, arg3, arg4, arg5);
    }
    #endregion
    
    public class ClassEvent
    {
        public static ClassEvent Global = new ClassEvent();
        protected Dictionary<Type, IEasyEvent> EventDic = new();

        public IUnRegisterHandle Register<T>(Action a) where T : IEventIndex<T>
        {
            if (!EventDic.ContainsKey(typeof(T))) { EventDic.Add(typeof(T), new EasyEvent()); }

            return EventDic[typeof(T)].Register(a);
        }

        public IUnRegisterHandle Register<T, T1>(Action<T1> a) where T : AEventIndex<T, T1>
        {
            if (!EventDic.ContainsKey(typeof(T))) { EventDic.Add(typeof(T), new EasyEvent<T1>()); }

            return ((EasyEvent<T1>)EventDic[typeof(T)]).Register(a);
        }

        public IUnRegisterHandle Register<T, T1, T2>(Action<T1, T2> a) where T : AEventIndex<T, T1, T2>
        {
            if (!EventDic.ContainsKey(typeof(T))) { EventDic.Add(typeof(T), new EasyEvent<T1, T2>()); }

            return ((EasyEvent<T1, T2>)EventDic[typeof(T)]).Register(a);
        }

        public IUnRegisterHandle Register<T, T1, T2, T3>(Action<T1, T2, T3> a) where T : AEventIndex<T, T1, T2, T3>
        {
            if (!EventDic.ContainsKey(typeof(T))) { EventDic.Add(typeof(T), new EasyEvent<T1, T2, T3>()); }

            return ((EasyEvent<T1, T2, T3>)EventDic[typeof(T)]).Register(a);
        }

        public IUnRegisterHandle Register<T, T1, T2, T3, T4>(Action<T1, T2, T3, T4> a) where T : AEventIndex<T, T1, T2, T3, T4>
        {
            if (!EventDic.ContainsKey(typeof(T))) { EventDic.Add(typeof(T), new EasyEvent<T1, T2, T3, T4>()); }

            return ((EasyEvent<T1, T2, T3, T4>)EventDic[typeof(T)]).Register(a);
        }

        public IUnRegisterHandle Register<T, T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> a) where T : AEventIndex<T, T1, T2, T3, T4, T5>
        {
            if (!EventDic.ContainsKey(typeof(T))) { EventDic.Add(typeof(T), new EasyEvent<T1, T2, T3, T4, T5>()); }

            return ((EasyEvent<T1, T2, T3, T4, T5>)EventDic[typeof(T)]).Register(a);
        }

        public void UnRegister<T>(Action a) where T : IEventIndex<T>
        {
            if (EventDic.ContainsKey(typeof(T))) { EventDic[typeof(T)].UnRegister(a); }
        }

        public void UnRegister<T, T1>(Action<T1> a) where T : AEventIndex<T, T1>
        {
            if (EventDic.ContainsKey(typeof(T))) { ((EasyEvent<T1>)EventDic[typeof(T)]).UnRegister(a); }
        }

        public void UnRegister<T, T1, T2>(Action<T1, T2> a) where T : AEventIndex<T, T1, T2>
        {
            if (EventDic.ContainsKey(typeof(T))) { ((EasyEvent<T1, T2>)EventDic[typeof(T)]).UnRegister(a); }
        }

        public void UnRegister<T, T1, T2, T3>(Action<T1, T2, T3> a) where T : AEventIndex<T, T1, T2, T3>
        {
            if (EventDic.ContainsKey(typeof(T))) { ((EasyEvent<T1, T2, T3>)EventDic[typeof(T)]).UnRegister(a); }
        }

        public void UnRegister<T, T1, T2, T3, T4>(Action<T1, T2, T3, T4> a) where T : AEventIndex<T, T1, T2, T3, T4>
        {
            if (EventDic.ContainsKey(typeof(T))) { ((EasyEvent<T1, T2, T3, T4>)EventDic[typeof(T)]).UnRegister(a); }
        }

        public void UnRegister<T, T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> a) where T : AEventIndex<T, T1, T2, T3, T4, T5>
        {
            if (EventDic.ContainsKey(typeof(T)))
            {
                ((EasyEvent<T1, T2, T3, T4, T5>)EventDic[typeof(T)]).UnRegister(a);
            }
        }

        public void Invoke<T>() where T : IEventIndex<T>
        {
            if (!EventDic.ContainsKey(typeof(T)))
                return;
            ((EasyEvent)EventDic[typeof(T)]).Invoke();
        }

        public void Invoke<T, T1>(T1 t1) where T : AEventIndex<T, T1>
        {
            if (!EventDic.ContainsKey(typeof(T)))
                return;
            ((EasyEvent<T1>)EventDic[typeof(T)]).Invoke(t1);
        }

        public void Invoke<T, T1, T2>(T1 t1, T2 t2) where T : AEventIndex<T, T1, T2>
        {
            if (!EventDic.ContainsKey(typeof(T)))
                return;
            ((EasyEvent<T1, T2>)EventDic[typeof(T)]).Invoke(t1, t2);
        }

        public void Invoke<T, T1, T2, T3>(T1 t1, T2 t2, T3 t3) where T : AEventIndex<T, T1, T2, T3>
        {
            if (!EventDic.ContainsKey(typeof(T)))
                return;
            ((EasyEvent<T1, T2, T3>)EventDic[typeof(T)]).Invoke(t1, t2, t3);
        }

        public void Invoke<T, T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4) where T : AEventIndex<T, T1, T2, T3, T4>
        {
            if (!EventDic.ContainsKey(typeof(T)))
                return;
            ((EasyEvent<T1, T2, T3, T4>)EventDic[typeof(T)]).Invoke(t1, t2, t3, t4);
        }

        public void Invoke<T, T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) where T : AEventIndex<T, T1, T2, T3, T4, T5>
        {
            if (!EventDic.ContainsKey(typeof(T)))
                return;
            ((EasyEvent<T1, T2, T3, T4, T5>)EventDic[typeof(T)]).Invoke(t1, t2, t3, t4, t5);
        }

        public void Clear<T>() where T : IEventIndex<T>
        {
            if (!EventDic.ContainsKey(typeof(T)))
                return;
            EventDic[typeof(T)].Clear();
        }

        public void ClearAll()
        {
            foreach (var item in EventDic)
            {
                item.Value.Clear();
            }
            EventDic.Clear();
        }
    }
}