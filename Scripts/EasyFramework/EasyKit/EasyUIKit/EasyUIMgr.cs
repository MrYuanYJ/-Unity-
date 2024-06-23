using System;
using System.Collections;
using System.Collections.Generic;
using EasyFramework.EasyTaskKit;
using UnityEngine;

namespace EasyFramework.EasyUIKit
{
    public class EasyUIMgr: Singleton<EasyUIMgr>
    {
        public EasyUIMgr()
        {
    
        }
        private IEasyUIRoot UIRoot;
        private Dictionary<Type, IEasyUI> _uiDic = new();
        private List<IEasyPanel> _showPanels = new();

        private IEnumerator GetOrCreateUI<T>(CoroutineHandle<T> handle) where T : Component, IEasyUI
        {
            if (!_uiDic.TryGetValue(typeof(T), out var panel))
            {
                //从资源中加载预制体
                var loadHandle = (CoroutineHandle<GameObject>)EasyRes.LoadPrefabByTypeAsync.InvokeFunc(typeof(T),true);
                yield return loadHandle;
                panel = loadHandle.Result.GetComponent<T>();
                _uiDic[typeof(T)] = panel;
            }

            EasyCoroutine.ReturnResult(handle, (T) panel);
        }

        public T GetUI<T>()
        {
            _uiDic.TryGetValue(typeof(T), out var panel);
            return (T)panel;
        }
        public CoroutineHandle<T> OpenUI<T>() where T : Component, IEasyUI
        {
            var handle = EasyTask.RegisterEasyResultCoroutine<T>(GetOrCreateUI);
            handle.Completed += h =>
            {
                var panel = h.Result;
                panel.SetCanvas(UIRoot, panel.Layer);
                UIRoot.OpenUI(panel);
            };
            return handle;
        }
        public void ShowUI<T>() where T : Component, IEasyUI
        {
            var panel = GetUI<T>();
            panel.UIRoot?.ShowUI(panel);
        }

        public void HideUI<T>() where T : Component, IEasyUI
        {
            var panel = GetUI<T>();
            panel.UIRoot?.HideUI(panel);
        }

        public void CloseUI<T>() where T : Component, IEasyUI
        {
            var panel = GetUI<T>();
            panel.UIRoot?.CloseUI(panel);
            _uiDic.Remove(typeof(T));
        }
        public void CloseAllPanel()
        {
            for (int i = _showPanels.Count-1; i > -1; i--)
            {
                _showPanels[i].UIRoot?.CloseUI(_showPanels[i]);
            }
        }

        public void HideCurrentPanelAndOpen<T>() where T : Component, IEasyPanel
        {
            if (_showPanels.Count == 0)
                return;
            _showPanels[^1].UIRoot?.HideUI(_showPanels[^1]);
            OpenUI<T>();
        }
        public void CloseCurrentPanelAndOpen<T>() where T : Component, IEasyPanel
        {
            if (_showPanels.Count == 0)
                return;
            _showPanels[^1].UIRoot?.CloseUI(_showPanels[^1]);
            OpenUI<T>();
        }


        public static void PanelShow(IEasyPanel panel)
        {
            Instance._showPanels.Add(panel);
        }
        public static void PanelHide(IEasyPanel panel)
        {
            Instance._showPanels.Remove(panel);
        }
        public static void PanelClose(IEasyPanel panel)
        {
            
        }

        protected override void OnInit()
        {
            UIRoot = EasyUIRoot.MEasyUIRoot.Value;
            UIRoot.EventSystem.gameObject.SetActive(true);
        }
    }
}