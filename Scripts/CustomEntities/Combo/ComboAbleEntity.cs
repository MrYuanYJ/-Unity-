using System;
using System.Collections.Generic;

namespace EasyFramework
{
    public class ComboAbleEntity: AMonoEntity<ComboAble>
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        public EasyFSM<ICombo> ComboFSM { get; private set; } = new();
        public Dictionary<Enum, ICombo> ComboDict { get; private set; } = new();
        public Dictionary<EInput, InputState> ListenerInputs { get; private set; } = new();
        public Enum CurrentCombo { get; private set; }
        public List<Enum> UsableCombosOnStart { get; private set; } = new();
        public List<Enum> UsableCombos { get; private set; } = new();
        public bool IsInCombo { get; set; } = false;

        protected override void OnActive()
        {
            var root = (IUnitEntity) Root;
        }

        protected override void OnUnActive()
        {
            var root = (IUnitEntity) Root;
        }


        private EasyFSM<ICombo> GetComboFsm()=> ComboFSM;

        public void AddCombo(ICombo combo)
        {
            ComboDict[combo.ComboName] = combo;
            foreach (var comboCondition in combo.InputConditions)
            {
                TryAddInputListener(comboCondition.Input);
            }
            if(combo.UsableOnStart)
                UsableCombosOnStart.Add(combo.ComboName);
        }

        public void RemoveCombo(ICombo combo)
        {
            ComboDict.Remove(combo.ComboName);
            foreach (var comboCondition in combo.InputConditions)
            {
                TryRemoveInputListener(comboCondition.Input);
            }

            if(combo.UsableOnStart)
                UsableCombosOnStart.Remove(combo.ComboName);
        }

        private void TryAddInputListener(EInput input)
        {
            if (!ListenerInputs.ContainsKey(input))
            {
                AddInputListener(input);
            }
        }

        private void TryRemoveInputListener(EInput input)
        {
            var isFind = false;
            foreach (var combo in ComboDict.Values)
            {
                foreach (var comboCondition in combo.InputConditions)
                {
                    if (comboCondition.Input==input)
                    {
                        isFind = true;
                    }
                }
            }

            if (!isFind)
            {
                RemoveInputListener(input);
            }
        }

        private void AddInputListener(EInput input)
        {
            ListenerInputs[input] = EasyInputSetting.Instance.InputStateDict[input];
            ListenerInputs[input].IsPressed.Register(OnInputChange);
        }

        private void RemoveInputListener(EInput input)
        {
            ListenerInputs[input].IsPressed.UnRegister(OnInputChange);
            ListenerInputs.Remove(input);
        }

        private void OnInputChange()
        {
            if(IsInCombo)
                return;
            var usableCombos = GetUsableCombos();
            ICombo selectedCombo = null;
            var isTure = true;
            foreach (var eCombo in usableCombos)
            {
                if (!ComboDict.TryGetValue(eCombo, out var combo))
                    continue;
                foreach (var comboCondition in combo.InputConditions)
                {
                    isTure = isTure && comboCondition.IsTure();
                }
                if (isTure && (selectedCombo == null || combo.Priority > selectedCombo.Priority))
                {
                    selectedCombo = combo;
                }
            }

            if (selectedCombo != null)
            {
                CurrentCombo = selectedCombo.ComboName;
                IsInCombo = true;
                ComboFSM.ChangeState(selectedCombo);
            }
        }

        private List<Enum> GetUsableCombos()
        {
            UsableCombos.Clear();
            if(CurrentCombo!=null&& ComboDict[CurrentCombo].UsableCombo.Length>0)
            {
                foreach (var comboEnum in ComboDict[CurrentCombo].UsableCombo)
                {
                    if(ComboDict.ContainsKey(comboEnum))
                        UsableCombos.Add(comboEnum);
                }
                if(UsableCombos.Count==0)
                    UsableCombos.AddRange(UsableCombosOnStart);
            }
            else
            {
                UsableCombos.AddRange(UsableCombosOnStart);
            }

            return UsableCombos;
        }
    }
}