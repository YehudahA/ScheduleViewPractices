using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectModel
{
    public sealed class CollectionWrapper<TBase, TDerived> : ICollection<TBase> where TDerived : TBase
    {
        private readonly ICollection<TDerived> source;

        public CollectionWrapper(ICollection<TDerived> source)
        {
            this.source = source;
        }

        public void Add(TBase item)
        {
            this.source.Add((TDerived)item);
        }

        public void Clear()
        {
            source.Clear();
        }

        public bool Contains(TBase item)
        {
            return source.Contains((TDerived)item);
        }

        public void CopyTo(TBase[] array, int arrayIndex)
        {
            int count = this.source.Count;
            try
            {
                for (int i = 0; i < count; i++)
                {
                    int num = arrayIndex;
                    arrayIndex = num + 1;
                    array[num] = this.source.Skip(i).FirstOrDefault();
                }
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        public int Count
        {
            get { return source.Count; }
        }

        public bool IsReadOnly
        {
            get { return source.IsReadOnly; }
        }

        public bool Remove(TBase item)
        {
            return source.Remove((TDerived)item);
        }

        public IEnumerator<TBase> GetEnumerator()
        {
            var enumerator = new WrapperEnumerator(source.GetEnumerator());
            return enumerator;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public class WrapperEnumerator : IEnumerator<TBase>
        {
            readonly IEnumerator<TDerived> sourceEnumerator;

            public WrapperEnumerator(IEnumerator<TDerived> sourceEnumerator)
            {
                this.sourceEnumerator = sourceEnumerator;
            }

            public TBase Current
            {
                get { return sourceEnumerator.Current; }
            }

            public void Dispose()
            {
                sourceEnumerator.Dispose();
            }

            object System.Collections.IEnumerator.Current
            {
                get { return this.Current; }
            }

            public bool MoveNext()
            {
                return sourceEnumerator.MoveNext();
            }

            public void Reset()
            {
                sourceEnumerator.Reset();
            }
        }
    }
}