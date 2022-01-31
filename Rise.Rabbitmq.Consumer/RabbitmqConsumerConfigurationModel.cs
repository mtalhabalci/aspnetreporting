using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Rabbitmq.Consumer
{
    public class RabbitmqConsumerConfigurationModel
    {
        public string ChannelName { get; internal set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
    }
}
