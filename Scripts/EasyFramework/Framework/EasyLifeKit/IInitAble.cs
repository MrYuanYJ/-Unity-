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
            InitEvent.Invoke();
            AfterInitEvent(this);
        }
        void OnInit();
        void InitDo()=>GlobalEvent.InitDo.InvokeEvent(this);
        
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

    public interface IInitAble<T> : IInitAble
    {
        new EasyEvent<T> InitEvent { get; set; }
        IEasyEvent IInitAble.InitEvent=>InitEvent;

        void Init(T t)
        {
            if(!BeforeInitEvent(this)) return;
            InitEvent.Invoke(t);
            AfterInitEvent(this);
        }
    }


    public interface IInitAble<T1, T2> : IInitAble
    {
        new EasyEvent<T1, T2> InitEvent { get; set; }
        IEasyEvent IInitAble.InitEvent=>InitEvent;

        void Init(T1 t1, T2 t2)
        {
            if(!BeforeInitEvent(this)) return;
            InitEvent.Invoke(t1, t2);
            AfterInitEvent(this);
        }
    }
}