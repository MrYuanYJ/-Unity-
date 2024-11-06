using System.Collections.Generic;

namespace EasyFramework
{
    public class CollisionDamageEntity:AMonoEntity<CollisionDamage>
    {
        public override IStructure Structure => BattleStructure.TryRegister();

        private Dictionary<string, CollisionDamageData> _collisionDamageDataDic = new();
        private Dictionary<string,HashSet<IUnitEntity>> targetsSettledDic = new();
        private IUnitEntity Unit;

        protected override void OnActive()
        {
            foreach (var data in Mono.CollisionDamageDatas)
            {
                _collisionDamageDataDic[data.CollisionGroup] = data;
            }

            Unit = UnitFunc.GetBelongUnit.InvokeFunc((IGetEasyFuncDic) Root);
            CollisionEvent.CollisionOccurs.RegisterEvent((IGetEasyEventDic) Root, DamageOnCollision);
            CollisionEvent.OnCloseCollider.RegisterEvent((IGetEasyEventDic) Root, OnCloseCollider);
        }

        protected override void OnUnActive()
        {
            _collisionDamageDataDic.Clear();
            CollisionEvent.CollisionOccurs.UnRegisterEvent((IGetEasyEventDic) Root, DamageOnCollision);
            CollisionEvent.OnCloseCollider.UnRegisterEvent((IGetEasyEventDic) Root, OnCloseCollider);
        }

        private void DamageOnCollision(string groupName)
        {
            if (_collisionDamageDataDic.TryGetValue(groupName, out var collisionDamageData))
            {
                if (!targetsSettledDic.ContainsKey(collisionDamageData.CollisionGroup))
                    targetsSettledDic[collisionDamageData.CollisionGroup] = new HashSet<IUnitEntity>();
                var collision2DInfo = CollisionFunc.GetCollisionInfo.InvokeFunc((IGetEasyFuncDic) Root,groupName);
                switch (collisionDamageData.CollisionDamageType)
                {
                    case ECollisionDamage.OnlyDamageToFirst: DamageToFirst(collision2DInfo, collisionDamageData);break;
                    case ECollisionDamage.DamageToAll: DamageToAll(collision2DInfo, collisionDamageData);break;
                }
            }
        }

        private void OnCloseCollider(string groupName)
        {
            targetsSettledDic.Remove(groupName);
        }

        private float GetDamageMultiplier(float weight, float weight2, float baseMultiplier)
        {
            return baseMultiplier * (weight + 1) / (weight2 + 1);
        }
        private void DamageToFirst(CollisionInfo collisionInfo, CollisionDamageData collisionDamageData)
        {
            var target = collisionInfo.GetOtherMaxWeightListener();
            if (target == null || !targetsSettledDic[collisionDamageData.CollisionGroup].Add(target.belongEntity))
                return;
            var self = collisionInfo.GetSelfMaxWeightListener();
            DamageInfo damageInfo = GetDamageInfo(self.data.weight, target,collisionDamageData);
            this.SendEvent<DamageInfo>(damageInfo);
        }
        private void DamageToAll(CollisionInfo collisionInfo, CollisionDamageData collisionDamageData)
        {
            var self = collisionInfo.GetSelfMaxWeightListener();
            foreach (var entity in collisionInfo.OtherCollision2DBoxListeners.Keys)
            {
                var target = collisionInfo.GetOtherMaxWeightListener(entity);
                if (target == null || !targetsSettledDic[collisionDamageData.CollisionGroup].Add(target.belongEntity))
                    continue;
                DamageInfo damageInfo = GetDamageInfo(self.data.weight, target,collisionDamageData);
                this.SendEvent<DamageInfo>(damageInfo);
            }
        }

        private DamageInfo GetDamageInfo(float attackerWeight, CollisionBoxListener target,CollisionDamageData collisionDamageData)
        {
            var damageMultiplier = GetDamageMultiplier(attackerWeight, target.data.weight, collisionDamageData.DamageMultiplier);
            return new DamageInfo
            {
                Attacker = UnitFunc.GetBelongUnit.InvokeFunc(Unit),
                Target = UnitFunc.GetBelongUnit.InvokeFunc(target.belongEntity),
                DamageType = collisionDamageData.DamageType,
                DamageAct = collisionDamageData.DamageAct,
                Damage = DamageSystem.GetDamage(collisionDamageData.Damage, new DamageBaseInfo
                {
                    Base = (IUnitEntity) Root,
                    BaseProperty = collisionDamageData.DamageBase.ToString(),
                    PropertyValue = collisionDamageData.DamageBaseValue,
                    Multiplier = damageMultiplier
                }),
            };
        }
    }
}