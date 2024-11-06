using System;
using System.IO;
using CodeGenKit;
using EasyFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonoEntityTemplate", menuName = "CodeGenKit/MonoEntityTemplate")]
public class MonoEntityTemplate : AScriptsTemplate<TestTemplate>
{
    public override Func<CreateData, string> CreateFunc => CreateData =>
    {
        return new CodeRoot()
            .NameSpace(CreateData.NameSpace, code => code
                .Class(CreateData.ScriptName, classCode =>
                {
                    classCode
                        .PublicType(EPublicType.Public)
                        .BaseClass("AMonoEntityCarrier");
                    CreateData.CreateFiled(classCode);
                })
            ).Gen();
    };

    public string entityPath="Assets/Scripts/ScriptGens/Entities";
    public string targetStructure;
    
    public override Action<CreateData> OnCreateEnd => data =>
    {
        var root = new CodeRoot()
            .Using("EasyFramework",null)
            .NameSpace(data.NameSpace, code => code
                .Class($"{data.ScriptName}Entity", classCode => classCode
                    .PublicType(EPublicType.Public)
                    .BaseClass($"AMonoEntity<{data.ScriptName}>")
                    .Property(typeof(IStructure), "Structure", code => code
                        .PublicType(EPublicType.Public)
                        .Keywords("override")
                        .Get(code => code.LogicSingleLine($"return {targetStructure}.TryRegister();"))
                    )
                )
            );
        if(!Directory.Exists(entityPath))
        {
            Directory.CreateDirectory(entityPath);
        }
        var writer = File.CreateText($"{entityPath}/{data.ScriptName}Entity.cs");
        writer.WriteLine(root.Gen());
        writer.Dispose();
    };
}
