using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class DropListener: APointerEventListener,IDropHandler
    {
        public void OnDrop(PointerEventData eventData) =>Invoke(eventData);
    }
}