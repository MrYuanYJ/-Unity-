using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class ScrollListener: APointerEventListener,IScrollHandler
    {
        public void OnScroll(PointerEventData eventData) =>Invoke(eventData);
    }
}