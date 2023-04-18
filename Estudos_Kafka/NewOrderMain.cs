using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace Estudos_Kafka
{
    public class NewOrderMain
    {
        public static void Main()
        {
            try
            {
                using var orderDispatcher = new KafkaDispatcher<Order>();
                using var emailDispatcher = new KafkaDispatcher<string>();

                for (int i = 0; i < 10; i++)
                {
                    var num = new Random().Next(1, 100);

                    var userId = Guid.NewGuid().ToString();
                    var orderId = Guid.NewGuid().ToString();
                    var amount = new decimal(num * 5000);

                    var order = new Order(userId, orderId, amount);
                    orderDispatcher.Send("ECOMMERCE_NEW_ORDER", userId, order);

                    var email = "Thank you for your order! We are processing your order!";
                    emailDispatcher.Send("ECOMMERCE_NEW_ORDER", userId, email);
                }
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Falha na entrega: {e.Error.Reason}");
            }
        }
    }
}
