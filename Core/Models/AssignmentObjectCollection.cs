namespace StatisticsPoland.VtlProcessing.Core.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The collection of <see cref="AssignmentObject"/>.
    /// </summary>
    public sealed class AssignmentObjectCollection : ICollection<AssignmentObject>
    {
        private readonly ICollection<AssignmentObject> _items;

        /// <summary>
        /// Initialises a new instance of the <see cref="AssignmentObjectCollection"/> class.
        /// </summary>
        public AssignmentObjectCollection()
        {
            this._items = new List<AssignmentObject>();
        }

        public AssignmentObject this[int index] => this._items.ToArray()[index];

        public AssignmentObject this[string name] => this._items.FirstOrDefault(item => item.Name == name);

        public int Count => this._items.Count;

        public bool IsReadOnly => false;

        public void Add(AssignmentObject item)
        {
            this._items.Add(item);
        }

        public void Clear()
        {
            this._items.Clear();
        }

        public bool Contains(AssignmentObject item)
        {
            return this._items.Contains(item);
        }

        public void CopyTo(AssignmentObject[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<AssignmentObject> GetEnumerator()
        {
            return this._items.GetEnumerator();
        }

        public bool Remove(AssignmentObject item)
        {
            return this._items.Remove(item);
        }

        /// <summary>
        /// Removes all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match"></param>
        public void RemoveAll(Predicate<AssignmentObject> match)
        {
            (this._items as List<AssignmentObject>).RemoveAll(match);
        }

        /// <summary>
        /// Creates an array from a <see cref="IEnumerable>"/>&lt;<see cref="AssignmentObject"/>&gt;.
        /// </summary>
        /// <returns>The array of assignment objects.</returns>
        public AssignmentObject[] ToArray()
        {
            return this._items.ToArray();
        }

        /// <summary>
        /// Creates a <see cref="List"/>&lt;<see cref="AssignmentObject"/>&gt; from a <see cref="IEnumerable>"/>&lt;<see cref="AssignmentObject"/>&gt;.
        /// </summary>
        /// <returns>The list of assignment objects.</returns>
        public List<AssignmentObject> ToList()
        {
            return this._items.ToList();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
