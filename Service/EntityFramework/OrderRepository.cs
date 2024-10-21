﻿using DeliveryServiceFiltersOrders.Model;

namespace DeliveryServiceFiltersOrders.Service.EntityFramework
{
    public class OrderRepository
    {
        private readonly DeliveryContext _context;

        public OrderRepository(DeliveryContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetFilteredOrders(string district, DateTime fromTime, DateTime toTime, int minOrdersInDistrict)
        {
            var districtOrderCount = _context.Orders
                .Count(o => o.District == district);

            if (districtOrderCount < minOrdersInDistrict)
                return Enumerable.Empty<Order>();

            return _context.Orders
                .Where(o => o.District == district && o.DeliveryTime >= fromTime && o.DeliveryTime <= toTime)
                .ToList();
        }

        public async Task ImportOrdersFromCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                var order = new Order
                {
                    OrderId = int.Parse(parts[0]),
                    Weight = double.Parse(parts[1]),
                    District = parts[2],
                    DeliveryTime = DateTime.Parse(parts[3])
                };

                if(order is not null)
                {
                    var result = await _context.Orders.FindAsync(order.OrderId);
                    if (result is null)
                    {
                        _context.Orders.Add(order);
                    }
                }               
               
            }

            _context.SaveChanges();
        }
    }
}