using EasyFramework.EasyUIKit;
using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    [CreateAssetMenu(menuName = "EasyFramework/EasyUI/Ani/XPosition")]
    public class XPosition : SingleUIAni,IGradientAni<float>
    {
        public float StartValue => startXPosition;
        public float EndValue => endXPosition;

        [SerializeField]private float startXPosition=0;
        [SerializeField]private float endXPosition=1;

        public override void Playing(float progress, params object[] args)
        {
           GradientAni(progress,args);
        }

        public void GradientAni(float progress, params object[] args)
        {
            if (args[0] is IEasyPanel panel)
                panel.Transform.ModifyLocalPosition(x: Mathf.Lerp(StartValue, EndValue, progress));
        }

    }
}