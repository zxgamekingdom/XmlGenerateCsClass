#region

using System.Collections.Immutable;
using System.Xml;
using Newtonsoft.Json;

#endregion

namespace 多Xml文档建立CShape类;

/// <summary>
///     多Xml文档节点树
/// </summary>
public class MultipleXmlDocumentNodeTree
{

    public XmlNode Root { get; private set; }

    public void Load(string[] paths)
    {
        var roots = new List<XmlElement>();

        foreach (var path in paths)
        {
            var doc = new XmlDocument();
            doc.Load(path);
            var root = doc.DocumentElement;
            if (root != null) roots.Add(root);
        }

        var trees = new List<XmlTempNodeTree>();
        var rootFirst = roots.First();

        foreach (var root in roots)
        {
            if (root.Name != rootFirst.Name)
                throw new XmlException("多Xml文档节点树的根节点名称不一致");

            var tree = new XmlTempNodeTree();
            tree.Build(root);
            trees.Add(tree);
        }

        var 群 = 全树剪枝(trees);
        Root = 从群构建树(群);
    }

    private XmlNode 从群构建树(Dictionary<XmlIdentifier, XmlNode> dictionary)
    {
        //找到根节点
        var root = dictionary.Values.Single(x => x.ParentId == null);

        foreach (var info in dictionary.Values)
            if (info.ParentId != null)
            {
                var parent = dictionary[info.ParentId];
                info.Parent = parent;
                parent.Children.Add(info);
            }

        return root;
    }

    private Dictionary<XmlIdentifier, XmlNode> 全树剪枝(List<XmlTempNodeTree> trees)
    {
        var infos = new Dictionary<XmlIdentifier, XmlNode>();
        var max = trees.Max(x => x.DictionaryCollection.Keys.Last());

        for (var i = 0; i <= max; i++)
        {
            var tempNodes = trees.SelectMany(x => x.DictionaryCollection[i]).ToArray();

            foreach (var grouping in tempNodes.GroupBy(x => x.Identifier))
            {
                var isArray = 元素是否是数组(grouping);
                infos.Add(grouping.Key, new XmlNode(isArray, grouping.ToArray()));
            }
        }

        return infos;
    }

    private bool 元素是否是数组(IGrouping<XmlIdentifier, XmlTempNode> grouping)
    {
        var parents = grouping.Select(x => x.Parent).Where(x => x != null).ToArray();
        bool isArray = default;

        if (parents.Any())
        {
            var parentFirst = parents.First()!;

            if (parents.Any(x => x.Identifier != parentFirst.Identifier))
                throw new Exception("不同节点的父节点不同");

            isArray = parents.Any(x =>
                x.Children.Count(x => x.Identifier == grouping.Key) > 1);
        }

        return isArray;
    }

}

public class XmlNode
{

    internal XmlNode(bool isArray, XmlTempNode[] xmlTempNodes)
    {
        var hashSet = new HashSet<string>();

        foreach (var s in xmlTempNodes
            .SelectMany(x => x.XmlElement.Attributes.OfType<XmlAttribute>())
            .Select(x => x.Name))
            hashSet.Add(s);

        属性 = hashSet.ToImmutableArray();
        IsArray = isArray;
        var first = xmlTempNodes.First();
        ParentId = first.Parent?.Identifier;
        CurrentId = first.Identifier;
        Name = first.XmlElement.Name;
    }

    [JsonIgnore]
    public XmlNode? Parent { get; internal set; }

    public string Name { get; }

    public bool IsArray { get; }

    public ImmutableArray<string> 属性 { get; }

    public List<XmlNode> Children { get; } = new();

    [JsonIgnore]
    public XmlIdentifier CurrentId { get; }

    [JsonIgnore]
    public XmlIdentifier? ParentId { get; }

}

/// <summary>
///     xml临时节点
/// </summary>
internal class XmlTempNode
{

    public XmlTempNode(XmlTempNode? parent, XmlElement xmlElement)
    {
        Parent = parent;
        XmlElement = xmlElement;
        var xmlIdentifier = new XmlIdentifier(xmlElement);
        Identifier = xmlIdentifier;
    }

    public XmlIdentifier Identifier { get; }

    [JsonIgnore]
    public XmlTempNode? Parent { get; }

    [JsonIgnore]
    public XmlElement XmlElement { get; }

    public List<XmlTempNode> Children { get; } = new();

}

/// <summary>
///     xml临时节点树
/// </summary>
internal class XmlTempNodeTree
{

    public readonly DictionaryCollection<int, XmlTempNode> DictionaryCollection = new();

    public XmlTempNode? Root => DictionaryCollection[0].Single();

    public void Build(XmlElement root)
    {
        var node = new XmlTempNode(default, root);
        DictionaryCollection.Add(0, node);

        for (var i = 0; i < int.MaxValue; i++)
        {
            var nodes = DictionaryCollection[i].ToArray();

            if (nodes.Any() is false) return;

            foreach (var tempNode in nodes)
            {
                var xmlElements = tempNode.XmlElement.ChildNodes.OfType<XmlElement>()
                    .ToArray();

                if (xmlElements.Any() is false) continue;

                foreach (var xmlElement in xmlElements)
                {
                    var xmlTempNode = new XmlTempNode(tempNode, xmlElement);
                    tempNode.Children.Add(xmlTempNode);
                    DictionaryCollection.Add(i + 1, xmlTempNode);
                }
            }
        }
    }

}
