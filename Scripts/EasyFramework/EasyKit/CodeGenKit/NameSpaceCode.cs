#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text;
using Sirenix.Utilities;

namespace CodeGenKit
{
    public class NameSpaceCode:IClassContainer,IStructContainer,IEnumContainer
    {
        public NameSpaceCode(string nameSpace,CodeRoot root)
        {
            _nameSpace = nameSpace;
            Root = root;
        }

        private string _nameSpace;
        private StringBuilder _str = new();
        private List<ICodeGen> _classes = new();
        private List<ICodeGen> _structs = new();
        private List<ICodeGen> _enums = new();
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
            if (!_nameSpace.IsNullOrWhitespace())
            {
                ((ICodeGen) this).Wrap();
                _str.Append($"namespace {_nameSpace}");
                ((ICodeGen) this).Wrap();
                _str.Append("{");
            }

            ((IEnumContainer)this).GenEnum();
            ((IStructContainer)this).GenStruct();
            ((IClassContainer)this).GenClass();
            if (!_nameSpace.IsNullOrWhitespace())
            {
                ((ICodeGen) this).Wrap();
                _str.Append("}");
            }

            return _str.ToString();
        }

        public string GetNameSpace() => _nameSpace;
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
    }
}
#endif