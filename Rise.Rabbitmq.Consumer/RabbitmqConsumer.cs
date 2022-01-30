using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rise.Application.Contracts.Managers.Report;
using Rise.Application.Managers;
using Rise.Domain;
using Rise.Rabbitmq.Consumer.Helpers;
using Rise.Rabbitmq.Models;
using SDIKit.Common;
using SDIKit.Common.Configurations;
using SDIKit.Common.Helpers;
using SDIKit.Data;
using SDIKit.Data.Interfaces;
using SDIKit.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rise.Rabbitmq.Consumer
{

    internal class RabbitmqConsumer
    {
        private readonly IReportManager _reportManager;
        private ServiceProvider Provider { get; }

        public RabbitmqConsumer()
        {
            var services = AddRequiredServices();
            Provider = services.BuildServiceProvider();
            _reportManager = Provider.GetService<IReportManager>();
        }
        public void Consume(RabbitmqConsumerConfigurationModel rabbitmqConsumerConfigurationModel)
        {
            var excelHelper = new ExcelHelper();
            var lipsum = new LipsumGeneratorHelper();
            var factory = new ConnectionFactory() { HostName = rabbitmqConsumerConfigurationModel.Host, UserName = rabbitmqConsumerConfigurationModel.Username, Password = rabbitmqConsumerConfigurationModel.Password };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                channel.QueueDeclare(queue: rabbitmqConsumerConfigurationModel.ChannelName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    object dictionaryTemp = new object();
                    var body = ea.Body.ToArray();
                    var data = Encoding.UTF8.GetString(body);
                    RabbitmqQueueModel stoc = JsonConvert.DeserializeObject<RabbitmqQueueModel>(data);
                    try
                    {
                        var sampleList = GetSample();
                        excelHelper.Export(sampleList, @$"C:\projects\poc\rabbitmqtelepati\excels\{lipsum.NextLoremIpsum(1)}.xlsx", "Report");
                        //rapor burada indirilecek.
                        Console.WriteLine(stoc.QueueObjectArguements.First().Value);
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception)
                    {
                    }
                };
                channel.BasicConsume(queue: rabbitmqConsumerConfigurationModel.ChannelName,
                                     autoAck: false,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }


        private IServiceCollection AddRequiredServices()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var services = new ServiceCollection();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRabbitmqPost, RabbitmqPost>();
            services.AddScoped<IReportManager, ReportManager>();
            var configuration = AppConfigurations.Get(Path.Combine(Directory.GetCurrentDirectory()), environmentName: envName);
            services.AddSDIKitSettings(configuration);
            var databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();

            services.AddSqlDb<RiseDbContext>(configuration, k => k.UseNpgsql(databaseSettings.ConnectionString, k => k.MigrationsHistoryTable("__MigrationsHistory", databaseSettings.DefaultScheme)));
            services.AddSDIKitWorkContext(configuration);
            return services;
        }
        public List<sample> GetSample()
        {
            return new List<sample>
            {
                new sample{ Email="mtalhabalci@gmail.com", Name="talha", Id=1 },
                new sample{ Email="ebrududak@gmail.com", Name="ebru", Id=2 },
                new sample{ Email="biz@gmail.com", Name="bize", Id=3 }
            };
        }
    }
    public class sample
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
