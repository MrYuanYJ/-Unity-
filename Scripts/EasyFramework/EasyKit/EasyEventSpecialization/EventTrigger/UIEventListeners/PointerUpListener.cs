using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class PointerUpListener: APointerEventListener,IPointerUpHandler
    {
        public void OnPointerUp(PointerEventData eventData) =>Invoke(eventData);
    }
}