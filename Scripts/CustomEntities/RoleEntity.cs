using System;

namespace EasyFramework
{
    public class RoleEntity: AUnitEntity<Unit,UnitStructure>,IUnitEntity
    {
        public override Enum UnitType => Mono.unitType;
        protected override IUnitEntity GetBelongUnitEntity()=> this;
        
        private IUnitEntity GetUnitEntity()=> this;

        protected override void OnInit()
        {
            base.OnInit();
            Mono.roleFsm = new RoleFsm();
            RoleEvent.ChangeRoleState.RegisterEvent(this,ChangeState);
            RoleEvent.AddRoleState.RegisterEvent(this,AddState);
            RoleEvent.RemoveRoleState.RegisterEvent(this,RemoveState);
            RoleEvent.ExitCurrentState.RegisterEvent(this,ExitCurrentState);
            RoleEvent.ExitState.RegisterEvent(this,ExitState);

            RoleFunc.GetRoleState.RegisterFunc(this,GetState);
            RoleFunc.GetCurrentRoleState.RegisterFunc(this,GetCurrentState);
            RoleFunc.GetCanInteract.RegisterFunc(this,GetCanInteract);
        }

        protected override void OnDispose(bool usePool)
        {
            base.OnDispose(usePool);
            RoleEvent.ChangeRoleState.UnRegisterEvent(this,ChangeState);
            RoleEvent.AddRoleState.UnRegisterEvent(this,AddState);
            RoleEvent.RemoveRoleState.UnRegisterEvent(this,RemoveState);
            RoleEvent.ExitCurrentState.UnRegisterEvent(this,ExitCurrentState);
            RoleEvent.ExitState.UnRegisterEvent(this,ExitState);
            
            RoleFunc.GetRoleState.UnRegisterFunc(this,GetState);
            RoleFunc.GetCurrentRoleState.UnRegisterFunc(this,GetCurrentState);
            RoleFunc.GetCanInteract.UnRegisterFunc(this,GetCanInteract);
        }

        private void ChangeState(ERoleState state)=>Mono.roleFsm.ChangeState(state);
        private void AddState(ERoleState state)=>Mono.roleFsm.AddState(state);
        private void RemoveState(ERoleState state)=>Mono.roleFsm.RemoveState(state);

        private void ExitCurrentState()=>Mono.roleFsm.ExitCurrentState();
        private void ExitState(ERoleState state)=>Mono.roleFsm.ExitState(state);
        private UniversalState GetState(ERoleState state)=> Mono.roleFsm.GetState(state);
        private ERoleState GetCurrentState()=> Mono.roleFsm.CurrentState;
        private bool GetCanInteract()=>Mono.roleFsm.CurrentState==ERoleState.None;
    }

    public enum ERoleState
    {
        None,
        Interacting,
        Attack,
        Skill,
        Die,
    }
    [System.Serializable]
    public class RoleFsm: AStateMachine<ERoleState,UniversalState>{}

    public struct RoleEvent
    {
        public sealed class ChangeRoleState: AEventIndex<ChangeRoleState, ERoleState>{}
        public sealed class AddRoleState: AEventIndex<AddRoleState, ERoleState>{}
        public sealed class RemoveRoleState: AEventIndex<RemoveRoleState, ERoleState>{}
        public sealed class ExitCurrentState: AEventIndex<ExitCurrentState>{}
        public sealed class ExitState: AEventIndex<ExitState, ERoleState>{}
        
        #region Hp事件
        public sealed class HpChanged : AEventIndex<HpChanged,float>{}
        public sealed class Death: AEventIndex<Death>{}
        #endregion
        
        #region Mp事件
        public sealed class MpChanged : AEventIndex<MpChanged,float>{}
        #endregion
        
        #region 护盾事件
        public sealed class ShieldChanged: AEventIndex<ShieldChanged,float>{}
        #endregion
    }

    public struct RoleFunc
    {
        public sealed class GetRoleState: AFuncIndex<GetRoleState, ERoleState, UniversalState>{}
        public sealed class GetCurrentRoleState: AFuncIndex<GetCurrentRoleState, ERoleState>{}
        public sealed class GetCanInteract: AFuncIndex<GetCanInteract, bool>{}
    }
}