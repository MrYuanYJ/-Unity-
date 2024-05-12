using EXFunctionKit;

namespace EasyFramework
{
    public interface IBuffSource
    {
        void AddBuff(IBuffAddData data)
        {
            data.BuffSource = this;
            BuffEvent.AddBuff.InvokeEvent(data);
        }
    }
}