namespace EasyFramework.EventKit
{
    public abstract class AutoEasyFunc<T,R>: IAutoRegisterEvent where T : struct
    {
        protected abstract R Run(T self);
        public IUnRegisterHandle Register()=>EasyFuncDic.Global.Register<T,R>(Run);

        public IUnRegisterHandle Register<TScope>(TScope scope) => GlobalEvent.GetEasyFuncDic(scope).Register<T,R>(Run);
        public void UnRegister()=>EasyFuncDic.Global.UnRegister<T,R>(Run);
        public void UnRegister<TScope>(TScope scope) => GlobalEvent.GetEasyFuncDic(scope).UnRegister<T,R>(Run);
    }
    public abstract class AutoEasyFunc<T>: IAutoRegisterEvent where T : struct
    {
        protected abstract IResult Run(T self);
        public IUnRegisterHandle Register()=>EasyFuncDic.Global.Register<T>(Run);
        public IUnRegisterHandle Register<TScope>(TScope scope) => GlobalEvent.GetEasyFuncDic(scope).Register<T>(Run);
        public void UnRegister()=>EasyFuncDic.Global.UnRegister<T>(Run);
        public void UnRegister<TScope>(TScope scope) => GlobalEvent.GetEasyFuncDic(scope).UnRegister<T>(Run);
    }
}