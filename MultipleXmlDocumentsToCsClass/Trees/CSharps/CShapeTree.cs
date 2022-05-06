#region

using System.Collections.Immutable;
using MultipleXmlDocumentsToCsClass.Trees.Xmls;

#endregion

namespace MultipleXmlDocumentsToCsClass.Trees.CSharps;

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