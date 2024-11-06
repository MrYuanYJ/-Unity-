using UnityEngine;

namespace EasyFramework
{
    public interface IUsableItem: IItem
    {
        int MaxUseCount { get; set; }
        int UseCount { get; protected set; }
        float Cooldown { get; set; }
        CoroutineHandle CdHandle { get; protected set; }

        int Use(int useCount = 1)
        {
            if (CdHandle == null || useCount <= 0)
                return 0;
            CdHandle = EasyCoroutine.Delay(Cooldown, true);
            CdHandle.OnDone(() => CdHandle = null);

            var realItemCount = 0;
            if (MaxUseCount > 0)
            {
                if (useCount >= MaxUseCount - UseCount)
                {
                    var count = useCount + UseCount - MaxUseCount;
                    var itemCount = count / MaxUseCount + 1;
                    realItemCount = Mathf.Min(itemCount, StackCount);
                    if (realItemCount == itemCount)
                    {
                        UseCount = count % MaxUseCount;
                    }
                    else
                    {
                        useCount = MaxUseCount - UseCount + realItemCount * MaxUseCount;
                        UseCount = 0;
                    }
                }
                else
                {
                    UseCount += useCount;
                }
            }
            else
            {
                UseCount += useCount;
            }

            OnUse(useCount);
            if (realItemCount > 0)
                Unstack(realItemCount);
            return useCount;
        }

        int Charge(int chargeCount)
        {
            var realChargeCount = Mathf.Min(chargeCount, UseCount);
            UseCount -= realChargeCount;
            OnCharge(realChargeCount);
            return realChargeCount;
        }

        protected void OnUse(int useCount);
        protected void OnCharge(int chargeCount);
    }
}