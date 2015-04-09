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
        public string SmtpServer;
        [DataMember]
        public string SmtpUsername;
        [DataMember]
        public string SmtpPassword;
        [DataMember]
        public string[] ToMailAddresses;
        [DataMember]
        public string FromMailAddress;
        [DataMember]
        public bool BanLocalIp;
        [DataMember]
        public bool SendAtStartUp;
        [DataMember]
        public bool SendWhenChange;

        internal static void Inital()
        {
            try
            {
                if (!File.Exists("config.json"))
                {
                    Trace.WriteLine("No Config.Json File Found.");
                }

                CurrentConfig = Config.DeSerialize(
                    File.ReadAllText(
                        "config.json"
                    ));

                Trace.WriteLine("Config loaded:");
                Trace.WriteLine(Config.Serialize(CurrentConfig));

            }
            catch(Exception e)
            {
                Trace.WriteLine(e);
            }
        }
    }
}
