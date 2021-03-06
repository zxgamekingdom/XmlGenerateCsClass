namespace MultipleXmlDocumentsToCsClass.Trees.CSharps;

public class CSharpPropertyNode
{

    public string Type { get; internal set; } = null!;

    public string Name { get; internal set; } = null!;

    public string? XmlName { get; internal set; }

    public XmlType XmlType { get; internal set; }

}