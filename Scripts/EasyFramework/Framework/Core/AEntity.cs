using EasyFramework.EventKit;

namespace EasyFramework
{
    public abstract class AEntity : IEntity
    {
        private readonly ContainerDic<IEntity> _container = new();
        private IEntity _parent;
        private IBindEntity _bind;
        public abstract IStructure GetStructure();
        public bool IsInit { get; set; }
        public EasyEvent<IInitAble> InitEvent { get; set; } = new();
        public EasyEvent<IStartAble> StartEvent { get; set; } = new();
        public EasyEvent<IDisposeAble> DisposeEvent { get; set; } = new();

        public virtual void OnInit()
        {
            GetStructure().RegisterOnDispose(this.Dispose);
            GlobalEvent.LifeCycleRegister.InvokeEvent(this);
            _container.InitAll();
        }
        public virtual void OnStart(){}
        public virtual void OnDispose()
        {
            _container.DisposeAll();
            _container.Clear();
            if (_parent != null && !_parent.IsDispose)
                _parent.Container.Remove(_parent.GetType());
            _parent = null;
            _bind = null;
        }
        public IBindEntity Bind => _bind;
        public IEntity Parent => _parent;
        public ContainerDic<IEntity> Container => _container;
        void IEntity.EntityBind(IBindEntity bind)=>_bind = bind;
        void IEntity.SetParent(IEntity parent)
        {
            _parent = parent;
            if (parent != null)
            {
                _bind = _parent.Bind;
                foreach (IEntity entity in _container.Values)
                    entity.EntityBind(_bind);
            }
        }
    }
}