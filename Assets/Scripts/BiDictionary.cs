using System.Collections.Generic;

public class BiDictionary<T1, T2>
{
    private Dictionary<T1, T2> forward = new();
    private Dictionary<T2, T1> reverse = new();

    public void Add(T1 key, T2 value)
    {
        forward.Add(key, value);
        reverse.Add(value, key);
    }

    public T2 Get(T1 key)
    {
        return forward[key];
    }

    public T1 Get(T2 value)
    {
        return reverse[value];
    }
    public bool Contains(T1 key)
    {
        return forward.ContainsKey(key);
    }
    public bool Contains(T2 value)
    {
        return forward.ContainsValue(value);
    }
}