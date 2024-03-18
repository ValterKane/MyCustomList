using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonArray
{
    public class MyCustomList<T> : IEnumerable<T>, ICollection<T>, ICollection
    {
        internal T[]? _innerArray;
        internal int _size;
        internal int _version;

        private static readonly T[] EmptyArray = new T[0];

        public int Count => _size;

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;

        public object SyncRoot => this;

        public MyCustomList()
        {
            _innerArray = EmptyArray;
            _size = 0;
        }

        public MyCustomList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();
            else
            {
               if(capacity == 0)
                    _innerArray = EmptyArray;
               else
                    _innerArray = new T[capacity];

               _size = capacity;
            }     
        }

        public MyCustomList(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();
            else
            {
                if (collection is ICollection<T> _collect)
                {
                    int count = _collect.Count;
                    if (count == 0)
                    {
                        _innerArray = EmptyArray;
                    }
                    else
                    {
                        _innerArray = new T[count];
                        _collect.CopyTo(_innerArray, 0);
                        _size = count;
                    }
                }
                else
                {
                    using (IEnumerator<T> enumerator = collection!.GetEnumerator())
                    {
                        while(enumerator.MoveNext())
                        {
                            Add(enumerator.Current);
                        }
                    }
                }
            }
        }



        public void Add(T item)
        {
            _version++;
            T[] _temp = _innerArray;
            int size = _size;
            if ((uint)size < (uint)_temp.Length)
            {
                _size = size + 1;
                _innerArray[size] = item;
            }
            else
            {
                AddWithResize();
            }
        }

        public void AddRange(IEnumerable<T> collection)
        {
            throw new NotImplementedException();
        }

        internal void AddWithResize()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _size; i++)
            {
                yield return _innerArray[i];
            }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }
    }
}
