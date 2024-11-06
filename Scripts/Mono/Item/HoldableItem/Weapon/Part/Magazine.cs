using UnityEngine;

namespace EasyFramework
{
    [AddComponentMenu("Easy Framework/Item/Equipment/Weapon Part/Magazine")]
    public class Magazine: AMonoEntityCarrier
    {
        public int singleConsumeAmmo;
        public int totalAmmo;
        public int magazineAmmo;
        public int reloadSpeed;
        public EReloadType reloadType;
        /// <summary>单次装填的持续时间</summary>
        public float singleReloadDuration;
        /// <summary>单次装填的弹药数量</summary>
        public int singleReloadAmmoCount;
        /// <summary>单次装填的弹药百分比</summary>
        public float singleReloadAmmoPct;
    }

    public enum EMagzineProperty
    {
        TotalAmmo,
        MagazineAmmo,
        ReloadSpeed,
    }
    public enum EReloadType
    {
        Once,
        Repeat,
    }
}