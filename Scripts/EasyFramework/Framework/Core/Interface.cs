using System;
namespace EasyFramework
{
    public interface IGetModelAble:IGetStructureAble{}

    public interface IGetSystemAble:IGetStructureAble { }

    public interface IRegisterEventAble:IGetStructureAble { }

    public interface ISendEventAble:IGetStructureAble { }

    public interface ISetStructureAbleAble:IGetStructureAble
    {
        IStructure IGetStructureAble.Structure => Structure;
        new IStructure Structure { get; protected set; }
        void SetStructure(IStructure structure)
        {
            Structure = structure;
            if (structure!= null 
                && structure.IsInit 
                && this is IEasyLife easyLife)
            {
                easyLife.Init();
            }
        }
    }
    public interface IGetStructureAble
    {
        IStructure Structure { get; }
    }
    public interface IBindEntity
    {
        object BindObj => this;
        public IEntity BindEntity { get; }
    }
    public interface IStructure : IEasyLife,IGetEasyEventDic, IGetEasyFuncDic
    {
        public ContainerDic<object> Container { get;}
        public EasyEventDic Event { get; }
        public EasyFuncDic Func { get; }
        EasyEventDic IGetEasyEventDic.EventDic => Event;
        EasyFuncDic IGetEasyFuncDic.FuncDic => Func;
    }

    public static class StructureExtensions
    {
        public static T GetModel<T>(this IStructure self) where T : class, IModel => self.Container.Get<T>();
        public static T GetSystem<T>(this IStructure self) where T : class, ISystem => self.Container.Get<T>();
        public static T GetMember<T>(this IStructure self) where T : class, IEasyLife => self.Container.Get<T>();
        
        
        public static T RegisterModel<T>(this IStructure self) where T : class, IModel, new()
        {
            var t = new T();
            self.Container.Set(t);
            t.SetStructure(self);
            return t;
        }
        public static T RegisterSystem<T>(this IStructure self) where T : class, ISystem, new()
        {
            var t = new T();
            self.Container.Set(t);
            t.SetStructure(self);
            return t;
        }
        
        public static T Member<T>(this IStructure self,T member) where T : IEasyLife
        {
            if(member==null) return default;
            self.Container.Set(member);
            if(member is ISetStructureAbleAble setStructureAble)
                setStructureAble.SetStructure(self);
            else if (self.IsInit)
                member.Init();
            return member;
        }
        
        
        public static T Model<T>(this IStructure self) where T : class, IModel, new()
        {
            if (!self.Container.TryGet(out T model))
            {
                model=self.RegisterModel<T>();
                
            }
            return model;
        }
        public static T System<T>(this IStructure self) where T : class, ISystem, new()
        {
            if (!self.Container.TryGet(out T system))
            {
                system = self.RegisterSystem<T>();
            }
            return system;
        }
        
        
        public static void SendCommand<T>(this IStructure self) where T : ICommand, new()
        {
            var command = new T();
            command.SetStructure(self);
            command.Execute();
        }
        public static void SendCommand<T>(this IStructure self, T command) where T : ICommand, new()
        {
            command.SetStructure(self);
            command.Execute();
        }
        public static TReturn SendCommand<TReturn>(this IStructure self, ICommand<TReturn> command)
        {
            command.SetStructure(self);
            return command.Execute();
        }
        
        
        public static void SendEvent<T>(this IStructure self) where T : struct =>self.Event.Invoke<T>(default);
        public static void SendEvent<T>(this IStructure self, T t) where T : struct => self.Event.Invoke(t);
        
        
        public static TReturn InvokeFunc<T, TReturn>(this IStructure self) where T : struct => self.Func.Invoke<T, TReturn>(default);
        public static TReturn InvokeFunc<T, TReturn>(this IStructure self, T t) where T : struct => self.Func.Invoke<T, TReturn>(t);
        public static IResult InvokeFunc<T>(this IStructure self) where T : struct => self.Func.Invoke<T>(default);
        public static IResult InvokeFunc<T>(this IStructure self, T t) where T : struct => self.Func.Invoke(t);
        public static TReturn[] InvokeFuncAndReturnAll<T, TReturn>(this IStructure self) where T : struct => self.Func.InvokeAndReturnAll<T,TReturn>(default);
        public static TReturn[] InvokeFuncAndReturnAll<T, TReturn>(this IStructure self, T t) where T : struct => self.Func.InvokeAndReturnAll<T,TReturn>(t);
        public static IResult[] InvokeFuncAndReturnAll<T>(this IStructure self) where T : struct => self.Func.InvokeAndReturnAll<T>(default);
        public static IResult[] InvokeFuncAndReturnAll<T>(this IStructure self, T t) where T : struct => self.Func.InvokeAndReturnAll(t);
        
        
        public static IUnRegisterHandle RegisterEvent<T>(this IStructure self, Action action) where T : struct => self.Event.Register<T>(action);
        public static IUnRegisterHandle RegisterEvent<T>(this IStructure self, Action<T> action) where T : struct => self.Event.Register(action);
        
        
        public static void UnRegisterEvent<T>(this IStructure self, Action action) where T : struct => self.Event.UnRegister<T>(action);
        public static void UnRegisterEvent<T>(this IStructure self, Action<T> action) where T : struct => self.Event.UnRegister(action);
        
        
        public static IUnRegisterHandle RegisterFunc<T, TReturn>(this IStructure self, Func<TReturn> func)where T : struct => self.Func.Register<T, TReturn>(func);
        public static IUnRegisterHandle RegisterFunc<T, TReturn>(this IStructure self, Func<T, TReturn> func)where T : struct => self.Func.Register(func);
        public static IUnRegisterHandle RegisterFunc<T>(this IStructure self, Func<IResult> func)where T : struct => self.Func.Register<T>(func);
        public static IUnRegisterHandle RegisterFunc<T>(this IStructure self, Func<T, IResult> func)where T : struct => self.Func.Register(func);
        
        
        public static void UnRegisterFunc<T, TReturn>(this IStructure self, Func<TReturn> func)where T : struct => self.Func.UnRegister<T, TReturn>(func);
        public static void UnRegisterFunc<T, TReturn>(this IStructure self, Func<T, TReturn> func)where T : struct => self.Func.UnRegister(func);
        public static void UnRegisterFunc<T>(this IStructure self, Func<IResult> func)where T : struct => self.Func.UnRegister<T>(func);
        public static void UnRegisterFunc<T>(this IStructure self, Func<T, IResult> func)where T : struct => self.Func.UnRegister(func);
    }

    public interface IEntity : IActiveAble,IGetModelAble, IGetSystemAble, IRegisterEventAble, ISendEventAble, ISendCommandAble
    {
        public object BindObj { get; }
        public IEntity BindEntity { get; }
        public IEntity Parent { get; }
        public ContainerDic<IEntity> Container { get;}
        public void SetBindObj(object bindObj);
        public void SetBindEntity(IEntity bindEntity);
        public void EntityBind(object bindObj, IEntity bindEntity);
        public void SetParent(IEntity parent);
    }

    public interface IEntityChildOf<TParent> : IEntity where TParent : IEntity
    {
    }

    public static class EntityExtensions
    {
        public static IEntity AddEntity(this IEntity self,IEntity entity)
        {
            if(entity==null) return null;
            entity.Parent?.Container.Remove(entity.GetType());
            self.Container.Add(entity.GetType(), entity);
            entity.SetParent(self);
            if (self.IsInit)
                entity.Init();

            return entity;
        }
        public static T AddNewEntity<T>(this IEntity self,bool usePool=false) where T : class,IEntity, new()
        {
            var entity = ReferencePool.Fetch<T>(false, usePool);
            self.Container.Add(typeof(T), entity);
            entity.SetParent(self);
            if (self.IsInit)
                entity.Init();

            return entity;
        }
        public static IEntity AddNewEntity(this IEntity self,Type type,bool usePool=false)
        {
            var entity = (IEntity)ReferencePool.Fetch(type,false,usePool);
            self.Container.Add(type, entity);
            entity.SetParent(self);
            if (self.IsInit)
                entity.Init();

            return entity;
        }
        
        
        public static void RemoveEntity<T>(this IEntity self,bool usePool = false) where T : IEntity
        {
            if (self.Container.TryGet(typeof(T), out var entity))
                entity.Dispose(usePool);
        }
        public static void RemoveEntity(this IEntity self,Type type,bool usePool = false)
        {
            if (self.Container.TryGet(type, out var entity))
                entity.Dispose(usePool);
        }
        
        
        public static T GetEntity<T>(this IEntity self) where T : IEntity => (T) self.Container.Get(typeof(T));
        public static IEntity GetEntity(this IEntity self,Type type) => self.Container.Get(type);
        
        
        public static T Entity<T>(this IEntity self,bool usePool=false) where T : class, IEntity, new()
        {
            if(!self.Container.TryGet(typeof(T), out var e))
                e = self.AddNewEntity<T>(usePool);
            return (T)e;
        }
        public static IEntity Entity(this IEntity self, Type type,bool usePool=false)
        {
            if (!self.Container.TryGet(type, out var e))
                e = self.AddNewEntity(type,usePool);
            return e;
        }
        
        
        public static bool TryGetEntity<T>(this IEntity self,out T entity) where T : IEntity
        {
            if (self.Container.TryGet(typeof(T), out var e))
            {
                entity = (T) e;
                return true;
            }

            entity = default;
            return false;
        }
        public static bool TryGetEntity(this IEntity self,Type type, out IEntity entity)
        {
            if (self.Container.TryGet(type, out entity))
                return true;

            entity = default;
            return false;
        }
        
        
        public static bool HasEntity<T>(this IEntity self) where T : IEntity => self.Container.Has(typeof(T));
        public static bool HasEntity(this IEntity self,Type type) => self.Container.Has(type);
    }

    public interface IModel : IEasyLife,ISetStructureAbleAble,IRegisterEventAble  { }

    public interface ISystem : IActiveAble,IGetModelAble,ISetStructureAbleAble, IRegisterEventAble,ISendEventAble,ISendCommandAble { }
}
