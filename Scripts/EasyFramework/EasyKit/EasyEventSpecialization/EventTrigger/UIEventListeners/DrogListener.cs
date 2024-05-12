using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class DropListener: APointerEventListener,IDropHandler
    {
        public void OnDrop(PointerEventData eventData) =>Invoke(eventData);
    }
}