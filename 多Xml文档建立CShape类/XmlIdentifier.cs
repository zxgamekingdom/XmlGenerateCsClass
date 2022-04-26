using System.Collections.Immutable;
using System.Xml;

namespace 多Xml文档建立CShape类;

/// <summary>
/// Xml标识
/// </summary>
public class XmlIdentifier
{

    public readonly ImmutableArray<string> Identifier;

    public readonly string IdentifierStr;

    public XmlIdentifier(XmlElement element)
    {
        var buff = element;
        var stack = new Stack<string>();

        while (buff != null)
        {
            stack.Push(buff.Name);
            buff = buff.ParentNode as XmlElement;
        }

        Identifier = stack.ToImmutableArray();
        IdentifierStr = string.Join("/", Identifier);
    }

    protected bool Equals(XmlIdentifier other)
    {
        return IdentifierStr == other.IdentifierStr;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        return Equals((XmlIdentifier) obj);
    }

    public override int GetHashCode() { return IdentifierStr.GetHashCode(); }

    public static bool operator ==(XmlIdentifier? left, XmlIdentifier? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(XmlIdentifier? left, XmlIdentifier? right)
    {
        return !Equals(left, right);
    }

}