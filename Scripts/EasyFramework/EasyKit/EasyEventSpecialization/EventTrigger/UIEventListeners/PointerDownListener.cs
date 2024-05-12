using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class PointerDownListener: APointerEventListener,IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData) =>Invoke(eventData);
    }
}