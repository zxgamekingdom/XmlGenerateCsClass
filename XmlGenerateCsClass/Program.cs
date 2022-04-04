#region

using System.Collections.Generic;
using XmlGenerateCsClass.CsharpClassInfos;
using XmlGenerateCsClass.XmlInfos;

#endregion

// var xmlPath =
//     @"C:\Users\Zhou Taurus\source\repos\XmlGenerateCsClass\XmlGenerateCsClass\test.xml";
// var xmlPath =
//     @"C:\Users\Zhou Taurus\Desktop\Deep-Learning\Classification\classify_pill_defects_deep_learning_1_preprocess.hdev";
//
// var xmlTree = await XmlTree.LoadFromFile(xmlPath);
// var csharpTree = CsharpTree.FromXmlTree(xmlTree);
// csharpTree.ToCsharpCode().WriteLine();
string[] ss = new string[]
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

ss.Length.WriteLine();
var hashSet = new HashSet<string>(ss);
hashSet.Count.WriteLine();
