using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using IpAddressViaMailCSharp.Network;

namespace IpAddressViaMailCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initial logging
            Logging.Initial();
            //Load from config file
            Config.Inital();
            //Start Monitor
            IpMonitor monitor=new IpMonitor();
            while(true)
            {
                Thread.Sleep(1024);
            }
        }
    }
}
