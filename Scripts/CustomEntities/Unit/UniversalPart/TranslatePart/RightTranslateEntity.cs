using UnityEngine;

namespace EasyFramework
{
    public class RightTranslateEntity: AMonoEntity<RightTranslate>
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        private TranslateEntity _translate;

        protected override void OnInit()
        {
            base.OnInit();
            _translate = (TranslateEntity) Parent;
        }

        protected override void OnDispose(bool usePool)
        {
            base.OnDispose(usePool);
            _translate = null;
        }
        public void InitTranslate(float speed, Ease ease)
        {
            Mono.speed = speed;
            Mono.ease = ease;
        }
        public Vector3 GetTranslate(float percent)
        {
            if(Mono.speed==0|| Mono.ease==null)
                return Vector3.zero;
            var speed = Mono.speed * Mono.ease.Evaluate(percent);
            return _translate.Mono.translateTarget.transform.right * speed;
        }
    }
}