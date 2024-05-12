using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class ScrollListener: APointerEventListener,IScrollHandler
    {
        public void OnScroll(PointerEventData eventData) =>Invoke(eventData);
    }
}