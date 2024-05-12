namespace EasyFramework
{
    public class NormalBuffAddData: IBuffAddData
    {
        public NormalBuffAddData(IBuffableUnit unit,EBuff eBuff, int damage,float magnification = 1)
        {
            Unit = unit;
            EBuff = eBuff;
            Damage = damage;
            Magnification = magnification;
        }
        public int Damage { get; set; }
        public IBuffSource BuffSource { get; set; }
        public IBuffableUnit Unit { get; set; }
        public EBuff EBuff { get; set; }
        public float Magnification { get; set; }
    }
}