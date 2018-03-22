namespace MoreLinq.Test
{
    using System.Collections;
    using System.Collections.Generic;

    partial class TestExtensions
    {
        internal static IEnumerable<T> ToBreakingList<T>(this IEnumerable<T> enumerable, bool readOnly) =>
            readOnly
            ? (IEnumerable<T>)new BreakingReadOnlyList<T>(enumerable.ToList())
            : new BreakingList<T>(enumerable.ToList());
    }

    /// <summary>
    /// This class implement <see cref="IList{T}"/> but specifically prohibits enumeration using GetEnumerator().
    /// It is provided to assist in testing extension methods that MUST NOT call the GetEnumerator()
    /// method of <see cref="IEnumerable"/> - either because they should be using the indexer or because they are
    /// expected to be lazily evaluated.
    /// </summary>

    sealed class BreakingList<T> : BreakingCollection<T>, IList<T>
    {
        public BreakingList() : this(new List<T>()) {}
        public BreakingList(List<T> list) : base(list) { }

        public int IndexOf(T item) => _list.IndexOf(item);
        public void Insert(int index, T item) => _list.Insert(index, item);
        public void RemoveAt(int index) => _list.RemoveAt(index);

        public T this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }
    }
}
