#region

using System;
using System.Collections.Generic;
using System.Linq;
using XmlGenerateCsClass.XmlInfos;

#endregion

namespace XmlGenerateCsClass.CsharpClassInfos;

internal readonly struct ImplementClassNodeInfoTool
{

    private readonly CsharpClassNode _classNode;

    private readonly CsharpTree _csharpTree;

    private readonly List<XmlElementNode>? _xmlNodes;

    public ImplementClassNodeInfoTool(CsharpClassNode classNode, CsharpTree csharpTree)
    {
        _classNode = classNode;
        _csharpTree = csharpTree;
        _xmlNodes = _classNode.XmlElementNodes;
    }

    public void 执行()
    {
        _classNode.PropertyNodes ??= new List<CsharpPropertyNode>();

        _classNode.PropertyNodes.Add(new CsharpPropertyNode
        {
            Name = _classNode.Name == "ContentText" ? "Content" : "ContentText",
            XmlType = XmlType.Text,
            Type = "string"
        });

        初始化Xml属性();
        初始化Xml子节点();
    }

    private void 初始化Xml子节点()
    {
        var xmlElementNodes = _xmlNodes?.Where(x => x.ElementNodes != null)
            .SelectMany(x => x.ElementNodes!)
            .ToArray();

        if (xmlElementNodes == null || xmlElementNodes.Length == 0) return;

        foreach (var grouping in xmlElementNodes.GroupBy(x => x.Name))
        {
            var nodes = grouping.ToArray();
            var childClassNode = _csharpTree.AddClassNode(nodes);
            var name = XmlElementNode获取属性名(nodes[0].Name);

            _classNode.PropertyNodes!.Add(new CsharpPropertyNode
            {
                IsArray = nodes.Length > 1,
                XmlSourceName = nodes[0].Name,
                XmlType = XmlType.Element,
                Type = childClassNode.Name,
                Name = name
            });

            var tool = new ImplementClassNodeInfoTool(childClassNode, _csharpTree);
            tool.执行();
        }
    }

    private string XmlElementNode获取属性名(string name)
    {
        name = CSharpKeywords.ConvertKeywordString(name);

        if (XmlElementNode属性名是否有效(name)) return name;

        for (var i = 1; i < int.MaxValue; i++)
        {
            var newName = $"{name}{i}";

            if (XmlElementNode属性名是否有效(newName)) return newName;
        }

        throw new InvalidOperationException();
    }

    private bool XmlElementNode属性名是否有效(string name)
    {
        var 属性名等于类名 = _classNode.Name == name;

        var propertyNodes =
            _classNode.PropertyNodes!.Where(x => x.Name == name).ToArray();

        var 属性名重复 = propertyNodes.Any(x => x.XmlType != XmlType.Element);
        var 属性名是否有效 = 属性名等于类名 is false && 属性名重复 is false;

        return 属性名是否有效;
    }

    private void 初始化Xml属性()
    {
        var attributeNodes = _xmlNodes?.Where(x => x.Attributes != null)
            .SelectMany(x => x.Attributes!)
            .ToLookup(x => x.Name);

        if (attributeNodes == null || attributeNodes.Count == 0) return;

        foreach (var attribute in attributeNodes)
        {
            _classNode.PropertyNodes ??= new List<CsharpPropertyNode>();
            var name = XmlAttributeNode获取属性名(attribute.Key);

            _classNode.PropertyNodes.Add(new CsharpPropertyNode
            {
                XmlSourceName = attribute.Key,
                Name = name,
                Type = "string",
                XmlType = XmlType.Attribute
            });
        }
    }

    private string XmlAttributeNode获取属性名(string name)
    {
        name = CSharpKeywords.ConvertKeywordString(name);

        if (XmlAttributeNode属性名是否有效(name)) return name;

        for (var i = 1; i < int.MaxValue; i++)
        {
            var newName = $"{name}{i}";

            if (XmlAttributeNode属性名是否有效(newName)) return newName;
        }

        throw new InvalidOperationException();
    }

    private bool XmlAttributeNode属性名是否有效(string name)
    {
        var 属性名等于类名 = _classNode.Name == name;

        var propertyNodes = _classNode.PropertyNodes!
            .Where(x => x.Name == name)
            .ToArray();

        var 属性名重复 = propertyNodes.Any(x => x.XmlType != XmlType.Attribute);
        var 属性名是否有效 = 属性名重复 is false && 属性名等于类名 is false;

        return 属性名是否有效;
    }

}
