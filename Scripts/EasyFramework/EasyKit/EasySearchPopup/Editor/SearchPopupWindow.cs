using System;
using UnityEditor;
using UnityEngine;

namespace EasyFramework.Editor
{
    public class SearchPopupWindow: PopupWindowContent
    {
        public IDataViewList DataList;
        public IDataViewList FilteredDataList;
        public string SearchText;
        public int SelectedIndex
        {
            get=>currentSelectedDataIndex;
            set
            {
                currentSelectedDataIndex = value;
                currentSelectedData=(IDataView)FilteredDataList[value];
            }
        }

        public GUIStyle elementStyle =new GUIStyle(EditorStyles.toolbarButton);
        
        private Vector2 scrollPosition;
        private Action<IDataView> m_onSelected;
        private Rect activatorRect;
        private int currentSelectedDataIndex;
        private IDataView currentSelectedData;

        public override Vector2 GetWindowSize()
        {
            var rawWindowSize = base.GetWindowSize();
            var realWindowWidth = Mathf.Max(rawWindowSize.x, activatorRect.width);
            return new Vector2(realWindowWidth, 
                Mathf.Min(
                (1 + FilteredDataList.Count) * EditorGUIUtility.singleLineHeight + (3 + FilteredDataList.Count) * EditorGUIUtility.standardVerticalSpacing,
                400));
        }

        public static void ShowPopup(Rect activatorRect,object selectedData,IDataViewList dataList,Action<IDataView> onSelected)
        {
            var searchPopupWindow = new SearchPopupWindow(dataList, onSelected);
            searchPopupWindow.RefreshDataBySearchText(string.Empty);
            for (int i = 0; i < searchPopupWindow.FilteredDataList.Count; i++)
            {
                var data = (IDataView)searchPopupWindow.FilteredDataList[i];
                if (Equals(data.Value,selectedData))
                {
                    searchPopupWindow.SelectedIndex = i;
                    break;
                }
            }
            searchPopupWindow.activatorRect = activatorRect;
            PopupWindow.Show(activatorRect,searchPopupWindow);
        }
        public static void ShowPopup(Rect activatorRect,string selectedData,IDataViewList dataList,Action<IDataView> onSelected)
        {
            var searchPopupWindow = new SearchPopupWindow(dataList, onSelected);
            searchPopupWindow.RefreshDataBySearchText(string.Empty);
            for (int i = 0; i < searchPopupWindow.FilteredDataList.Count; i++)
            {
                var data = (IDataView)searchPopupWindow.FilteredDataList[i];
                if (data.Key==selectedData)
                {
                    searchPopupWindow.SelectedIndex = i;
                    break;
                }
            }
            searchPopupWindow.activatorRect = activatorRect;
            PopupWindow.Show(activatorRect,searchPopupWindow);
        }

        public SearchPopupWindow(IDataViewList dataList,Action<IDataView> onSelected)
        {
            DataList = dataList;
            m_onSelected = onSelected;
        }
        public override void OnOpen()
        {
            elementStyle.alignment = TextAnchor.MiddleLeft;
            elementStyle.padding.left = 10;
            
            LocateTo(SelectedIndex);
        }

        public override void OnGUI(Rect rect)
        {
            HandleKeyboardDown();
            DrawSearchField();
            DrawSearchContent(rect);
        }

        private void DrawSearchField()
        {
            EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            GUI.SetNextControlName("SearchField");
            var newSearchText = EditorGUILayout.TextField(SearchText,EditorStyles.toolbarSearchField);
            if (newSearchText != SearchText)
            {
                RefreshDataBySearchText(newSearchText);
                LocateTo(SelectedIndex);
            }
            if(GUI.GetNameOfFocusedControl()!="SearchField")
                GUI.FocusControl("SearchField");
        }

        private void DrawSearchContent(Rect rect)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            for (int i = 0; i < FilteredDataList.Count; i++)
            {
                var data = FilteredDataList[i];
                if (i == SelectedIndex)
                {
                    GUI.backgroundColor = Color.grey;//new Color(0.0f, 0.7f, 1f);
                }
                else
                {
                    GUI.backgroundColor = Color.white;
                }

                if (GUILayout.Button(new GUIContent(data.ToString()), elementStyle))
                {
                    Selected(i);
                }

            }
            EditorGUILayout.EndScrollView();
        }

        private void RefreshDataBySearchText(string searchText)
        {
            SearchText = searchText;
            FilteredDataList ??= (IDataViewList) Activator.CreateInstance(DataList.GetType());
            FilteredDataList.Clear();
            if(string.IsNullOrEmpty(searchText))
                FilteredDataList.AddDefault("<Default>");
            for (int i = 0; i < DataList.Count; i++)
            {
                var data = DataList[i];
                if (string.IsNullOrEmpty(searchText)||data.ToString().ToLower().Contains(searchText.ToLower()))
                {
                    FilteredDataList.Add(data);
                }
            }
            if(FilteredDataList.Count==0)
                FilteredDataList.AddDefault("<Default>");
            if(FilteredDataList.Contains(currentSelectedData))
                SelectedIndex = FilteredDataList.IndexOf(currentSelectedData);
            else
                SelectedIndex = 0;
        }

        private void Selected(int index)
        {
            SelectedIndex = index;
            m_onSelected?.Invoke((IDataView)FilteredDataList[index]);
            editorWindow.Close();
        }

        private void LocateTo(int index)
        {
            scrollPosition = new Vector2(0,
                index * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) -
                (GetWindowSize().y - EditorGUIUtility.singleLineHeight -
                 2 * EditorGUIUtility.standardVerticalSpacing)/2);
        }

        private void HandleKeyboardDown()
        {
            if (Event.current.type == EventType.KeyDown)
            {
                if (Event.current.keyCode == KeyCode.UpArrow)
                {
                    SelectedIndex = (SelectedIndex - 1 + FilteredDataList.Count) % FilteredDataList.Count;
                    LocateTo(SelectedIndex);
                    Event.current.Use();
                }
                else if (Event.current.keyCode == KeyCode.DownArrow)
                {
                    SelectedIndex = (SelectedIndex + 1) % FilteredDataList.Count;
                    LocateTo(SelectedIndex);
                    Event.current.Use();
                }
                else if (Event.current.keyCode == KeyCode.Escape)
                {
                    editorWindow.Close();
                    Event.current.Use();
                }
                else if (Event.current.keyCode == KeyCode.Return||Event.current.keyCode == KeyCode.KeypadEnter)
                {
                    Selected(SelectedIndex);
                    Event.current.Use();
                }
            }
        }
        
    }
}