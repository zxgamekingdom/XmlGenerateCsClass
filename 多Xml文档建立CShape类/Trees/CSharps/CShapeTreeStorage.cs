using 多Xml文档建立CShape类.Tools;
using 多Xml文档建立CShape类.Trees.Xmls;

namespace 多Xml文档建立CShape类.Trees.CSharps;

/// <summary>
///     CShapeTree存储类
/// </summary>
internal class CShapeTreeStorage
{

    private readonly CSharpKeyword _keyword = new();

    private readonly HashSet<string> _类名集合 = new();

    private readonly DictionaryCollection<CShapeClassNode, string> _属性名集合 = new();

    public List<CShapeClassNode> ClassNodes { get; } = new(10);

    public string XmlIntferfaceNodeName { get; internal set; }

    /// <summary>
    ///     申请类名
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string 申请类名(XmlNode node)
    {
        var name = node.Name;
        name = _keyword.处理关键字(name);
        if (_类名集合.Contains(name)) name = node.CurrentId.IdentifierStr.Replace("/", "_");

        if (_类名集合.Contains(name)) throw new Exception("类名重复");

        _类名集合.Add(name);

        return name;
    }

    public string 申请属性名(string attribute, CShapeClassNode classNode)
    {
        attribute = _keyword.处理关键字(attribute);

        if (_属性名集合.ContainsKey(classNode) is false)
        {
            _属性名集合.Add(classNode, attribute);

            return attribute;
        }

        var buff = attribute;

        for (var i = 1; i < int.MaxValue; i++)
        {
            if (_属性名集合[classNode].Contains(buff) is false)
            {
                _属性名集合.Add(classNode, buff);

                return buff;
            }

            buff = $"{attribute}{i}";
        }

        throw new Exception("属性名重复");
    }

    public string 申请类名(string name)
    {
        name = _keyword.处理关键字(name);
        var buff = name;

        for (var i = 1; i < int.MaxValue; i++)
        {
            if (_类名集合.Contains(buff) is false)
            {
                _类名集合.Add(buff);

                return buff;
            }

            buff = $"{name}{i}";
        }

        throw new Exception("类名重复");
    }

}