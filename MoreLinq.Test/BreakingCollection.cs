#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
// Copyright (c) 2008 Jonathan Skeet. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

namespace MoreLinq.Test
{
    using System;
    using System.Collections.Generic;

    partial class TestExtensions
    {
        internal static IEnumerable<T> ToBreakingCollection<T>(this IEnumerable<T> enumerable, bool readOnly) =>
            readOnly
            ? (IEnumerable<T>)new BreakingReadOnlyCollection<T>(enumerable.ToList())
            : new BreakingCollection<T>(enumerable.ToList());
    }

    class BreakingCollection<T> : BreakingSequence<T>, ICollection<T>
    {
        protected readonly IList<T> List;

        public BreakingCollection(params T[] values) : this ((IList<T>) values) {}
        public BreakingCollection(IList<T> list) => List = list;
        public BreakingCollection(int count) :
            this(Enumerable.Repeat(default(T), count).ToList()) {}

        public int Count => List.Count;

        public void Add(T item) => throw new NotImplementedException();
        public void Clear() => throw new NotImplementedException();
        public bool Contains(T item) => List.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);
        public bool Remove(T item) => throw new NotImplementedException();
        public bool IsReadOnly => true;
    }
}
