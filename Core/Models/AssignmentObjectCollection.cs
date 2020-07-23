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
        private ICollection<AssignmentObject> items;

        /// <summary>
        /// Initialises a new instance of the <see cref="AssignmentObjectCollection"/> class.
        /// </summary>
        public AssignmentObjectCollection()
        {
            this.items = new List<AssignmentObject>();
        }

        public AssignmentObject this[int index] => this.items.ToArray()[index];

        public AssignmentObject this[string name] => this.items.FirstOrDefault(item => item.Name == name);

        public int Count => this.items.Count;

        public bool IsReadOnly => false;

        public void Add(AssignmentObject item)
        {
            this.items.Add(item);
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public bool Contains(AssignmentObject item)
        {
            return this.items.Contains(item);
        }

        public void CopyTo(AssignmentObject[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<AssignmentObject> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        public bool Remove(AssignmentObject item)
        {
            return this.items.Remove(item);
        }

        /// <summary>
        /// Removes all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match"></param>
        public void RemoveAll(Predicate<AssignmentObject> match)
        {
            (this.items as List<AssignmentObject>).RemoveAll(match);
        }

        /// <summary>
        /// Creates an array from a <see cref="IEnumerable>"/>&lt;<see cref="AssignmentObject"/>&gt;.
        /// </summary>
        /// <returns>The array of assignment objects.</returns>
        public AssignmentObject[] ToArray()
        {
            return this.items.ToArray();
        }

        /// <summary>
        /// Creates a <see cref="List"/>&lt;<see cref="AssignmentObject"/>&gt; from a <see cref="IEnumerable>"/>&lt;<see cref="AssignmentObject"/>&gt;.
        /// </summary>
        /// <returns>The list of assignment objects.</returns>
        public List<AssignmentObject> ToList()
        {
            return this.items.ToList();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
