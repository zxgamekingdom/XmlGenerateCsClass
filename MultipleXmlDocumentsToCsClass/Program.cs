// #region
//
// using System.Xml;
// using System.Xml.Serialization;
// using MultipleXmlDocumentsToCsClass.Extensions;
// using MultipleXmlDocumentsToCsClass.Trees.CSharps;
// using MultipleXmlDocumentsToCsClass.Trees.Xmls;
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
