namespace Estudos_Kafka_Producer
{
    public class OrderModel
    {
        private readonly string userId, orderId;
        private readonly double amount;

        public OrderModel(string userId, string orderId, double amount)
        {
            this.userId = userId;
            this.orderId = orderId;
            this.amount = amount;
        }
    }
}
