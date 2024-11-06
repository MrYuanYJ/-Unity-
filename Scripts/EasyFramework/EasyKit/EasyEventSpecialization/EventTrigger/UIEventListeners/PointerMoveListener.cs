using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class PointerMoveListener: APointerEventListener,IPointerMoveHandler
    {
        public void OnPointerMove(PointerEventData eventData) =>Invoke(eventData);
    }
}