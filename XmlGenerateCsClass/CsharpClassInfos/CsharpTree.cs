#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmlGenerateCsClass.XmlInfos;

#endregion

namespace XmlGenerateCsClass.CsharpClassInfos;

public class CsharpTree
{

    private CsharpTree() { }

    public XmlTree XmlTree { get; set; }

    public List<CsharpClassNode>? ClassNodes { get; set; }

    public string ToCsharpCode()
    {
        var s = new StringBuilder(1000);

        if (ClassNodes != null)
            foreach (var node in ClassNodes)
                s.AppendLine(node.ToCsharpCode());

        return s.ToString();
    }

    public static CsharpTree FromXmlTree(XmlTree tree)
    {
        var csharpTree = new CsharpTree {XmlTree = tree};
        csharpTree.初始化XmlTree的根节点();

        return csharpTree;
    }

    private void 初始化XmlTree的根节点()
    {
        var root = XmlTree.Root;
        ClassNodes ??= new List<CsharpClassNode>();
        var classNode = AddClassNode(root);
        classNode.IsRoot = true;
        var tool = new ImplementClassNodeInfoTool(classNode, this);
        tool.执行();
    }

    public CsharpClassNode AddClassNode(params XmlElementNode[] xmlElementNodes)
    {
        if (xmlElementNodes.Length == 0)
            throw new ArgumentException("xmlElementNodes不能为空");

        var name = xmlElementNodes[0].Name;

        if (xmlElementNodes.Any(x => x.Name != name))
            throw new ArgumentException("xmlElementNodes中的名字不一致");

        var node = new CsharpClassNode
        {
            Name = 获取类名(xmlElementNodes[0]), XmlSourceName = name
        };

        node.XmlElementNodes ??= new List<XmlElementNode>();
        node.XmlElementNodes.AddRange(xmlElementNodes);
        ClassNodes!.Add(node);

        return node;
    }

    private string 获取类名(XmlElementNode xmlElementNode)
    {
        var name = xmlElementNode.Name;
        name = CSharpKeywords.ConvertKeywordString(name);

        if (是否存在类名(name) is false) return name;

        name = 合成父节点名(xmlElementNode);

        if (是否存在类名(name) is false) return name;

        const int maxValue = int.MaxValue;

        for (var i = 1; i < maxValue; i++)
        {
            var newName = $"{name}{i}";

            if (是否存在类名(newName) is false) return newName;
        }

        throw new InvalidOperationException();
    }

    private string 合成父节点名(XmlElementNode xmlElementNode)
    {
        var stack = new Stack<string>();
        stack.Push(xmlElementNode.Name);
        var buff = xmlElementNode.Parent;

        while (buff is { })
        {
            stack.Push(buff.Name);
            buff = buff.Parent;
        }

        return string.Join('_', stack);
    }

    private bool 是否存在类名(string name)
    {
        return ClassNodes!.Any(node => node.Name == name);
    }

}
