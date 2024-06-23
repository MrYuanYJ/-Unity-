using EasyFramework.EventKit;

namespace EasyFramework
{
    public abstract class AEntity : IEntity
    {
        private readonly ContainerDic<IEntity> _container = new();
        private IEntity _parent;
        private IStructure _structure;
        public abstract IStructure Structure{ get; }
        public bool IsInit { get; set; }
        public bool IsStart { get; set; }
        public ESProperty<bool> IsActive { get; set; } = new(true);
        public IEasyEvent InitEvent { get; } = new EasyEvent();
        public IEasyEvent StartEvent { get; } = new EasyEvent();
        public IEasyEvent ActiveEvent { get; } = new EasyEvent();
        public IEasyEvent UnActiveEvent { get; } = new EasyEvent();
        public IEasyEvent DisposeEvent { get; } = new EasyEvent();

        void IInitAble.OnInit()
        {
            Structure.RegisterOnDispose(this.Dispose);
            OnInit();
            GlobalEvent.LifeCycleRegister<IEntity>.InvokeEvent(this);
        }

        void IInitAble.InitDo()
        {
            GlobalEvent.InitDo.InvokeEvent(this);
            _container.InitAll();
        }
        void IDisposeAble.OnDispose(bool usePool)
        {
            OnDispose(usePool);
            _container.DisposeAll();
            _container.Clear();
            if (_parent != null && !_parent.IsDispose)
                _parent.Container.Remove(_parent.GetType());
            _parent = null;
            BindObj = null;
            BindEntity = null;
        }

        void IStartAble.OnStart() => OnStart();
        void IActiveAble.OnActive() => OnActive();
        void IActiveAble.OnUnActive() => OnUnActive();

        protected virtual void OnInit() { }
        protected virtual void OnActive() { }
        protected virtual void OnStart() { }
        protected virtual void OnUnActive() { }
        protected virtual void OnDispose(bool usePool) { }
        public object BindObj { get; private set; }
        public IEntity BindEntity { get; private set; }
        public IEntity Parent => _parent;
        public ContainerDic<IEntity> Container => _container;

        void IEntity.EntityBind(object bindObj, IEntity bindEntity)
        {
            BindObj = bindObj;
            BindEntity = bindEntity;
        }

        void IEntity.SetParent(IEntity parent)
        {
            _parent = parent;
            if (parent != null)
            {
                BindEntity = _parent.BindEntity;
                foreach (IEntity entity in _container.Values)
                    entity.SetParent(this);
            }
        }
    }
}