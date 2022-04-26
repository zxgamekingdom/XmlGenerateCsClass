namespace 多Xml文档建立CShape类;

public static class StackExtensions
{

    public static void PushArray<T>(this Stack<T> collection, IEnumerable<T> items)
    {
        foreach (var item in items) collection.Push(item);
    }

}
