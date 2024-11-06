using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class PointerEnterListener: APointerEventListener,IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData) =>Invoke(eventData);
    }
}