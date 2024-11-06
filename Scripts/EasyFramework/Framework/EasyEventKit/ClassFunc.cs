using System;

namespace EasyFramework
{
    #region AFuncIndex
    
    public interface IFuncIndex{}
    public abstract class IFuncIndex<T>:IFuncIndex where T : IFuncIndex<T>
    {
        public static void ClearEvent() => EasyFuncDic.Global.Clear<T>();
        public static void ClearEvent(IGetEasyFuncDic f) => f.FuncDic.Clear<T>();
        public static IEasyFunc TryGet() => EasyFuncDic.Global.TryGet<T>(out var func)? func : null;
        public static IEasyFunc TryGet(IGetEasyFuncDic f) => f.FuncDic.TryGet<T>(out var func)? func : null;
    }

    public abstract class IFuncIndex<T, R> : IFuncIndex<T> where T : IFuncIndex<T, R>
    {
        public static R[] BaseInvokeAndReturnAll() => EasyFuncDic.Global.InvokeAndReturnAllClassFunc<T,R>();
        public static R[] BaseInvokeAndReturnAll(IGetEasyFuncDic f) => f.FuncDic.InvokeAndReturnAllClassFunc<T,R>();
        public static int BaseInvokeAndReturnAllNonAlloc(R[] results) => EasyFuncDic.Global.InvokeAndReturnAllClassFuncNonAlloc<T,R>(results);
        public static int BaseInvokeAndReturnAllNonAlloc(IGetEasyFuncDic f, R[] results) => f.FuncDic.InvokeAndReturnAllClassFuncNonAlloc<T,R>(results);
        public static R BaseInvoke() => EasyFuncDic.Global.InvokeClassFunc<T,R>();
        public static R BaseInvoke(IGetEasyFuncDic f) => f.FuncDic.InvokeClassFunc<T,R>();
    }
    public abstract class AFuncIndex<T, R> : IFuncIndex<T,R> where T : AFuncIndex<T, R>
    {
        public static IUnRegisterHandle RegisterFunc(Func<R> a) => EasyFuncDic.Global.RegisterClassFunc<T,R>(a);
        public static IUnRegisterHandle RegisterFunc(IGetEasyFuncDic f, Func<R> a) => f.FuncDic.RegisterClassFunc<T,R>(a);
        public static void UnRegisterFunc(Func<R> a) => EasyFuncDic.Global.UnRegisterClassFunc<T,R>(a);
        public static void UnRegisterFunc(IGetEasyFuncDic f, Func<R> a) => f.FuncDic.UnRegisterClassFunc<T,R>(a);
        public static R[] InvokeAndReturnAll() => EasyFuncDic.Global.InvokeAndReturnAllClassFunc<T,R>();
        public static R[] InvokeAndReturnAll(IGetEasyFuncDic f) => f.FuncDic.InvokeAndReturnAllClassFunc<T,R>();
        public static int InvokeAndReturnAllNonAlloc(R[] results) => EasyFuncDic.Global.InvokeAndReturnAllClassFuncNonAlloc<T,R>(results);
        public static int InvokeAndReturnAllNonAlloc(IGetEasyFuncDic f, R[] results) => f.FuncDic.InvokeAndReturnAllClassFuncNonAlloc<T,R>(results);
        public static R InvokeFunc() => EasyFuncDic.Global.InvokeClassFunc<T,R>();
        public static R InvokeFunc(IGetEasyFuncDic f) => f.FuncDic.InvokeClassFunc<T,R>();
    }

    public abstract class AFuncIndex<T, T1, R> : IFuncIndex<T,R> where T : AFuncIndex<T, T1, R>
    {
        public static IUnRegisterHandle RegisterFunc(Func<R> a) => EasyFuncDic.Global.GetOrAddFunc<T,T1,R>().Register(a);
        public static IUnRegisterHandle RegisterFunc(IGetEasyFuncDic f, Func<R> a) => f.FuncDic.GetOrAddFunc<T,T1,R>().Register(a);
        public static void UnRegisterFunc(Func<R> a) => EasyFuncDic.Global.Get<T,T1,R>().UnRegister(a);
        public static void UnRegisterFunc(IGetEasyFuncDic f, Func<R> a) => f.FuncDic.Get<T,T1,R>().UnRegister(a);
        public static IUnRegisterHandle RegisterFunc(Func<T1, R> a) => EasyFuncDic.Global.RegisterClassFunc<T, T1, R>(a);
        public static IUnRegisterHandle RegisterFunc(IGetEasyFuncDic f, Func<T1, R> a) => f.FuncDic.RegisterClassFunc<T, T1, R>(a);
        public static void UnRegisterFunc(Func<T1, R> a) => EasyFuncDic.Global.UnRegisterClassFunc<T, T1, R>(a);
        public static void UnRegisterFunc(IGetEasyFuncDic f, Func<T1, R> a) => f.FuncDic.UnRegisterClassFunc<T, T1, R>(a);
        public static R[] InvokeAndReturnAll(T1 t1) => EasyFuncDic.Global.InvokeAndReturnAllClassFunc<T, T1, R>(t1);
        public static R[] InvokeAndReturnAll(IGetEasyFuncDic f, T1 t1) => f.FuncDic.InvokeAndReturnAllClassFunc<T, T1, R>(t1);
        public static int InvokeAndReturnAllNonAlloc(T1 t1, R[] results) => EasyFuncDic.Global.InvokeAndReturnAllClassFuncNonAlloc<T, T1, R>(t1, results);
        public static int InvokeAndReturnAllNonAlloc(IGetEasyFuncDic f, T1 t1, R[] results) => f.FuncDic.InvokeAndReturnAllClassFuncNonAlloc<T, T1, R>(t1, results);
        public static R InvokeFunc(T1 t1) => EasyFuncDic.Global.InvokeClassFunc<T, T1, R>(t1);
        public static R InvokeFunc(IGetEasyFuncDic f, T1 t1) => f.FuncDic.InvokeClassFunc<T, T1, R>(t1);
    }
    public abstract class AFuncIndex<T, T1, T2, R> : IFuncIndex<T,R> where T : AFuncIndex<T, T1, T2, R>
    {
        public static IUnRegisterHandle RegisterFunc(Func<R> a) => EasyFuncDic.Global.GetOrAddFunc<T,T1,T2,R>().Register(a);
        public static IUnRegisterHandle RegisterFunc(IGetEasyFuncDic f, Func<R> a) => f.FuncDic.GetOrAddFunc<T,T1,T2,R>().Register(a);
        public static void UnRegisterFunc(Func<R> a) => EasyFuncDic.Global.Get<T,T1,T2,R>().UnRegister(a);
        public static void UnRegisterFunc(IGetEasyFuncDic f, Func<R> a) => f.FuncDic.Get<T,T1,T2,R>().UnRegister(a);
        public static IUnRegisterHandle RegisterFunc(Func<T1, T2, R> a) => EasyFuncDic.Global.RegisterClassFunc<T, T1, T2, R>(a);
        public static IUnRegisterHandle RegisterFunc(IGetEasyFuncDic f, Func<T1, T2, R> a) => f.FuncDic.RegisterClassFunc<T, T1, T2, R>(a);
        public static void UnRegisterFunc(Func<T1, T2, R> a) => EasyFuncDic.Global.UnRegisterClassFunc<T, T1, T2, R>(a);
        public static void UnRegisterFunc(IGetEasyFuncDic f, Func<T1, T2, R> a) => f.FuncDic.UnRegisterClassFunc<T, T1, T2, R>(a);
        public static R[] InvokeAndReturnAll(T1 t1, T2 t2) => EasyFuncDic.Global.InvokeAndReturnAllClassFunc<T, T1, T2, R>(t1, t2);
        public static R[] InvokeAndReturnAll(IGetEasyFuncDic f, T1 t1, T2 t2) => f.FuncDic.InvokeAndReturnAllClassFunc<T, T1, T2, R>(t1, t2);
        public static int InvokeAndReturnAllNonAlloc(T1 t1, T2 t2, R[] results) => EasyFuncDic.Global.InvokeAndReturnAllClassFuncNonAlloc<T, T1, T2, R>(t1, t2, results);
        public static int InvokeAndReturnAllNonAlloc(IGetEasyFuncDic f, T1 t1, T2 t2, R[] results) => f.FuncDic.InvokeAndReturnAllClassFuncNonAlloc<T, T1, T2, R>(t1, t2, results);
        public static R InvokeFunc(T1 t1, T2 t2) => EasyFuncDic.Global.InvokeClassFunc<T, T1, T2, R>(t1, t2);
        public static R InvokeFunc(IGetEasyFuncDic f, T1 t1, T2 t2) => f.FuncDic.InvokeClassFunc<T, T1, T2, R>(t1, t2);
    }
    public abstract class AFuncIndex<T, T1, T2, T3, R> : IFuncIndex<T,R> where T : AFuncIndex<T, T1, T2, T3, R>
    {
        public static IUnRegisterHandle RegisterFunc(Func<R> a) => EasyFuncDic.Global.GetOrAddFunc<T,T1,T2,T3,R>().Register(a);
        public static IUnRegisterHandle RegisterFunc(IGetEasyFuncDic f, Func<R> a) => f.FuncDic.GetOrAddFunc<T,T1,T2,T3,R>().Register(a);
        public static void UnRegisterFunc(Func<R> a) => EasyFuncDic.Global.Get<T,T1,T2,T3,R>().UnRegister(a);
        public static void UnRegisterFunc(IGetEasyFuncDic f, Func<R> a) => f.FuncDic.Get<T,T1,T2,T3,R>().UnRegister(a);
        public static IUnRegisterHandle RegisterFunc(Func<T1, T2, T3, R> a) => EasyFuncDic.Global.RegisterClassFunc<T, T1, T2, T3, R>(a);
        public static IUnRegisterHandle RegisterFunc(IGetEasyFuncDic f, Func<T1, T2, T3, R> a) => f.FuncDic.RegisterClassFunc<T, T1, T2, T3, R>(a);
        public static void UnRegisterFunc(Func<T1, T2, T3, R> a) => EasyFuncDic.Global.UnRegisterClassFunc<T, T1, T2, T3, R>(a);
        public static void UnRegisterFunc(IGetEasyFuncDic f, Func<T1, T2, T3, R> a) => f.FuncDic.UnRegisterClassFunc<T, T1, T2, T3, R>(a);
        public static R[] InvokeAndReturnAll(T1 t1, T2 t2, T3 t3) => EasyFuncDic.Global.InvokeAndReturnAllClassFunc<T, T1, T2, T3, R>(t1, t2, t3);
        public static R[] InvokeAndReturnAll(IGetEasyFuncDic f, T1 t1, T2 t2, T3 t3) => f.FuncDic.InvokeAndReturnAllClassFunc<T, T1, T2, T3, R>(t1, t2, t3);
        public static int InvokeAndReturnAllNonAlloc(T1 t1, T2 t2, T3 t3, R[] results) => EasyFuncDic.Global.InvokeAndReturnAllClassFuncNonAlloc<T, T1, T2, T3, R>(t1, t2, t3, results);
        public static int InvokeAndReturnAllNonAlloc(IGetEasyFuncDic f, T1 t1, T2 t2, T3 t3, R[] results) => f.FuncDic.InvokeAndReturnAllClassFuncNonAlloc<T, T1, T2, T3, R>(t1, t2, t3, results);
        public static R InvokeFunc(T1 t1, T2 t2, T3 t3) => EasyFuncDic.Global.InvokeClassFunc<T, T1, T2, T3, R>(t1, t2, t3);
        public static R InvokeFunc(IGetEasyFuncDic f, T1 t1, T2 t2, T3 t3) => f.FuncDic.InvokeClassFunc<T, T1, T2, T3, R>(t1, t2, t3);
    }
    
    public abstract class AFuncIndex<T, T1, T2, T3, T4, R> : IFuncIndex<T,R> where T : AFuncIndex<T, T1, T2, T3, T4, R>
    {
        public static IUnRegisterHandle RegisterFunc(Func<R> a) => EasyFuncDic.Global.GetOrAddFunc<T,T1,T2,T3,T4,R>().Register(a);
        public static IUnRegisterHandle RegisterFunc(IGetEasyFuncDic f, Func<R> a) => f.FuncDic.GetOrAddFunc<T,T1,T2,T3,T4,R>().Register(a);
        public static void UnRegisterFunc(Func<R> a) => EasyFuncDic.Global.Get<T,T1,T2,T3,T4,R>().UnRegister(a);
        public static void UnRegisterFunc(IGetEasyFuncDic f, Func<R> a) => f.FuncDic.Get<T,T1,T2,T3,T4,R>().UnRegister(a);
        public static IUnRegisterHandle RegisterFunc(Func<T1, T2, T3, T4, R> a) => EasyFuncDic.Global.RegisterClassFunc<T, T1, T2, T3, T4, R>(a);
        public static IUnRegisterHandle RegisterFunc(IGetEasyFuncDic f, Func<T1, T2, T3, T4, R> a) => f.FuncDic.RegisterClassFunc<T, T1, T2, T3, T4, R>(a);
        public static void UnRegisterFunc(Func<T1, T2, T3, T4, R> a) => EasyFuncDic.Global.UnRegisterClassFunc<T, T1, T2, T3, T4, R>(a);
        public static void UnRegisterFunc(IGetEasyFuncDic f, Func<T1, T2, T3, T4, R> a) => f.FuncDic.UnRegisterClassFunc<T, T1, T2, T3, T4, R>(a);
        public static R[] InvokeAndReturnAll(T1 t1, T2 t2, T3 t3, T4 t4) => EasyFuncDic.Global.InvokeAndReturnAllClassFunc<T, T1, T2, T3, T4, R>(t1, t2, t3, t4);
        public static R[] InvokeAndReturnAll(IGetEasyFuncDic f, T1 t1, T2 t2, T3 t3, T4 t4) => f.FuncDic.InvokeAndReturnAllClassFunc<T, T1, T2, T3, T4, R>(t1, t2, t3, t4);
        public static int InvokeAndReturnAllNonAlloc(T1 t1, T2 t2, T3 t3, T4 t4, R[] results) => EasyFuncDic.Global.InvokeAndReturnAllClassFuncNonAlloc<T, T1, T2, T3, T4, R>(t1, t2, t3, t4, results);
        public static int InvokeAndReturnAllNonAlloc(IGetEasyFuncDic f, T1 t1, T2 t2, T3 t3, T4 t4, R[] results) => f.FuncDic.InvokeAndReturnAllClassFuncNonAlloc<T, T1, T2, T3, T4, R>(t1, t2, t3, t4, results);
        public static R InvokeFunc(T1 t1, T2 t2, T3 t3, T4 t4) => EasyFuncDic.Global.InvokeClassFunc<T, T1, T2, T3, T4, R>(t1, t2, t3, t4);
        public static R InvokeFunc(IGetEasyFuncDic f, T1 t1, T2 t2, T3 t3, T4 t4) => f.FuncDic.InvokeClassFunc<T, T1, T2, T3, T4, R>(t1, t2, t3, t4);
    }
    
    public abstract class AFuncIndex<T, T1, T2, T3, T4, T5, R> : IFuncIndex<T,R> where T : AFuncIndex<T, T1, T2, T3, T4, T5, R>
    {
        public static IUnRegisterHandle RegisterFunc(Func<R> a) => EasyFuncDic.Global.GetOrAddFunc<T,T1,T2,T3,T4,T5,R>().Register(a);
        public static IUnRegisterHandle RegisterFunc(IGetEasyFuncDic f, Func<R> a) => f.FuncDic.GetOrAddFunc<T,T1,T2,T3,T4,T5,R>().Register(a);
        public static void UnRegisterFunc(Func<R> a) => EasyFuncDic.Global.Get<T,T1,T2,T3,T4,T5,R>().UnRegister(a);
        public static void UnRegisterFunc(IGetEasyFuncDic f, Func<R> a) => f.FuncDic.Get<T,T1,T2,T3,T4,T5,R>().UnRegister(a);
        public static IUnRegisterHandle RegisterFunc(Func<T1, T2, T3, T4, T5, R> a) => EasyFuncDic.Global.RegisterClassFunc<T, T1, T2, T3, T4, T5, R>(a);
        public static IUnRegisterHandle RegisterFunc(IGetEasyFuncDic f, Func<T1, T2, T3, T4, T5, R> a) => f.FuncDic.RegisterClassFunc<T, T1, T2, T3, T4, T5, R>(a);
        public static void UnRegisterFunc(Func<T1, T2, T3, T4, T5, R> a) => EasyFuncDic.Global.UnRegisterClassFunc<T, T1, T2, T3, T4, T5, R>(a);
        public static void UnRegisterFunc(IGetEasyFuncDic f, Func<T1, T2, T3, T4, T5, R> a) => f.FuncDic.UnRegisterClassFunc<T, T1, T2, T3, T4, T5, R>(a);
        public static R[] InvokeAndReturnAll(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) => EasyFuncDic.Global.InvokeAndReturnAllClassFunc<T, T1, T2, T3, T4, T5, R>(t1, t2, t3, t4, t5);
        public static R[] InvokeAndReturnAll(IGetEasyFuncDic f, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) => f.FuncDic.InvokeAndReturnAllClassFunc<T, T1, T2, T3, T4, T5, R>(t1, t2, t3, t4, t5);
        public static int InvokeAndReturnAllNonAlloc(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, R[] results) => EasyFuncDic.Global.InvokeAndReturnAllClassFuncNonAlloc<T, T1, T2, T3, T4, T5, R>(t1, t2, t3, t4, t5, results);
        public static int InvokeAndReturnAllNonAlloc(IGetEasyFuncDic f, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, R[] results) => f.FuncDic.InvokeAndReturnAllClassFuncNonAlloc<T, T1, T2, T3, T4, T5, R>(t1, t2, t3, t4, t5, results);
        public static R InvokeFunc(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) => EasyFuncDic.Global.InvokeClassFunc<T, T1, T2, T3, T4, T5, R>(t1, t2, t3, t4, t5);
        public static R InvokeFunc(IGetEasyFuncDic f, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) => f.FuncDic.InvokeClassFunc<T, T1, T2, T3, T4, T5, R>(t1, t2, t3, t4, t5);
    }

    #endregion
    public static class EasyFuncDicExtensions
    {
        public static IUnRegisterHandle RegisterClassFunc<T,R>(this IEasyFuncDic dic, Func<R> a) where T : IFuncIndex<T, R>
        {
            return dic.GetOrAddFunc<T, R>().Register(a);
        }
        public static IUnRegisterHandle RegisterClassFunc<T, T1, R>(this IEasyFuncDic dic, Func<T1, R> a) where T : AFuncIndex<T, T1, R>
        {
            return dic.GetOrAddFunc<T, T1, R>().Register(a);
        }
        public static IUnRegisterHandle RegisterClassFunc<T, T1, T2, R>(this IEasyFuncDic dic, Func<T1, T2, R> a) where T : AFuncIndex<T, T1, T2, R>
        {
            return dic.GetOrAddFunc<T, T1, T2, R>().Register(a);
        }
        public static IUnRegisterHandle RegisterClassFunc<T, T1, T2, T3, R>(this IEasyFuncDic dic, Func<T1, T2, T3, R> a) where T : AFuncIndex<T, T1, T2, T3, R>
        {
            return dic.GetOrAddFunc<T, T1, T2, T3, R>().Register(a);
        }
        public static IUnRegisterHandle RegisterClassFunc<T, T1, T2, T3, T4, R>(this IEasyFuncDic dic, Func<T1, T2, T3, T4, R> a) where T : AFuncIndex<T, T1, T2, T3, T4, R>
        {
            return dic.GetOrAddFunc<T, T1, T2, T3, T4, R>().Register(a);
        }
        public static IUnRegisterHandle RegisterClassFunc<T, T1, T2, T3, T4, T5, R>(this IEasyFuncDic dic, Func<T1, T2, T3, T4, T5, R> a) where T : AFuncIndex<T, T1, T2, T3, T4, T5, R>
        {
            return dic.GetOrAddFunc<T, T1, T2, T3, T4, T5, R>().Register(a);
        }

        public static void UnRegisterClassFunc<T, R>(this IEasyFuncDic dic, Func<R> a) where T : IFuncIndex<T, R>
        {
            if (dic.TryGet<T>(out var func))
                ((EasyFunc<R>) func).UnRegister(a);
        }
        public static void UnRegisterClassFunc<T, T1, R>(this IEasyFuncDic dic, Func<T1, R> a) where T : AFuncIndex<T, T1, R>
        {
            if (dic.TryGet<T>(out var func))
                ((EasyFunc<T1, R>) func).UnRegister(a);
        }
        public static void UnRegisterClassFunc<T, T1, T2, R>(this IEasyFuncDic dic, Func<T1, T2, R> a) where T : AFuncIndex<T, T1, T2, R>
        {
            if (dic.TryGet<T>(out var func))
                ((EasyFunc<T1, T2, R>) func).UnRegister(a);
        }
        public static void UnRegisterClassFunc<T, T1, T2, T3, R>(this IEasyFuncDic dic, Func<T1, T2, T3, R> a) where T : AFuncIndex<T, T1, T2, T3, R>
        {
            if (dic.TryGet<T>(out var func))
                ((EasyFunc<T1, T2, T3, R>) func).UnRegister(a);
        }
        public static void UnRegisterClassFunc<T, T1, T2, T3, T4, R>(this IEasyFuncDic dic, Func<T1, T2, T3, T4, R> a) where T : AFuncIndex<T, T1, T2, T3, T4, R>
        {
            if (dic.TryGet<T>(out var func))
                ((EasyFunc<T1, T2, T3, T4, R>) func).UnRegister(a);
        }
        public static void UnRegisterClassFunc<T, T1, T2, T3, T4, T5, R>(this IEasyFuncDic dic, Func<T1, T2, T3, T4, T5, R> a) where T : AFuncIndex<T, T1, T2, T3, T4, T5, R>
        {
            if (dic.TryGet<T>(out var func))
                ((EasyFunc<T1, T2, T3, T4, T5, R>) func).UnRegister(a);
        }

        public static R[] InvokeAndReturnAllClassFunc<T, R>(this IEasyFuncDic dic) where T : IFuncIndex<T, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((IEasyFunc<R>) func).BaseInvokeAndReturnAll();
            return null;
        }
        public static R[] InvokeAndReturnAllClassFunc<T, T1, R>(this IEasyFuncDic dic, T1 t1) where T : AFuncIndex<T, T1, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, R>) func).InvokeAndReturnAll(t1);
            return null;
        }
        public static R[] InvokeAndReturnAllClassFunc<T, T1, T2, R>(this IEasyFuncDic dic, T1 t1, T2 t2) where T : AFuncIndex<T, T1, T2, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, T2, R>) func).InvokeAndReturnAll(t1, t2);
            return null;
        }
        public static R[] InvokeAndReturnAllClassFunc<T, T1, T2, T3, R>(this IEasyFuncDic dic, T1 t1, T2 t2, T3 t3) where T : AFuncIndex<T, T1, T2, T3, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, T2, T3, R>) func).InvokeAndReturnAll(t1, t2, t3);
            return null;
        }
        public static R[] InvokeAndReturnAllClassFunc<T, T1, T2, T3, T4, R>(this IEasyFuncDic dic, T1 t1, T2 t2, T3 t3, T4 t4) where T : AFuncIndex<T, T1, T2, T3, T4, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, T2, T3, T4, R>) func).InvokeAndReturnAll(t1, t2, t3, t4);
            return null;
        }
        public static R[] InvokeAndReturnAllClassFunc<T, T1, T2, T3, T4, T5, R>(this IEasyFuncDic dic, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) where T : AFuncIndex<T, T1, T2, T3, T4, T5, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, T2, T3, T4, T5, R>) func).InvokeAndReturnAll(t1, t2, t3, t4, t5);
            return null;
        }

        public static int InvokeAndReturnAllClassFuncNonAlloc<T, R>(this IEasyFuncDic dic, R[] results) where T : IFuncIndex<T, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((IEasyFunc<R>) func).BaseInvokeAndReturnAllNonAlloc(results);
            return 0;
        }
        public static int InvokeAndReturnAllClassFuncNonAlloc<T, T1, R>(this IEasyFuncDic dic, T1 t1, R[] results) where T : AFuncIndex<T, T1, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, R>) func).InvokeAndReturnAllNonAlloc(t1, results);
            return 0;
        }
        public static int InvokeAndReturnAllClassFuncNonAlloc<T, T1, T2, R>(this IEasyFuncDic dic, T1 t1, T2 t2, R[] results) where T : AFuncIndex<T, T1, T2, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, T2, R>) func).InvokeAndReturnAllNonAlloc(t1, t2, results);
            return 0;
        }
        public static int InvokeAndReturnAllClassFuncNonAlloc<T, T1, T2, T3, R>(this IEasyFuncDic dic, T1 t1, T2 t2, T3 t3, R[] results) where T : AFuncIndex<T, T1, T2, T3, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, T2, T3, R>) func).InvokeAndReturnAllNonAlloc(t1, t2, t3, results);
            return 0;
        }
        public static int InvokeAndReturnAllClassFuncNonAlloc<T, T1, T2, T3, T4, R>(this IEasyFuncDic dic, T1 t1, T2 t2, T3 t3, T4 t4, R[] results) where T : AFuncIndex<T, T1, T2, T3, T4, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, T2, T3, T4, R>) func).InvokeAndReturnAllNonAlloc(t1, t2, t3, t4, results);
            return 0;
        }
        public static int InvokeAndReturnAllClassFuncNonAlloc<T, T1, T2, T3, T4, T5, R>(this IEasyFuncDic dic, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, R[] results) where T : AFuncIndex<T, T1, T2, T3, T4, T5, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, T2, T3, T4, T5, R>) func).InvokeAndReturnAllNonAlloc(t1, t2, t3, t4, t5, results);
            return 0;
        }

        public static R InvokeClassFunc<T,R>(this IEasyFuncDic dic) where T : IFuncIndex<T, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((IEasyFunc<R>) func).BaseInvoke();
            return default;
        }
        public static R InvokeClassFunc<T, T1, R>(this IEasyFuncDic dic, T1 t1) where T : AFuncIndex<T, T1, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, R>) func).Invoke(t1);
            return default;
        }
        public static R InvokeClassFunc<T, T1, T2, R>(this IEasyFuncDic dic, T1 t1, T2 t2) where T : AFuncIndex<T, T1, T2, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, T2, R>) func).Invoke(t1, t2);
            return default;
        }
        public static R InvokeClassFunc<T, T1, T2, T3, R>(this IEasyFuncDic dic, T1 t1, T2 t2, T3 t3) where T : AFuncIndex<T, T1, T2, T3, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, T2, T3, R>) func).Invoke(t1, t2, t3);
            return default;
        }
        public static R InvokeClassFunc<T, T1, T2, T3, T4, R>(this IEasyFuncDic dic, T1 t1, T2 t2, T3 t3, T4 t4) where T : AFuncIndex<T, T1, T2, T3, T4, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, T2, T3, T4, R>) func).Invoke(t1, t2, t3, t4);
            return default;
        }
        public static R InvokeClassFunc<T, T1, T2, T3, T4, T5, R>(this IEasyFuncDic dic, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) where T : AFuncIndex<T, T1, T2, T3, T4, T5, R>
        {
            if (dic.TryGet<T>(out var func))
                return ((EasyFunc<T1, T2, T3, T4, T5, R>) func).Invoke(t1, t2, t3, t4, t5);
            return default;
        }


        public static bool TryGet<T>(this IEasyFuncDic dic, out IEasyFunc func) => dic.TryGet<T>(out func);

        public static EasyFunc<R> Get<T, R>(this IEasyFuncDic dic) where T : IFuncIndex<T, R>
        {
            if (dic.TryGet<T>(out var func))
                return (EasyFunc<R>) func;
            return null;
        }
        public static EasyFunc<T1, R> Get<T, T1, R>(this IEasyFuncDic dic) where T : AFuncIndex<T, T1, R>
        {
            if (dic.TryGet<T>(out var func))
                return (EasyFunc<T1, R>) func;
            return null;
        }
        public static EasyFunc<T1, T2, R> Get<T, T1, T2, R>(this IEasyFuncDic dic) where T : AFuncIndex<T, T1, T2, R>
        {
            if (dic.TryGet<T>(out var func))
                return (EasyFunc<T1, T2, R>) func;
            return null;
        }
        public static EasyFunc<T1, T2, T3, R> Get<T, T1, T2, T3, R>(this IEasyFuncDic dic) where T : AFuncIndex<T, T1, T2, T3, R>
        {
            if (dic.TryGet<T>(out var func))
                return (EasyFunc<T1, T2, T3, R>) func;
            return null;
        }
        public static EasyFunc<T1, T2, T3, T4, R> Get<T, T1, T2, T3, T4, R>(this IEasyFuncDic dic) where T : AFuncIndex<T, T1, T2, T3, T4, R>
        {
            if (dic.TryGet<T>(out var func))
                return (EasyFunc<T1, T2, T3, T4, R>) func;
            return null;
        }
        public static EasyFunc<T1, T2, T3, T4, T5, R> Get<T, T1, T2, T3, T4, T5, R>(this IEasyFuncDic dic) where T : AFuncIndex<T, T1, T2, T3, T4, T5, R>
        {
            if (dic.TryGet<T>(out var func))
                return (EasyFunc<T1, T2, T3, T4, T5, R>) func;
            return null;
        }
        
        public static EasyFunc<R> GetOrAddFunc<T, R>(this IEasyFuncDic dic) where T : IFuncIndex<T, R> => dic.GetOrAddFunc<T, R>();
        public static EasyFunc<T1, R> GetOrAddFunc<T, T1, R>(this IEasyFuncDic dic) where T : AFuncIndex<T, T1, R> => dic.GetOrAddFunc<T, T1, R>();
        public static EasyFunc<T1, T2, R> GetOrAddFunc<T, T1, T2, R>(this IEasyFuncDic dic) where T : AFuncIndex<T, T1, T2, R> => dic.GetOrAddFunc<T, T1, T2, R>();
        public static EasyFunc<T1, T2, T3, R> GetOrAddFunc<T, T1, T2, T3, R>(this IEasyFuncDic dic) where T : AFuncIndex<T, T1, T2, T3, R> => dic.GetOrAddFunc<T, T1, T2, T3, R>();
        public static EasyFunc<T1, T2, T3, T4, R> GetOrAddFunc<T, T1, T2, T3, T4, R>(this IEasyFuncDic dic) where T : AFuncIndex<T, T1, T2, T3, T4, R> => dic.GetOrAddFunc<T, T1, T2, T3, T4, R>();
        public static EasyFunc<T1, T2, T3, T4, T5, R> GetOrAddFunc<T, T1, T2, T3, T4, T5, R>(this IEasyFuncDic dic) where T : AFuncIndex<T, T1, T2, T3, T4, T5, R> => dic.GetOrAddFunc<T, T1, T2, T3, T4, T5, R>();
        public static void Clear<T>(this IEasyFuncDic dic) =>dic.Clear<T>();
        public static void ClearAll(this IEasyFuncDic dic) { dic.ClearAll(); }
    }
}
