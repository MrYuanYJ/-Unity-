using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class PointerExitListener: APointerEventListener,IPointerExitHandler
    {
        public void OnPointerExit(PointerEventData eventData) =>Invoke(eventData);
    }
}