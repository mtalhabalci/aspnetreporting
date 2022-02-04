using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Rabbitmq.Models
{
    public class RabbitmqQueueModel
    {
        public string ChannelName { get; set; }
        public IDictionary<string, object> QueueObjectArguements { get; set; }
        public bool Durable { get; set; } = false;
        public bool Exclusive { get; set; } = false;
        public bool AutoDelete { get; set; } = false;
    }
}
