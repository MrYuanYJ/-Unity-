using UnityEngine;

namespace EasyFramework
{
    [System.Serializable]
    public struct ReactionMaterial : IReactionMaterial
    {
        public EBuff Buff => buff;
        public int Consumption => consumption;
        
        [SerializeField]private EBuff buff;
        [SerializeField]private int consumption;
    }
    
    [System.Serializable]
    public class Reactor: IReactor
    {
        public IReactionMaterial Material1=> material1;
        public IReactionMaterial Material2=> material2;
        public EBuff NewBuff=> newBuff;

        [SerializeField]private ReactionMaterial material1;
        [SerializeField]private ReactionMaterial material2;
        [SerializeField]private EBuff newBuff;
        
        public (IReactionMaterial self, IReactionMaterial other) GetMaterial(EBuff buff)
        {
            if(buff == Material1.Buff)
                return (material1,material2);
            else
                return (material2,material1);
        }
    }
}