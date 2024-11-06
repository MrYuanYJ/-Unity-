using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace EasyFramework
{
    public class DrawFrameworkSettings: AFrameworkModule
    {

        public override ScriptableObject Data => FrameworkSettings.Instance;
        public override string ModuleName => "Main Settings";
        public override int Priority => Int32.MaxValue;

        // public override VisualElement DrawUI()
        // {
        //     var gridContainer = new VisualElement();
        //     gridContainer.style.flexDirection = FlexDirection.Row;
        //     gridContainer.style.flexWrap = Wrap.Wrap;
        //
        //     // 设置网格项之间的间距  
        //     gridContainer.style.justifyContent = Justify.SpaceAround;
        //     gridContainer.style.alignItems = Align.Center;
        //
        //     // 假设我们要创建一个3x3的网格  
        //     for (int i = 0; i < 9; i++)
        //     {
        //         // 创建网格项  
        //         var gridItem = new VisualElement();
        //         gridItem.Add(new Blackboard());
        //         gridItem.style.width = 100; // 设置网格项的宽度  
        //         gridItem.style.height = 100; // 设置网格项的高度  
        //         gridItem.style.borderBottomWidth = 1; 
        //         gridItem.style.borderRightWidth = 1; 
        //         gridItem.style.borderTopWidth = 1; 
        //         gridItem.style.borderLeftWidth = 1; 
        //         gridItem.style.borderTopColor = Color.white;
        //         gridItem.style.borderRightColor = Color.white;
        //         gridItem.style.borderLeftColor = Color.white;
        //         gridItem.style.borderBottomColor = Color.white;
        //         //gridItem.style.backgroundColor = new Color(Random.value, Random.value, Random.value); // 随机颜色  
        //
        //         // 将网格项添加到容器中  
        //         gridContainer.Add(gridItem);
        //     }
        //
        //     return gridContainer;
        // }
    }
}