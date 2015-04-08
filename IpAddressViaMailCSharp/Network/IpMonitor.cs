using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace IpAddressViaMailCSharp.Network
{
    class IpMonitor
    {
        Timer monitor=new Timer();
        List<string> LastIpAddress=new List<string>();

        /// <summary>
        /// 将LastIpAddress返回成邮件中的正文
        /// </summary>
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
            if(!GetIp.GetLocalIps().SequenceEqual(LastIpAddress))
            {
                if (Config.CurrentConfig.SendWhenChange)
                {
                    SendIpAddressViaSMTP("IP address changed");
                }
            }
        }
    }
}
