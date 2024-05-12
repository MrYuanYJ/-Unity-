#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text;

namespace CodeGenKit
{
    public class FunctionCode: 
        IHasAttribute,
        IHasPublicType,
        IHasKeywords,
        IHasReturnType,
        IHasParameter,
        IHasLogic
    {
        public FunctionCode(string functionName,CodeRoot root)
        {
            _functionName = functionName;
            Root = root;
        }
        private string _functionName;
        private StringBuilder _str = new();
        private List<string> _attributes = new();
        private EPublicType _publicType;
        private string _keywords;
        private string _returnType;
        private List<string> _parameters = new();
        private List<string> _logics = new();

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
            ((IHasReturnType)this).GenReturnType();
            _str.Append($" {_functionName}(");
            ((IHasParameter)this).GenParameter();

            _str.Append(")");
            ((ICodeGen)this).Wrap();
            _str.Append("{");
            
            ((IHasLogic)this).GenLogic();
            
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

        string IHasReturnType.ReturnType
        {
            get => _returnType;
            set => _returnType = value;
        }

        List<string> IHasParameter.Parameters
        {
            get => _parameters;
            set => _parameters = value;
        }

        List<string> IHasLogic.Logics
        {
            get => _logics;
            set => _logics = value;
        }
    }
}
#endif