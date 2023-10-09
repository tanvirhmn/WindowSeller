using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsoleRabbitConsumerSignalRHub
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory { HostName = "http://factory-mq.intus.lt:8080/" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "inventory-scan",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] Received {message}");
            };
            channel.BasicConsume(queue: "inventory-scan",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}