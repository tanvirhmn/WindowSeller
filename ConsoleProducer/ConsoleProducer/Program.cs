using System;
using System.Text;
using RabbitMQ.Client;

namespace ConsoleProducer
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            ConnectionFactory factory = new ConnectionFactory { HostName = "http://factory-mq.intus.lt:8080/" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "inventory-scan", durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            const string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($" [x] Sent {message}");

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();


        }
       
    }
}