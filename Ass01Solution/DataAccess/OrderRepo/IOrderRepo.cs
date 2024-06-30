using BusinessObject.Models;
using Repository.QueryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.OrderRepo
{
    public interface IOrderRepo
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> FindAllByFilter(OrderFilter filter);
        bool Create(Order order);
        IEnumerable<Order> FindById(int id);
    }
}
