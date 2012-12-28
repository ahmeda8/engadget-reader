using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Phone.Net.NetworkInformation;

namespace Engadget_Reader
{
    class Network
    {
        public static bool IsOnline()
        {
#if DEBUG
            return true;
#else
            return NetworkInterface.GetIsNetworkAvailable();
#endif
        }
    }
}
