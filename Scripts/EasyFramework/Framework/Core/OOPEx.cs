using System;
using System.Threading;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public static class OOPEx
    {
        public static T GetModel<T>(this IGetModelAble self) where T : class, IModel => self.GetStructure().GetModel<T>();
        public static T GetSystem<T>(this IGetModelAble self) where T : class, ISystem => self.GetStructure().GetSystem<T>();
        
        public static IUnRegisterHandle RegisterEvent<T>(this IRegisterEventAble self,Action action) where T : struct => self.GetStructure().RegisterEvent<T>(action);
        public static IUnRegisterHandle RegisterEvent<T>(this IRegisterEventAble self, Action<T> action) where T : struct => self.GetStructure().RegisterEvent(action);

        public static void UnRegisterEvent<T>(this IRegisterEventAble self, Action action) where T : struct => self.GetStructure().UnRegisterEvent<T>(action);
        public static void UnRegisterEvent<T>(this IRegisterEventAble self, Action<T> action) where T : struct => self.GetStructure().UnRegisterEvent(action);
        
        public static IUnRegisterHandle RegisterFunc<T, TReturn>(this IRegisterEventAble self, Func<TReturn> func) where T : struct => self.GetStructure().RegisterFunc<T,TReturn>(func);
        public static IUnRegisterHandle RegisterFunc<T, TReturn>(this IRegisterEventAble self, Func<T, TReturn> func) where T : struct => self.GetStructure().RegisterFunc(func);
        public static IUnRegisterHandle RegisterFunc<T>(this IRegisterEventAble self, Func<IResult> func) where T : struct => self.GetStructure().RegisterFunc<T>(func);
        public static IUnRegisterHandle RegisterFunc<T>(this IRegisterEventAble self, Func<T, IResult> func) where T : struct => self.GetStructure().RegisterFunc(func);

        public static void UnRegisterFunc<T, TReturn>(this IRegisterEventAble self, Func<TReturn> func) where T : struct => self.GetStructure().UnRegisterFunc<T, TReturn>(func);
        public static void UnRegisterFunc<T, TReturn>(this IRegisterEventAble self, Func<T, TReturn> func) where T : struct => self.GetStructure().UnRegisterFunc(func);
        public static void UnRegisterFunc<T>(this IRegisterEventAble self, Func<IResult> func) where T : struct => self.GetStructure().UnRegisterFunc<T>(func);
        public static void UnRegisterFunc<T>(this IRegisterEventAble self, Func<T, IResult> func) where T : struct => self.GetStructure().UnRegisterFunc(func);

        public static void SendEvent<T>(this ISendEventAble self) where T : struct => self.GetStructure().SendEvent<T>();
        public static void SendEvent<T>(this ISendEventAble self,T t) where T : struct => self.GetStructure().SendEvent<T>(t);

        public static Results<TReturn> SendFunc<T, TReturn>(this ISendEventAble self) where T : struct => self.GetStructure().SendFunc<T, TReturn>();
        public static Results<TReturn> SendFunc<T,TReturn>(this ISendEventAble self,T t)where T : struct => self.GetStructure().SendFunc<T, TReturn>(t);
        public static Results<IResult> SendFunc<T>(this ISendEventAble self) where T : struct => self.GetStructure().SendFunc<T>();
        public static Results<IResult> SendFunc<T>(this ISendEventAble self,T t) where T : struct => self.GetStructure().SendFunc<T>(t);

        public static void SendCommand<T>(this ISendCommandAble self) where T : ICommand, new() => self.GetStructure().SendCommand<T>();
        public static void SendCommand<T>(this ISendCommandAble self,T command) where T : ICommand, new() => self.GetStructure().SendCommand<T>(command);
        public static TReturn SendCommand<TReturn>(this ISendCommandAble self,ICommand<TReturn> command)=> self.GetStructure().SendCommand<TReturn>(command);
        
        public static IUnRegisterHandle UnRegisterOnStructureDispose(this IUnRegisterHandle self,IGetStructureAble getStructureAble)
        {
            self.UnRegisterOnDispose(getStructureAble.GetStructure());
            return self;
        }
        public static IUnRegisterHandle UnRegisterOnStructureDispose(this IUnRegisterHandle self,IStructure structure)
        {
            self.UnRegisterOnDispose(structure);
            return self;
        }
    }
}