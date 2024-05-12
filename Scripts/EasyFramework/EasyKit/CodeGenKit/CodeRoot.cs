#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenKit
{
    [System.Serializable]
    public class CodeRoot: IClassContainer,IStructContainer,IEnumContainer
    {
        public CodeRoot()
        {
            Root = this;
        }
        
        private ICodeGen _using;
        private ICodeGen _nameSpace;
        private StringBuilder _str = new();
        private List<ICodeGen> _classes= new();
        private List<ICodeGen> _structs= new();
        private List<ICodeGen> _enums= new();

        public CodeRoot Using(string usingNamespaceName,Action<UsingCode> setCode)
        {
            _using??= new UsingCode(usingNamespaceName,this);
            ((UsingCode)_using).Using(usingNamespaceName);
            setCode?.Invoke((UsingCode)_using);
            return this;
        }
        public CodeRoot Using(Action<UsingCode> setCode)
        {
            _using??= new UsingCode(this);
            setCode?.Invoke((UsingCode)_using);
            return this;
        }

        public CodeRoot NameSpace(string namespaceName,Action<NameSpaceCode> setCode)
        {
            _nameSpace ??= new NameSpaceCode(namespaceName,this);
            setCode?.Invoke((NameSpaceCode)_nameSpace);
            return this;
        }

        public string GetNameSpace() => ((NameSpaceCode) _nameSpace).GetNameSpace();
        public int Indent { get; set; }
        public CodeRoot Root { get; set; }

        StringBuilder ICodeGen.str
        {
            get => _str;
            set => _str = value;
        }

        public string Gen()
        {
            _str.Clear();
   
            _str.Append(_using.Gen());
            ((IEnumContainer)this).GenEnum();
            ((IStructContainer)this).GenStruct();
            ((IClassContainer)this).GenClass();
            _str.Append(_nameSpace.Gen());

            return _str.ToString();
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
    }
}
#endif