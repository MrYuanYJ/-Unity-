using System;
using System.Threading;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public static class OOPEx
    {
        public static T Model<T>(this IGetModelAble self) where T : class, IModel, new() => self.Structure.Model<T>();
        public static T System<T>(this IGetModelAble self) where T : class, ISystem, new() => self.Structure.System<T>();
        public static T GetModel<T>(this IGetModelAble self) where T : class, IModel => self.Structure.GetModel<T>();
        public static T GetSystem<T>(this IGetModelAble self) where T : class, ISystem => self.Structure.GetSystem<T>();
        
        public static IUnRegisterHandle RegisterEvent<T>(this IRegisterEventAble self,Action action) where T : struct => self.Structure.RegisterEvent<T>(action);
        public static IUnRegisterHandle RegisterEvent<T>(this IRegisterEventAble self, Action<T> action) where T : struct => self.Structure.RegisterEvent(action);

        public static void UnRegisterEvent<T>(this IRegisterEventAble self, Action action) where T : struct => self.Structure.UnRegisterEvent<T>(action);
        public static void UnRegisterEvent<T>(this IRegisterEventAble self, Action<T> action) where T : struct => self.Structure.UnRegisterEvent(action);
        
        public static IUnRegisterHandle RegisterFunc<T, TReturn>(this IRegisterEventAble self, Func<TReturn> func) where T : struct => self.Structure.RegisterFunc<T,TReturn>(func);
        public static IUnRegisterHandle RegisterFunc<T, TReturn>(this IRegisterEventAble self, Func<T, TReturn> func) where T : struct => self.Structure.RegisterFunc(func);
        public static IUnRegisterHandle RegisterFunc<T>(this IRegisterEventAble self, Func<IResult> func) where T : struct => self.Structure.RegisterFunc<T>(func);
        public static IUnRegisterHandle RegisterFunc<T>(this IRegisterEventAble self, Func<T, IResult> func) where T : struct => self.Structure.RegisterFunc(func);

        public static void UnRegisterFunc<T, TReturn>(this IRegisterEventAble self, Func<TReturn> func) where T : struct => self.Structure.UnRegisterFunc<T, TReturn>(func);
        public static void UnRegisterFunc<T, TReturn>(this IRegisterEventAble self, Func<T, TReturn> func) where T : struct => self.Structure.UnRegisterFunc(func);
        public static void UnRegisterFunc<T>(this IRegisterEventAble self, Func<IResult> func) where T : struct => self.Structure.UnRegisterFunc<T>(func);
        public static void UnRegisterFunc<T>(this IRegisterEventAble self, Func<T, IResult> func) where T : struct => self.Structure.UnRegisterFunc(func);

        public static void SendEvent<T>(this ISendEventAble self) where T : struct => self.Structure.SendEvent<T>();
        public static void SendEvent<T>(this ISendEventAble self,T t) where T : struct => self.Structure.SendEvent<T>(t);
        
        
        
        public static TReturn InvokeFunc<T, TReturn>(this ISendEventAble self) where T : struct => self.Structure.InvokeFunc<T, TReturn>();
        public static TReturn InvokeFunc<T, TReturn>(this ISendEventAble self,T t) where T : struct => self.Structure.InvokeFunc<T, TReturn>(t);
        public static IResult InvokeFunc<T>(this ISendEventAble self) where T : struct => self.Structure.InvokeFunc<T>();
        public static IResult InvokeFunc<T>(this ISendEventAble self,T t) where T : struct => self.Structure.InvokeFunc<T>(t);
        public static TReturn[] InvokeFuncAndReturnAll<T, TReturn>(this ISendEventAble self) where T : struct => self.Structure.InvokeFuncAndReturnAll<T, TReturn>();
        public static TReturn[] InvokeFuncAndReturnAll<T,TReturn>(this ISendEventAble self,T t)where T : struct => self.Structure.InvokeFuncAndReturnAll<T, TReturn>(t);
        public static IResult[] InvokeFuncAndReturnAll<T>(this ISendEventAble self) where T : struct => self.Structure.InvokeFuncAndReturnAll<T>();
        public static IResult[] InvokeFuncAndReturnAll<T>(this ISendEventAble self,T t) where T : struct => self.Structure.InvokeFuncAndReturnAll<T>(t);

        public static void SendCommand<T>(this ISendCommandAble self) where T : ICommand, new() => self.Structure.SendCommand<T>();
        public static void SendCommand<T>(this ISendCommandAble self,T command) where T : ICommand, new() => self.Structure.SendCommand<T>(command);
        public static TReturn SendCommand<TReturn>(this ISendCommandAble self,ICommand<TReturn> command)=> self.Structure.SendCommand<TReturn>(command);
        
        public static IUnRegisterHandle UnRegisterOnStructureDispose(this IUnRegisterHandle self,IGetStructureAble getStructureAble)
        {
            self.UnRegisterOnDispose(getStructureAble.Structure);
            return self;
        }
        public static IUnRegisterHandle UnRegisterOnStructureDispose(this IUnRegisterHandle self,IStructure structure)
        {
            self.UnRegisterOnDispose(structure);
            return self;
        }
    }
}