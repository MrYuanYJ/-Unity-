
namespace EasyFramework
{
    public abstract class AEntity : IEntity
    {
        private readonly ContainerDic<IEntity> _container = new();
        private IEntity _parent;
        public abstract IStructure Structure { get; }
        public bool IsInit { get; set; }
        public bool InitDone { get; set; }
        public bool IsActive { get; set; } = true;
        public bool Enable => IsActive && (_parent?.Enable?? true);
        public IEasyEvent InitEvent { get; } = new EasyEvent();
        public IEasyEvent ActiveEvent { get; } = new EasyEvent();
        public IEasyEvent UnActiveEvent { get; } = new EasyEvent();
        public IEasyEvent DisposeEvent { get; } = new EasyEvent();

        void IInitAble.OnInit()
        {
            OnInit();
            GlobalEvent.LifeCycleRegister<IEntity>.InvokeEvent(this);
        }

        void IInitAble.InitDo()
        {
            EasyLifeCycle.InitDo.InvokeEvent(this);
            IActiveAble.ActiveAbleInit(this);
            _container.InitAll();
        }
        void IDisposeAble.OnDispose(bool usePool)
        {
            _container.DisposeAll();
            OnDispose(usePool);
            _container.Clear();
            if (_parent != null && !_parent.IsDispose)
                _parent.Container.Remove(_parent.GetType());
            _parent = null;
            BindObj = null;
            BindEntity = null;
        }
        void IActiveAble.OnActive()
        {
            OnActive();
            if(!InitDone)
                return;
            foreach (IEntity entity in _container.Values)
                if (entity.IsActive)
                {
                    entity.ActiveInvoke();
                }
        }

        void IActiveAble.OnUnActive()
        {
            if (InitDone)
            {
                foreach (IEntity entity in _container.Values)
                    if (entity.IsActive)
                    {
                        entity.UnActiveInvoke();
                    }
            }

            OnUnActive();
        }

        protected virtual void OnInit() { }
        protected virtual void OnActive() { }
        protected virtual void OnUnActive() { }
        protected virtual void OnDispose(bool usePool) { }
        public object BindObj { get; private set; }
        public IEntity BindEntity { get; private set; }
        public IEntity Parent => _parent;
        public ContainerDic<IEntity> Container => _container;

        void IEntity.SetBindObj(object bindObj)=> BindObj = bindObj;
        void IEntity.SetBindEntity(IEntity bindEntity)=> BindEntity = bindEntity;
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