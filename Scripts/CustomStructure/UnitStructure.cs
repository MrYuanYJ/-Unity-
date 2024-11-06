
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace EasyFramework
{
    public class UnitStructure: AStructure<UnitStructure>
    {
        protected override void OnInit()
        {
            this.System<UnitSystem>();
            this.System<MovementSystem2D>();
            this.System<BuffSystem>();
            this.DisposeWith(Game.TryRegister());
        }
    }

    public interface IUnitEntity : IEntity, IGetEasyEventDic, IGetEasyFuncDic
    {
        long UnitId { get; }
        Enum UnitType{ get; }
    }

    public struct UnitEvent
    {
        public sealed class Register: AEventIndex<Register,IUnitEntity>{}
        public sealed class UnRegister: AEventIndex<UnRegister,IUnitEntity>{}
        public static void SetLifeCycle(IUnitEntity entity,float lifeTime,bool isIgnoreTimeScale=false)
        {
            var lifeCycle=(LifeCycleEntity)((AMonoEntityCarrier) entity.BindObj).GetOrAddMonoEntity<LifeCycle>().Entity;
            lifeCycle.SetLifeCycle(lifeTime, isIgnoreTimeScale);
            lifeCycle.LifeCycleStart();
        }
        
        #region 运动相关
        
        public sealed class SetSpeed2D: AEventIndex<SetSpeed2D,float,float>{}
        public sealed class SetSpeed2D_X: AEventIndex<SetSpeed2D_X,float>{}
        public sealed class SetSpeed2D_Y: AEventIndex<SetSpeed2D_Y,float>{}
        public sealed class SetMoveDir2D: AEventIndex<SetMoveDir2D,Vector2>{}
        public sealed class BanMove: AEventIndex<BanMove>{}
        public sealed class ResetHorizontalAndVerticalMoveAble: AEventIndex<ResetHorizontalAndVerticalMoveAble>{}
        public sealed class JumpStart: AEventIndex<JumpStart>{}
        public sealed class JumpEnd: AEventIndex<JumpEnd>{}
        public sealed class SetJumpDir2D: AEventIndex<SetJumpDir2D,Vector2>{}
        public sealed class LastFrameSpeed2D: AEventIndex<LastFrameSpeed2D,Vector2>{}
        public sealed class AddMovementState: AEventIndex<AddMovementState,EMovementState,MovementState>{}
        public sealed class RemoveMovementState: AEventIndex<RemoveMovementState,EMovementState>{}
        public sealed class ChangeMovementState: AEventIndex<ChangeMovementState,EMovementState>{}
        

        #endregion
        #region 战斗事件
        public static async void ShootBullet(IUnitEntity belongEntity,string bulletPath, ShootInfo shootInfo)
        {
            var bullet = await UnitFunc.CreateGo(new CreateInfo()
            {
                Path = bulletPath,
                Position = shootInfo.StartPosition,
                Rotation = Quaternion.LookRotation(shootInfo.StartDirection)
            });
            bullet.SetActive(true);
            ((BulletEntity) bullet.GetComponentInChildren<Bullet>().Entity)
                .Shoot(belongEntity, shootInfo);
        }
        public sealed class Attack: AEventIndex<Attack>{}
        public sealed class AttackEnd: AEventIndex<AttackEnd>{}
        #endregion

        #region 交互事件
        /// <summary>交互者与可交互对象进行交互</summary>
        public sealed class StartInteract: AEventIndex<StartInteract,IUnitEntity>{}
        /// <summary>交互者与可交互对象进行交互时,触发的事件</summary>
        public sealed class OnInteractStart : AEventIndex<OnInteractStart, IUnitEntity>{}
        public sealed class EndInteract: AEventIndex<EndInteract>{}
        public sealed class OnInteractEnd: AEventIndex<OnInteractEnd>{}
        public sealed class SetCanInteract: AEventIndex<SetCanInteract,bool>{}
        public sealed class SetInteractTarget: AEventIndex<SetInteractTarget,IUnitEntity>{}
        #endregion
    }
    public struct UnitFunc
    {
        public static async Task<GameObject> CreateGo(CreateInfo info)
        {
            var handle = (CoroutineHandle<GameObject>)EasyRes.LoadPrefabByPathAsync.InvokeFunc(info.Path,true);
            await handle;
            handle.Result.transform.SetPositionAndRotation(info.Position, info.Rotation);
            return handle.Result;
        }
        public static ERelationship GetUnitEntityRelationship(IUnitEntity unitEntity, IUnitEntity otherUnitEntity)
        {
            if(unitEntity == null || otherUnitEntity == null)
                return ERelationship.Neutral;
            var unitType = unitEntity.UnitType.As<EUnit>();
            var otherUnitType = otherUnitEntity.UnitType.As<EUnit>();
            if(unitType== otherUnitType)
                return ERelationship.Ally;
            if(unitType== EUnit.Player|| otherUnitType == EUnit.Player)
            {
                if(unitType==EUnit.Enemy|| otherUnitType == EUnit.Enemy)
                    return ERelationship.Enemy;
            }
               
            return ERelationship.Neutral;
        }
        public sealed class GetPositionOffset: AFuncIndex<GetPositionOffset,Vector3>{}
        public sealed class GetBelongUnit: AFuncIndex<GetBelongUnit,IUnitEntity>{}
        public sealed class UnitProperty: AFuncIndex<UnitProperty,EUnitProperty,RoleProperties>{}
        public sealed class CurrentMovementState: AFuncIndex<CurrentMovementState,EMovementState>{}
        public sealed class MoveDir2D: AFuncIndex<MoveDir2D,Vector2>{}
        public sealed class JumpCondition: AFuncIndex<JumpCondition,bool>{}
        public sealed class JumpCoyoteTimer: AFuncIndex<JumpCoyoteTimer,float,CoroutineHandle>{}
        public sealed class RedirectMovementState: AFuncIndex<RedirectMovementState,EMovementState>{}
        
        #region 交互事件
        public sealed class GetCanInteract: AFuncIndex<GetCanInteract,bool>{}
        public sealed class GetInteractTarget: AFuncIndex<GetInteractTarget,IUnitEntity>{}
  
        #endregion
    }

    [Flags]
    public enum ERelationship
    {
        Enemy=1,
        Ally=2,
        Neutral=4,
    }
}