using EasyFramework.EasyUIKit;
using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    [CreateAssetMenu(menuName = "EasyFramework/EasyUI/Ani/YPosition")]
    public class YPosition : SingleUIAni,IGradientAni<float>
    {
        public float StartValue => startYPosition;
        public float EndValue => endYPosition;

        [SerializeField]private float startYPosition=0;
        [SerializeField]private float endYPosition=1;

        public override void Playing(float progress, params object[] args)
        {
           GradientAni(progress,args);
        }

        public void GradientAni(float progress, params object[] args)
        {
            if (args[0] is IEasyPanel panel)
                panel.Transform.ModifyLocalPosition(y: Mathf.Lerp(StartValue, EndValue, progress));
        }

    }
}