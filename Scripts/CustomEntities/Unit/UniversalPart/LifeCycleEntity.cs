using System;

namespace EasyFramework
{
    public class LifeCycleEntity: AMonoEntity<LifeCycle>
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        private float _currentLife;
        private CoroutineHandle _lifeCycleHandle;

        protected override void OnInit()
        {
            base.OnInit();
            _currentLife = 0;
        }
        protected override void OnUnActive()
        {
            base.OnUnActive();
            _lifeCycleHandle.Cancel();
        }

        public float GetLifePercent()=> _currentLife / Mono.duration;
        public void SetCurrentLife(float life)=> _currentLife = life;
        public void SetCurrentLifePct(float percent)=> _currentLife = percent * Mono.duration;
        public void SetLifeCycle(float duration, bool isIgnoreTimeScale)
        {
            Mono.duration = duration;
            Mono.isIgnoreTimeScale = isIgnoreTimeScale;
        }
        public void LifeCycleStart()
        {
            if(Mono.duration<=0) 
                return;
            _lifeCycleHandle=EasyCoroutine.TimeTask(Mono.duration-_currentLife, SetCurrentLife, Mono.isIgnoreTimeScale);
            _lifeCycleHandle.OnCompleted(()=>Root.Dispose(true));
            Mono.enabled = false;
        }
    }
}