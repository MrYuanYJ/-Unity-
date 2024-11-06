using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace EasyFramework
{
    [System.Serializable]
    public struct ShootInfo
    {
        public Vector3 StartPosition;
        public Vector3 StartDirection;
        public TranslateInfo TranslateInfo;
        public float LifeTime;
        public bool IsIgnoreTimeScale;
    }
    public class BulletEntity: AUnitEntity<Bullet,BattleStructure>
    {
        public override Enum UnitType =>EObject.Bullet;
        public IUnitEntity Shooter;
        public ShootInfo ShootInfo;
        protected override IUnitEntity GetBelongUnitEntity() => Shooter;

        protected override void OnInit()
        {
            base.OnInit();
            if (Mono.bulletRootTrans == null)
                Mono.bulletRootTrans = Mono.transform;
        }

        protected override void OnDispose(bool usePool)
        {
            base.OnDispose(usePool);
            if (usePool)
            {
                EasyRes.RecycleGo.InvokeEvent(Mono.gameObject);
            }
        }

        public void Shoot(IUnitEntity shooter,ShootInfo shootInfo)
        {
            Shooter=shooter;
            ShootInfo = shootInfo;
            var bulletEntity = (IUnitEntity)Root;
            this.GetEntity<TranslateEntity>().SetTranslate(shootInfo.TranslateInfo);
            UnitEvent.SetLifeCycle(bulletEntity,shootInfo.LifeTime,shootInfo.IsIgnoreTimeScale);
        }
    }
}