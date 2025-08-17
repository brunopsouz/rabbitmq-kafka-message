using Confluent.Kafka;
using Models.App.Entities;
using Repositories.App.Interfaces;
using System.Text.Json;

namespace Repositories.App
{
    public class KafkaMessageRepository : IMessageRepository
    {
        public async void SendMessage(MessageModel message)
        {
            var config = new ProducerConfig { 
                BootstrapServers = "localhost:9092",
            };
            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                try
                {
                    string json = JsonSerializer.Serialize(message);

                    var result = await producer.ProduceAsync(
                    "kafka_messages_queue",
                    new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = json.ToString() });

                    Console.WriteLine($"Enviado para {result.TopicPartitionOffset}");

                }
                catch (ProduceException<Null, string> e) 
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }

            }

        }
    }
}
