namespace EasyFramework
{
    public abstract class AutoEasyEvent<T>: IAutoRegisterEvent where T : struct
    {
        protected abstract void Run(T self);
        public IUnRegisterHandle Register()=>EasyEventDic.Global.Register<T>(Run);
        public IUnRegisterHandle Register<TScope>(TScope scope) => GlobalEvent.GetEasyEventDic(scope).Register<T>(Run);
        public void UnRegister()=>EasyEventDic.Global.UnRegister<T>(Run);
        public void UnRegister<TScope>(TScope scope) => GlobalEvent.GetEasyEventDic(scope).UnRegister<T>(Run);
    }
}