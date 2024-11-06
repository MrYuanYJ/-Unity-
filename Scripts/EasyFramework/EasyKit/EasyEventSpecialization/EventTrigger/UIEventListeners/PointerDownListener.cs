using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class PointerDownListener: APointerEventListener,IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData) =>Invoke(eventData);
    }
}