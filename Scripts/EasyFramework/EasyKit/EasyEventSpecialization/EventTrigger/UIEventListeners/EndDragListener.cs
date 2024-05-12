using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class EndDragListener: APointerEventListener,IEndDragHandler
    {
        public void OnEndDrag(PointerEventData eventData) =>Invoke(eventData);
    }
}