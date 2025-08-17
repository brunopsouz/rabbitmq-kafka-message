using Models.App.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Repositories.App.Interfaces;
using System.Text;
using System.Text.Json;

namespace Repositories.App
{
    public class RabbitMessageRepository : IMessageRepository
    {
        public async void SendMessage(MessageModel message)
        {
            var factory = new ConnectionFactory { HostName = "localhost", UserName = "admin", Password = "123456" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "rabbit-messages-queue", durable: true, exclusive: false, autoDelete: false,
                arguments: null);

            var msg = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(msg);

            await channel.BasicPublishAsync(
                exchange: string.Empty, 
                routingKey: "rabbit-messages-queue",
                mandatory: true,
                basicProperties: new BasicProperties { Persistent = true },
                body: body);
            
            
            Console.WriteLine($"Sent - {message}");

            await Task.Delay(3000);
        }
    }
}
