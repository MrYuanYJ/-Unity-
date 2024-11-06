using UnityEngine;

namespace EasyFramework
{
    public struct TranslateInfo
    {
        public float ForwardSpeed;
        public Ease ForwardEase;
        public float UpSpeed;
        public Ease UpEase;
        public float RightSpeed;
        public Ease RightEase;
    }
    public class TranslateEntity: AMonoEntity<Translate>,IFixedUpdateAble
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        private float _mTime;
        private ForwardTranslateEntity _forwardTranslate;
        private RightTranslateEntity _rightTranslate;
        private UpTranslateEntity _upTranslate;
        private Vector3 _translate;

        protected override void OnInit()
        {
            base.OnInit();
            _mTime = 0;
        }

        protected override void OnActive()
        {
            base.OnActive();
            _forwardTranslate = (ForwardTranslateEntity)Mono.GetOrAddMonoEntity<ForwardTranslate>().Entity;
            _rightTranslate = (RightTranslateEntity)Mono.GetOrAddMonoEntity<RightTranslate>().Entity;
            _upTranslate = (UpTranslateEntity)Mono.GetOrAddMonoEntity<UpTranslate>().Entity;
        }


        public void SetTranslate(TranslateInfo translateInfo)
        {
            _forwardTranslate.InitTranslate(translateInfo.ForwardSpeed, translateInfo.ForwardEase);
            _rightTranslate.InitTranslate(translateInfo.RightSpeed, translateInfo.RightEase);
            _upTranslate.InitTranslate(translateInfo.UpSpeed, translateInfo.UpEase);
        }

        private void Move(float deltaTime)
        {
            _mTime += deltaTime;
            if (_mTime >= Mono.singleCycleDuration)
                _mTime -= Mono.singleCycleDuration;
            RefreshTranslate();
            Mono.translateTarget.Translate(_translate * deltaTime, Space.World);
        }

        public void RefreshTranslate()
        {
            var percent=_mTime/Mono.singleCycleDuration;
            var translate = _forwardTranslate.GetTranslate(percent);
            translate+=_rightTranslate.GetTranslate(percent);
            translate+=_upTranslate.GetTranslate(percent);
            _translate = translate;
            
        }
        public Vector3 GetTranslate()=> _translate;
        public IEasyEvent FixedUpdateEvent{ get; }= new EasyEvent();
        void IFixedUpdateAble.OnFixedUpdate()
        {
            Move(EasyTime.FixedDeltaTime);
        }
    }
}