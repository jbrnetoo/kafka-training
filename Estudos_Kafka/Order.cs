namespace Estudos_Kafka
{
    public class Order
    {
        private readonly string userId, orderId;
        private readonly decimal amount;

        public Order(string userId, string orderId, decimal amount)
        {
            this.userId = userId;
            this.orderId = orderId;
            this.amount = amount;
        }
    }
}
