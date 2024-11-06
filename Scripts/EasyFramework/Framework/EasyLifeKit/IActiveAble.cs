
namespace EasyFramework
{
    public interface IActiveAble: IEasyLife
    {
        bool _isActive
        {
            get => IsActive;
            set
            {
                if(value==IsActive)
                    return;
                var enable = Enable;
                IsActive = value;
                if (!IsDispose 
                    &&(value && Enable ||!value && enable) )
                {
                    ActiveChange();
                }
            }
        }

        bool Enable => IsActive;
        bool IsActive { get; set; }

        IEasyEvent ActiveEvent { get; }
        IEasyEvent UnActiveEvent { get; }

        internal static void ActiveAbleInit(IActiveAble activeAble)
        {
            if (activeAble.Enable)
                activeAble.ActiveInvoke();
        }
        internal static void ActiveAbleDispose(IActiveAble activeAble)
        {
            if(activeAble.Enable)
                activeAble.UnActiveInvoke();
        }
        void ActiveChange()
        {
            if (IsActive)
                ActiveInvoke();
            else 
                UnActiveInvoke();
        }

        public void ActiveInvoke()
        {
            if(!IsInit)
                return;
            EasyLifeEx.EnableLifeCycle(this);
            OnActive();
            ActiveEvent.BaseInvoke();
        }
        public void UnActiveInvoke()
        {
            if(!IsInit)
                return;
            EasyLifeEx.DisableLifeCycle(this);
            OnUnActive();
            UnActiveEvent.BaseInvoke();
        }
        


        protected void OnActive();
        protected void OnUnActive();
    }
}