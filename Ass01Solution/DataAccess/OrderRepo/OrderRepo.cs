using BusinessObject;
using BusinessObject.Models;
using Repository.QueryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.OrderRepo
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ShoppingCartContext _context = new ShoppingCartContext();
        public OrderRepo()
        {

        }

        public bool Create(Order order)
        {
            _context.Orders.Add(order);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Order> FindAllByFilter(OrderFilter filter)
        {
            var query = _context.Orders.AsQueryable();

            if (filter.StartDate != null)
            {
                query = query.Where(x => x.OrderDate >= filter.StartDate);
            }

            if (filter.EndDate != null)
            {
                query = query.Where(x => x.OrderDate <= filter.EndDate);
            }

            if (filter.StartDate != null && filter.EndDate != null)
            {
                query = query.Where(x => x.OrderDate >= filter.StartDate && x.OrderDate <= filter.EndDate);
            }

            return query.ToList();
        }

        public IEnumerable<Order> FindById(int id)
        {
            return _context.Orders.Where(order => order.MemberId == id).ToList();
        }

        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }
    }
}
