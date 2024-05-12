#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text;

namespace CodeGenKit
{
    public class EnumCode: IHasAttribute,IHasPublicType
    {
        public EnumCode(string enumName,CodeRoot root)
        {
            _enumName = enumName;
            Root = root;
        }
        private List<string> _attributes = new();
        private EPublicType _publicType = EPublicType.Public;
        private string _enumName;
        private List<string> _enums = new();
        private StringBuilder _str = new();

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
            _str.Append($" enum {_enumName}");
            
            ((ICodeGen)this).Wrap();
            _str.Append("{");

            bool needComma = false;
            for (int i = 0; i < _enums.Count; i++)
            {
                if(needComma)
                    _str.Append(",");
                ((ICodeGen)this).Wrap(1);
                _str.Append(_enums[i]);
                needComma = true;
            }
            
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

        public EnumCode EnumParameter(params string[] enums)
        {
            _enums.AddRange(enums);
            return this;
        }
    }
}
#endif