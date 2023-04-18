using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using System;

namespace Estudos_Kafka
{
    public class KafkaDispatcher<T> : IDisposable
    {
        private readonly IProducer<string, T> _producerBuilder;

        public KafkaDispatcher()
        {
            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = "localhost:8081",
                RequestTimeoutMs = 5000,
                MaxCachedSchemas = 10
            };

            using var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);

            _producerBuilder = new ProducerBuilder<string, T>(Properties())
                //.SetValueSerializer(new AvroSerializer<T>(schemaRegistry))
                .Build();
        }

        public void Send(string topic, string key, T value)
        {
            _producerBuilder.Produce(topic, new Message<string, T> { Key = key, Value = value }, (deliveryReport) =>
            {
                if (deliveryReport.Error.Code != ErrorCode.NoError)
                {
                    Console.WriteLine($"Falha na entrega: {deliveryReport.Error.Reason}");
                }
                else
                {
                    Console.WriteLine($"Entregou a mensagem com key '{deliveryReport.Message.Key}', value '{deliveryReport.Message.Value}' na partição '{deliveryReport.Partition}' e offset '{deliveryReport.Offset}'");
                }
            });
        }

        private static ProducerConfig Properties() => new()
        {
            BootstrapServers = "localhost:9092"
        };

        public void Dispose()
        {
            _producerBuilder.Dispose();
        }
    }
}
