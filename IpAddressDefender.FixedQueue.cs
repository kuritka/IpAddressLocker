using System;
using System.Collections.Generic;

namespace IpAddressLocker
{
    partial class IpAddressDefender
    {
        private sealed class FixedQueue<T>
        {
            private readonly Queue<T> _queue = new Queue<T>();
            private readonly object _toLock = new object();
            private readonly int _size;

            public FixedQueue(int size)
            {
                if (size < 1) throw new ArgumentException("size");
                _size = size;
            }

            public void Enqueue(T item)
            {
                _queue.Enqueue(item);
                lock (_toLock)
                {
                    if (_queue.Count > _size) _queue.Dequeue();
                }
            }

            public T[] Data
            {
                get { return _queue.ToArray(); }
            }
        }
    }
}
