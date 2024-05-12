using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class DragListener: APointerEventListener,IDragHandler
    {
        public void OnDrag(PointerEventData eventData) =>Invoke(eventData);
    }
}