

namespace EasyFramework
{
    public interface IInitAble
    {
        bool IsInit { get; set; }
        bool InitDone { get; set; }
        IEasyEvent InitEvent { get;}
        void Init()
        {
            if(!BeforeInitEvent(this)) return;
            InitEvent.BaseInvoke();
            AfterInitEvent(this);
        }
        protected void OnInit();
        protected void InitDo()
        {
            EasyLifeCycle.InitDo.InvokeEvent(this);
            if (this is IActiveAble activeAble)
                IActiveAble.ActiveAbleInit(activeAble);
            else
                EasyLifeEx.EnableLifeCycle(this);
        }

        public static bool BeforeInitEvent(IInitAble self)
        {
            if (self.IsInit) return false;
            self.IsInit = true;
            self.InitDone = false;
            self.OnInit();
            
            return true;
        }
        public static void AfterInitEvent(IInitAble self)
        {
            self.InitDo();
            self.InitDone = true;
        }
    }
}