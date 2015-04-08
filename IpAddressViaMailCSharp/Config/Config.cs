using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using IpAddressViaMailCSharp.SerializeBase;

namespace IpAddressViaMailCSharp
{
    [DataContract]
    class Config: SerializeBase<Config>
    {
        public static Config CurrentConfig;
        [DataMember]
        public string SmtpServer { get; private set; }
        [DataMember]
        public string SmtpUsername { get; private set; }
        [DataMember]
        public string SmtpPassword { get; private set; }
        [DataMember]
        public string ToMailAddress { get; private set; }
        [DataMember]
        public string FromMailAddress { get; private set; }
        [DataMember]
        public bool BanLocalIp { get; private set; }

        internal static void Inital()
        {
            try
            {
                CurrentConfig = Config.DeSerialize(
                    File.ReadAllText(
                        "config.json"
                    ));
            }
            catch(Exception e)
            {
                Console.WriteLine("[{0}] {1}", e.Message, e.StackTrace);
                Trace.WriteLine(e);
            }
        }
    }
}
