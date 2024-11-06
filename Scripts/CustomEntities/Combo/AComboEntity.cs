using System;

namespace EasyFramework
{
    public abstract class AComboEntity<T,TKey>: AMonoEntity<T> where T : ACombo<TKey> where TKey : Enum
    {
        public override IStructure Structure => UnitStructure.GetInstance();
        protected CoroutineHandle ComboDurationCoroutineHandle;
        protected CoroutineHandle ComboWindDownCoroutineHandle;

        protected override void OnActive()
        {
            var comboAble=(ComboAbleEntity)Parent;
            
            comboAble.AddCombo(Mono);
            
            var comboFsm=comboAble.ComboFSM;
            var comboState = comboFsm[Mono];
            comboState.OnEnterCondition(OnEnterComboCondition);
            comboState.OnEnter(EnterCombo);
            comboState.OnExitCondition(OnExitComboCondition);
            comboState.OnExit(OnEndCombo);
        }

        protected override void OnUnActive()
        {
            var comboAble=(ComboAbleEntity)Parent;
            
            comboAble.RemoveCombo(Mono);
            
            var comboFsm=comboAble.ComboFSM;
            var comboState = comboFsm[Mono];
            comboState.EnterCondition = null;
            comboState.Enter.Clear();
            comboState.ExitCondition = null;
            comboState.Exit.Clear();
        }

        private async void EnterCombo()
        {
            var comboAble=(ComboAbleEntity)Parent;
            OnEnterCombo();
            if(ComboDurationCoroutineHandle!= null)
                await ComboDurationCoroutineHandle;
            comboAble.IsInCombo = false;
            ComboDurationCoroutineHandle = null;
            if (ComboWindDownCoroutineHandle != null)
                await ComboWindDownCoroutineHandle;
            ComboWindDownCoroutineHandle = null;
            comboAble.ComboFSM.ChangeState(null);
        }

        protected virtual bool OnEnterComboCondition() => true;
        protected abstract void OnEnterCombo();
        protected virtual bool OnExitComboCondition() => true;
        protected virtual void OnEndCombo(){}
    }
}