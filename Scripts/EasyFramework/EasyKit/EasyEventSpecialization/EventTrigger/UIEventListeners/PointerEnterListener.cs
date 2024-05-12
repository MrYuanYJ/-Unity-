using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class PointerEnterListener: APointerEventListener,IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData) =>Invoke(eventData);
    }
}