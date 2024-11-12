using System;
using System.Collections.Generic;

public class ObservableList<T>
{
    private List<T> items = new List<T>();
    public event Action<List<T>> OnListChanged;

    public void Add(T item)
    {
        items.Add(item);
        OnListChanged?.Invoke(items);
    }

    public void Remove(T item)
    {
        if (items.Remove(item))
        {
            OnListChanged?.Invoke(items); 
        }
    }

    public List<T> GetItems() => items;
}
