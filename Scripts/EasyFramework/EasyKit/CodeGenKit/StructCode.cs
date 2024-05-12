#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text;

namespace CodeGenKit
{
    public class StructCode: 
        IHasAttribute,
        IHasKeywords,
        IHasBaseInterface,
        IHasPublicType,
        
        IFieldContainer,
        IPropertyContainer,
        IConstructorContainer,
        IClassContainer,
        IFunctionContainer
    {
        public StructCode(string structName,CodeRoot root)
        {
            _structName = structName;
            Root = root;
        }
        
        private string _structName;
        private StringBuilder _str = new();
        private List<string> _attributes = new();
        private string _keywords;
        private List<string> _baseInterfaces = new();
        private EPublicType _publicType= EPublicType.Public;
        private List<ICodeGen> _fileds = new();
        private List<ICodeGen> _propertys = new();
        private List<ICodeGen> _constructors = new();
        private List<ICodeGen> _classes = new();
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
            _str.Append($" struct {_structName}");
            ((IHasBaseInterface)this).GenBaseInterface();
            
            ((ICodeGen)this).Wrap();
            _str.Append("{");
            
            ((IFieldContainer)this).GenFiled();
            ((IPropertyContainer)this).GenProperty();
            ((IClassContainer)this).GenClass();
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

        EPublicType IHasPublicType.PublicType
        {
            get => _publicType;
            set => _publicType = value;
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

        string IConstructorContainer.constructorName { get=>_structName; set=>_structName=value; }

        List<ICodeGen> IConstructorContainer.Constructors
        {
            get => _constructors;
            set => _constructors = value;
        }

        List<ICodeGen> IClassContainer.Classes
        {
            get => _classes;
            set => _classes = value;
        }

        List<ICodeGen> IFunctionContainer.Functions
        {
            get => _functions;
            set => _functions = value;
        }
    }
}
#endif