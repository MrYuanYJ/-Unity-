using UnityEngine;
using UnityEngine.Events;

namespace EasyFramework.EventKit
{
    public interface IUnityEventBinder
    {
        UnityEvent Register{ get; }
    }

    public interface IUnityEventBinder<T> : IUnityEventBinder
    {
        UnityEvent<T> TRegister { get; }
    }
    public abstract class AUnityEventBinder: MonoBehaviour,IUnityEventBinder
    {
        protected EasyEvent easyEvent=new EasyEvent();
        [SerializeField]protected UnityEvent registerEvent=new UnityEvent();
        
        public EasyEvent Event => easyEvent;
        public UnityEvent Register => registerEvent;

        public void Invoke()
        {
            easyEvent?.Invoke();
            registerEvent?.Invoke();
        }
    }
    public abstract class AUnityEventBinder<T>: MonoBehaviour,IUnityEventBinder<T>
    {
        protected EasyEvent<T> easyEvent=new EasyEvent<T>();
        [SerializeField]protected UnityEvent registerEvent=new UnityEvent();
        [SerializeField]protected UnityEvent<T> tRegisterEvent=new UnityEvent<T>();


        public EasyEvent<T> Event => easyEvent;
        public UnityEvent Register => registerEvent;
        public UnityEvent<T> TRegister => tRegisterEvent;

        public void Invoke(T t)
        {
            easyEvent?.Invoke(t);
            registerEvent?.Invoke();
            tRegisterEvent?.Invoke(t);
        }
    }
}