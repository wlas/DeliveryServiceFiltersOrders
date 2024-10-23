using DeliveryServiceFiltersOrders;
using DeliveryServiceFiltersOrders.Service;
using DeliveryServiceFiltersOrders.Service.EntityFramework;
using DeliveryServiceFiltersOrders.Utils;

string logFileOutput = @"Logs\log_output.log";

try
{
    // Инициализация логирования
    LogService.Initialize(logFileOutput);
    LogService.Log("Запуск программы.");

    using var context = new DeliveryContext();

    var orderService = new OrderRepository(context);
    await orderService.ImportOrdersFromCsvAsync(@"Resources/orders.csv");

    Console.WriteLine("Введите район для фильтрации:");
    var district = Console.ReadLine();
    if (string.IsNullOrEmpty(district))
    {
        message("Ошибка, не введен район.");
        return;
    }

    Console.WriteLine("Введите начальное время (yyyy-MM-dd HH:mm:ss):");
    DateTime fromTime;
    if (!DateTime.TryParse(Console.ReadLine(), out fromTime))
    {
        message("Ошибка, неверный формат даты. Валидный формат: yyyy-MM-dd HH:mm:ss");
        return;
    }


    Console.WriteLine("Введите конечное время (yyyy-MM-dd HH:mm:ss):");
    DateTime toTime;
    if (!DateTime.TryParse(Console.ReadLine(), out toTime))
    {
        message("Ошибка, неверный формат даты. Валидный формат: yyyy-MM-dd HH:mm:ss");
        return;
    }

    Console.WriteLine("Введите минимальное количество заказов в районе:");
    var minOrdersInDistrict = int.Parse(Console.ReadLine());


    var filteredOrders = await orderService.GetFilteredOrdersAsync(district.Trim(), fromTime, toTime, minOrdersInDistrict);

    Console.WriteLine("Отфильтрованные заказы:");

    if (filteredOrders.Any())
    {
        try
        {
            var nameFile = @"Resources\" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".txt";
            WriteToFile.WriteResults(nameFile, filteredOrders.ToList());
            message($"Результаты фильтрации успешно записаны в файл. {Path.Combine(Environment.CurrentDirectory, nameFile)}");
        }
        catch (Exception ex)
        {
            message($"Ошибка: {ex.Message}");
        }

        LogService.Log($"Найдены следующие заказы по фильтру: Район - {district}, Начальное время - {fromTime}, Конечное время - {toTime}");

        foreach (var order in filteredOrders)
        {
            LogService.Log(order.ToString());
            Console.WriteLine(order.ToString());
        }
    }
    else
    {
        LogService.Log($"Заказы отсутствуют по данному фильтру. Район: {district}, Начальное время: {fromTime}, Конечное время: {toTime}");
        Console.WriteLine("Заказы отсутствуют по данному фильтру.");
    }
}
catch (Exception ex)
{
    message($"Ошибка: {ex.Message}");
}
finally
{
    LogService.Log("Завершение работы программы.");
}

void message(string msg)
{
    LogService.Log(msg);
    Console.WriteLine(msg);
    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
    Console.ReadKey();
    
    
}