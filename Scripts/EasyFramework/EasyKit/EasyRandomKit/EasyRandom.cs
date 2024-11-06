using System;

namespace EasyFramework
{
    public class EasyRandom
    {
        public static Random R = new Random();
        
        public static int RandomInt(int min, int max)
        {
            return R.Next(min, max);
        }
        public static float RandomFloat(float min, float max)
        {
            return (float) R.NextDouble() * (max - min) + min;
        }
        public static bool RandomBool()
        {
            return R.Next(2) == 0;
        }
        public static T RandomEnum<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            int index = R.Next(values.Length);
            return (T) values.GetValue(index);
        }
        public static T RandomElement<T>(T[] array)
        {
            return array[R.Next(array.Length)];
        }
        public static T RandomElement<T>(T[] array, int min, int max)
        {
            return array[R.Next(min, max)];
        }

        public static T RandomByWeight<T>(RamdomDic<T> dic)
        {
            var weightSum = dic.WeightSum;
            var randomValue = RandomFloat(0, weightSum);
            
            foreach (var item in dic.Values)
            {
                randomValue -= item.Weight;
                if (randomValue <= 0)
                {
                    return item.Value;
                }
            }

            return default(T);
        }
    }
}