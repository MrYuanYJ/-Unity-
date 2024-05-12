using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class BeginDragListener: APointerEventListener,IBeginDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData) =>Invoke(eventData);
    }
}