using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class PointerUpListener: APointerEventListener,IPointerUpHandler
    {
        public void OnPointerUp(PointerEventData eventData) =>Invoke(eventData);
    }
}