using EasyFramework.EventKit;

namespace EasyFramework
{
    public interface IActiveAble: IEasyLife
    {
        ESProperty<bool> IsActive{ get; set; }
        IEasyEvent ActiveEvent { get; }
        IEasyEvent UnActiveEvent { get; }

        void ActiveChange()
        {
            if (IsActive.Value)
                ActiveInvoke();
            else
                UnActiveInvoke();
        }

        public void ActiveInvoke()
        {
            GlobalEvent.Enable.InvokeEvent(this);
            OnActive();
            ActiveEvent.BaseInvoke();
        }
        public void UnActiveInvoke()
        {
            GlobalEvent.Disable.InvokeEvent(this);
            OnUnActive();
            UnActiveEvent.BaseInvoke();
        }


        protected void OnActive();
        protected void OnUnActive();
    }
}