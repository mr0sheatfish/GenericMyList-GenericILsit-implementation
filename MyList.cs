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
            //int[] arr = new int[10];
            //int[] arrTest = new int[6];
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    arr[i] = i + 1;
            //}
            //MyList<int> intList0 = new MyList<int>();
            //MyList<int> intList1 = new MyList<int>(3);
            //MyList<int> intList2 = new MyList<int>(arr);
            //intList0.Count = 12;
            //intList0.Count = 6;
            //intList2.CopyTo(arrTest, 0);
            //for (int i = 0; i < intList1.Count; i++)
            //{
            //    Console.Write(intList1[i] + " | ");
            //}
            //Console.WriteLine(intList2.IndexOf(1));
            //Console.WriteLine();
            //Console.WriteLine(intList2.Count);
            //Console.ReadKey();
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
            get => _items.Length;
            set
            {
                if (value > _capacity)
                {
                    try
                    {
                        Array.Resize(ref _items, value);
                        _count = value - 1;
                        _capacity = value;
                    }
                    catch (ArgumentException exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                }
                else
                {
                    _count = value;
                }
            }
        }
        public bool IsReadOnly
        {
            get => ((ICollection<T>)_items).IsReadOnly;
        }
        public T this[int i]
        {
            get => _items[i];
            set => _items[i] = value;
        }
        public void Add(T item)
        {
            try
            {
                if (_count < _items.Length - 1)
                {
                    _items[_count] = item;
                    _count++;
                    return;
                }
                _capacity++;
                _count++;
                Array.Resize(ref _items, _capacity);
                _items[_count] = item;
            }
            catch (NotSupportedException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        public void Clear()
        {
            _count = 0;
        }
        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Equals(_items[i], item))
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
                if (Equals(_items[i], item))
                {
                    return i;
                }
            }
            return -1;
        }
        public void Insert(int index, T item)
        {
            if (index >= 0)
            {
                try
                {
                    if (index > _count & index < _capacity)
                    {
                        _items[index] = item;
                    }
                    else if (index < _count)
                    {
                        for (int i = _count - 2; i >= index; i--)
                        {
                            _items[i + 1] = _items[i];
                        }
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
                }
                catch (IndexOutOfRangeException exc)
                {
                    Console.WriteLine(exc.Message);
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
        public bool Remove(T item)
        {
            try
            {
                if ((IndexOf(item) != -1))
                {
                    RemoveAt(IndexOf(item));
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
                if ((index >= 0) && (index < Count))
                {
                    for (int i = index; i < Count - 1; i++)
                    {
                        _items[i] = _items[i + 1];
                    }
                    _count--;
                }
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
                        return;
                    }
                    else
                    {
                        for (int i = 0; i <= Count && arrayIndex < array.Length; i++)
                        {
                            array.SetValue(_items[i], arrayIndex++);
                        }
                        return;
                    }
                }
                throw new ArgumentOutOfRangeException();
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
