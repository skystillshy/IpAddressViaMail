using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace IpAddressViaMailCSharp.Network
{
    class GetIp
    {
        /// <summary>
        /// Get local ip address
        /// </summary>
        public static List<string> GetLocalIps()
        {
            //get local hostname
            string hostname = Dns.GetHostName(); 
            IPHostEntry localhost = Dns.GetHostEntry(hostname);

            List<string> ret=new List<string>();

            foreach(var i in localhost.AddressList)
            {
                if(i.IsIPv6LinkLocal && Config.CurrentConfig.BanLocalIp)
                    continue;
                //Add title for sort
                //Ipv4 address will always at front
                if(i.ToString().Contains(':'))
                    ret.Add("Ipv6:" + i.ToString());
                else
                    ret.Add("Ipv4:" + i.ToString());
            }

            ret.Sort();

            return ret;
        }
    }
}
