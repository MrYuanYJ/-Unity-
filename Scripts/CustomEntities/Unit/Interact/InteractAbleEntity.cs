namespace EasyFramework
{
    public class InteractAbleEntity: AMonoEntity<InteractAble>
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        private IUnitEntity _interactTarget;

        protected override void OnActive()
        {
            var entity = (IUnitEntity) Root;
            UnitEvent.StartInteract.RegisterEvent(entity, InteractWith);
            UnitEvent.EndInteract.RegisterEvent(entity, EndInteract);
            UnitEvent.SetCanInteract.RegisterEvent(entity, SetCanInteract);
            UnitEvent.SetInteractTarget.RegisterEvent(entity, SetInteractTarget);

            UnitFunc.GetCanInteract.RegisterFunc(entity,GetCanInteract);
            UnitFunc.GetInteractTarget.RegisterFunc(entity, GetInteractTarget);
        }

        protected override void OnUnActive()
        {
            var entity = (IUnitEntity) Root;
            UnitEvent.StartInteract.UnRegisterEvent(entity, InteractWith);
            UnitEvent.EndInteract.UnRegisterEvent(entity, EndInteract);
            UnitEvent.SetCanInteract.UnRegisterEvent(entity, SetCanInteract);
            UnitEvent.SetInteractTarget.UnRegisterEvent(entity, SetInteractTarget);
            
            UnitFunc.GetCanInteract.UnRegisterFunc(entity, GetCanInteract);
            UnitFunc.GetInteractTarget.UnRegisterFunc(entity, GetInteractTarget);
        }

        private void InteractWith(IUnitEntity target)
        {
            UnitEvent.StartInteract.InvokeEvent(target, (IUnitEntity) Root);
        }
        private void EndInteract()
        {
            UnitEvent.EndInteract.InvokeEvent(_interactTarget);
        }

        private bool GetCanInteract()=> Mono.canInteract && RoleFunc.GetCanInteract.InvokeFunc((IUnitEntity)Root);
        private IUnitEntity GetInteractTarget() => _interactTarget;
        private void SetCanInteract(bool state) => Mono.canInteract = state;
        private void SetInteractTarget(IUnitEntity target) => _interactTarget = target;
    }
}