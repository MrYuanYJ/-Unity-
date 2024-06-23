using System.Collections;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public interface IInitAble
    {
        bool IsInit { get; set; }
        IEasyEvent InitEvent { get;}
        void Init()
        {
            if(!BeforeInitEvent(this)) return;
            InitEvent.BaseInvoke();
            AfterInitEvent(this);
        }
        protected void OnInit();
        protected void InitDo()=>GlobalEvent.InitDo.InvokeEvent(this);
        
        public static bool BeforeInitEvent(IInitAble self)
        {
            if (self.IsInit) return false;
            self.IsInit = true;
            self.OnInit();
            
            return true;
        }
        public static void AfterInitEvent(IInitAble self)
        {
            self.InitDo();
        }
    }
}