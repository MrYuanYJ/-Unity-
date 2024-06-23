#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text;

namespace CodeGenKit
{
    public class UsingCode:ICodeGen
    {
        public UsingCode(CodeRoot root)
        {
            Root = root;
        }
        public UsingCode(string usingNamespace,CodeRoot root)
        {
            if (!_usingNameSpaseLst.Contains(usingNamespace))
                _usingNameSpaseLst.Add(usingNamespace);
            Root = root;
        }
        
        private HashSet<string> _usingNameSpaseLst = new();
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
            foreach (var usingNameSpace in _usingNameSpaseLst)
            {
                _str.Append($"using {usingNameSpace};\n");
            }

            return _str.ToString();
        }

        public UsingCode Using(string nameSpace)
        {
            _usingNameSpaseLst.Add(nameSpace);
            return this;
        }
    }
}
#endif