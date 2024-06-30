using BusinessObject.Models;
using Repository;
using Repository.ProductRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo productRepo = new ProductRepo();
        public ProductService() { }
        public bool Create(Product product)
        {
            return productRepo.Create(product);
        }

        public IEnumerable<Product> FindByFilter(ProductFilter filter)
        {
            return productRepo.FindByFilter(filter);
        }

        public Product? FindById(int id)
        {
            return productRepo.FindById(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return productRepo.GetProducts();
        }

        public bool Remove(int id)
        {
            return productRepo.Remove(id);
        }

        public bool Update(Product product)
        {
            return productRepo.Update(product);
        }
    }
}
