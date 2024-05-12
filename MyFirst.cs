using EasyFramework;
using EasyFramework.EasyTaskKit;
using EasyFramework.EasyUIKit;
using EasyFramework.EventKit;


namespace First
{
    public class MyFirst  :AMonoEntity<ECBuffAble>
    {
    public EasyEvent myEvent = new();
    public EasyEvent<IEasyLife> myEvent2{ get; set; } = new();


    protected override void OnEnable()
    {
        base.OnEnable();
        EasyUI.OpenUI<EasyPanel>().Completed += _ => EasyUI.CloseUI<EasyPanel>();
        // var handle=EasyTask.Seconds(0.01f,5,_ =>
        // {
        //     BuffEvent.AddBuffByMeans.InvokeEvent(EMeans.Attack,new NormalBuffAddData(MEntity, EBuff.Water, 10));
        //     BuffEvent.AddBuffByMeans.InvokeEvent(EMeans.Attack,new NormalBuffAddData(MEntity, EBuff.Ice, 10,2));
        //     BuffEvent.AddBuffByMeans.InvokeEvent(EMeans.Skill,new NormalBuffAddData(MEntity, EBuff.Fire, 5));
        // });
        // handle.Completed += _ =>
        // {
        //     Game.Instance.Dispose();
        // };
    }
    }
}
