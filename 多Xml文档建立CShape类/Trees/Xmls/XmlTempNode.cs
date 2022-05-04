#region

using System.Xml;
using Newtonsoft.Json;

#endregion

namespace 多Xml文档建立CShape类.Trees.Xmls;

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