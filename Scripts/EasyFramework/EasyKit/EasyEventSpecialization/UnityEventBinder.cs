using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace EasyFramework.EventKit
{
    public interface IUnityEventBinder
    {
        IEasyEvent Event { get; }
        UnityEventBase UnityEvent{ get; }
    }
    public abstract class AUnityEventBinder: MonoBehaviour,IUnityEventBinder
    {
        protected EasyEvent easyEvent=new EasyEvent();
        [SerializeField]protected UnityEvent unityEvent=new UnityEvent();
        
        IEasyEvent IUnityEventBinder.Event => easyEvent;
        public EasyEvent Event => easyEvent;
        public UnityEventBase UnityEvent => unityEvent;

        public void Invoke()
        {
            easyEvent?.Invoke();
            unityEvent?.Invoke();
        }
    }
    public abstract class AUnityEventBinder<T>: MonoBehaviour,IUnityEventBinder
    {
        protected EasyEvent<T> easyEvent=new EasyEvent<T>();
        [SerializeField]protected UnityEvent<T> unityEvent=new UnityEvent<T>();
        
        IEasyEvent IUnityEventBinder.Event => easyEvent;
        public EasyEvent<T> Event => easyEvent;
        public UnityEventBase UnityEvent => unityEvent;

        public void Invoke(T t)
        {
            easyEvent?.Invoke(t);
            unityEvent?.Invoke(t);
        }
    }
}