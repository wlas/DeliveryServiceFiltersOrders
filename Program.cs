using DeliveryServiceFiltersOrders;
using DeliveryServiceFiltersOrders.Service.EntityFramework;

using var context = new DeliveryContext();

var orderService = new OrderRepository(context);
await orderService.ImportOrdersFromCsvAsync(@"Resources/orders.csv");

Console.WriteLine("Введите район для фильтрации:");
var district = Console.ReadLine();

Console.WriteLine("Введите начальное время (yyyy-MM-dd HH:mm:ss):");
var fromTime = DateTime.Parse(Console.ReadLine());

Console.WriteLine("Введите конечное время (yyyy-MM-dd HH:mm:ss):");
var toTime = DateTime.Parse(Console.ReadLine());

Console.WriteLine("Введите минимальное количество заказов в районе:");
var minOrdersInDistrict = int.Parse(Console.ReadLine());

var filteredOrders = await orderService.GetFilteredOrdersAsync(district.Trim(), fromTime, toTime, minOrdersInDistrict);

Console.WriteLine("Отфильтрованные заказы:");

if (filteredOrders.Any())
{ 
    foreach (var order in filteredOrders)
    {
        Console.WriteLine(order.ToString());
    }
}
else
{
    Console.WriteLine("Заказы отсутствуют по данному фильтру.");
}

