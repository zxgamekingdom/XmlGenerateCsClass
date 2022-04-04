#region

using System.Collections.Generic;
using System.Xml;
using Newtonsoft.Json;

#endregion

namespace XmlGenerateCsClass.XmlInfos;

public class XmlElementNode
{

    public string Name { get; set; }

    [JsonIgnore]
    public XmlElement XmlElement { get; set; } = null!;

    public List<XmlAttributeNode>? Attributes { get; set; }

    public List<XmlElementNode>? ElementNodes { get; set; }

    [JsonIgnore]
    public XmlElementNode? Parent { get; set; }

}
