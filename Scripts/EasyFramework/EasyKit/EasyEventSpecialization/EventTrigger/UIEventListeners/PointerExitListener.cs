using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class PointerExitListener: APointerEventListener,IPointerExitHandler
    {
        public void OnPointerExit(PointerEventData eventData) =>Invoke(eventData);
    }
}