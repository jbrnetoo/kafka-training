using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Estudos_Kafka_Producer
{
    public class KafkaDispatcher<T>
    {
        public async Task Send(string topic, string key, T value)
        {
            try
            {
                using var schemaRegistry = new CachedSchemaRegistryClient(GetSchemeRegistryConfig());

                using var producer = new ProducerBuilder<string, T>(Properties())
                  .SetKeySerializer(new AvroSerializer<string>(schemaRegistry))
                  .SetValueSerializer(new AvroSerializer<T>(schemaRegistry))
                  .Build();

                var headers = new Headers { new Header("x-app", Encoding.ASCII.GetBytes("chatbot")) };

                var deliveryReport = await producer.ProduceAsync(topic, new Message<string, T> { Key = key, Value = value, Headers = headers });

                Console.WriteLine($"Entregou um objeto '{deliveryReport.Message.Value}' com a key '{deliveryReport.Message.Key}' em '{deliveryReport.TopicPartition} e offset '{deliveryReport.Offset}'");
            }
            catch (ProduceException<string, string> e)
            {
                Console.WriteLine($"Falha na entrega: {e.Error.Reason}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Falha na entrega: {ex.Message}");
            }
        }

        private static ProducerConfig Properties() => new()
        {
            BootstrapServers = "localhost:9092"
        };

        private static SchemaRegistryConfig GetSchemeRegistryConfig() => new()
        {
            Url = "localhost:8081",
            RequestTimeoutMs = 5000,
            MaxCachedSchemas = 10,
            BasicAuthUserInfo = "",
            BasicAuthCredentialsSource = AuthCredentialsSource.UserInfo
        };
    }
}
