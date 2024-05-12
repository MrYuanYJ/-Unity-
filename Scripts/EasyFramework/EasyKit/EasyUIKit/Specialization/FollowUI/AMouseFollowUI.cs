using UnityEngine;

namespace EasyFramework.EasyUIKit
{
    public abstract class AMouseFollowUI: AEasyFollowUI
    {
        protected override Vector3 GetFollowPosition()
        {
             return EasyUI.GetScreenMousePosition(this);
        }
    }
}