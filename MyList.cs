using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyList
{
    class Program
    {
        public static void Main()
        {
            int[] arr = new int[7];
            //int[] arrTest = new int[6];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i + 1;
            }
            //MyList<int> intList0 = new MyList<int>();
            //MyList<int> intList1 = new MyList<int>(3);
            MyList<int> intList2 = new MyList<int>(arr);
            Console.WriteLine(intList2.Count);
            intList2.Count = 5;
            intList2.Remove(-3);
            //intList0.Count = 6;
            for (int i = 0; i < intList2.Count; i++)
            {
                Console.Write(intList2[i] + " | ");
            }
            Console.WriteLine();
            Console.Write(intList2.Count);
            Console.Write(intList2.IndexOf(6));
            //Console.WriteLine(intList2.IndexOf(1));
            //Console.WriteLine();
            //Console.WriteLine(intList2.Count);
            Console.ReadKey();
            //intList2.RemoveAt(7);
            //foreach (int it in intList1)
            //{
            //    Console.Write(it + " | ");
            //}
            //for (int i = 0; i < arrTest.Length; i++)
            //{
            //    Console.Write(arrTest[i] + " | ");
            //}
            //Console.WriteLine(intList2.Count);
            //Console.WriteLine(intList0.IsReadOnly);
            //Console.ReadKey();
        }
    }

    class MyList<T> : IList<T>
    {
        int _count;
        int _capacity;
        T[] _items;
        public MyList()
        {
            _count = -1;
            _capacity = 0;
            _items = new T[_capacity];
        }
        public MyList(int capacity)
        {
            _capacity = capacity;
            _count = 0;
            _items = new T[capacity];
        }
        public MyList(IEnumerable<T> collection)
        {
            _capacity = collection.Count();
            _count = _capacity - 1;
            _items = (T[])collection;
        }
        public T[] Items
        {
            get => _items;
        }
        public int Count
        {
            get => _capacity;
            set
            {
                try
                {
                    if (value > _capacity)
                    {
                        Array.Resize(ref _items, value);
                        _capacity = value;
                    }
                    else
                    {
                        Array.Resize(ref _items, value);
                        _count = value - 1;
                        _capacity = value;
                    }
                }
                catch (ArgumentException exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }
        public bool IsReadOnly
        {
            get => ((ICollection<T>)_items).IsReadOnly;
        }
        public T this[int i]
        {
            get
            {
                if (i < Count)
                    return _items[i];
                else
                {
                    ArgumentOutOfRangeException exc = new ArgumentOutOfRangeException();
                    Console.WriteLine(exc.Message);
                    throw exc;
                }
            }
            set
            {
                try
                {
                    _items[i] = value;
                }
                catch (ArgumentOutOfRangeException exc)
                {
                    Console.WriteLine(exc.Message);
                }
                catch (NotSupportedException exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }
        public void Add(T item)
        {
            try
            {
                if (_count < Count - 1)
                {
                    _count++;
                    _items[_count] = item;
                }
                else
                {
                    _capacity++;
                    _count++;
                    Array.Resize(ref _items, _capacity);
                    _items[_count] = item;
                }
                return;
            }
            catch (NotSupportedException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        public void Clear()
        {
            _items = Array.Empty<T>();
            Array.Resize(ref _items, _capacity);
        }
        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (IndexOf(item) != -1)
                {
                    return true;
                }
            }
            return false;
        }
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_items[i].GetHashCode() == item.GetHashCode())
                {
                    return i;
                }
            }
            return -1;
        }
        public void Insert(int index, T item)
        {
            if (index >= 0 & index < Count)
            {
                try
                {
                    if (index > _count & index < Count)
                    {
                        _items[index] = item;
                    }
                    else
                    {
                        _capacity++;
                        Array.Resize(ref _items, _capacity);
                        for (int i = _capacity - 2; i >= index; i--)
                        {
                            _items[i + 1] = _items[i];
                        }
                        _items[index] = item;
                    }
                    return;
                }
                catch (NotSupportedException exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
            else
            {
                ArgumentOutOfRangeException exc = new ArgumentOutOfRangeException();
                Console.WriteLine(exc.Message);
                throw exc;
            }
        }
        public bool Remove(T item)
        {
            try
            {
                if ((IndexOf(item) != -1))
                {
                    RemoveAt(IndexOf(item));
                    _capacity--;
                    Array.Resize(ref _items, _capacity);
                    return true;
                }
            }
            catch (NotSupportedException exc)
            {
                Console.WriteLine(exc.Message);
            }
            return false;
        }
        public void RemoveAt(int index)
        {
            try
            {
                if (index >= 0 & index < Count)
                {
                    for (int i = index; i < Count - 1; i++)
                    {
                        _items[i] = _items[i + 1];
                    }
                    _capacity--;
                    Array.Resize(ref _items, _capacity);
                    return;
                }
                else
                {
                    ArgumentOutOfRangeException exc = new ArgumentOutOfRangeException();
                    Console.WriteLine(exc.Message);
                    throw exc;
                }
            }
            catch (NotSupportedException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            try
            {
                if (array.Length >= arrayIndex)
                {
                    if (arrayIndex <= (array.Length - Count))
                    {
                        for (int i = 0; i < Count; i++)
                        {
                            array.SetValue(_items[i], arrayIndex++);
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= Count && arrayIndex < array.Length; i++)
                        {
                            array.SetValue(_items[i], arrayIndex++);
                        }
                    }
                    return;
                }
                ArgumentOutOfRangeException exc = new ArgumentOutOfRangeException();
                Console.WriteLine(exc.Message);
                throw exc;
            }
            catch (ArgumentNullException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch (ArgumentException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Items).GetEnumerator();
        }
    }
}
