#region

using System;
using System.Collections.Generic;

#endregion

namespace XmlGenerateCsClass.CsharpClassInfos;

internal class CSharpKeywords
{

    private static readonly WeakReference<CSharpKeywords> WeakReference =
        new(new CSharpKeywords());

    /// <summary>
    /// </summary>
    /// <remarks>线程安全</remarks>
    public static CSharpKeywords Instance
    {
        get
        {
            lock (WeakReference)
            {
                if (WeakReference.TryGetTarget(out var target)) return target;

                var cSharpKeywords = new CSharpKeywords();
                WeakReference.SetTarget(cSharpKeywords);

                return cSharpKeywords;
            }
        }
    }

    private HashSet<string> Keywords { get; } = new()
    {
        "abstract",
        "as",
        "base",
        "bool",
        "break",
        "byte",
        "case",
        "catch",
        "char",
        "checked",
        "class",
        "const",
        "continue",
        "decimal",
        "default",
        "delegate",
        "do",
        "double",
        "else",
        "enum",
        "event",
        "explicit",
        "extern",
        "false",
        "finally",
        "fixed",
        "float",
        "for",
        "foreach",
        "goto",
        "if",
        "implicit",
        "in",
        "int",
        "interface",
        "internal",
        "is",
        "lock",
        "long",
        "namespace",
        "new",
        "null",
        "object",
        "operator",
        "out",
        "override",
        "params",
        "private",
        "protected",
        "public",
        "readonly",
        "ref",
        "return",
        "sbyte",
        "sealed",
        "short",
        "sizeof",
        "stackalloc",
        "static",
        "string",
        "struct",
        "switch",
        "this",
        "throw",
        "true",
        "try",
        "typeof",
        "uint",
        "ulong",
        "unchecked",
        "unsafe",
        "ushort",
        "using",
        "virtual",
        "void",
        "volatile",
        "while"
    };

    public bool IsKeyword(string name) { return Keywords.Contains(name); }

    /// <summary>
    /// 将C#关键字的字符串转换为@C#关键字的字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ConvertKeywordString(string str)
    {
        return Instance.IsKeyword(str) ? $"@{str}" : str;
    }

}
