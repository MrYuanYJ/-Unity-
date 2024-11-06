using System;

namespace EasyFramework
{
    public class MenuAttribute : Attribute
    {
        public string Path;

        public MenuAttribute(string path)
        {
            Path = path;
        }
    }
}