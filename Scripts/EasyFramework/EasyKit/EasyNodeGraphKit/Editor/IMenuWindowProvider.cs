using System;
using System.Collections;
using UnityEngine;

namespace EasyFramework.Editor
{
    public interface IMenuWindowProvider
    {
        Action<Vector2> OnOpenMenuWindowAction { get; set; }
        Action<Vector2,object> OnMenuSelectEntryAction { get; set; }
        IEnumerable AllEntry { get;}
        Func<object,string> GetPath { get;}
        
        private void InitMenuWindow()
        {
            OnInitMenuWindow();
        }
        protected virtual void OnInitMenuWindow(){}

        public void OpenMenuWindow(Vector2 position)
        {
            InitMenuWindow();
            
            OnOpenMenuWindow(position);
            OnOpenMenuWindowAction?.Invoke(position);
        }
        protected virtual void OnOpenMenuWindow(Vector2 position){}

    }
}