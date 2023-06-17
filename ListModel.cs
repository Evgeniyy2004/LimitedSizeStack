using System;
using System.Collections.Generic;

namespace LimitedSizeStack;

public class ListModel<TItem>
{
    public List<TItem> Items { get; }
    public int UndoLimit;
    public LimitedSizeStack<Dictionary<int, TItem>> Operations;

    public ListModel(int undoLimit) : this(new List<TItem>(), undoLimit)
    {
        UndoLimit = undoLimit;
        Operations = new LimitedSizeStack<Dictionary<int, TItem>>(undoLimit);
    }

    public ListModel(List<TItem> items, int undoLimit)
    {
        Items = items;
        UndoLimit = undoLimit;
        Operations = new LimitedSizeStack<Dictionary<int, TItem>>(undoLimit);
    }

    

    public void AddItem(TItem item)
    {
        Items.Add(item);
        Operations.Push(new Dictionary<int, TItem> { { -1, item } });
    }

    public void RemoveItem(int index)
    {
        Operations.Push(new Dictionary<int, TItem> { { index, Items[index] } });
        Items.RemoveAt(index);
    }

    public bool CanUndo()
    {
        return Operations!= null && Operations.Count != 0 ;
    }

    public void Undo()
    {
        //throw new NotImplementedException();
        var doo = Operations.Pop();
        foreach (var kee in doo.Keys)
        {
            if (kee < 0) Items.RemoveAt(Items.Count - 1);
            else Items.Insert(kee, doo[kee]);
        }
    }
}