#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

#endregion

namespace XmlGenerateCsClass.XmlInfos;

public class XmlTree
{

    private XmlElement _document = null!;

    private XmlTree() { }

    public XmlElementNode Root { get; set; }

    public static async Task<XmlTree> LoadFromFile(string fileName)
    {
        var xmlTree = new XmlTree();
        await xmlTree.从文件加载(fileName);
        xmlTree.初始化树();

        return xmlTree;
    }

    public static XmlTree LoadFromXml(string xml)
    {
        var xmlTree = new XmlTree();
        xmlTree.从Xml字符串加载(xml);
        xmlTree.初始化树();

        return xmlTree;
    }

    private void 初始化树()
    {
        初始化根节点();
        var stack = new Stack<XmlElementNode>(100);
        stack.Push(Root);

        while (stack.TryPop(out var node))
        {
            初始化XmlElementNode的属性(node);
            初始化XmlElementNode的子节点(node);

            if (node.ElementNodes != null)
                foreach (var child in node.ElementNodes)
                    stack.Push(child);
        }
    }

    private static void 初始化XmlElementNode的子节点(XmlElementNode node)
    {
        node.ElementNodes ??= new List<XmlElementNode>();

        node.ElementNodes.AddRange(node.XmlElement.ChildNodes.OfType<XmlElement>()
            .Select(x =>
                new XmlElementNode {XmlElement = x, Parent = node, Name = x.Name}));
    }

    private static void 初始化XmlElementNode的属性(XmlElementNode node)
    {
        node.Attributes ??= new List<XmlAttributeNode>();

        node.Attributes.AddRange(node.XmlElement.Attributes.Cast<XmlAttribute>()
            .Select(a => new XmlAttributeNode {Name = a.Name}));
    }

    private void 初始化根节点()
    {
        var root = new XmlElementNode {XmlElement = _document, Name = _document.Name};
        Root = root;
    }

    private void 从Xml字符串加载(string xml)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xml);
        var document = doc.DocumentElement;
        _document = document ?? throw new InvalidOperationException();
    }

    private async Task 从文件加载(string path)
    {
        var xml = await File.ReadAllTextAsync(path);
        从Xml字符串加载(xml);
    }

}
