using Confluent.Kafka;
using Models.App.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

using System;
using System.Threading;

/// <summary>
/// RABBITMQ CONSUMER:
/// </summary>

//var factory = new ConnectionFactory { 
//    HostName = "localhost",
//    UserName = "admin",
//    Password = "123456"
//};

//using var connection = await factory.CreateConnectionAsync();
//using var channel = await connection.CreateChannelAsync();

//await channel.QueueDeclareAsync(queue: "rabbit-messages-queue", durable: true, exclusive: false, autoDelete: false,
//    arguments: null);

//Console.WriteLine("Waiting for messages...");

//var consumer = new AsyncEventingBasicConsumer(channel);
//consumer.ReceivedAsync += async (sender, eventArgs) =>
//{
//    byte[] body = eventArgs.Body.ToArray();
//    string json = Encoding.UTF8.GetString(body);

//    MessageModel message = JsonSerializer.Deserialize<MessageModel>(json)!;

//    await Task.Delay(1000);
//    Console.WriteLine($"Title: {message.Title}, Description: {message.Description}");

//    await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
//};

//await channel.BasicConsumeAsync(queue: "rabbit-messages-queue", autoAck: false, consumer: consumer);

//Console.WriteLine(" Press [enter] to exit.");
//Console.ReadLine();



/// <summary>
/// KAFKA CONSUMER:
/// </summary>
class Program
{
    public static void Main(string[] args)
    {
        var conf = new ConsumerConfig
        {
            GroupId = "test-consumer-group",
            BootstrapServers = "localhost:9092",
            // Note: The AutoOffsetReset property determines the start offset in the event
            // there are not yet any committed offsets for the consumer group for the
            // topic/partitions of interest. By default, offsets are committed
            // automatically, so in this example, consumption will only start from the
            // earliest message in the topic 'my-topic' the first time you run the program.
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using (var c = new ConsumerBuilder<string, string>(conf).Build())
        {
            c.Subscribe("kafka_messages_queue");

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                // Prevent the process from terminating.
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    try
                    {
                        var cr = c.Consume(cts.Token);
                        Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'. ");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Ensure the consumer leaves the group cleanly and final offsets are committed.
                c.Close();
            }
        }
    }
}