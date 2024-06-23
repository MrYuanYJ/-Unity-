using System;

namespace EasyFramework.EventKit
{
    #region AEventIndex
    
    public abstract class IEventIndex<T> where T : IEventIndex<T>
    {
        public static void ClearEvent() => EasyEventDic.Global.Clear<T>();
        public static void ClearEvent(IGetEasyEventDic e) => e.EventDic.Clear<T>();
        public static IEasyEvent TryGet()=>EasyEventDic.Global.TryGet<T>(out var easyEvent) ? easyEvent : null;
        public static IEasyEvent TryGet(IGetEasyEventDic e) => e.EventDic.TryGet<T>(out var easyEvent) ? easyEvent : null;
        public static void BaseInvoke()
        {
            if (EasyEventDic.Global.TryGet<T>(out var easyEvent))
                EventExtension.BaseInvoke(easyEvent);
        }
        public static void BaseInvoke(IGetEasyEventDic e)
        {
            if (e.EventDic.TryGet<T>(out var easyEvent))
                EventExtension.BaseInvoke(easyEvent);
        }
    }
    public abstract class AEventIndex<T> : IEventIndex<T> where T : AEventIndex<T>
    {
        public static IUnRegisterHandle RegisterEvent(Action a) => EasyEventDic.Global.RegisterClassEvent<T>(a);
        public static IUnRegisterHandle RegisterEvent(IGetEasyEventDic e, Action a) => e.EventDic.RegisterClassEvent<T>(a);
        public static void UnRegisterEvent(Action a) => EasyEventDic.Global.UnRegisterClassEvent<T>(a);
        public static void UnRegisterEvent(IGetEasyEventDic e, Action a) => e.EventDic.UnRegisterClassEvent<T>(a);
        public static void InvokeEvent() => EasyEventDic.Global.InvokeClassEvent<T>();
        public static void InvokeEvent(IGetEasyEventDic e) => e.EventDic.InvokeClassEvent<T>();
    }

    public abstract class AEventIndex<T, T1>:IEventIndex<T> where T : AEventIndex<T, T1>
    {
        public static IUnRegisterHandle RegisterEvent(Action a) => EasyEventDic.Global.GetOrAddEvent<T, T1>().Register(a);
        public static IUnRegisterHandle RegisterEvent(IGetEasyEventDic e, Action a) => e.EventDic.GetOrAddEvent<T, T1>().Register(a);
        public static void UnRegisterEvent(Action a) => EasyEventDic.Global.Get<T, T1>().UnRegister(a);
        public static void UnRegisterEvent(IGetEasyEventDic e, Action a) => e.EventDic.Get<T, T1>().UnRegister(a);
        public static IUnRegisterHandle RegisterEvent(Action<T1> a) => EasyEventDic.Global.RegisterClassEvent<T, T1>(a);
        public static IUnRegisterHandle RegisterEvent(IGetEasyEventDic e, Action<T1> a) => e.EventDic.RegisterClassEvent<T, T1>(a);
        public static void UnRegisterEvent(Action<T1> a) => EasyEventDic.Global.UnRegisterClassEvent<T, T1>(a);
        public static void UnRegisterEvent(IGetEasyEventDic e, Action<T1> a) => e.EventDic.UnRegisterClassEvent<T, T1>(a);
        public static void InvokeEvent(T1 arg1) => EasyEventDic.Global.InvokeClassEvent<T, T1>(arg1);
        public static void InvokeEvent(IGetEasyEventDic e, T1 arg1) => e.EventDic.InvokeClassEvent<T, T1>(arg1);
    }
    public abstract class AEventIndex<T, T1, T2>:IEventIndex<T> where T : AEventIndex<T, T1, T2>
    {
        public static IUnRegisterHandle RegisterEvent(Action a) => EasyEventDic.Global.GetOrAddEvent<T, T1, T2>().Register(a);
        public static IUnRegisterHandle RegisterEvent(IGetEasyEventDic e, Action a) => e.EventDic.GetOrAddEvent<T, T1, T2>().Register(a);
        public static void UnRegisterEvent(Action a) => EasyEventDic.Global.Get<T, T1, T2>().UnRegister(a);
        public static void UnRegisterEvent(IGetEasyEventDic e, Action a) => e.EventDic.Get<T, T1, T2>().UnRegister(a);
        public static IUnRegisterHandle RegisterEvent(Action<T1, T2> a) => EasyEventDic.Global.RegisterClassEvent<T, T1, T2>(a);
        public static IUnRegisterHandle RegisterEvent(IGetEasyEventDic e, Action<T1, T2> a) => e.EventDic.RegisterClassEvent<T, T1, T2>(a);
        public static void UnRegisterEvent(Action<T1, T2> a) => EasyEventDic.Global.UnRegisterClassEvent<T, T1, T2>(a);
        public static void UnRegisterEvent(IGetEasyEventDic e, Action<T1, T2> a) => e.EventDic.UnRegisterClassEvent<T, T1, T2>(a);
        public static void InvokeEvent(T1 arg1, T2 arg2) => EasyEventDic.Global.InvokeClassEvent<T, T1, T2>(arg1, arg2);
        public static void InvokeEvent(IGetEasyEventDic e, T1 arg1, T2 arg2) => e.EventDic.InvokeClassEvent<T, T1, T2>(arg1, arg2);
    }
    public abstract class AEventIndex<T, T1, T2, T3>:IEventIndex<T> where T : AEventIndex<T, T1, T2, T3>
    {
        public static IUnRegisterHandle RegisterEvent(Action a) => EasyEventDic.Global.GetOrAddEvent<T, T1, T2, T3>().Register(a);
        public static IUnRegisterHandle RegisterEvent(IGetEasyEventDic e, Action a) => e.EventDic.GetOrAddEvent<T, T1, T2, T3>().Register(a);
        public static void UnRegisterEvent(Action a) => EasyEventDic.Global.Get<T, T1, T2, T3>().UnRegister(a);
        public static void UnRegisterEvent(IGetEasyEventDic e, Action a) => e.EventDic.Get<T, T1, T2, T3>().UnRegister(a);
        public static IUnRegisterHandle RegisterEvent(Action<T1, T2, T3> a) => EasyEventDic.Global.RegisterClassEvent<T, T1, T2, T3>(a);
        public static IUnRegisterHandle RegisterEvent(IGetEasyEventDic e, Action<T1, T2, T3> a) => e.EventDic.RegisterClassEvent<T, T1, T2, T3>(a);
        public static void UnRegisterEvent(Action<T1, T2, T3> a) => EasyEventDic.Global.UnRegisterClassEvent<T, T1, T2, T3>(a);
        public static void UnRegisterEvent(IGetEasyEventDic e, Action<T1, T2, T3> a) => e.EventDic.UnRegisterClassEvent<T, T1, T2, T3>(a);
        public static void InvokeEvent(T1 arg1, T2 arg2, T3 arg3) => EasyEventDic.Global.InvokeClassEvent<T, T1, T2, T3>(arg1, arg2, arg3);
        public static void InvokeEvent(IGetEasyEventDic e, T1 arg1, T2 arg2, T3 arg3) => e.EventDic.InvokeClassEvent<T, T1, T2, T3>(arg1, arg2, arg3);
    }
    public abstract class AEventIndex<T, T1, T2, T3, T4>:IEventIndex<T> where T : AEventIndex<T, T1, T2, T3, T4>
    {
        public static IUnRegisterHandle RegisterEvent(Action a) => EasyEventDic.Global.GetOrAddEvent<T, T1, T2, T3, T4>().Register(a);
        public static IUnRegisterHandle RegisterEvent(IGetEasyEventDic e, Action a) => e.EventDic.GetOrAddEvent<T, T1, T2, T3, T4>().Register(a);
        public static void UnRegisterEvent(Action a) => EasyEventDic.Global.Get<T, T1, T2, T3, T4>().UnRegister(a);
        public static void UnRegisterEvent(IGetEasyEventDic e, Action a) => e.EventDic.Get<T, T1, T2, T3, T4>().UnRegister(a);
        public static IUnRegisterHandle RegisterEvent(Action<T1, T2, T3, T4> a) => EasyEventDic.Global.RegisterClassEvent<T, T1, T2, T3, T4>(a);
        public static IUnRegisterHandle RegisterEvent(IGetEasyEventDic e, Action<T1, T2, T3, T4> a) => e.EventDic.RegisterClassEvent<T, T1, T2, T3, T4>(a);
        public static void UnRegisterEvent(Action<T1, T2, T3, T4> a) => EasyEventDic.Global.UnRegisterClassEvent<T, T1, T2, T3, T4>(a);
        public static void UnRegisterEvent(IGetEasyEventDic e, Action<T1, T2, T3, T4> a) => e.EventDic.UnRegisterClassEvent<T, T1, T2, T3, T4>(a);
        public static void InvokeEvent(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => EasyEventDic.Global.InvokeClassEvent<T, T1, T2, T3, T4>(arg1, arg2, arg3, arg4);
        public static void InvokeEvent(IGetEasyEventDic e, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => e.EventDic.InvokeClassEvent<T, T1, T2, T3, T4>(arg1, arg2, arg3, arg4);
    }
    public abstract class AEventIndex<T, T1, T2, T3, T4, T5>:IEventIndex<T> where T : AEventIndex<T, T1, T2, T3, T4, T5>
    {
        public static IUnRegisterHandle RegisterEvent(Action a) => EasyEventDic.Global.GetOrAddEvent<T, T1, T2, T3, T4, T5>().Register(a);
        public static IUnRegisterHandle RegisterEvent(IGetEasyEventDic e, Action a) => e.EventDic.GetOrAddEvent<T, T1, T2, T3, T4, T5>().Register(a);
        public static void UnRegisterEvent(Action a) => EasyEventDic.Global.Get<T, T1, T2, T3, T4, T5>().UnRegister(a);
        public static void UnRegisterEvent(IGetEasyEventDic e, Action a) => e.EventDic.Get<T, T1, T2, T3, T4, T5>().UnRegister(a);
        public static IUnRegisterHandle RegisterEvent(Action<T1, T2, T3, T4, T5> a) => EasyEventDic.Global.RegisterClassEvent<T, T1, T2, T3, T4, T5>(a);
        public static IUnRegisterHandle RegisterEvent(IGetEasyEventDic e, Action<T1, T2, T3, T4, T5> a) => e.EventDic.RegisterClassEvent<T, T1, T2, T3, T4, T5>(a);
        public static void UnRegisterEvent(Action<T1, T2, T3, T4, T5> a) => EasyEventDic.Global.UnRegisterClassEvent<T, T1, T2, T3, T4, T5>(a);
        public static void UnRegisterEvent(IGetEasyEventDic e, Action<T1, T2, T3, T4, T5> a) => e.EventDic.UnRegisterClassEvent<T, T1, T2, T3, T4, T5>(a);
        public static void InvokeEvent(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => EasyEventDic.Global.InvokeClassEvent<T, T1, T2, T3, T4, T5>(arg1, arg2, arg3, arg4, arg5);
        public static void InvokeEvent(IGetEasyEventDic e, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => e.EventDic.InvokeClassEvent<T, T1, T2, T3, T4, T5>(arg1, arg2, arg3, arg4, arg5);
    }
    #endregion

    public static class EasyEventDicExtension
    {
        public static IUnRegisterHandle RegisterClassEvent<T>(this IEasyEventDic dic,Action action) where T : AEventIndex<T>
        {
            return dic.GetOrAddEvent<T>().Register(action);
        }
        public static IUnRegisterHandle RegisterClassEvent<T, T1>(this IEasyEventDic dic, Action<T1> action) where T : AEventIndex<T, T1>
        {
            return dic.GetOrAddEvent<T, T1>().Register(action);
        }
        public static IUnRegisterHandle RegisterClassEvent<T, T1, T2>(this IEasyEventDic dic, Action<T1, T2> action) where T : AEventIndex<T, T1, T2>
        {
            return dic.GetOrAddEvent<T, T1, T2>().Register(action);
        }
        public static IUnRegisterHandle RegisterClassEvent<T, T1, T2, T3>(this IEasyEventDic dic, Action<T1, T2, T3> action) where T : AEventIndex<T, T1, T2, T3>
        {
            return dic.GetOrAddEvent<T, T1, T2, T3>().Register(action);
        }
        public static IUnRegisterHandle RegisterClassEvent<T, T1, T2, T3, T4>(this IEasyEventDic dic, Action<T1, T2, T3, T4> action) where T : AEventIndex<T, T1, T2, T3, T4>
        {
            return dic.GetOrAddEvent<T, T1, T2, T3, T4>().Register(action);
        }
        public static IUnRegisterHandle RegisterClassEvent<T, T1, T2, T3, T4, T5>(this IEasyEventDic dic, Action<T1, T2, T3, T4, T5> action) where T : AEventIndex<T, T1, T2, T3, T4, T5>
        {
            return dic.GetOrAddEvent<T, T1, T2, T3, T4, T5>().Register(action);
        }
        
        public static void UnRegisterClassEvent<T>(this IEasyEventDic dic, Action action) where T : IEventIndex<T>
        {
            if(dic.TryGet<T>(out var easyEvent))
                ((EasyEvent)easyEvent).UnRegister(action);
        }
        public static void UnRegisterClassEvent<T, T1>(this IEasyEventDic dic, Action<T1> action) where T : AEventIndex<T, T1>
        {
            if (dic.TryGet<T>(out var easyEvent))
                ((EasyEvent<T1>)easyEvent).UnRegister(action);
        }
        public static void UnRegisterClassEvent<T, T1, T2>(this IEasyEventDic dic, Action<T1, T2> action) where T : AEventIndex<T, T1, T2>
        {
            if (dic.TryGet<T>(out var easyEvent))
                ((EasyEvent<T1, T2>)easyEvent).UnRegister(action);
        }
        public static void UnRegisterClassEvent<T, T1, T2, T3>(this IEasyEventDic dic, Action<T1, T2, T3> action) where T : AEventIndex<T, T1, T2, T3>
        {
            if (dic.TryGet<T>(out var easyEvent))
                ((EasyEvent<T1, T2, T3>)easyEvent).UnRegister(action);
        }
        public static void UnRegisterClassEvent<T, T1, T2, T3, T4>(this IEasyEventDic dic, Action<T1, T2, T3, T4> action) where T : AEventIndex<T, T1, T2, T3, T4>
        {
            if (dic.TryGet<T>(out var easyEvent))
                ((EasyEvent<T1, T2, T3, T4>)easyEvent).UnRegister(action);
        }
        public static void UnRegisterClassEvent<T, T1, T2, T3, T4, T5>(this IEasyEventDic dic, Action<T1, T2, T3, T4, T5> action) where T : AEventIndex<T, T1, T2, T3, T4, T5>
        {
            if (dic.TryGet<T>(out var easyEvent))
                ((EasyEvent<T1, T2, T3, T4, T5>)easyEvent).UnRegister(action);
        }
       
        public static void InvokeClassEvent<T>(this IEasyEventDic dic) where T : IEventIndex<T>
        {
            if (dic.TryGet<T>(out var easyEvent))
                ((EasyEvent)easyEvent).Invoke();
        }
        public static void InvokeClassEvent<T, T1>(this IEasyEventDic dic, T1 t1) where T : AEventIndex<T, T1>
        {
            if (dic.TryGet<T>(out var easyEvent))
                ((EasyEvent<T1>)easyEvent).Invoke(t1);
        }
        public static void InvokeClassEvent<T, T1, T2>(this IEasyEventDic dic, T1 t1, T2 t2) where T : AEventIndex<T, T1, T2>
        {
            if (dic.TryGet<T>(out var easyEvent))
                ((EasyEvent<T1, T2>)easyEvent).Invoke(t1, t2);
        }
        public static void InvokeClassEvent<T, T1, T2, T3>(this IEasyEventDic dic, T1 t1, T2 t2, T3 t3) where T : AEventIndex<T, T1, T2, T3>
        {
            if (dic.TryGet<T>(out var easyEvent))
                ((EasyEvent<T1, T2, T3>)easyEvent).Invoke(t1, t2, t3);
        }
        public static void InvokeClassEvent<T, T1, T2, T3, T4>(this IEasyEventDic dic, T1 t1, T2 t2, T3 t3, T4 t4) where T : AEventIndex<T, T1, T2, T3, T4>
        {
            if (dic.TryGet<T>(out var easyEvent))
                ((EasyEvent<T1, T2, T3, T4>)easyEvent).Invoke(t1, t2, t3, t4);
        }
        public static void InvokeClassEvent<T, T1, T2, T3, T4, T5>(this IEasyEventDic dic, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) where T : AEventIndex<T, T1, T2, T3, T4, T5>
        {
            if (dic.TryGet<T>(out var easyEvent))
                ((EasyEvent<T1, T2, T3, T4, T5>)easyEvent).Invoke(t1, t2, t3, t4, t5);
        }
        
        public static bool TryGet<T>(this IEasyEventDic dic, out IEasyEvent easyEvent)=>dic.TryGet<T>(out easyEvent);

        public static EasyEvent Get<T>(this IEasyEventDic dic)
        {
            if (dic.TryGet<T>(out var easyEvent))
                return (EasyEvent)easyEvent;
            return default;
        }
        public static EasyEvent<T1> Get<T, T1>(this IEasyEventDic dic)
        {
            if (dic.TryGet<T>(out var easyEvent))
                return (EasyEvent<T1>)easyEvent;
            return default;
        }
        public static EasyEvent<T1, T2> Get<T, T1, T2>(this IEasyEventDic dic)
        {
            if (dic.TryGet<T>(out var easyEvent))
                return (EasyEvent<T1, T2>)easyEvent;
            return default;
        }
        public static EasyEvent<T1, T2, T3> Get<T, T1, T2, T3>(this IEasyEventDic dic)
        {
            if (dic.TryGet<T>(out var easyEvent))
                return (EasyEvent<T1, T2, T3>)easyEvent;
            return default;
        }
        public static EasyEvent<T1, T2, T3, T4> Get<T, T1, T2, T3, T4>(this IEasyEventDic dic)
        {
            if (dic.TryGet<T>(out var easyEvent))
                return (EasyEvent<T1, T2, T3, T4>)easyEvent;
            return default;
        }
        public static EasyEvent<T1, T2, T3, T4, T5> Get<T, T1, T2, T3, T4, T5>(this IEasyEventDic dic)
        {
            if (dic.TryGet<T>(out var easyEvent))
                return (EasyEvent<T1, T2, T3, T4, T5>)easyEvent;
            return default;
        }
        
        
        public static EasyEvent GetOrAddEvent<T>(this IEasyEventDic dic) where T : IEventIndex<T>
        {
            return dic.GetOrAddEvent<T>();
        }
        public static EasyEvent<T1> GetOrAddEvent<T, T1>(this IEasyEventDic dic) where T : AEventIndex<T, T1>
        {
            return dic.GetOrAddEvent<T, T1>();
        }
        public static EasyEvent<T1, T2> GetOrAddEvent<T, T1, T2>(this IEasyEventDic dic) where T : AEventIndex<T, T1, T2>
        {
            return dic.GetOrAddEvent<T, T1, T2>();
        }
        public static EasyEvent<T1, T2, T3> GetOrAddEvent<T, T1, T2, T3>(this IEasyEventDic dic) where T : AEventIndex<T, T1, T2, T3>
        {
            return dic.GetOrAddEvent<T, T1, T2, T3>();
        }
        public static EasyEvent<T1, T2, T3, T4> GetOrAddEvent<T, T1, T2, T3, T4>(this IEasyEventDic dic) where T : AEventIndex<T, T1, T2, T3, T4>
        {
            return dic.GetOrAddEvent<T, T1, T2, T3, T4>();
        }
        public static EasyEvent<T1, T2, T3, T4, T5> GetOrAddEvent<T, T1, T2, T3, T4, T5>(this IEasyEventDic dic) where T : AEventIndex<T, T1, T2, T3, T4, T5>
        {
            return dic.GetOrAddEvent<T, T1, T2, T3, T4, T5>();
        }
        
        public static void Clear<T>(this IEasyEventDic dic) =>dic.Clear<T>();

        public static void ClearAll(this IEasyEventDic dic) { dic.ClearAll(); }
    }
}