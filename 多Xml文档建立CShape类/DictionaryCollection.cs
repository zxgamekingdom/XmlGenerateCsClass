namespace 多Xml文档建立CShape类;

//字典集合
internal class DictionaryCollection<TK, TV> where TK : notnull
{

    private readonly Dictionary<TK, List<TV>> _dictionary = new();

    public IEnumerable<TK> Keys => _dictionary.Keys;

    public IEnumerable<TV> this[TK k] =>
        _dictionary.TryGetValue(k, out var list) ? list : Enumerable.Empty<TV>();

    public void Add(TK key, TV value)
    {
        if (!_dictionary.TryGetValue(key, out var list))
        {
            list = new List<TV>();
            _dictionary.Add(key, list);
        }

        list.Add(value);
    }

}
