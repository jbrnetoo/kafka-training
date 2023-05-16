using Confluent.Kafka;
using Streaming.Joao;
using System;
using System.Threading.Tasks;

namespace Estudos_Kafka_Producer
{
    public class NewOrderMain
    {
        public static async Task Main()
        {
            try
            {
                var orderDispatcher = new KafkaDispatcher<Order>();
                var emailDispatcher = new KafkaDispatcher<string>();

                for (int i = 0; i < 10; i++)
                {
                    var num = new Random().Next(1, 100);

                    var userId = Guid.NewGuid().ToString();
                    var orderId = Guid.NewGuid().ToString();
                    var amount = Convert.ToDouble(num * 5000);

                    var order = new Order()
                    {
                        userId = userId,
                        orderId = orderId,
                        amount = amount,
                    };

                    await orderDispatcher.Send("ECOMMERCE_NEW_ORDER", userId, order);

                    var email = "Thank you for your order! We are processing your order!";

                    await emailDispatcher.Send("ECOMMERCE_SEND_EMAIL", userId, email);
                }
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Falha na entrega: {e.Error.Reason}");
            }
        }
    }
}
