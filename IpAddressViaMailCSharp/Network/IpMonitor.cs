using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace IpAddressViaMailCSharp.Network
{
    /// <summary>
    /// Ip Monitor Class
    /// 
    /// Scan ip every 60 seconds and send email if necessary.
    /// </summary>
    class IpMonitor
    {
        Timer monitor=new Timer();
        List<string> LastIpAddress=new List<string>();

        /// <summary>
        /// Format ip address for email.
        /// </summary>
        /// <param name="addtional_info"></param>
        /// <returns></returns>
        string IpAddressToString(string addtional_info)
        {
            string ret=addtional_info + "\n";

            foreach(var i in LastIpAddress)
                ret += i + "\n";

            return ret;
        }


        void SendIpAddressViaSMTP(string addtional_info)
        {
            MailServer.SendMail(IpAddressToString(addtional_info),
                "[IAVM]" + addtional_info);
        }

        public IpMonitor()
        {
            LastIpAddress = GetIp.GetLocalIps();
            if (Config.CurrentConfig.SendAtStartUp)
                SendIpAddressViaSMTP("Program Initialed");

            //监视时间
            monitor = new Timer(60000);
            monitor.Elapsed += monitor_Elapsed;
            monitor.Start();
        }

        private void monitor_Elapsed(object sender, ElapsedEventArgs e)
        {
            var NewIpAddress=GetIp.GetLocalIps();
            if(!NewIpAddress.SequenceEqual(LastIpAddress))
            {
                LastIpAddress = NewIpAddress;
                if (Config.CurrentConfig.SendWhenChange)
                {
                    SendIpAddressViaSMTP("IP address changed");
                }
            }
        }
    }
}
