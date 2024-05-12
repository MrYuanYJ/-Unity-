#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenKit
{
    public class FiledCode: 
        IHasAttribute,
        IHasPublicType,
        IHasKeywords
    {
        public FiledCode(string type,string filedName,CodeRoot root)
        {
            _type = type;
            _filedName = filedName;
            Root = root;
        }
        public FiledCode(Type type,string filedName,CodeRoot root)
        {
            _type = type.Name;
            _filedName = filedName;
            Root = root;
        }
        
        private string _filedName;
        private StringBuilder _str = new();
        private List<string> _attributes = new();
        private EPublicType _publicType;
        private string _keywords;
        private string _type;
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
            ((IHasPublicType)this).GenPublicType();
            ((IHasKeywords)this).GenKeywords();
            _str.Append($" {_type} {_filedName};");
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
    }
}
#endif