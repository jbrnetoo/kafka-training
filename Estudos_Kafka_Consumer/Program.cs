using Confluent.Kafka;
using Streaming.Joao;
using System;
using System.Threading;

namespace Estudos_Kafka_Consumer
{
    public class Program
    {
        private const string _nomeTopico = "ECOMMERCE_NEW_ORDER";

        static void Main(string[] args)
        {
            ConsumirMensagemDeTopico();
        }

        public static void ConsumirMensagemDeTopico()
        {
            try
            {
                var config = new ConsumerConfig()
                {
                    BootstrapServers = "localhost:9092",
                    GroupId = "fraudDetectorService-group-0",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };

                using (var consumer = new ConsumerBuilder<string, Order>(config)
                    .SetValueDeserializer(new Deserializer<Order>())
                    .Build())
                {
                    consumer.Subscribe(_nomeTopico);

                    while (true)
                    {
                        var record = consumer.Consume(cts.Token);
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine($"Processing new order, checking for fraud");
                        Console.WriteLine($"{record.Message.Key}");
                        Console.WriteLine($"OrderId: {record.Message.Value.orderId}, UserId: {record.Message.Value.userId}, Amount: {record.Message.Value.amount}");
                        Console.WriteLine($"{record.Partition}");
                        Console.WriteLine($"{record.Offset}\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
