using System;
using System.Collections;
using System.Collections.Generic;
using EasyFramework.Editor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace EasyFramework
{
    public struct MenuEntryTree
    {
        public string Name;
        public Dictionary<string,object> Content;
        public Dictionary<string,MenuEntryTree> Branches;

        public void CreateSearchTree(List<SearchTreeEntry> searchTreeEntries, int depth)
        {
            if (!string.IsNullOrEmpty(Name))
                searchTreeEntries.Add(new SearchTreeGroupEntry(new GUIContent(Name)) {level = depth});
            if (Branches != null)
                foreach (var branches in Branches.Values)
                {
                    branches.CreateSearchTree(searchTreeEntries, depth + 1);
                }

            if (Content != null)
                foreach (var content in Content)
                {
                    searchTreeEntries.Add(new SearchTreeEntry(new GUIContent(content.Key)) {level = depth+1, userData = content.Value});
                }
        }
    }
    public abstract class SearchMenuWindowProvider: ScriptableObject,ISearchWindowProvider,IMenuWindowProvider
    {
        public abstract IEnumerable AllEntry { get; }
        public abstract Func<object, string> GetPath { get; }
        void IMenuWindowProvider.OnOpenMenuWindow(Vector2 position)
        {
            SearchWindow.Open(new SearchWindowContext(position), this);
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            //List<Dictionary<string,List<object>>> groups = new();
            MenuEntryTree tree = new()
            {
                Name = "",
                Content = new Dictionary<string, object>(),
                Branches = new Dictionary<string, MenuEntryTree>()
            };
            foreach (var entry in AllEntry)
            {
                var Path = GetPath(entry);
                MenuEntryTree currentBranch = tree;
                if (!string.IsNullOrEmpty(Path))
                {
                    var path=Path.Split('/');
                    for (int i = 0; i < path.Length; i++)
                    {
                        if (i == path.Length - 1)
                        {
                            currentBranch.Content.Add(path[i],entry);
                            break;
                        }
                        if (currentBranch.Branches.ContainsKey(path[i]))
                        {
                            currentBranch = currentBranch.Branches[path[i]];
                        }
                        else
                        {
                            currentBranch.Branches[path[i]] = new MenuEntryTree()
                            {
                                Name = path[i],
                                Content = new Dictionary<string, object>(),
                                Branches = new Dictionary<string, MenuEntryTree>()
                            };
                            currentBranch = currentBranch.Branches[path[i]];
                        }
                    }
                }
                else
                {
                    tree.Content.Add(entry.ToString().Split('.')[^1], entry);
                }
            }
            var entries = new List<SearchTreeEntry>();
            entries.Add(new SearchTreeGroupEntry(new GUIContent("Search")));
            tree.CreateSearchTree(entries, 0);
            return entries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            var userData = SearchTreeEntry.userData;
            OnMenuSelectEntryAction?.Invoke(context.screenMousePosition, userData);
            return true;
        }

        
        public Action<Vector2> OnOpenMenuWindowAction { get; set; }
        public Action<Vector2, object> OnMenuSelectEntryAction { get; set; }
    }
}