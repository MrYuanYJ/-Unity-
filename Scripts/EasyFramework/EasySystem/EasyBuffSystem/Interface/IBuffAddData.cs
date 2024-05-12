namespace EasyFramework
{
    public interface IBuffAddData
    {
        IBuffSource BuffSource { get; set; }
        IBuffableUnit Unit { get; set; }
        EBuff EBuff { get; set; }
        float Magnification { get; set; }
    }
}