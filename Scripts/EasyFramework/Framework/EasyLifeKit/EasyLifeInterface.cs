using EasyFramework.EventKit;

namespace EasyFramework
{
    public interface IInitAble
    {
        bool IsInit { get; set; }
        EasyEvent<IInitAble> InitEvent { get; set; }
        void Init()
        {
            if (IsInit) return;
            IsInit = true;
            OnInit();
            InitEvent.Invoke(this);
            InitDo();
        }
    
        void OnInit();
        void InitDo()=>GlobalEvent.InitDo.InvokeEvent(this);
    }
    
    public interface IStartAble
    {
        EasyEvent<IStartAble> StartEvent { get; set; }
        void Start()
        {
            OnStart();
            StartEvent.Invoke(this);
        }
        void OnStart();
    }
    
    public interface IDisposeAble: System.IDisposable
    {
        bool IsDispose { get; set; }
        
        EasyEvent<IDisposeAble> DisposeEvent { get; set; }
    
        void System.IDisposable.Dispose()=> Dispose();
        void Dispose(bool usePool=false)
        {
            if (IsDispose) return;
            IsDispose = true;
            OnDispose();
            DisposeEvent.Invoke(this);
            DisposeDo();
            DisposeEvent.Clear();
            if(this is IInitAble initAble)
                initAble.InitEvent.Clear();
            if(this is IEasyUpdate update)
                update.UpdateEvent.Clear();
            if(this is IEasyFixedUpdate fixedUpdate)
                fixedUpdate.FixedUpdateEvent.Clear();
            if (usePool)
                GlobalEvent.RecycleClass.InvokeEvent(this);
        }
        void OnDispose();
        void DisposeDo() => GlobalEvent.DisposeDo.InvokeEvent(this);
    }

    public interface IEasyLife : IInitAble, IStartAble, IDisposeAble
    {
        bool IDisposeAble.IsDispose
        {
            get => !IsInit;
            set => IsInit = !value;
        }
    }

    public interface IEasyUpdate : IEasyLife
    {
        EasyEvent<IEasyUpdate> UpdateEvent { get; set; }

        void Update()
        {
            OnUpdate();
            UpdateEvent.Invoke(this);
        }
        void OnUpdate();
    }

    public interface IEasyFixedUpdate : IEasyLife
    {
        EasyEvent<IEasyFixedUpdate> FixedUpdateEvent { get; set; }

        void FixedUpdate()
        {
            OnFixedUpdate();
            FixedUpdateEvent.Invoke(this);
        }
        void OnFixedUpdate();
    }

    public static class EasyLifeExtensions
    {
        public static void Init(this IEasyLife self)=> self.Init();
        public static void Dispose(this IEasyLife self)=> self.Dispose();
        public static void Dispose(this IEasyLife self,bool usePool)=> self.Dispose(usePool);
    }

    public static class EasyLifeCycleExtensions
    {
        public static T DisposeWith<T>(this T self, IDisposeAble disposable) where T : IDisposeAble
        {
            disposable.DisposeEvent.Register(self.Dispose).OnlyPlayOnce();
            return self;
        }
    }
}