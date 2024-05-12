using EasyFramework.EasyUIKit;
using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    [CreateAssetMenu(menuName = "EasyFramework/EasyUI/Ani/XScale")]
    public class XScale : SingleUIAni,IGradientAni<float>
    {
        public float StartValue => startXScale;
        public float EndValue => endXScale;

        [SerializeField]private float startXScale=0;
        [SerializeField]private float endXScale=1;

        public override void Playing(float progress, params object[] args)
        {
           GradientAni(progress,args);
        }

        public void GradientAni(float progress, params object[] args)
        {
            if (args[0] is IEasyPanel panel)
                panel.Transform.ModifyLocalScale(x: Mathf.Lerp(StartValue, EndValue, progress));
        }

    }
}