using System;
using System.Linq;
using UnityEngine;

namespace EasyFramework.EasyUIKit
{
    [RequireComponent(typeof(UnityEngine.CanvasGroup))]
    public abstract class AEasyUI : MonoBehaviour, IEasyUI
    {
        protected IEasyUIRoot uiRoot;
        protected bool isInit = false;
        protected bool isOpen = false;
        [SerializeField] protected EasyUIAni showAni = new EasyUIAni(true);
        [SerializeField] protected EasyUIAni hideAni = new EasyUIAni(false);
        protected CoroutineHandle handle;
        protected CanvasGroup canvasGroup;
        bool IEasyUI.IsInit => isInit;
        bool IEasyUI.IsOpen => isOpen;
        public event Action Initialized;
        public event Action Opened;
        public event Action Showed;
        public event Action Hidden;
        public event Action Closed;

        IEasyUIRoot IEasyUI.UIRoot
        {
            get => uiRoot;
            set => uiRoot = value;
        }

        public Transform Transform => transform;
        public CanvasGroup CanvasGroup => canvasGroup;
        public IEasyAni ShowAni => showAni;
        public IEasyAni HideAni => hideAni;
        public abstract EPanel Layer { get; }
        
        protected void InvokeInitializeEvent()=> Initialized?.Invoke();
        protected void InvokeOpenedEvent()=> Opened?.Invoke();
        protected void InvokeShowedEvent()=> Showed?.Invoke();
        protected void InvokeHidedEvent()=> Hidden?.Invoke();
        protected void InvokeClosedEvent()=> Closed?.Invoke();

        void IEasyUI.Init()
        {
            if (isInit) return;

            isInit = true;
            canvasGroup = transform.GetComponent<CanvasGroup>();

            if (ShowAni != null) ShowAni.Playing += OnShowAniPlaying;
            if (HideAni != null) HideAni.Playing += OnHideAniPlaying;

            OnInit();
            InvokeInitializeEvent();
        }

        void IEasyUI.Open()
        {
            if (!isOpen)
            {
                isOpen = true;
                OnOpen();
                InvokeOpenedEvent();
            }

            ((IEasyUI) this).Show(null, true);
        }

        void IEasyUI.Show(Action showed, bool compulsion)
        {
            if (!compulsion && gameObject.activeSelf) return;

            gameObject.SetActive(true);
            handle?.Cancel();
            if (ShowAni != null && ShowAni.Anis.Any())
            {
                handle = ShowAni.Run(() =>
                {
                    OnShow();
                    InvokeShowedEvent();
                    showed?.Invoke();
                    handle = null;
                }, this);
            }
            else
            {
                OnShow();
                InvokeShowedEvent();
                showed?.Invoke();
            }
        }

        void IEasyUI.Hide(Action hided, bool compulsion)
        {
            if (!compulsion && !gameObject.activeSelf) return;

            gameObject.SetActive(false);
            handle?.Cancel();
            if (HideAni != null && HideAni.Anis.Any())
            {
                handle = HideAni.Run(() =>
                {
                    OnHide();
                    InvokeHidedEvent();
                    hided?.Invoke();
                    handle = null;
                }, this);
            }
            else
            {
                OnHide();
                InvokeHidedEvent();
                hided?.Invoke();
            }
        }

        void IEasyUI.Close()
        {
            ((IEasyUI) this).Hide(() =>
            {
                OnClose();
                InvokeClosedEvent();
                isOpen = false;
                EasyRes.RecycleGo.InvokeEvent(gameObject);
            });
        }

        protected virtual void OnInit(){}
        protected virtual void OnOpen(){}
        protected virtual void OnShowAniStart() { }
        protected virtual void OnShowAniPlaying(float progress) { }
        protected virtual void OnShow(){}
        protected virtual void OnHideAniStart() { }
        protected virtual void OnHideAniPlaying(float progress) { }
        protected virtual void OnHide(){}
        protected virtual void OnClose(){}
    }
}