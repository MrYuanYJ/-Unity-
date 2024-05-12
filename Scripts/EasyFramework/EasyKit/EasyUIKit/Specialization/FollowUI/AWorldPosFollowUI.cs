using UnityEngine;

namespace EasyFramework.EasyUIKit
{
    public abstract class AWorldPosFollowUI: AEasyFollowUI
    {
        public Transform target;
        protected override Vector3 GetFollowPosition()
        {
            if (null == target) { return Vector3.zero; }

            return EasyUI.GetWorldPosToUIPos(target.position, this);
        }
    }
}