//------------------------------------------------------------------------------
// <copyright file="CsvBindingList.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
// </copyright>
//
// <license>
//    Licensed under the Microsoft Public License (Ms-PL) (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </license>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Cadru.Data.Csv
{
#if !NETSTANDARD1_3

    /// <summary>
    /// Represents a binding list wrapper for a CSV reader.
    /// </summary>
    public class CsvBindingList : IBindingList, ITypedList, IList<string[]>
    {
        /// <summary>
        /// Contains the linked CSV reader.
        /// </summary>
        private readonly CachedCsvReader _csv;

        /// <summary>
        /// Contains the cached record count.
        /// </summary>
        private int _count;

        /// <summary>
        /// Contains the cached property descriptors.
        /// </summary>
        private PropertyDescriptorCollection _properties;

        /// <summary>
        /// Contains the current sort property.
        /// </summary>
        private CsvPropertyDescriptor _sort;

        /// <summary>
        /// Contains the current sort direction.
        /// </summary>
        private ListSortDirection _direction;

        /// <summary>
        /// Initializes a new instance of the CsvBindingList class.
        /// </summary>
        /// <param name="csv"></param>
        public CsvBindingList(CachedCsvReader csv)
        {
            this._csv = csv;
            this._count = -1;
            this._direction = ListSortDirection.Ascending;
        }

        public void AddIndex(PropertyDescriptor property)
        {
        }

        public bool AllowNew => false;

        public void ApplySort(PropertyDescriptor property, System.ComponentModel.ListSortDirection direction)
        {
            this._sort = (CsvPropertyDescriptor)property;
            this._direction = direction;

            this._csv.ReadToEnd();

            this._csv.Records.Sort(new CsvRecordComparer(this._sort.Index, this._direction));
        }

        public PropertyDescriptor SortProperty => this._sort;

        public int Find(PropertyDescriptor property, object key)
        {
            var fieldIndex = ((CsvPropertyDescriptor)property).Index;
            var value = (string)key;

            var recordIndex = 0;
            var count = this.Count;

            while (recordIndex < count && this._csv[recordIndex, fieldIndex] != value)
                recordIndex++;

            return recordIndex == count ? -1 : recordIndex;
        }

        public bool SupportsSorting => true;

        public bool IsSorted => this._sort != null;

        public bool AllowRemove => false;

        public bool SupportsSearching => true;

        public System.ComponentModel.ListSortDirection SortDirection => this._direction;

        public event System.ComponentModel.ListChangedEventHandler ListChanged
        {
            add { }
            remove { }
        }

        public bool SupportsChangeNotification => false;

        public void RemoveSort()
        {
            this._sort = null;
            this._direction = ListSortDirection.Ascending;
        }

        public object AddNew()
        {
            throw new NotSupportedException();
        }

        public bool AllowEdit => false;

        public void RemoveIndex(PropertyDescriptor property)
        {
        }

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (this._properties == null)
            {
                var properties = new PropertyDescriptor[this._csv.FieldCount];

                for (var i = 0; i < properties.Length; i++)
                {
                    properties[i] = new CsvPropertyDescriptor(((System.Data.IDataReader)this._csv).GetName(i), i);
                }

                this._properties = new PropertyDescriptorCollection(properties);
            }

            return this._properties;
        }

        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return String.Empty;
        }

        public int IndexOf(string[] item)
        {
            throw new NotSupportedException();
        }

        public void Insert(int index, string[] item)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        public string[] this[int index]
        {
            get
            {
                this._csv.MoveTo(index);
                return this._csv.Records[index];
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public void Add(string[] item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(string[] item)
        {
            throw new NotSupportedException();
        }

        public void CopyTo(string[][] array, int arrayIndex)
        {
            this._csv.MoveToStart();

            while (this._csv.ReadNextRecord())
            {
                this._csv.CopyCurrentRecordTo(array[arrayIndex++]);
            }
        }

        public int Count
        {
            get
            {
                if (this._count < 0)
                {
                    this._csv.ReadToEnd();
                    this._count = (int)this._csv.CurrentRecordIndex + 1;
                }

                return this._count;
            }
        }

        public bool IsReadOnly => true;

        public bool Remove(string[] item)
        {
            throw new NotSupportedException();
        }

        public IEnumerator<string[]> GetEnumerator()
        {
            return this._csv.GetEnumerator();
        }

        public int Add(object value)
        {
            throw new NotSupportedException();
        }

        public bool Contains(object value)
        {
            throw new NotSupportedException();
        }

        public int IndexOf(object value)
        {
            throw new NotSupportedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotSupportedException();
        }

        public bool IsFixedSize => true;

        public void Remove(object value)
        {
            throw new NotSupportedException();
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public void CopyTo(Array array, int index)
        {
            this._csv.MoveToStart();

            while (this._csv.ReadNextRecord())
            {
                this._csv.CopyCurrentRecordTo((string[])array.GetValue(index++));
            }
        }

        public bool IsSynchronized => false;

        public object SyncRoot => null;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

#endif
}