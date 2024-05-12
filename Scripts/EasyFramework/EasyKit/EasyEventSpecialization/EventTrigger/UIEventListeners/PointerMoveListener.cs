using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class PointerMoveListener: APointerEventListener,IPointerMoveHandler
    {
        public void OnPointerMove(PointerEventData eventData) =>Invoke(eventData);
    }
}