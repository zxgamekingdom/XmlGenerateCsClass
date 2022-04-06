#region

using System;
using System.Collections.Generic;
using System.Text;
using XmlGenerateCsClass.XmlInfos;

#endregion

namespace XmlGenerateCsClass.CsharpClassInfos;

public class CsharpClassNode
{

    public string Name { get; set; }

    public Stack<string>? XmlNodeNameChain { get; set; }

    public List<XmlElementNode>? XmlElementNodes { get; set; }

    public List<CsharpPropertyNode>? PropertyNodes { get; set; }

    public bool IsRoot { get; set; }

    public string XmlSourceName { get; set; }

    public string ToCsharpCode()
    {
        var s = new StringBuilder(1000);

        if (XmlNodeNameChain != null)
            s.AppendLine($"//{string.Join('/', XmlNodeNameChain)}");

        if (IsRoot) s.AppendLine($"[XmlRoot(\"{XmlSourceName}\")]");
        s.AppendLine($"public class {Name}");
        s.AppendLine("{");

        if (PropertyNodes != null)
            foreach (var propertyNode in PropertyNodes)
            {
                s.AppendLine(propertyNode.XmlType switch
                {
                    XmlType.Attribute =>
                        $"    [XmlAttribute(\"{propertyNode.XmlSourceName}\")]",
                    XmlType.Element =>
                        $"    [XmlElement(\"{propertyNode.XmlSourceName}\")]",
                    XmlType.Text => "    [XmlText]",
                    _ => throw new ArgumentOutOfRangeException()
                });

                var arr = propertyNode.IsArray ? "[]" : string.Empty;

                s.AppendLine(
                    $"    public {propertyNode.Type}{arr}? {propertyNode.Name} {{ get; set; }}");
            }

        s.AppendLine("}");

        return s.ToString();
    }

}
