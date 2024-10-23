namespace DeliveryServiceFiltersOrders.Service
{
    public static class LogService
    {
        private static string _logFilePath;

        public static void Initialize(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public static void Log(string message)
        {
            try
            {
                File.AppendAllText(_logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка записи в лог файл: " + ex.Message);
            }
        }
    }
}
