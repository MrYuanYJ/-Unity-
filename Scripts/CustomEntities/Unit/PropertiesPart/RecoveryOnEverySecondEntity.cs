namespace EasyFramework
{
    public class RecoveryOnEverySecondEntity: ARecoveryEntity<RecoveryOnEverySecond>
    {
        protected override void OnActive()
        {
            base.OnActive();
            EasyCoroutine.RegisterEverySecond(Recovery);
        }

        protected override void OnUnActive()
        {
            base.OnUnActive();
            EasyCoroutine.UnRegisterEverySecond(Recovery);
        }
    }
}