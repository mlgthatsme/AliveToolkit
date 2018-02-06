using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliveAPIDotNet
{
    public class UnmanagedArray<T> : UnmanagedObject, IReadOnlyList<T>
    {
        public UnmanagedArray(IntPtr addr, int elementSize, int elementCount) : base(addr, true, elementSize * elementCount)
        {
            Entries.Clear();
            for (int i = 0; i < elementCount;i++)
            {
                Entries.Add((T)Activator.CreateInstance(typeof(T), addr + (i * elementSize)));
            }
        }

        public static implicit operator List<T>(UnmanagedArray<T> op)
        {
            return op.Entries;
        }

        public static implicit operator T[](UnmanagedArray<T> op)
        {
            return op.Entries.ToArray();
        }

        public List<T> Entries { get; protected set; } = new List<T>();

        public T this[int index] => ((IReadOnlyList<T>)Entries)[index];
        public int Count => ((IReadOnlyList<T>)Entries).Count;
        public IEnumerator<T> GetEnumerator()
        {
            return ((IReadOnlyList<T>)Entries).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IReadOnlyList<T>)Entries).GetEnumerator();
        }
    }
}
