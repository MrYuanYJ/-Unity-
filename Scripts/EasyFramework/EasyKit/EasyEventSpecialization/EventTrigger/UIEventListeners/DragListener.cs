using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class DragListener: APointerEventListener,IDragHandler
    {
        public void OnDrag(PointerEventData eventData) =>Invoke(eventData);
    }
}