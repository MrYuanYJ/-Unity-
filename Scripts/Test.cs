using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EasyFramework;
using EasyFramework.EasySystem;
using EXFunctionKit;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    [DropDown]
    public EaseType easeType = EaseType.Linear;
    private Vector3 lastMousePos;
    private Vector3 startOffset;
    public Transform rotateTarget;
    public float x_speed = 1f;
    public float y_speed = 1f;
    public AudioParam audioParam = AudioParam.Default3D(EAudioTrack.Ui,null);
    public AudioSource audioSource;
    public InputCondition inputCondition;
    public EasyFramework.BlackBoard blackBoard = new();
    public List<BBValueView> list;
    public SESProperty<AudioParam> testProperty = new();
    public RoleProperties testProperties = new(10);
    
    void Start()
    {
        //startOffset = transform.position - rotateTarget.position;
        StartCoroutine(WaitEndOfFrameCoroutine(()=>TestFunc().ViewError()));
        EasyInputSetting.Instance.InputStateDict[inputCondition.Input].IsPressed.Register(OnInput);
        //ExCSharp.Modify(rotateTarget, t => t.position, new Vector3(100, 100, 100));
        ExCSharp.Modify(() => audioParam.audioTrack, EAudioTrack.Music, onChange: value => Debug.Log(value));
        testProperty.Register(value => audioParam = value);
    }
CoroutineHandle handle;
    public async Task TestFunc()
    {
        handle = EasyTask.Seconds(1, 6, second => Debug.Log(second))
                .OnCompleted(() => Debug.Log("handle completed"))
                .OnCanceled(() => Debug.Log("handle canceled"));
        await handle;
        Debug.Log("Hello World");
    }
[Button]
    public void Cancel()
    {
        for (int i = 0; i < 360; i++)
        {
            UnitEvent.ShootBullet(gameObject.GetComponent<Weapon>().Entity as IUnitEntity, "Assets/洞.prefab",new ShootInfo()
            {
                StartPosition = transform.position,
                StartDirection = Quaternion.AngleAxis(i,Vector3.up)*transform.forward,
                LifeTime = 10,
                TranslateInfo = new()
                {
                    ForwardSpeed = 1,
                    ForwardEase = new Ease(EaseType.None)
                }
            });
        }
    }
    [Button]
    public void PlayAudio()
    {
        Game.Instance.System<EasyAudioSystem>().PlayAudio3D(gameObject,audioParam,.5f);
    }
    [Button]
    public void StopAudio()
    {
        Game.Instance.System<EasyAudioSystem>().StopAudios3D(EAudioTrack.Ui,1);
    }
    [Button]
    public void PauseAudio()
    {
        Game.Instance.System<EasyAudioSystem>().PauseAudios3D(EAudioTrack.Ui,0.5f);
    }
    [Button]
    public void ResumeAudio()
    {
        Game.Instance.System<EasyAudioSystem>().ResumeAudios3D(EAudioTrack.Ui,0.5f);
    }
    public void Volume(float value)
    {
        Game.Instance.System<EasyAudioSystem>().GetAudioSource3D(gameObject, audioParam,out var source);
        source.SetVolume(value);
    }

    private static IEnumerator WaitEndOfFrameCoroutine(Action completed)
    {
        yield return new WaitForEndOfFrame();
        completed?.Invoke();
    }

    private void OnInput()
    {
        if (inputCondition.IsTure())
        {
            Debug.Log("Input is true");
        }
    }
    [Button]
    public void BBTestInt(int value)
    {
        blackBoard.Add<object>("TestInt",value);
    }
    [Button]
    public void BBTestFloat(float value)
    {
        blackBoard.Add<float>("TestFloat",value);
    }
    [Button]
    public void BBTestString(string value)
    {
        blackBoard.Add<string>("TestString",value);
    }
    [Button]
    public void BBTestBool(bool value)
    {
        blackBoard.Add<bool>("TestBool",value);
    }
    [Button]
    public void BBTestGameObject(GameObject[] value)
    {
        blackBoard.Add<GameObject[]>("TestGameObject",value);
    }
    [Button]
    public void BBTestGradient(Gradient value)
    {
        blackBoard.Add<Gradient>("TestGradient",value);
    }
    [Button]
    public void TestGet(string key)
    {
        Debug.Log(blackBoard.Get(key).GetType());
    }
    // Update is called once per frame
    /*void Update()
    {
        transform.position = rotateTarget.position + Quaternion.Euler(0, Input.mousePosition.x*x_speed, 0) * startOffset;
        transform.LookAt(rotateTarget.position+new Vector3(0,Input.mousePosition.y*y_speed));
    }*/
}


