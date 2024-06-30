using BusinessObject.Models;
using Repository.OrderRepo;
using Repository.QueryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Service
{
    public class OrderService : IOrderSerivce
    {
        private readonly IOrderRepo orderRepo = new OrderRepo();
        public OrderService()
        {
            
        }
        public bool Create(Order order)
        {
            return orderRepo.Create(order);
        }

        public IEnumerable<Order> FindAllByFilter(OrderFilter filter)
        {
            return orderRepo.FindAllByFilter(filter);
        }

        public IEnumerable<Order> FindById(int id)
        {
            return orderRepo.FindById(id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return orderRepo.GetOrders();
        }
    }
}
