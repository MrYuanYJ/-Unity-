#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text;

namespace CodeGenKit
{
    public class SetCode : IHasLogic
    {
        public SetCode(CodeRoot root)
        {
            Root = root;
        }
        
        public int Indent { get; set; }
        public CodeRoot Root { get; set; }

        StringBuilder ICodeGen.str
        {
            get => _str;
            set => _str = value;
        }
        private StringBuilder _str = new();
        private List<string> _logics = new();

        string ICodeGen.Gen()
        {
            _str.Clear();
            ((ICodeGen)this).Wrap();
            _str.Append("set");
            ((ICodeGen)this).Wrap();
            _str.Append("{");

            ((IHasLogic)this).GenLogic();
                
            ((ICodeGen)this).Wrap();
            _str.Append("}");
            return _str.ToString();
        }

        List<string> IHasLogic.Logics
        {
            get => _logics;
            set => _logics = value;
        }
    }
}
#endif