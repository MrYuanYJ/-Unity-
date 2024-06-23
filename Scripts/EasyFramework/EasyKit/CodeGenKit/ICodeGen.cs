#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenKit
{
    public interface ICodeGen
    {
        public int Indent { get; set; }
        public CodeRoot Root { get; set; }
        public StringBuilder str { get; set; }
        public string Gen();
        public void Wrap(int spIndent=0)
        {
            str.Append("\n");
            for (int i = 0; i < Indent+spIndent; i++)
            {
                str.Append("    ");
            }
        }
    }

    public interface IScriptsTemplate
    {
        public string Namespace { get; }
        public string Path { get; }
        public Func<CreateData,string> CreateFunc { get; }
        public Action<CreateData> OnCreateEnd { get; }
        void CreateScript(ScriptCreator creator);
    }
    public interface IHasAttribute : ICodeGen
    {
        List<string> Attributes { get; set; }
        
        public void GenAttributes()
        {
            if (Attributes.Count > 0)
            {
                Wrap();
                str.Append("[");
                bool needComma = false;
                foreach (var attribute in Attributes)
                {
                    if (needComma)
                        str.Append($",{attribute}");
                    else
                        str.Append(attribute);
                    needComma = true;
                }
                str.Append("] ");
            }
        }
    }

    public interface IHasPublicType : ICodeGen
    {
        EPublicType PublicType { get; set; }
        public void GenPublicType()
        {
            str.Append(PublicType switch
            {
                EPublicType.Public => "public",
                EPublicType.Private => "private",
                EPublicType.Protected => "protected",
                _=>throw new Exception()
            });
        }
    }

    public interface IHasKeywords : ICodeGen
    {
        string Keywords { get; set; }
        public void GenKeywords()
        {
            if (!string.IsNullOrWhiteSpace(Keywords))
                str.Append($" {Keywords}");
        }
    }

    public interface IHasReturnType : ICodeGen
    {
        string ReturnType { get; set; }
        public void GenReturnType()
        {
            str.Append(string.IsNullOrWhiteSpace(ReturnType)?" void":$" {ReturnType}");
        }
    }

    public interface IHasParameter : ICodeGen
    {
        List<string> Parameters { get; set; }
        public void GenParameter()
        {
            bool needComma = false;
            foreach (var parameter in Parameters)
            {
                if(needComma)
                    str.Append($", {parameter}");
                else
                    str.Append($"{parameter}");
                needComma = true;
            }
        }
    }

    public interface IHasLogic : ICodeGen
    {
        List<string> Logics { get; set; }
        public void GenLogic()
        {
            for (int i = 0; i < Logics.Count; i++)
            {
                Wrap(1);
                str.Append(Logics[i]);
            }
        }
    }

    public interface IHasBaseClass : IHasBaseInterface
    {
        string BaseClass { get; set; }
        public void GenBase()
        {
            if(string.IsNullOrWhiteSpace(BaseClass)&&BaseInterfaces.Count==0)
                return;
            str.Append(" :");
            bool needComma = false;
            if (!string.IsNullOrWhiteSpace(BaseClass))
            {
                str.Append($" {BaseClass}");
                needComma = true;
            }

            foreach (var iBaseInterface in BaseInterfaces)
            {
                if(needComma)
                    str.Append($", {iBaseInterface}");
                else
                    str.Append($" {iBaseInterface}");
                needComma = true;
            }
        }
    }

    public interface IHasBaseInterface : ICodeGen
    {
        List<string> BaseInterfaces { get; set; }
        public void GenBaseInterface()
        {
            if(BaseInterfaces.Count==0)
                return;
            str.Append(" :");
            bool needComma = false;
            foreach (var iBaseInterface in BaseInterfaces)
            {
                if(needComma)
                    str.Append($", {iBaseInterface}");
                else
                    str.Append($" {iBaseInterface}");
                needComma = true;
            }
        }
    }

    public interface IClassContainer : ICodeGen
    {
        List<ICodeGen> Classes { get; set; }
        
        public void GenClass()
        {
            foreach (var code in Classes)
            {
                str.Append(code.Gen());
            }
        }
    }

    public interface IStructContainer : ICodeGen
    {
        List<ICodeGen> Structs { get; set; } 
        
        public void GenStruct()
        {
            foreach (var code in Structs)
            {
                str.Append(code.Gen());
            }
        }
    }

    public interface IEnumContainer : ICodeGen
    {
        List<ICodeGen> Enums { get; set; }
        
        public void GenEnum()
        {
            foreach (var code in Enums)
            {
                str.Append(code.Gen());
            }
        }
    }

    public interface IFieldContainer : ICodeGen
    {
        List<ICodeGen> Fields { get; set; }
        
        public void GenFiled()
        {
            foreach (var code in Fields)
            {
                str.Append(code.Gen());
            }
        }
    }

    public interface IIndexerContainer : ICodeGen
    {
        ICodeGen Get { get; set;  }
        ICodeGen Set { get; set;  }
        
        public void GenIndexer()
        {
            if (Get == null && Set == null)
            {
                Wrap(1);
                str.Append("get; set;");
            }
            else
            {
                str.Append(Get.Gen());
                if (Set != null)
                    str.Append(Set.Gen());
            }
        }

    }

    public interface IPropertyContainer : ICodeGen
    {
        List<ICodeGen> Propertys { get; set; }
        
        public void GenProperty()
        {
            foreach (var code in Propertys)
            {
                str.Append(code.Gen());
            }
        }
    }

    public interface IFunctionContainer : ICodeGen
    {
        List<ICodeGen> Functions { get; set; }
        
        public void GenFunction()
        {
            foreach (var code in Functions)
            {
                str.Append(code.Gen());
            }
        }
    }

    public interface IConstructorContainer : ICodeGen
    {
        string constructorName { get; set; }
        List<ICodeGen> Constructors { get; set; }
        
        public void GenConstructor()
        {
            foreach (var code in Constructors)
            {
                str.Append(code.Gen());
            }
        }
    }

    public static class ICodeGenEX
    {
        public static T Attribute<T>(this T self, params string[] attributes) where T: IHasAttribute
        {
            self.Attributes.AddRange(attributes);
            return self;
        }
        public static T PublicType<T>(this T self, EPublicType publicType) where T: IHasPublicType
        {
            self.PublicType = publicType;
            return self;
        }
        public static T Keywords<T>(this T self, string keywords) where T: IHasKeywords
        {
            self.Keywords = keywords;
            return self;
        }
        public static T ReturnType<T>(this T self, string typeAssemblyQualifiedName) where T: IHasReturnType
        {
            return self.ReturnType(Type.GetType(typeAssemblyQualifiedName));
        }

        public static T ReturnType<T>(this T self, Type returnType) where T : IHasReturnType
        {
            if (returnType.Namespace != null)
                self.Root.Using(returnType.Namespace, null);
            self.ReturnType = returnType.Name;
            return self;
        }

        public static T Parameter<T>(this T self, string typeAssemblyQualifiedName,string name) where T: IHasParameter
        {
            return self.Parameter(Type.GetType(typeAssemblyQualifiedName),name);
        }
        public static T Parameter<T>(this T self, Type type,string name) where T: IHasParameter
        {
            self.Root.Using(type.Namespace, null);
            self.Parameters.Add($"{type.Name} {name}");
            return self;
        }
        public static T LogicSingleLine<T>(this T self, string logic) where T: IHasLogic
        {
            self.Logics.Add(logic);
            return self;
        }
        public static T BaseClass<T>(this T self, string baseClass) where T: IHasBaseClass
        {
            self.BaseClass=baseClass;
            return self;
        }
        public static T BaseInterface<T>(this T self, string baseInterface) where T: IHasBaseInterface
        {
            self.BaseInterfaces.Add(baseInterface);
            return self;
        }
        public static T Class<T>(this T self, string name,Action<ClassCode> setCode) where T: IClassContainer
        {
            var code = new ClassCode(name,self.Root);
            code.Indent = self.Indent + 1;
            self.Classes.Add(code);
            setCode?.Invoke(code);
            return self;
        }
        public static T Struct<T>(this T self, string name,Action<StructCode> setCode) where T: IStructContainer
        {
            var code = new StructCode(name,self.Root);
            code.Indent = self.Indent + 1;
            self.Structs.Add(code);
            setCode?.Invoke(code);
            return self;
        }
        public static T Enum<T>(this T self, string name,Action<EnumCode> setCode) where T: IEnumContainer
        {
            var code = new EnumCode(name,self.Root);
            code.Indent = self.Indent + 1;
            self.Enums.Add(code);
            setCode?.Invoke(code);
            return self;
        }
        public static T Field<T>(this T self,string typeAssemblyQualifiedName, string name,Action<FiledCode> setCode) where T: IFieldContainer
        {
            return self.Field(Type.GetType(typeAssemblyQualifiedName),name,setCode);
        }

        public static T Field<T>(this T self, IScriptCreator creator,ReferenceLinkData data, Action<FiledCode> setCode) where T : IFieldContainer
        {
            var code = new FiledCode(creator.ScriptName, data.Name,self.Root);
            if (creator.NameSpace != self.Root.GetNameSpace())
                self.Root.Using(creator.NameSpace, null);
            code.Indent = self.Indent + 1;
            self.Fields.Add(code);
            setCode?.Invoke(code);
            return self;
        }
        public static T Field<T>(this T self, Type type, string name, Action<FiledCode> setCode) where T : IFieldContainer
        {
            var code = new FiledCode(type, name,self.Root);
            self.Root.Using(type.Namespace, null);
            code.Indent = self.Indent + 1;
            self.Fields.Add(code);
            setCode?.Invoke(code);
            return self;
        }

        public static T Property<T>(this T self,string typeAssemblyQualifiedName, string name,Action<PropertyCode> setCode) where T: IPropertyContainer
        {
            return self.Property(Type.GetType(typeAssemblyQualifiedName),name,setCode);
        }
 
        public static T Property<T>(this T self,Type type, string name,Action<PropertyCode> setCode) where T: IPropertyContainer
        {
            var code = new PropertyCode(type,name,self.Root);
            self.Root.Using(type.Namespace, null);
            code.Indent = self.Indent + 1;
            self.Propertys.Add(code);
            setCode?.Invoke(code);
            return self;
        }
        public static T Function<T>(this T self,string name,Action<FunctionCode> setCode) where T: IFunctionContainer
        {
            var code = new FunctionCode(name,self.Root);
            code.Indent = self.Indent + 1;
            self.Functions.Add(code);
            setCode?.Invoke(code);
            return self;
        }
        public static T Constructor<T>(this T self,Action<ConstructorCode> setCode) where T: IConstructorContainer
        {
            var code = new ConstructorCode(self.constructorName,self.Root);
            code.Indent = self.Indent + 1;
            self.Constructors.Add(code);
            setCode?.Invoke(code);
            return self;
        }
        public static T Get<T>(this T self,Action<GetCode> setCode) where T: IIndexerContainer
        {
            var code = new GetCode(self.Root);
            code.Indent = self.Indent + 1;
            self.Get=code;
            setCode?.Invoke(code);
            return self;
        }
        public static T Set<T>(this T self,Action<SetCode> setCode) where T: IIndexerContainer
        {
            var code = new SetCode(self.Root);
            code.Indent = self.Indent + 1;
            self.Set=code;
            setCode?.Invoke(code);
            return self;
        }
    }

    public enum EPublicType
    {
        Private,
        Public,
        Protected
    }
}
#endif