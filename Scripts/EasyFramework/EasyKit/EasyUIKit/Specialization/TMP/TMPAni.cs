using System;
using System.Collections;
using System.Collections.Generic;
using EasyFramework.EasyTaskKit;
using EXFunctionKit;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace EasyFramework.EasyUIKit
{
    [RequireComponent(typeof(TMP_Text)), DisallowMultipleComponent]
    public class TMPAni: MonoBehaviour
    {
        public int loopCount = -1;
        public float loopInterval = 0.5f;
        public float interval = 0.5f;
        public float speed = 0.5f;
        public Ease ease;

        private readonly HashSet<CoroutineHandle> _handles = new();
        private CoroutineHandle _mainHandle;
        private TMP_Text _text;
        private int _charCount;
        private int _currentVisibleCharCount;
        private bool needRefresh;
        
        private TMP_MeshInfo[] _meshInfoCopy;
        private TMP_MeshInfo[] _meshInfoOriginal;

        private void Awake()
        {
            TryGetComponent(out _text);
        }

        private void Start()
        {
            _text.ForceMeshUpdate();
            _charCount= _text.textInfo.characterCount;
            _meshInfoOriginal = new TMP_MeshInfo[_text.textInfo.meshInfo.Length];
            _meshInfoCopy = new TMP_MeshInfo[_text.textInfo.meshInfo.Length];
            for (int i = 0; i < _text.textInfo.meshInfo.Length; i++)
            {
                _meshInfoOriginal[i].colors32 =  _text.textInfo.meshInfo[i].mesh.colors32;
                _meshInfoOriginal[i].vertices =  _text.textInfo.meshInfo[i].mesh.vertices;
                _meshInfoCopy[i].colors32 = _text.textInfo.meshInfo[i].mesh.colors32;
                _meshInfoCopy[i].vertices = _text.textInfo.meshInfo[i].mesh.vertices;
            }

            _currentVisibleCharCount = _text.textInfo.characterCount;
            _mainHandle=EasyTask.RegisterEasyCoroutine(AllAni);
            //EasyTask.RegisterEasyCoroutine(OneByOneAni);
        }

        private IEnumerator AllAni()
        {
            while (loopCount<0)
            {
                var handle=OneByOneAni(_currentVisibleCharCount, UpAndDown).RegisterEasyCoroutine();
                handle.Completed += h => _handles.Remove(h);
                handle.Canceled += h => _handles.Remove(h);
                _handles.Add(handle);
                yield return EasyCoroutine.Delay(loopInterval, false);
            }
        }
        private IEnumerator OneByOneAni(int count,params Func<int,int, int,CoroutineHandle>[] tmpAniCoroutines)
        {
            for (int i = 0; i < count; i++)
            {
                var charInfo = _text.textInfo.characterInfo[i];

                if (charInfo.character is '\0' or ' ')
                    continue;
                SingleCharAni(i,tmpAniCoroutines);
                yield return EasyCoroutine.Delay(interval*ease.Evaluate((i+1).AsFloat()/count), false);
            }
        }
        private IEnumerator OneByOneAni()
        {
            for (int i = 0; i < _charCount; i++)
            {
                var charInfo = _text.textInfo.characterInfo[i];

                if (charInfo.character is '\0' or ' ')
                    continue;
                SingleCharAni(i,Writer);
                yield return EasyCoroutine.Delay(interval*ease.Evaluate((i+1).AsFloat()/_charCount), false);
            }
        }

        private CoroutineHandle[] SingleCharAni(int currentCharIndex,params Func<int,int, int,CoroutineHandle>[] tmpAniCoroutines)
        {
            var charInfo = _text.textInfo.characterInfo[currentCharIndex];
            
            var vertexIndex = charInfo.vertexIndex;
            var materialIndex = charInfo.materialReferenceIndex;

            var tmpAniHandles = new CoroutineHandle[tmpAniCoroutines.Length];
            for(int i=0;i<tmpAniCoroutines.Length;i++)
            {
                var tmpAniCoroutine = tmpAniCoroutines[i];
                var handle = tmpAniCoroutine(currentCharIndex,vertexIndex, materialIndex);
                tmpAniHandles[i]=handle;
                handle.Completed += h => _handles.Remove(h);
                handle.Canceled += h => _handles.Remove(h);
                _handles.Add(handle);
            }
            return tmpAniHandles;
        }

        private void Update()
        {
            RefreshView();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus && needRefresh)
            {
                needRefresh = false;
                for (int j = 0; j < _text.textInfo.meshInfo.Length; j++)
                {
                    //更新几何网格数据
                    _text.textInfo.meshInfo[j].mesh.SetColors(_meshInfoCopy[j].colors32);
                    _text.textInfo.meshInfo[j].mesh.SetVertices(_meshInfoCopy[j].vertices);
                    _text.UpdateGeometry(_text.textInfo.meshInfo[j].mesh, j);
                }
            }
        }

        private void RefreshView()
        {
            if (_handles.Count > 0)
            {
                if (!Application.isFocused)
                {
                    needRefresh = true;
                    return;
                }
                for (int j = 0; j < _text.textInfo.meshInfo.Length; j++)
                {
                    //更新几何网格数据
                    _text.textInfo.meshInfo[j].mesh.SetColors(_meshInfoCopy[j].colors32);
                    _text.textInfo.meshInfo[j].mesh.SetVertices(_meshInfoCopy[j].vertices);
                    _text.UpdateGeometry(_text.textInfo.meshInfo[j].mesh, j);
                }
            }
        }

        private CoroutineHandle Writer(int currentCharIndex,int vertexIndex, int materialIndex)
        {
            _currentVisibleCharCount = currentCharIndex;
            if (currentCharIndex == 0)
            {
                for (int i = 0; i < _text.textInfo.meshInfo.Length; i++)
                {
                    _meshInfoCopy[i].colors32 = new Color32[_text.textInfo.meshInfo[i].mesh.colors32.Length];
                    _meshInfoCopy[i].vertices = new Vector3[_text.textInfo.meshInfo[i].mesh.vertices.Length];
                }
            }

            for (int i = 0; i < 4; i++)
            {
                _meshInfoCopy[materialIndex].colors32[vertexIndex + i] = _meshInfoOriginal[materialIndex].colors32[vertexIndex + i];
                _meshInfoCopy[materialIndex].vertices[vertexIndex + i] = _meshInfoOriginal[materialIndex].vertices[vertexIndex + i];
            }

            var handle = EasyCoroutine.Delay(speed, false);
            handle.Completed += _ => _currentVisibleCharCount = currentCharIndex + 1;
            return handle;
        }
        private CoroutineHandle FadeIn(int currentCharIndex,int vertexIndex, int materialIndex)
        {
            var colorsCopy = _meshInfoCopy[materialIndex].colors32;
            var colors = new Color32[]
            {
                colorsCopy[vertexIndex],
                colorsCopy[vertexIndex + 1],
                colorsCopy[vertexIndex + 2],
                colorsCopy[vertexIndex + 3]
            }; 
            return EasyCoroutine.TimeTask(speed, progress =>
            {
                for (int i = 0; i < 4; i++)
                {
                    colorsCopy[vertexIndex + i] = new Color32(colors[i].r, colors[i].g, colors[i].b,
                        (byte) Mathf.Lerp(0, colors[i].a, progress));
                }
            }, false);
        }

        private CoroutineHandle SineWave(int currentCharIndex,int vertexIndex, int materialIndex)
        {
            var verticesCopy = _meshInfoCopy[materialIndex].vertices;
            var verticesOriginal = _meshInfoOriginal[materialIndex].vertices;

            return EasyCoroutine.TimeTask(speed, progress =>
            {
                for (int i = 0; i < 4; i++)
                {
                    verticesCopy[vertexIndex + i] = Mathf.Sin(progress*0.5f*Mathf.PI)*verticesOriginal[vertexIndex + i];
                    //Vector3.Lerp(verticesOriginal[vertexIndex+i].Add(x:-1000,y:0),verticesOriginal[vertexIndex+i],Mathf.Sin(progress*0.5f*Mathf.PI));
                }
            }, false);
        }

        private CoroutineHandle UpAndDown(int currentCharIndex,int vertexIndex, int materialIndex)
        {
            var verticesCopy = _meshInfoCopy[materialIndex].vertices;
            var verticesOriginal = _meshInfoOriginal[materialIndex].vertices;

            return EasyCoroutine.TimeTask(speed, progress =>
            {
                for (int i = 0; i < 4; i++)
                {
                    verticesCopy[vertexIndex + i] = verticesOriginal[vertexIndex + i]
                        .Add(y: Mathf.Sin(progress * 2 * Mathf.PI) * 5);
                }
            }, false);
        }
    }
}