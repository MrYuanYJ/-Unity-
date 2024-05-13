using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace EasyFramework.EventKit
{
    public interface IUnityEventBinder
    {
        IEasyEvent Event { get; }
        UnityEvent UnityEvent{ get; }
    }

    public interface IUnityEventBinder<T> : IUnityEventBinder
    {
        UnityEvent<T> TUnityEvent { get; }
    }
    public abstract class AUnityEventBinder: MonoBehaviour,IUnityEventBinder
    {
        protected EasyEvent easyEvent=new EasyEvent();
        [SerializeField]protected UnityEvent unityEvent=new UnityEvent();
        
        IEasyEvent IUnityEventBinder.Event => easyEvent;
        public EasyEvent Event => easyEvent;
        public UnityEvent UnityEvent => unityEvent;

        public void Invoke()
        {
            easyEvent?.Invoke();
            unityEvent?.Invoke();
        }
    }
    public abstract class AUnityEventBinder<T>: MonoBehaviour,IUnityEventBinder<T>
    {
        protected EasyEvent<T> easyEvent=new EasyEvent<T>();
        [SerializeField]protected UnityEvent unityEvent=new UnityEvent();
        [SerializeField]protected UnityEvent<T> tUnityEvent=new UnityEvent<T>();
        
        IEasyEvent IUnityEventBinder.Event => easyEvent;
        public EasyEvent<T> Event => easyEvent;
        public UnityEvent UnityEvent => unityEvent;
        public UnityEvent<T> TUnityEvent => tUnityEvent;

        public void Invoke(T t)
        {
            easyEvent?.Invoke(t);
            unityEvent?.Invoke();
            tUnityEvent?.Invoke(t);
        }
    }
}