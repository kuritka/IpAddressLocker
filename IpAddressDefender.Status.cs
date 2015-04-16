using System;

namespace IpAddressLocker
{
    partial class IpAddressDefender
    {
        private class Status
        {
            private readonly int _requestLimitPerSecond ;

            private readonly FixedQueue<DateTime> _queue;

            public Status(int requestLimitPerSecond)
            {
                _requestLimitPerSecond = requestLimitPerSecond;
                _queue = new FixedQueue<DateTime>(_requestLimitPerSecond);
            }

            public bool Enabled
            {
                get
                {
                    if (_queue.Data.Length > _requestLimitPerSecond) throw new OverflowException("Size");
                    if (_queue.Data.Length < _requestLimitPerSecond)
                    {
                        return true;
                    }
                    return (_queue.Data[9] - _queue.Data[0]).TotalMilliseconds>= 1000;
                }
            }

            public void Enqueue(DateTime ipAddress)
            {
                _queue.Enqueue(ipAddress);
            }
        }
    }
}
