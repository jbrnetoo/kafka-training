using Confluent.Kafka;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Estudos_Kafka
{
    public static class FraudDetectorService
    {
        private const string _nomeTopico = "LOJA_NOVO_PEDIDO";

        public static async Task ConsumirMensagemDeTopico()
        {
            try
            {
                var config = new ConsumerConfig()
                {
                    BootstrapServers = "localhost:9092",
                    GroupId = $"{typeof(FraudDetectorService).Name}-group-0",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };

                using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    consumer.Subscribe(_nomeTopico);

                    while (true)
                    {
                        var record = consumer.Consume(cts.Token);
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine($"Processing new order, checking for fraud");
                        Console.WriteLine($"{record.Message.Key}");
                        Console.WriteLine($"{record.Message.Value}");
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

        public static async Task PublicarMensagemEmTopico()
        {
            try
            {
                var config = new ProducerConfig()
                {
                    BootstrapServers = "localhost:9092"
                };

                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        var result = await producer.ProduceAsync(
                            _nomeTopico,
                            new Message<Null, string>
                            {
                                Value = $"Novo Pedido {i}"
                            });

                        Console.WriteLine(
                            $"Mensagem: Novo Pedido {i} | " +
                            $"Status: { result.Status }");
                    }
                }

                Console.WriteLine("Concluído o envio de mensagens");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exceção: {ex.GetType().FullName} | " +
                             $"Mensagem: {ex.Message}");
            }
        }
    }
}
