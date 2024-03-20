﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonArray
{
    public class MyCustomList<T> : IEnumerable<T>, ICollection<T>, ICollection
    {
        // Внутренее хранилище данных
        internal T[]? _innerArray;
        // Размер внутреннего хранилища
        internal int _size;
        // Текущая версия объекта
        internal int _version;
        internal int _capacity;

        // Пустая область памяти
        private static readonly T[] EmptyArray = new T[0];

        // Своства количества элементов в списке
        public int Count => _size;

        // Свойство вместимости коллекци
        public int Capacity
        {
            get => _capacity;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("value");
                else
                {
                    if (value != _innerArray.Length)
                    {
                        if (value > 0)
                        {
                            T[] newItems = new T[value];
                            if (_size > 0)
                            {
                                Array.Copy(_innerArray, newItems, _size);
                            }
                            _innerArray = newItems;
                            _capacity = _innerArray.Length;
                        }
                        else
                        {
                            _innerArray = EmptyArray;
                            _capacity = 0;
                        }
                    }
                }
            }
        }

        // Флаги списка
        public bool IsReadOnly => false; // Списк только для чтения?
        public bool IsSynchronized => false; // Список синхронизирован?
        public object SyncRoot => this; // Где находится корень синхронизации?

        // Конструкторы
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
               Capacity = _size;
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
                        Capacity = _size;
                        
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

        // Метод добавления элементов в конец
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                AddWithResize(item);
            }
        }

        #region Методы, которые нужно реализовать (Lvl-Noob)
        public void AddRange(IEnumerable<T> collection)
        {
            throw new NotImplementedException();
        }

        internal void AddWithResize(T item)
        {
            bool overflow = _size + 1 > Capacity ? true : false;

            if (overflow)
            {
                Capacity = _size + 1;
                _size += 1;
                _innerArray[_size] = item;
            }
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

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item) => throw new NotImplementedException();
        #endregion

        #region Методы, которые нужно реализовать (Lvl-PRO)
        // Метод, который конвертирует все объекты списка из одного типа в другой
        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) => throw new NotImplementedException();
        public int EnsureCapacity(int capacity) => throw new NotImplementedException();
        public bool Exists(Predicate<T> match) => throw new NotImplementedException();
        public T? Find(Predicate<T> match) => throw new NotImplementedException();
        public List<T> FindAll(Predicate<T> match) => throw new NotImplementedException();
        public int BinarySearch(int index, int count, T item, IComparer<T>? comparer) => throw new NotImplementedException();
        public void QuickSort(bool isASC) => throw new NotImplementedException();

        #endregion


        // Итераторы
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _size; i++)
            {
                yield return _innerArray[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerArray.GetEnumerator();
        }

        // Индексатор
        public T this[int index]
        {
            get
            {
                if((uint) index > _size)
                    throw new ArgumentOutOfRangeException();

                return _innerArray[index];
            }

            set 
            {
                if((uint)index > _size)
                    throw new ArgumentOutOfRangeException();

                _innerArray[index] = value;
                _version++;

            }
        }

        
    }
}
