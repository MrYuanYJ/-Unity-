using UnityEngine.Playables;

namespace EasyFramework
{
    public interface IReactor
    {
        IReactionMaterial Material1{ get;}
        IReactionMaterial Material2{ get;}
        EBuff NewBuff{ get;}
    }

    public interface IReactionMaterial
    {
        EBuff Buff { get; }
        int Consumption{ get; }
    }
}