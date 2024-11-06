namespace EasyFramework
{
    public interface ICondition
    {
        bool IsTure();
    }
    public interface ICondition<in T>
    {
        bool IsTure(T input);
    }

    public enum EConditionType
    {
        And,
        Or,
    }
}