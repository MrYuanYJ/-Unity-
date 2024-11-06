using System.Collections.Generic;

namespace EasyFramework
{
    public class RamdomDic<T>
    {
        Dictionary<T,RandomUnit<T>> _dic = new();
        public float WeightSum = 0;

        public Dictionary<T,RandomUnit<T>>.ValueCollection Values => _dic.Values;
        public RandomUnit<T> this[T key]
        {
            get => _dic[key];
            set => Set(key, value);
        }
        public void Set(T key, RandomUnit<T> value)
        {
            _dic[key]=value;
            RefreshWeight();
        }
        public void Set(T key, float weight)
        {
            if (!_dic.TryGetValue(key, out RandomUnit<T> unit))
            {
                unit = new RandomUnit<T>(key, weight);
                _dic.Add(key, unit);
            }
            else
                unit.Weight = weight;
            
            RefreshWeight();
        }
        public void Remove(T key)
        {
            _dic.Remove(key);
            RefreshWeight();
        }

        public void RefreshWeight()
        {
            WeightSum = 0;
            foreach (var randomUnit in _dic.Values)
            {
                WeightSum += randomUnit.Weight;
            }
        }
        public  T RandomByWeight()
        {
            var weightSum = WeightSum;
            var randomValue = EasyRandom.RandomFloat(0, weightSum);
            
            foreach (var item in _dic.Values)
            {
                randomValue -= item.Weight;
                if (randomValue <= 0)
                {
                    return item.Value;
                }
            }

            return default;
        }
    }
}