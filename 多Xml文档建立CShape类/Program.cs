#region

using Newtonsoft.Json;
using 多Xml文档建立CShape类;

#endregion

var files = new List<string>();
var dir = @"C:\Users\Zhou Taurus\Desktop\新建文件夹\procedures";

// var dir2 =
//     @"C:\Users\Zhou Taurus\AppData\Roaming\MVTec\HALCON-21.11-Progress\examples\hdevelop";
files.AddRange(Directory.GetFiles(dir, "*", SearchOption.AllDirectories));

// files.AddRange(Directory.GetFiles(dir2, "*", SearchOption.AllDirectories));
var tree = new MultipleXmlDocumentNodeTree();

// var paths = files.Where(x =>
//     {
//         try
//         {
//             var xmlDocument = new XmlDocument();
//             xmlDocument.Load(x);
//
//             return true;
//         }
//         catch { return false; }
//     })
//     .ToArray();
var paths = files.ToArray();
tree.Load(paths);
JsonConvert.SerializeObject(tree, Formatting.Indented).WriteLine();
Console.ReadKey();
