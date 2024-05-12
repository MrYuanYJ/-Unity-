using UnityEngine;

namespace EasyFramework.EasyUIKit
{
    public abstract class AEasyPanel: AEasyUI,IEasyPanel
    {
        void IEasyUI.Init()
        {
            if (isInit) return;

            isInit = true;
            canvasGroup = transform.GetComponent<CanvasGroup>();

            if (ShowAni != null) ShowAni.Playing += OnShowAniPlaying;
            if (HideAni != null) HideAni.Playing += OnHideAniPlaying;

            OnInit();
            InvokeInitializeEvent();
            Showed+=()=>EasyUIMgr.PanelShow(this);
            Hidden+=()=>EasyUIMgr.PanelHide(this);
            Closed+=()=>EasyUIMgr.PanelClose(this);
        }
    }
}