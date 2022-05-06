#region

using System.Xml;

#endregion

namespace MultipleXmlDocumentsToCsClass.Trees.Xmls;

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
