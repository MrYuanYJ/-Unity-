using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EasyFramework.EasyUIKit
{
    public class EasyUIRoot: AEasyUIRoot,IEasyCanvas
    {
        [SerializeField] protected Transform[] panelParents;
        [SerializeField] protected EventSystem eventSystem;
        private Camera _uiCamera;
        private EasyUIRoot _mCanvas;
        private Canvas _canvas;
        public override IEasyCanvas MainCanvas => _mCanvas;
        public override Camera UICamera => _uiCamera;
        public Canvas Canvas => _canvas;
        public override EventSystem EventSystem => eventSystem;
        public Transform[] PanelParents => panelParents;

        public static Lazy<EasyUIRoot> MEasyUIRoot = new Lazy<EasyUIRoot>(() =>
        {
            var uiRoot = DontDestroy.Instantiate(EDontDestroy.UI, (GameObject)Instantiate(Resources.Load("UIRoot"))).GetComponent<EasyUIRoot>();
            uiRoot._mCanvas = uiRoot;
            uiRoot._mCanvas._canvas = uiRoot._mCanvas.GetComponent<Canvas>();
            uiRoot._uiCamera = uiRoot._mCanvas._canvas.worldCamera;
            return uiRoot;
        });
    }
}