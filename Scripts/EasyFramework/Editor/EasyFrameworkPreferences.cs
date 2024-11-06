using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EasyFramework
{
    public static class EasyFrameworkPreferences
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            RefreshModule();
            var settingsProvider = new SettingsProvider("Easy Framework/Settings", SettingsScope.User)
            {
                label = "框架设置",
                keywords = new[] { "框架","设置", "Framework","setting" },
                activateHandler = (searchContext, rootElement) =>
                {
                    var allModule=FrameworkSettings.Instance.AllModule;
                    var list = new ScrollView();
                    var moduleList = new List<(int index, VisualElement module)>();
                    foreach (var module in allModule)
                    {
                        var frameworkModule = (IFrameworkModule)module;
                        var moduleRootElement = new Foldout()
                        {
                            text = frameworkModule.ModuleName,
                            style = 
                            {
                                paddingTop = 5,
                                paddingBottom = 5,
                                fontSize = 16,
                                unityFontStyleAndWeight = FontStyle.Bold,
                                color = Color.white
                            },
                        };
                        var content = new Box()
                        {
                            style =
                            {
                                paddingTop = 5,
                                paddingBottom = 5,
                                paddingLeft = 5,
                                paddingRight = 5,

                                fontSize = 13,
                                unityFontStyleAndWeight = FontStyle.Normal,
                                color = Color.white
                            }
                        };
                        content.Add(new ObjectField()
                        {
                            label = "Data",
                            value = frameworkModule.Data,
                            style =
                            {
                                paddingBottom = 10,
                            }
                        });
                        content.Add(new IMGUIContainer(() =>
                        {
                            var serializedObject = frameworkModule.SerializedData;
                            serializedObject.Update();
                            var property = serializedObject.GetIterator();
                            
                            while (property.isArray ? property.NextVisible(false) : property.NextVisible(true))
                            {
                                if (property.name == "m_Script")
                                    continue;
                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.PropertyField(property);
                                EditorGUILayout.EndHorizontal();
                            }

                            serializedObject.ApplyModifiedProperties();
                        }));
                        content.Add(frameworkModule.DrawUI());
                        moduleRootElement.Add(content);
                        moduleList.Add((frameworkModule.Priority,moduleRootElement));
                    }
                    moduleList.Sort((item1,item2) => item1.index - item2.index);
                    moduleList.Reverse();
                    foreach (var item in moduleList)
                    {
                        list.Add(item.module);
                    }
                    
                    rootElement.Add(list);
                }
            };

            return settingsProvider;
        }
        
        public static void RefreshModule()
        {
            var frameworkSettings=FrameworkSettings.Instance;
            frameworkSettings.AllModule.Clear();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var baseType = typeof(AFrameworkModule);
            foreach (var assembly in assemblies)
            {
                if (assembly.GetName().Name == FrameworkSettings.Framework ||
                    assembly.GetReferencedAssemblies().Any(targetAssembly => targetAssembly.Name == FrameworkSettings.Framework))
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.IsSubclassOf(baseType)
                            && !type.IsAbstract)
                        {
                            frameworkSettings.AllModule.Add(Activator.CreateInstance(type));
                        }
                    }
                }
            }
        }
    }
}