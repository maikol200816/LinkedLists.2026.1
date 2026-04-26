using Shared;

namespace DoubleList;

public class DoubleLinkedList<T> : ILinkedList<T> where T : IComparable<T>
{
    private Node<T>? _head;
    private Node<T>? _tail;

    public DoubleLinkedList()
    {
        _head = null;
        _tail = null;
    }

    public bool Contains(T data)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data != null && current.Data.Equals(data))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public void InsertAtBeginning(T data)
    {
        var newNode = new Node<T>(data);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
        }
    }

    public void InsertAtEnding(T data)
    {
        var newNode = new Node<T>(data);
        if (_tail == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail.Next = newNode;
            newNode.Previous = _tail;
            _tail = newNode;
        }
    }

    public void InsertOrdered(T data)
    {
        var newNode = new Node<T>(data);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            return;
        }
        if (data.CompareTo(_head.Data) < 0)
        {
            newNode.Next =_head;
            newNode.Previous = null;
            _head = newNode;
           
            return;
        }
        var current = _head;

        while (current.Next != null && current.Next.Data.CompareTo(data) < 0)
        {
            current = current.Next;
        }
        newNode.Next = current.Next;
        newNode.Previous = current;
        current.Next?.Previous = newNode;
        current.Next = newNode;
    }



    public void Remove(T data)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data!.Equals(data))
            {
                if (current == _head) // Found at the head
                {
                    _head = _head.Next;
                    _head!.Previous = null;
                }
                else if (current == _tail) // Found at the tail
                {
                    _tail = _tail.Previous;
                    _tail!.Next = null;
                }
                else // Found in the middle
                {
                    current.Previous!.Next = current.Next;
                    current.Next!.Previous = current.Previous;
                }
                return;
            }
            current = current.Next;
        }
    }

    public void Reverse()
    {
        var current = _head;
        while (current != null)
        {
            var next = current.Next;
            current.Next = current.Previous;
            current.Previous = next;
            current = next;
        }
       var exchange = _head;
        _head= _tail;
        _tail = exchange;
    }
    

    public void Sort()
    {
        if (_head == null) return;

        bool swapped;

        do
        {
            swapped = false;
            var current = _head;

            while (current.Next != null)
            {
                if (current.Data.CompareTo(current.Next.Data) > 0)
                {
                    var temp = current.Data;
                    current.Data = current.Next.Data;
                    current.Next.Data = temp;

                    swapped = true;
                }

                current = current.Next;
            }

        } while (swapped);
    }
    

    override public string ToString()
    {
        var current = _head;
        var result = string.Empty;
        while (current != null)
        {
            result += $"{current.Data} -> ";
            current = current.Next;
        }
        result += "null";
        return result;
    }

    public string ToStringReverse()
    {
        var current = _tail;
        var result = string.Empty;
        while (current != null)
        {
            result += $"{current.Data} -> ";
            current = current.Previous;
        }
        result += "null";
        return result;
    }
}