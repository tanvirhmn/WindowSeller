﻿using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ServiceBusClient client;

            // the sender used to publish messages to the topic
            ServiceBusSender sender;

            // number of messages to be sent to the topic
            const int numOfMessages = 3;
            // The Service Bus client types are safe to cache and use as a singleton for the lifetime
            // of the application, which is best practice when messages are being published or read
            // regularly.

            //TODO: Replace the "<NAMESPACE-NAME>" and "<TOPIC-NAME>" placeholders.
            client = new ServiceBusClient(
                "Endpoint=sb://intusproduction.servicebus.windows.net/;SharedAccessKeyName=ScannerAppAccessKey;SharedAccessKey=q1OY1oFlik5dYkrk+Jj0qScFoCu1GpOf3+ASbOWH/Cc=");

            sender = client.CreateSender("testtanvirtopic");
            // create a batch 
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            for (int i = 1; i <= numOfMessages; i++)
            {
                // try adding a message to the batch
                if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
                {
                    // if it is too large for the batch
                    throw new Exception($"The message {i} is too large to fit in the batch.");
                }
            }

            try
            {
                // Use the producer client to send the batch of messages to the Service Bus topic
                await sender.SendMessagesAsync(messageBatch);
                Console.WriteLine($"A batch of {numOfMessages} messages has been published to the topic.");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }

            Console.WriteLine("Press any key to end the application");
            Console.ReadKey();
        }
    }
}