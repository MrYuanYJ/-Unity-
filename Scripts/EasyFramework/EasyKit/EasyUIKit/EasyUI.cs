
using UnityEngine;

namespace EasyFramework.EasyUIKit
{
    public enum EPanel
    {
        LowerPanel,
        MiddlePanel,
        UpperPanel,
        
        TopUI,
        Tip
    }
    public static class EasyUI
    {
        public static T GetUI<T>() where T : Component, IEasyUI => EasyUIMgr.TryRegister().GetUI<T>();
        public static CoroutineHandle<T> OpenUI<T>() where T : Component, IEasyUI => EasyUIMgr.TryRegister().OpenUI<T>();
        public static void ShowUI<T>() where T : Component, IEasyUI => EasyUIMgr.TryRegister().ShowUI<T>();
        public static void HideUI<T>() where T : Component, IEasyUI => EasyUIMgr.Instance?.HideUI<T>();
        public static void CloseUI<T>() where T : Component, IEasyUI => EasyUIMgr.Instance?.CloseUI<T>();
        public static void CloseAllPanels() => EasyUIMgr.Instance?.CloseAllPanel();
        public static void CloseCurrentPanelAndOpen<T>() where T : Component, IEasyPanel => EasyUIMgr.Instance?.CloseCurrentPanelAndOpen<T>();
        public static void HideCurrentPanelAndOpen<T>() where T : Component, IEasyPanel => EasyUIMgr.Instance?.HideCurrentPanelAndOpen<T>();

        /// <summary>
        /// 取得Canvas坐标系下的鼠标位置
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        public static Vector2 GetScreenMousePosition(IEasyUI ui)
        {
            var pos = Vector3.zero;
#if ENABLE_INPUT_SYSTEM
            pos = Mouse.current.position.ReadValue();
#else
            pos = Input.mousePosition;
#endif
            if (ui.UIRoot.MainCanvas.Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                return pos;
            else
            {
                RectTransformUtility.ScreenPointToWorldPointInRectangle(ui.UIRoot.MainCanvas.Canvas.transform as RectTransform, pos,
                    ui.UIRoot.UICamera, out pos);
                return pos;
            }
        }
        public static Vector2 GetWorldPosToUIPos(Vector2 worldPos, IEasyUI ui)
        {
            return Camera.main.WorldToScreenPoint(worldPos);
        }
    }
}