namespace DeliveryServiceFiltersOrders.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public double Weight { get; set; }
        public string District { get; set; }
        public DateTime DeliveryTime { get; set; }

        public override string? ToString() => $"Номер заказа: {OrderId}, Вес заказа в килограммах: {Weight}, Время доставки заказа: {DeliveryTime}";

    }
}
