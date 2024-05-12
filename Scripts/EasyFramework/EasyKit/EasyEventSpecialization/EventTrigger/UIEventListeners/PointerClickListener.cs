using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class PointerClickListener: APointerEventListener,IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) =>Invoke(eventData);
    }
}