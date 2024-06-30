using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ProductRepo
{
    public interface IProductRepo
    {
        IEnumerable<Product> GetProducts();
        bool Create(Product product);
        bool Update(Product product);
        bool Remove(int id);
        Product? FindById(int id);
        IEnumerable<Product> FindByFilter(ProductFilter filter);
    }
}
