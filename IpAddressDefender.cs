using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;


namespace IpAddressLocker
{
    public partial class IpAddressDefender
    {
        private readonly Dictionary<string, Status> _ipStatus = new Dictionary<string, Status>();
        private readonly int _requestLimitPerSecond;
       

        public IpAddressDefender(int requestLimitPerSecond)
        {
            if (requestLimitPerSecond < 1) throw new ArgumentException("requestLimitPerSecond");
            _requestLimitPerSecond = requestLimitPerSecond;
        }

        public bool Scan(IPAddress address)
        {
            if(address == null) throw new ArgumentNullException("address");
            
            var strAdress = address.ToString();
            if (!_ipStatus.ContainsKey(strAdress))
            {
                _ipStatus.Add(strAdress, new Status(_requestLimitPerSecond));
            }
            _ipStatus[strAdress].Enqueue(DateTime.Now);
            return _ipStatus[strAdress].Enabled;
        }
    }
}
