namespace EasyFramework.EventKit
{
    public abstract class AutoEasyEvent<T>: IAutoRegisterEvent where T : struct
    {
        protected abstract void Run(T self);
        public void Register()=>EasyEventDic.Global.Register<T>(Run);
        public void Register<TScope>(TScope scope) => GlobalEvent.GetEasyEventDic(scope).Register<T>(Run);
        public void UnRegister()=>EasyEventDic.Global.UnRegister<T>(Run);
        public void UnRegister<TScope>(TScope scope) => GlobalEvent.GetEasyEventDic(scope).UnRegister<T>(Run);
    }
}