using DeliveryServiceFiltersOrders.Model;

namespace DeliveryServiceFiltersOrders.Utils
{
    static class WriteToFile
    {
        public static void WriteResults(string path, List<Order> orders)
        {
            using (var writer = new StreamWriter(path))
            {
                foreach (var order in orders)
                {
                    writer.WriteLine(order.ToString());
                }
            }
        }
    }
}
