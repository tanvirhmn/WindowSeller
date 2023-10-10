using System;
using System.Text;
using RabbitMQ.Client;

namespace ConsoleProducer
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Producer Begin!");

            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = "factory-mq.intus.lt",
                Port = 5672,
                UserName = "stock-module",
                Password = "stockm123",
                VirtualHost = "/"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "TestDevQ", 
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            const string message = "Hello Rabbit!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "TestDevQ",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($" [x] Sent {message}");

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();


        }
       
    }
}