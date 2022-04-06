#region

using XmlGenerateCsClass.CsharpClassInfos;
using XmlGenerateCsClass.XmlInfos;

#endregion

var xmlPath =
    @"C:\Users\Zhou Taurus\source\repos\XmlGenerateCsClass\XmlGenerateCsClass\test.xml";

//var xmlPath =
//    @"C:\Users\Zhou Taurus\Desktop\Deep-Learning\Classification\classify_pill_defects_deep_learning_1_preprocess.hdev";
var xmlTree = await XmlTree.LoadFromFile(xmlPath);
var csharpTree = CsharpTree.FromXmlTree(xmlTree);
csharpTree.ToCsharpCode().WriteLine();
