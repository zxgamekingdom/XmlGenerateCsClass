#region

using System.Collections.Immutable;
using System.Xml;
using Newtonsoft.Json;

#endregion

namespace MultipleXmlDocumentsToCsClass.Trees.Xmls;

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