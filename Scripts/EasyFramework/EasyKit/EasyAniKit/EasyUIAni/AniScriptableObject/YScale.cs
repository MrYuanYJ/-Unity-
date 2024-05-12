using EasyFramework.EasyUIKit;
using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    [CreateAssetMenu(menuName = "EasyFramework/EasyUI/Ani/YScale")]
    public class YScale : SingleUIAni,IGradientAni<float>
    {
        public float StartValue => startYScale;
        public float EndValue => endYScale;

        [SerializeField]private float startYScale=0;
        [SerializeField]private float endYScale=1;

        public override void Playing(float progress, params object[] args)
        {
           GradientAni(progress,args);
        }

        public void GradientAni(float progress, params object[] args)
        {
            if (args[0] is IEasyPanel panel)
                panel.Transform.ModifyLocalScale(y: Mathf.Lerp(StartValue, EndValue, progress));
        }

    }
}