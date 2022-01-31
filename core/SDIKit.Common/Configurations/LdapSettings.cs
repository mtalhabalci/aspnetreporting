using System;
using System.Collections.Generic;
using System.Text;

namespace SDIKit.Common.Configurations
{
    public class LdapSettings
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int DefaultPort { get; set; }
        public int DefaultSSLPort { get; set; }
        public int LdapVersion { get; set; }
    }
}