using Rise.Rabbitmq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Rabbitmq
{
    public interface IRabbitmqPost
    {
        string Post(RabbitmqQueueModel model);
    }
}
