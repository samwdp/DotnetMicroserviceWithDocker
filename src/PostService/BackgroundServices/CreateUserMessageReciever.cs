using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PostService.Entities;
using PostService.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PostService.BackgroundServices
{
    public class CreateUserMessageReciever : RabbitMQBackgroundTask
    {
        private IUserService _userService;

        public CreateUserMessageReciever(IUserService userService)
        {
            _userService = userService;
            InitialiseListner("user.postservice");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ch, ea) =>
            {
                var type = ea.RoutingKey;
                if (type == "user.add")
                {
                    var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                    Console.WriteLine(" [x] Received {0}", body);
                    var data = JObject.Parse(body);
                    _userService.CreateUserAsync(new User()
                    {
                        ID = data["id"].Value<int>(),
                        Name = data["name"].Value<string>()
                    });
                }
            };
            _channel.BasicConsume(queue: "user.postservice",
                                                 autoAck: true,
                                                 consumer: consumer);
            return Task.CompletedTask;
        }
    }
}
