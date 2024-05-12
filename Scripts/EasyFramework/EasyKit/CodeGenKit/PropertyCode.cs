#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenKit
{
    public class PropertyCode: 
        IHasAttribute,
        IHasPublicType,
        IHasKeywords,
        IIndexerContainer
    {
        public PropertyCode(string typeAssemblyQualifiedName, string propertyName,CodeRoot root)
        {
            _type = Type.GetType(typeAssemblyQualifiedName)?.Name;
            _propertyName = propertyName;
            Root = root;
        }
        public PropertyCode(Type type, string propertyName,CodeRoot root)
        {
            _type = type.Name;
            _propertyName = propertyName;
            Root = root;
        }

        private string _propertyName;
        private StringBuilder _str = new();
        private List<string> _attributes = new();
        private EPublicType _publicType;
        private string _keywords;
        private string _type;
        private ICodeGen _get;
        private ICodeGen _set;

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
            _str.Append($" {_type} {_propertyName}");
            
            ((ICodeGen)this).Wrap();
            _str.Append("{");
            
            ((IIndexerContainer)this).GenIndexer();
            
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

        ICodeGen IIndexerContainer.Get
        {
            get => _get;
            set => _get = value;
        }

        ICodeGen IIndexerContainer.Set
        {
            get => _set;
            set => _set = value;
        }
    }
}
#endif