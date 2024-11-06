using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelMaker : MonoBehaviour
{
    public Vector2Int levelSize;
    public Vector2 gridSize;
    public List<GameObject> tilePrefab;
    public Grid grid;
    
    //GUI绘制格子
    public void DrawGrid()
    {    
        var startPos = Vector3.zero;
        for (int x = 0; x <= levelSize.x; x++)
        {
            Handles.DrawLine(startPos + new Vector3(gridSize.x * x, 0, 0), startPos + new Vector3(gridSize.x * x, 0, levelSize.y * gridSize.y));
        }

        for (int y = 0; y <= levelSize.y; y++)
        {
            Handles.DrawLine(startPos + new Vector3(0, 0, gridSize.y * y), startPos + new Vector3(levelSize.x * gridSize.x, 0, gridSize.y * y));
        }
    }
    Vector2Int GetCellPosition(Vector2 mousePosition)
    {
        Vector3 mouseWorldPos = GUIToWorld(mousePosition);
        int cellX = Mathf.FloorToInt((mouseWorldPos.x) / levelSize.x);
        int cellY = Mathf.FloorToInt((mouseWorldPos.z) / levelSize.y);
        return new Vector2Int(cellX, cellY);
    }
    Vector3 GUIToWorld(Vector2 guiPosition)
    {
        guiPosition.y = Screen.height - guiPosition.y;
        
        Ray ray = Camera.current.ScreenPointToRay(guiPosition);
        var worldPosition=Camera.current.ScreenToWorldPoint(guiPosition);
        GetIntersectWithLineAndPlane(ray.origin,
            ray.direction, Vector3.up, Vector3.zero, out var position);
        return position;
    }
    /// <summary>
    /// 计算直线与平面的交点
    /// </summary>
    /// <param name="point">直线上某一点</param>
    /// <param name="direct">直线的方向</param>
    /// <param name="planeNormal">垂直于平面的的向量</param>
    /// <param name="planePoint">平面上的任意一点</param>
    /// <returns></returns>
    private bool GetIntersectWithLineAndPlane(Vector3 point, Vector3 direct, Vector3 planeNormal, Vector3 planePoint,out Vector3 result)
    {
        result = Vector3.zero; 
        //要注意直线和平面平行的情况
        float d1 = Vector3.Dot(direct.normalized, planeNormal);
        if(d1 == 0)return false;
        float d2 = Vector3.Dot(planePoint - point, planeNormal);
        float d3 = d2 / d1;
 
        result = d3 * direct.normalized + point;
        return true;
    }
    private void OnDrawGizmos()
    {
        DrawGrid();
        if (Event.current.type == EventType.MouseUp)
        {
            Vector2 mousePos = Event.current.mousePosition;
            Vector2Int cellPosition = GetCellPosition(mousePos);
            if (cellPosition.x >= 0 && cellPosition.x <= levelSize.x &&
                cellPosition.y >= 0 && cellPosition.y <= levelSize.y)
            {
                // 鼠标在网格上，显示格子位置
                Debug.Log("Mouse is in cell: " + cellPosition.ToString());
            }
        }
        //OnGUI();
    }
        private void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            Debug.LogError(EventType.MouseDown);//鼠标按下
        }
        else if (Event.current.type == EventType.MouseUp)
        {
            Debug.LogError(EventType.MouseUp);//鼠标抬起
        }
        else if (Event.current.type == EventType.MouseMove)
        {
            Debug.LogError(EventType.MouseMove);
        }
        else if (Event.current.type == EventType.MouseDrag)
        {
            Debug.LogError(EventType.MouseDrag);//鼠标拖动
        }
        else if (Event.current.type == EventType.KeyDown)
        {
            Debug.LogError(EventType.KeyDown);//按键按下
        }
        else if (Event.current.type == EventType.KeyUp)
        {
            Debug.LogError(EventType.KeyUp);//按键抬起
        }
        else if (Event.current.type == EventType.ScrollWheel)
        {
            Debug.LogError(EventType.ScrollWheel);//中轮滚动
        }
        else if (Event.current.type == EventType.Repaint)
        {
            Debug.LogError(EventType.Repaint);//每一帧重新渲染会发
        }
        else if (Event.current.type == EventType.Layout)
        {
            Debug.LogError(EventType.Layout);
        }
        else if (Event.current.type == EventType.DragUpdated)
        {
            Debug.LogError(EventType.DragUpdated);//拖拽的资源进入界面
        }
        else if (Event.current.type == EventType.DragPerform)
        {
            Debug.LogError(EventType.DragPerform);//拖拽的资源放到了某个区域里
        }
        else if (Event.current.type == EventType.Ignore)
        {
            Debug.LogError(EventType.Ignore);//操作被忽略
        }
        else if (Event.current.type == EventType.Used)
        {
            Debug.LogError(EventType.Used);//操作已经被使用过了
        }
        else if (Event.current.type == EventType.ValidateCommand)
        {
            Debug.LogError(EventType.ValidateCommand);//有某种操作被触发（例如复制和粘贴）
        }
        else if (Event.current.type == EventType.ExecuteCommand)
        {
            Debug.LogError(EventType.ExecuteCommand);//有某种操作被执行（例如复制和粘贴）
        }
        else if (Event.current.type == EventType.DragExited)
        {
            Debug.LogError(EventType.DragExited);//松开拖拽的资源
        }
        else if (Event.current.type == EventType.ContextClick)
        {
            Debug.LogError(EventType.ContextClick);//右键点击
        }
        else if (Event.current.type == EventType.MouseEnterWindow)
        {
            Debug.LogError(EventType.MouseEnterWindow);
        }
        else if (Event.current.type == EventType.MouseLeaveWindow)
        {
            Debug.LogError(EventType.MouseLeaveWindow);
        }
    }
}