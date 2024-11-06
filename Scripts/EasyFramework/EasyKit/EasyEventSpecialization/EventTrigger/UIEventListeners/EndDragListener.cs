using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class EndDragListener: APointerEventListener,IEndDragHandler
    {
        public void OnEndDrag(PointerEventData eventData) =>Invoke(eventData);
    }
}