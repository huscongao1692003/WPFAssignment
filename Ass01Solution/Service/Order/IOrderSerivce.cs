using BusinessObject.Models;
using Repository.QueryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Service
{
    public interface IOrderSerivce
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> FindAllByFilter(OrderFilter filter);
        bool Create(Order order);
        IEnumerable<Order> FindById(int id);
    }
}
