#region

using System.Xml;
using MultipleXmlDocumentsToCsClass.Tools;

#endregion

namespace MultipleXmlDocumentsToCsClass.Trees.Xmls;

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