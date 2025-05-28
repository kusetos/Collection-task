// See https://aka.ms/new-console-template for more information
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Task;
using Microsoft.VisualBasic;

class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine();
        var myList = new Task.MyList<int>();
        myList.Add(1);
        myList.Add(2);
        myList.Add(3);
        myList.Add(4);
        myList.Add(5);

        for (int i = 0; i < myList.Count; i++)
        {
            Console.Write(myList[i] + " ");
        }
        Console.WriteLine();

        myList.RemoveAt(1);
        foreach (var item in myList)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        var filtered = myList.Where(x => x > 20);
        foreach (var item in filtered)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        var array = myList.GetArray();
        foreach (var item in array)
        {
            Console.WriteLine(item + " ");
        }
    }
}

namespace Task
{
    public class MyList<T> : IEnumerable<T>
    {
        private T[] _items;
        private int _count;

        public MyList()
        {
            _items = new T[4];
            _count = 0;
        }

        public int Count => _count;
        private bool IsOutOfRange(int index) => index < 0 || index >= _count;

        public void Add(T item)
        {
            if (_count == _items.Length)
                Resize();

            _items[_count++] = item;
        }
        public void RemoveAt(int index)
        {
            if (IsOutOfRange(index))
                throw new ArgumentOutOfRangeException(nameof(index));

            for (int i = index; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _count--;
            _items[_count] = default!;
        }

        public T this[int index]
        {
            get
            {
                if (IsOutOfRange(index))
                    throw new ArgumentOutOfRangeException(nameof(index));
                return _items[index];
            }
        }

        private void Resize()
        {
            T[] newArray = new T[_items.Length * 2];
            Array.Copy(_items, newArray, _items.Length);
            _items = newArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
                yield return _items[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class MyListExtensions
    {
        public static T[] GetArray<T>(this MyList<T> list)
        {
            T[] result = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                result[i] = list[i];
            }
            return result;
        }
    }
}
