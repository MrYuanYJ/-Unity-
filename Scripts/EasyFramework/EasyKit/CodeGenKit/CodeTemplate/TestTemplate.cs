#if UNITY_EDITOR
using System;
using System.IO;
using EasyFramework.EasyUIKit;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace CodeGenKit
{
    public class TestTemplate: AScriptsTemplate<TestTemplate>
    {
        public string prefabPath=PrefabPath.Value;
        private static Lazy<string> PrefabPath = new Lazy<string>(() =>
        {
            var path = "Assets/Res/Prefabs";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        });
        public override Func<CreateData,string> CreateFunc => CreateData =>
            new CodeRoot()
                .Using("EasyFramework.EasyUIKit",null)
                .NameSpace(CreateData.NameSpace, code =>
                    code
                        .Class(CreateData.ScriptName, classCode =>
                        {
                            classCode
                                .BaseClass(nameof(AEasyPanel))
                                .Property(typeof(EPanel),"Layer",propertyCode =>propertyCode.PublicType(EPublicType.Public).Keywords("override").Get(null) )
                                .Function("OnInit", functionCode => functionCode.Keywords("override").PublicType(EPublicType.Protected))
                                .Function("OnOpen", functionCode => functionCode.Keywords("override").PublicType(EPublicType.Protected))
                                .Function("OnShow", functionCode => functionCode.Keywords("override").PublicType(EPublicType.Protected))
                                .Function("OnHide", functionCode => functionCode.Keywords("override").PublicType(EPublicType.Protected))
                                .Function("OnClose", functionCode => functionCode.Keywords("override").PublicType(EPublicType.Protected));
                            CreateData.CreateFiled(classCode);
                        })
                ).Gen();
        
        
        public override Action<CreateData> OnCreateEnd=>(createData)=>
        {
            var path = $"{prefabPath}/{createData.ScriptName}.prefab";
            var prefab =PrefabUtility.SaveAsPrefabAsset(createData.RootGameObject, path);
            AssetDatabase.SaveAssets(); 
        };
        
        
    }
}
#endif