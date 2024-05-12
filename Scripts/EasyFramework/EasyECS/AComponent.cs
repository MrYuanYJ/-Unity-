using System;

namespace EasyECS
{
    public abstract class AComponent : AbstractContainer, IComponent
    {
        public long ID { get; set; }
        public IEntity Entity { get; set; }
        public IComponentContainer Parent { get; set; }
        public IChildComponentContainer ChildContainer { get; set; }
        public Action OnDisposed { get; set; }


        public T GetParent<T>(Action<T> callBack) where T : IComponentContainer
        {
            return (T) Parent;
        }

        public T GetChildContainer<T>(Action<T> callBack) where T : IChildComponentContainer
        {
            return (T) ChildContainer;
        }
    }
}