using EasyFramework.EventKit;

namespace EasyFramework
{
    // public interface IInitable
    // {
    //     bool IsInit { get; set; }
    //     EasyEvent<IInitable> InitEvent { get; set; }
    //     void Init()
    //     {
    //         if (IsInit) return;
    //         IsInit = true;
    //         OnInit();
    //         InitEvent.Invoke(this);
    //         InitDo();
    //     }
    //
    //     void OnInit();
    //     void InitDo()=>GlobalEvent.InitDo.InvokeEvent(this);
    // }
    //
    // public interface IStartable
    // {
    //     EasyEvent<IStartable> StartEvent { get; set; }
    //     void Start()
    //     {
    //         OnStart();
    //         StartEvent.Invoke(this);
    //     }
    //     void OnStart();
    // }
    //
    // public interface IDisposable: System.IDisposable
    // {
    //     bool IsDispose { get; set; }
    //     EasyEvent<IDisposable> DisposeEvent { get; set; }
    //
    //     void System.IDisposable.Dispose()
    //     {
    //         if (IsDispose) return;
    //         IsDispose = true;
    //         OnDispose();
    //         DisposeEvent.Invoke(this);
    //         DisposeDo();
    //     }
    //     void OnDispose();
    //     void DisposeDo() => GlobalEvent.DisposeDo.InvokeEvent(this);
    // }
    public interface IEasyLife : System.IDisposable
    {
        #region Init

        bool IsInit { get; set; }
        EasyEvent<IEasyLife> InitEvent { get; set; }
        void Init()
        {
            if (IsInit) return;
            IsInit = true;
            OnInit();
            InitDo();
            InitEvent.Invoke(this);
        }

        void OnInit();
        void InitDo()=>GlobalEvent.InitDo.InvokeEvent(this);

        #endregion
        
        #region Start
        
        EasyEvent<IEasyLife> StartEvent { get; set; }
        void Start()
        {
            OnStart();
            StartEvent.Invoke(this);
        }
        void OnStart();
        
        #endregion
        
        #region Dispose
        
        bool IsDispose { get=> !IsInit; set=>IsInit=!value; }
        EasyEvent<IEasyLife> DisposeEvent { get; set; }

        void System.IDisposable.Dispose()
        {
            if (IsDispose) return;
            IsDispose = true;
            OnDispose();
            DisposeEvent.Invoke(this);
            DisposeDo();
            InitEvent.Clear();
            StartEvent.Clear();
            DisposeEvent.Clear();
            if(this is IEasyUpdate update)
                update.UpdateEvent.Clear();
            if(this is IEasyFixedUpdate fixedUpdate)
                fixedUpdate.FixedUpdateEvent.Clear();
        }
        void OnDispose();
        void DisposeDo() => GlobalEvent.DisposeDo.InvokeEvent(this);
        
        #endregion
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
    }
}