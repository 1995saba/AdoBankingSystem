using AdoBankingSystem.BLL.Interfaces;
using AdoBankingSystem.DAL.DAOs;
using AdoBankingSystem.Shared.DTOs;
using AdoBankingSystem.Shared.Utils;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.BLL.Services
{
    public class RabbitMqBusService  : IDataPublisher
    {
        private void PublishMessageToQueue<T>(string queueName, T message) where T :class
        {
            using (IConnection connection = ConnectionManagerUtil.GetConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: properties,
                                 body: body);
            }
        }

        public void PublishMessageToStorage<T>(T data) where T : EntityDto
        {
            PublishMessageToQueue<T>(typeof(T).ToString(), data);
        }

        public RabbitMqBusService()
        {
        }
    }
}
