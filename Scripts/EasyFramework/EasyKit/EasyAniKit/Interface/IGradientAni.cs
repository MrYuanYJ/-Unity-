namespace EasyFramework
{
    public interface IGradientAni<T>
    {
        T StartValue { get; } 
        T EndValue { get; }

        void GradientAni(float progress,params object[] args);
    }
}