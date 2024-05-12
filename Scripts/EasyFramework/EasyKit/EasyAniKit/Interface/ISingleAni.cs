
namespace EasyFramework
{
    public interface ISingleAni
    {
        IEase Ease { get; }

        void Playing(float progress,params object[] args);
    }
}