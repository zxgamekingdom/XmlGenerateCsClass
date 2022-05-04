#region

using System.Collections.Immutable;
using 多Xml文档建立CShape类.Trees.Xmls;

#endregion

namespace 多Xml文档建立CShape类.Trees.CSharps;

public class CShapeTree
{

    public ImmutableArray<CShapeClassNode> ClassNodes { get; private set; }

    public void Load(MultipleXmlDocumentNodeTree tree,
        ArrayType arrayType = ArrayType.Array)
    {
        var storage = new CShapeTreeStorage();
        _ = new CShapeClassNode(storage);
        _ = new CShapeClassNode(tree.Root, storage, arrayType);
        ClassNodes = storage.ClassNodes.ToImmutableArray();
    }

}