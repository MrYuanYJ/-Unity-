using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class BeginDragListener: APointerEventListener,IBeginDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData) =>Invoke(eventData);
    }
}