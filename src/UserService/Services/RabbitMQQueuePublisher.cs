using System;
using System.Text;
using RabbitMQ.Client;

namespace UserService.Services
{
    public class RabbitMQQueuePublisher : IQueuePublisher
    {
        public void PublishMessage(string integrationEvent, string eventData)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                UserName = "user",
                Password = "password",
                Port = 5672
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "user.postservice", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(eventData);
            Console.WriteLine($"[x] sending {body}");
            channel.BasicPublish(exchange: "user",
                                             routingKey: integrationEvent,
                                             basicProperties: null,
                                             body: body);
        }
    }
}
