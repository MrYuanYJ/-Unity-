using System;
using EasyFramework.EventKit;
using EXFunctionKit;
using UnityEngine;

namespace EasyFramework.EasySystem
{
    public class EasyInputSystem: ASystem,IUpdateAble
    {
        private EasyInputSetting _setting;
        public IEasyEvent UpdateEvent { get; }= new EasyEvent();
        private bool _hasInputInLastFrame;
        protected override void OnInit()
        {
            _setting = EasyInputSetting.Instance;
            _setting.Init();
        }

        protected override void OnUnActive()
        {
            foreach (var item in _setting.KeyCodeListenerDict)
            {
                foreach (var data in item.Value)
                {
                    data.IsPressed = false;
                    data.TimePressed = 0;
                }
            }

            foreach (var item in _setting.InputStateDict)
            {
                item.Value.IsPressed.Value = false;
                item.Value.TimePressed.Value = 0;
            }
            _setting.MoveInput.Set(Vector2.zero);
        }

        void IUpdateAble.OnUpdate()
        {
            if (!Input.anyKey&&!_hasInputInLastFrame)
            {
                if (_setting.MoveInput.Value != Vector2.zero)
                    _setting.MoveInput.Value = Vector2.MoveTowards(_setting.MoveInput.Value, Vector2.zero,
                        _setting.InputSmooth*_setting.InputSmoothMultiplier * Time.deltaTime);
                
                return;
            }
            _hasInputInLastFrame = false;
            
            OnOldInput();
            SetMoveInput();
        }

        private void OnOldInput()
        {
            foreach (var item in _setting.KeyCodeListenerDict)
            {
                float maxTime = 0;
                foreach (var data in item.Value)
                {
                    if (Input.GetKeyDown(data.KeyCode))
                    {
                        data.IsPressed = true;
                        data.TimePressed = 0;
                    }
                    else if (Input.GetKeyUp(data.KeyCode))
                    {
                        data.IsPressed = false;
                        data.TimePressed = 0;
                    }
                    else if (Input.GetKey(data.KeyCode))
                    {
                        _hasInputInLastFrame = true;
                        data.TimePressed += Time.deltaTime;
                    }
                    else// if (!Application.isFocused && data.IsPressed)
                    {
                        data.IsPressed = false;
                        data.TimePressed = 0;
                    }
                    
                    maxTime=Mathf.Max(maxTime,data.TimePressed);
                }

                if (maxTime > 0)
                {
                    _setting.InputStateDict[item.Key].IsPressed.Value = true;
                    _setting.InputStateDict[item.Key].TimePressed.Value = maxTime;
                }
                else
                {
                    _setting.InputStateDict[item.Key].IsPressed.Value = false;
                    _setting.InputStateDict[item.Key].TimePressed.Value = 0;
                }
            }
        }
        private void SetMoveInput()
        {
            var direction = Vector2.zero;
            if(_setting.InputStateDict[InputType.Left].IsPressed.Value)
                direction.x--;
            if(_setting.InputStateDict[InputType.Right].IsPressed.Value)
                direction.x ++;
            if(_setting.InputStateDict[InputType.Up].IsPressed.Value)
                direction.y ++;
            if(_setting.InputStateDict[InputType.Down].IsPressed.Value)
                direction.y --;

            _setting.MoveInput.Value = new Vector2(
                Mathf.MoveTowards(_setting.MoveInput.Value.x, direction.x,
                    _setting.MoveInput.Value.x * direction.x >= 0
                        ? _setting.InputSmooth * Time.deltaTime
                        : _setting.InputSmooth * _setting.InputSmoothMultiplier * Time.deltaTime),
                Mathf.MoveTowards(_setting.MoveInput.Value.y, direction.y,
                    _setting.MoveInput.Value.y * direction.y >= 0
                        ? _setting.InputSmooth * Time.deltaTime
                        : _setting.InputSmooth * _setting.InputSmoothMultiplier * Time.deltaTime));
        }
        
        public IUnRegisterHandle OnInputChange(InputType inputType,Action<bool> action)=> _setting.InputStateDict[inputType].IsPressed.Register(action);
        public IUnRegisterHandle OnInputPress(InputType inputType,Action<float> action)=> _setting.InputStateDict[inputType].TimePressed.Register(action);
    }
}