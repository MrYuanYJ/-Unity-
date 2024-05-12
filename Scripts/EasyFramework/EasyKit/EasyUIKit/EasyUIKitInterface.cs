using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EasyFramework.EasyUIKit
{
    public interface IEasyUIRoot
    {
        EventSystem EventSystem { get; }
        IEasyCanvas MainCanvas{ get; }
        Camera UICamera { get; }
        Transform CloseUIParent{ get; }
        Transform TipParents { get; }
        void OpenUI(IEasyUI panel)
        {
            panel.Init();
            panel.Open();
        }
        void ShowUI(IEasyUI panel) => panel.Show();
        void HideUI(IEasyUI panel) => panel.Hide();
        void CloseUI(IEasyUI panel) => panel.Close();
    }

    public interface IEasyCanvas
    {
        Canvas Canvas { get; }
        EventSystem EventSystem { get; }
        Transform[] PanelParents { get; }
    }

    public interface IEasyUI
    {
        IEasyUIRoot UIRoot { get; protected set; }
        Transform Transform { get; }
        CanvasGroup CanvasGroup { get; }
        
        IEasyAni ShowAni { get; }
        IEasyAni HideAni { get; }
        
        bool IsInit { get; }
        bool IsOpen { get; }
        event Action Initialized;
        event Action Opened;
        event Action Showed;
        event Action Hidden;
        event Action Closed;
        
        EPanel Layer { get; }

        public void SetCanvas(IEasyUIRoot uiRoot,EPanel ePanel)
        {
            this.UIRoot = uiRoot;
            try
            {
                SetParent(uiRoot.MainCanvas.PanelParents[ePanel.GetHashCode()]);
            }
            catch (Exception e)
            {
                throw new Exception("EasyUI Error: Can not find the panel parent in the main canvas.", e);
            }
        }
        public void SetParent(Transform parent)
        {
            Transform trans = Transform;
            var local = (trans.localPosition,trans.localRotation,trans.localScale);
            trans.SetParent(parent.transform);
            (trans.localPosition, trans.localRotation, trans.localScale) = local;
        }

        void Init();
        void Open();
        void Show(Action showed=null,bool compulsion=false);
        void Hide(Action hided=null,bool compulsion=false);
        void Close();
    }
    public interface IEasyPanel: IEasyUI
    {
    }

    public interface IEasyTip: IEasyUI
    {
         EPanel IEasyUI.Layer => EPanel.Tip;
    }

    public abstract class AEasyUIRoot : MonoBehaviour, IEasyUIRoot
    {
        [SerializeField] protected Transform tipParents;
        [SerializeField] protected Transform closeUIParent;

        public abstract EventSystem EventSystem { get; }
        public abstract IEasyCanvas MainCanvas { get; }
        public abstract Camera UICamera { get; }
        public Transform CloseUIParent => closeUIParent;
        public Transform TipParents => tipParents;
    }
}