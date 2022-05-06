// #region
//
// using System.Xml;
// using System.Xml.Serialization;
// using 多Xml文档建立CShape类.Extensions;
// using 多Xml文档建立CShape类.Trees.CSharps;
// using 多Xml文档建立CShape类.Trees.Xmls;
//
// #endregion
//
// var files = new List<string>();
//
// files.AddRange(Directory.GetFiles(@"C:\Users\Zhou Taurus\Desktop\新建文件夹\hdevelop",
//     "*",
//     SearchOption.AllDirectories));
//
// files.AddRange(Directory.GetFiles(@"C:\Users\Zhou Taurus\Desktop\新建文件夹\procedures",
//     "*",
//     SearchOption.AllDirectories));
//
// var tree = new MultipleXmlDocumentNodeTree();
// tree.Load(files.ToArray());
// var cShapeTree = new CShapeTree();
// cShapeTree.Load(tree, ArrayType.List);
//
// foreach (var classNode in cShapeTree.ClassNodes) classNode.CodeStr.WriteLine();
