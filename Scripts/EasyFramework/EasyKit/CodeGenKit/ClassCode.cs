#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text;

namespace CodeGenKit
{
    public class ClassCode: 
        IHasAttribute,
        IHasPublicType,
        IHasKeywords,
        IHasBaseClass,
        
        IClassContainer,
        IStructContainer,
        IEnumContainer,
        IFieldContainer,
        IPropertyContainer,
        IConstructorContainer,
        IFunctionContainer
    {
        public ClassCode(string className,CodeRoot root)
        {
            _className = className;
            Root = root;
        }
        
        private string _className;
        private StringBuilder _str = new();
        private List<string> _attributes = new();
        private EPublicType _publicType = EPublicType.Public;
        private string _keywords;
        private List<string> _baseInterfaces = new();
        private string _baseClass;
        private List<ICodeGen> _classes = new();
        private List<ICodeGen> _structs = new();
        private List<ICodeGen> _enums = new();
        private List<ICodeGen> _fileds = new();
        private List<ICodeGen> _propertys = new();
        private List<ICodeGen> _constructors = new();
        private List<ICodeGen> _functions = new();


        public int Indent { get; set; }
        public CodeRoot Root { get; set; }

        StringBuilder ICodeGen.str
        {
            get => _str;
            set => _str = value;
        }

        string ICodeGen.Gen()
        {
            _str.Clear();
            ((ICodeGen)this).Wrap();

            ((IHasAttribute)this).GenAttributes();
            
            ((ICodeGen)this).Wrap();
            
            ((IHasPublicType)this).GenPublicType();
            ((IHasKeywords)this).GenKeywords();
            _str.Append($" class {_className}");
            ((IHasBaseClass)this).GenBase();
            
            ((ICodeGen)this).Wrap();
            
            _str.Append("{");
            
            ((IEnumContainer)this).GenEnum();
            ((IStructContainer)this).GenStruct();
            ((IClassContainer)this).GenClass();
            ((IFieldContainer)this).GenFiled();
            ((IPropertyContainer)this).GenProperty();
            ((IConstructorContainer)this).GenConstructor();
            ((IFunctionContainer)this).GenFunction();

            ((ICodeGen)this).Wrap();
            _str.Append("}");
            return _str.ToString();
        }
        

        List<string> IHasAttribute.Attributes
        {
            get => _attributes;
            set => _attributes = value;
        }

        EPublicType IHasPublicType.PublicType
        {
            get => _publicType;
            set => _publicType = value;
        }

        string IHasKeywords.Keywords
        {
            get => _keywords;
            set => _keywords = value;
        }

        List<string> IHasBaseInterface.BaseInterfaces
        {
            get => _baseInterfaces;
            set => _baseInterfaces = value;
        }

        string IHasBaseClass.BaseClass
        {
            get => _baseClass;
            set => _baseClass = value;
        }

        List<ICodeGen> IClassContainer.Classes
        {
            get => _classes;
            set => _classes = value;
        }

        List<ICodeGen> IStructContainer.Structs
        {
            get => _structs;
            set => _structs = value;
        }

        List<ICodeGen> IEnumContainer.Enums
        {
            get => _enums;
            set => _enums = value;
        }

        List<ICodeGen> IFieldContainer.Fields
        {
            get => _fileds;
            set => _fileds = value;
        }

        List<ICodeGen> IPropertyContainer.Propertys
        {
            get => _propertys;
            set => _propertys = value;
        }

        string IConstructorContainer.constructorName
        {
            get => _className;
            set => _className = value;
        }
        List<ICodeGen> IConstructorContainer.Constructors
        {
            get => _constructors;
            set => _constructors = value;
        }

        List<ICodeGen> IFunctionContainer.Functions
        {
            get => _functions;
            set => _functions = value;
        }
    }
}
#endif