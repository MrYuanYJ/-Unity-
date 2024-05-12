using System;
using System.Threading;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public static class OOPEx
    {
        public static IUnRegisterHandle UnRegisterOnDispose(this IUnRegisterHandle self, IEasyLife easyLife)
        {
            easyLife.DisposeEvent.Register(self.UnRegister).OnlyPlayOnce();
            return self;
        }

        public static void RegisterOnDispose(this IEasyLife self, Action action) => self.DisposeEvent.Register(action).OnlyPlayOnce();

        public static CancellationTokenSource CancelOnDispose(this IEasyLife self)
        {
            CancellationTokenSource tokenSource = new();
            self.RegisterOnDispose(tokenSource.Cancel);
            return tokenSource;
        }

        public static CancellationTokenSource CancelOnDispose(this CancellationTokenSource self, IEasyLife disposable)
        {
            disposable.RegisterOnDispose(self.Cancel);
            return self;
        }

        public static T GetModel<T>(this IGetModelAble self) where T : class, IModel => self.GetStructure().GetModel<T>();
        public static T GetSystem<T>(this IGetModelAble self) where T : class, ISystem => self.GetStructure().GetSystem<T>();
        public static IUnRegisterHandle RegisterEvent<T>(this IRegisterEventAble self, Action<T> action) where T : struct => self.GetStructure().RegisterEvent(action);
        public static void UnRegisterEvent<T>(this IRegisterEventAble self, Action<T> action) where T : struct => self.GetStructure().UnRegisterEvent(action);
        public static IUnRegisterHandle RegisterFunc<T, TReturn>(this IRegisterEventAble self, Func<T, TReturn> func) where T : struct => self.GetStructure().RegisterFunc(func);
        public static IUnRegisterHandle RegisterFunc<T>(this IRegisterEventAble self, Func<T, IResult> func) where T : struct => self.GetStructure().RegisterFunc(func);

        public static void UnRegisterFunc<T, TReturn>(this IRegisterEventAble self, Func<T, TReturn> func) where T : struct => self.GetStructure().UnRegisterFunc(func);
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
            getStructureAble.GetStructure().DisposeEvent.Register(self.UnRegister).OnlyPlayOnce();
            return self;
        }
        public static IUnRegisterHandle UnRegisterOnStructureDispose(this IUnRegisterHandle self,IStructure structure)
        {
            structure.DisposeEvent.Register(self.UnRegister).OnlyPlayOnce();
            return self;
        }
    }
}