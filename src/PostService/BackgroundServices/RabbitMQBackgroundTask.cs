using System;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace PostService.BackgroundServices
{
    public abstract class RabbitMQBackgroundTask : BackgroundService
    {
        internal IConnection _connection;
        internal IModel _channel;

        public void InitialiseListner(string queueName)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = "rabbitmq",
                    UserName = "user",
                    Password = "password",
                    Port = 5672
                };

                _connection = factory.CreateConnection();
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            }
            catch
            {
                Console.WriteLine("[x] Client Unreachable");
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
