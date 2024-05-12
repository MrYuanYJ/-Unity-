#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text;

namespace CodeGenKit
{
    public class ConstructorCode: 
        IHasPublicType,
        IHasKeywords,
        IHasParameter,
        IHasLogic
    {
        public ConstructorCode(string constructorName,CodeRoot root)
        {
            _constructorName = constructorName;
            Root = root;
        }
        
        private string _constructorName;
        private StringBuilder _str = new();
        private EPublicType _publicType;
        private string _keywords;
        private List<string> _parameters= new();
        private List<string> _logics= new();

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
            
            ((IHasPublicType)this).GenPublicType();
            _str.Append($" {_constructorName}(");
            ((IHasParameter)this).GenParameter();

            _str.Append(")");
            ((ICodeGen)this).Wrap();
            _str.Append("{");
            
            ((IHasLogic)this).GenLogic();
            
            ((ICodeGen)this).Wrap();
            _str.Append("}");
            return _str.ToString();
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