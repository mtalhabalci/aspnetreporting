using System;

namespace Rise.Rabbitmq.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rabbitmqConsumer = new RabbitmqConsumer();
            rabbitmqConsumer.Consume(new RabbitmqConsumerConfigurationModel { ChannelName = "Report", Host = "localhost", Username="test", Password = "test" });
        }
    }
}
