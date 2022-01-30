using Newtonsoft.Json;
using RabbitMQ.Client;
using Rise.Rabbitmq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Rabbitmq
{
    public class RabbitmqPost : IRabbitmqPost
    {
        public string Post(RabbitmqQueueModel model)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Password="test", UserName = "test" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: model.ChannelName,
                                     durable: model.Durable,
                                     exclusive: model.Exclusive,
                                     autoDelete: model.AutoDelete,
                                     arguments: model.QueueObjectArguements);

                var stocData = JsonConvert.SerializeObject(model);
                var body = Encoding.UTF8.GetBytes(stocData);

                channel.BasicPublish(exchange: "",
                                     routingKey: model.ChannelName,
                                     basicProperties: null,
                                     body: body);
                return $"[x] Sent {model.ChannelName}";
            }
        }
    }
}
