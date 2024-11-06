namespace EasyFramework
{
    public interface IRandomUnit
    {
        object Value { get; }
        float Weight { get; set; }
    }
    public class RandomUnit<T>:IRandomUnit
    {
        public readonly T Value;
        object IRandomUnit.Value => Value;
        public float Weight { get; set; }
        public RandomUnit(T value, float weight = 1)
        {
            Value = value;
            Weight = weight;
        }
    }
}