
using EasyFramework.EasyUIKit;
using UnityEngine;

namespace EasyFramework
{
    [CreateAssetMenu(menuName = "EasyFramework/EasyUI/Ani/Fade")]
    public class Fade: SingleUIAni,IGradientAni<float>
    {
        public float StartValue => startAlpha;
        public float EndValue => endAlpha;

        [SerializeField]private float startAlpha=0;
        [SerializeField]private float endAlpha=1;

        public override void Playing(float progress, params object[] args)
        {
           GradientAni(progress,args);
        }

        public void GradientAni(float progress, params object[] args)
        {
            if (args[0] is IEasyPanel panel) 
                panel.CanvasGroup.alpha = Mathf.Lerp(StartValue, EndValue, progress);
        }

    }
}