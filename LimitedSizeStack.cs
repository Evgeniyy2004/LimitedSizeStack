// Вставьте сюда финальное содержимое файла LimitedSizeStack.cs
using System;

namespace LimitedSizeStack;

public class LimitedSizeStack<T>
{
    private Element<T> head { get; set; }
    private Element<T> tail { get; set; }
    int length { get; set; }

    public LimitedSizeStack(int undoLimit)
    {
        length = undoLimit;
    }

    public void Push(T item)
    {
        if (head == null && tail == null)
        {
            if (length > 0)
            {
                head = tail = new Element<T> { Value = item };
                Count++;
            }
        }

        else
        {
            if (length == Count)
            {
                head = head.Next;
                if (head !=null)  head.Prev = null;
                Count--;
            }

            var now = new Element<T> { Value = item, Prev = tail };
            tail.Next = now;
            tail = now;
            Count++;
        }
    }

    public T Pop()
    {
        if (Count == 0 || length == 0) throw new Exception();
        var result = tail.Value;
        if (Count == 1)
        {
            head = null;
            tail = null;
            Count--;
        }

        else
        {
            tail = tail.Prev;
            Count--;
        }

        return result;
    }

    public int Count { get; private set; }
}
public class Element<T>
{
    public T Value;
    public Element<T> Next;
    public Element<T> Prev;
}
