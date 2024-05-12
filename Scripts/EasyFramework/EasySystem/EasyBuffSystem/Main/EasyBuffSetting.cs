using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyFramework
{
    public partial class EasyBuffSetting : AScriptableObjectSingleton<EasyBuffSetting>
    {
        public List<Reactor> reactors = new List<Reactor>();
        public List<MeanCd> meanToElementBuffCds = new List<MeanCd>();

        public HashSet<BuffTag> DefaultBuffTags = new() {BuffTag.Normal};
        public readonly Dictionary<EBuff, Type> AllBuff = new();
        public readonly Dictionary<EBuff, HashSet<BuffTag>> BuffTagDic = new();
        public readonly Dictionary<EBuff, HashSet<Reactor>> ReactorDic = new();
        public readonly Dictionary<EMeans, float> MeansToElementBuffDic = new();

        private void OnEnable()
        {
            foreach (var reactor in reactors)
            {
                if (!ReactorDic.ContainsKey(reactor.Material1.Buff))
                    ReactorDic.Add(reactor.Material1.Buff, new HashSet<Reactor>());
                if (!ReactorDic.ContainsKey(reactor.Material2.Buff))
                    ReactorDic.Add(reactor.Material2.Buff, new HashSet<Reactor>());
                ReactorDic[reactor.Material1.Buff].Add(reactor);
                ReactorDic[reactor.Material2.Buff].Add(reactor);
            }

            var fields = typeof(EBuff).GetFields();
            var values = (IList) Enum.GetValues(typeof(EBuff));
            for (int i = 0; i < fields.Length; i++)
            {
                var attributes = fields[i].GetCustomAttributes(typeof(BindAttribute), false);
                if (attributes.Length > 0)
                {
                    var attribute = (BindAttribute) attributes[0];
                    var ebuff = (EBuff) values[i - 1];
                    AllBuff.Add(ebuff, attribute.Type);
                    var buffCategoryAttributes = attribute.Type.GetCustomAttributes(typeof(BuffTagAttribute), true);
                    foreach (BuffTagAttribute buffCategoryAttribute in buffCategoryAttributes)
                    {
                        if (!BuffTagDic.ContainsKey(ebuff))
                            BuffTagDic.Add(ebuff, buffCategoryAttribute.Tags);
                        foreach (var buffTag in buffCategoryAttribute.Tags)
                        {
                            BuffTagDic[ebuff].Add(buffTag);
                        }
                    }
                }
            }

            foreach (var meanCd in meanToElementBuffCds)
            {
                MeansToElementBuffDic.TryAdd(meanCd.Mean, meanCd.Cd);
            }
        }

        public float GetAddElementBuffByMeanCd(EMeans mean)
        {
            return MeansToElementBuffDic.GetValueOrDefault(mean, 0);
        }

        public HashSet<BuffTag> GetBuffTags(EBuff ebuff)
        {
            return BuffTagDic.GetValueOrDefault(ebuff, DefaultBuffTags);
        }
    }
    
}