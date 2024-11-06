namespace EasyFramework
{
    public class InteractorEntity: AMonoEntity<Interactor>
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        private IUnitEntity _interactTarget;

        protected override void OnActive()
        {
            var entity = (IUnitEntity) Root;
            UnitEvent.StartInteract.RegisterEvent(entity,InteractTo);
            UnitEvent.EndInteract.RegisterEvent(entity, EndInteract);
            UnitEvent.SetCanInteract.RegisterEvent(entity, SetCanInteract);
            UnitEvent.SetInteractTarget.RegisterEvent(entity,SetInteractTarget);
                
            UnitFunc.GetCanInteract.RegisterFunc(entity,GetCanInteract);
            UnitFunc.GetInteractTarget.RegisterFunc(entity,GetInteractTarget);
        }

        protected override void OnUnActive()
        {
            var entity = (IUnitEntity) Root;
            UnitEvent.StartInteract.UnRegisterEvent(entity,InteractTo);
            UnitEvent.EndInteract.UnRegisterEvent(entity, EndInteract);
            UnitEvent.SetCanInteract.UnRegisterEvent(entity, SetCanInteract);
            UnitEvent.SetInteractTarget.UnRegisterEvent(entity,SetInteractTarget);
            
            UnitFunc.GetCanInteract.UnRegisterFunc(entity,GetCanInteract);
            UnitFunc.GetInteractTarget.UnRegisterFunc(entity,GetInteractTarget);
        }

        private void InteractTo(IUnitEntity target)
        {
            if (!GetCanInteract()
                || !UnitFunc.GetCanInteract.InvokeFunc(target))
                return;
            var entity = (IUnitEntity) Root;
            
            _interactTarget = target;
            UnitEvent.SetInteractTarget.InvokeEvent(entity, target);
            
            RoleEvent.ChangeRoleState.InvokeEvent(entity, ERoleState.Interacting);
            RoleEvent.ChangeRoleState.InvokeEvent(target, ERoleState.Interacting);
            
            UnitEvent.OnInteractStart.InvokeEvent(entity);
            UnitEvent.OnInteractStart.InvokeEvent(target);
        }

        private void EndInteract()
        {
            if (_interactTarget == null
                || UnitFunc.GetInteractTarget.InvokeFunc(_interactTarget) == null)
                return;
            var entity = (IUnitEntity) Root;
            
            RoleEvent.ExitState.InvokeEvent(_interactTarget, ERoleState.Interacting);
            RoleEvent.ExitState.InvokeEvent(entity, ERoleState.Interacting);
            
            UnitEvent.OnInteractEnd.InvokeEvent(_interactTarget);
            UnitEvent.OnInteractEnd.InvokeEvent(entity);

            UnitEvent.SetInteractTarget.InvokeEvent(entity, null);
            _interactTarget = null;
        }

        private bool GetCanInteract()=> Mono.canInteract && RoleFunc.GetCanInteract.InvokeFunc((IUnitEntity)Root);
        private IUnitEntity GetInteractTarget() => _interactTarget;
        private void SetCanInteract(bool state) => Mono.canInteract = state;
        private void SetInteractTarget(IUnitEntity target) => _interactTarget = target;
    }
}