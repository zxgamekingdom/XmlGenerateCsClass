using System.Collections.Immutable;
using System.Text;
using System.Xml.Serialization;
using 多Xml文档建立CShape类.Trees.Xmls;

namespace 多Xml文档建立CShape类.Trees.CSharps;

public class CShapeClassNode
{

    internal CShapeClassNode(CShapeTreeStorage storage)
    {
        XmlName = "";
        Name = "IXmlRawContent";
        Id = "IXmlRawContent";
        Name = storage.申请类名(Name);

        var pNode = new CSharpPropertyNode
        {
            Type = $"{typeof(string)}?",
            Name = storage.申请属性名("RawContent", this),
            XmlName = default,
            XmlType = XmlType.Text
        };

        var builder = new StringBuilder(100);
        builder.Append("public interface ").AppendLine(Name);
        builder.AppendLine("{");
        builder.Append('[').Append(nameof(XmlTextAttribute)).AppendLine("]");

        builder.Append(pNode.Type)
            .Append(' ')
            .Append(pNode.Name)
            .AppendLine(" { get; }");

        builder.AppendLine("}");
        CodeStr = builder.ToString();
        storage.XmlIntferfaceNodeName = Name;
        storage.ClassNodes.Add(this);
    }

    internal CShapeClassNode(XmlNode xmlNode,
        CShapeTreeStorage storage,
        ArrayType arrayType)
    {
        XmlName = xmlNode.Name;
        Name = storage.申请类名(xmlNode);
        Id = xmlNode.CurrentId.IdentifierStr;

        var propertyNodes = new List<CSharpPropertyNode>(10)
        {
            new()
            {
                Type = $"{typeof(string)}?",
                Name = storage.申请属性名("RawContent", this),
                XmlName = default,
                XmlType = XmlType.Text
            }
        };

        foreach (var att in xmlNode.属性)
            propertyNodes.Add(new CSharpPropertyNode
            {
                Type = $"{typeof(string)}?",
                Name = storage.申请属性名(att, this),
                XmlName = att,
                XmlType = XmlType.Attribute
            });

        foreach (var child in xmlNode.Children)
        {
            var childClassNode = new CShapeClassNode(child, storage, arrayType);

            propertyNodes.Add(new CSharpPropertyNode
            {
                Type = child.IsArray
                    ? arrayType switch
                    {
                        ArrayType.Array => $"{childClassNode.Name}[]?",
                        ArrayType.List => $"List<{childClassNode.Name}>?",
                        _ => throw new ArgumentOutOfRangeException(
                            nameof(arrayType),
                            arrayType,
                            null)
                    }
                    : $"{childClassNode.Name}?",
                Name = storage.申请属性名(childClassNode.Name, this),
                XmlName = child.Name,
                XmlType = XmlType.Element
            });
        }

        Properties = propertyNodes.ToImmutableArray();
        CodeStr = 拼接代码(storage);
        storage.ClassNodes.Add(this);
    }

    public string CodeStr { get; }

    public string XmlName { get; }

    public ImmutableArray<CSharpPropertyNode> Properties { get; }

    public string Id { get; }

    public string Name { get; }

    private string 拼接代码(CShapeTreeStorage storage)
    {
        var builder = new StringBuilder(1000);
        builder.AppendLine("/// <summary>");
        builder.Append("/// ").AppendLine(Id);
        builder.AppendLine("/// </summary>");
        builder.Append("[XmlRoot(\"").Append(XmlName).AppendLine("\")]");

        builder.Append("public class ")
            .Append(Name)
            .Append(" : ")
            .AppendLine(storage.XmlIntferfaceNodeName);

        builder.AppendLine("{");

        foreach (var propertyNode in Properties)
        {
            switch (propertyNode.XmlType)
            {
                case XmlType.Text:
                    builder.Append('[')
                        .Append(nameof(XmlTextAttribute))
                        .AppendLine("()]");

                    break;
                case XmlType.Attribute:
                    builder.Append('[')
                        .Append(nameof(XmlAttributeAttribute))
                        .Append("(\"")
                        .Append(propertyNode.XmlName)
                        .AppendLine("\")]");

                    break;
                case XmlType.Element:
                    builder.Append('[')
                        .Append(nameof(XmlElementAttribute))
                        .Append("(\"")
                        .Append(propertyNode.XmlName)
                        .AppendLine("\")]");

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(propertyNode.XmlType),
                        propertyNode.XmlType,
                        "");
            }

            builder.Append("public ")
                .Append(propertyNode.Type)
                .Append(' ')
                .Append(propertyNode.Name)
                .AppendLine(" { get; set; }");
        }

        builder.AppendLine("}");

        return builder.ToString();
    }

    [XmlRoot("")]
    public class MNy
    {

        [XmlElement("")]
        public string? Type { get; set; }

    }

}