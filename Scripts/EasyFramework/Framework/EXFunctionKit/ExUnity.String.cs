using System.Collections;
using System.Text;
using UnityEngine;

namespace EXFunctionKit
{
    public class RichText
    {
        public StringBuilder Header;
        public StringBuilder Content;
        public StringBuilder Footer;

        public RichText(string content)
        {
            Header=new StringBuilder();
            Footer=new StringBuilder();
            Content=new StringBuilder(content);
        }
        public RichText(StringBuilder content)
        {
            Header=new StringBuilder();
            Footer=new StringBuilder();
            Content = content;
        }

        public override string ToString()
        {
            return $"{Header}{Content}{Footer}";
        }
    }
    public static partial class ExUnity
    {
        public static T DebugLog<T>(this T self)
        {
#if DEBUG
            Debug.Log(self);
#endif
            return self;
        }
        public static T DebugLogs<T>(this T self) where T : IEnumerable
        {
#if DEBUG
            foreach (var str in self)
            {
                Debug.Log(str);
            }
#endif
            return self;
        }

        public static RichText RichText(this string self)=>new RichText(self);
        public static RichText RichColor(this RichText self, Color color)
        {
            self.Header.Insert(0,$"<color=#{ColorUtility.ToHtmlStringRGB(color)}>");
            self.Footer.Append("</color>");
            return self;
        }

        public static RichText RichBold(this RichText self)
        {
            self.Header.Insert(0, "<b>");
            self.Footer.Append("</b>");
            return self;
        }

        public static RichText RichItalic(this RichText self)
        {
            self.Header.Insert(0, "<i>");
            self.Footer.Append("</i>");
            return self;
        }

        public static RichText RichSize(this RichText self, int size)
        {
            self.Header.Insert(0, $"<size={size}>");
            self.Footer.Append("</size>");
            return self;
        }

        public static RichText RichLink(this RichText self, string url)
        {
            self.Header.Insert(0, $"<link={url}>");
            self.Footer.Append("</link>");
            return self;
        }

        public static RichText RichImage(this RichText self, string path)
        {
            self.Header.Insert(0, $"<image={path}>");
            self.Footer.Append("</image>");
            return self;
        }
    }
}