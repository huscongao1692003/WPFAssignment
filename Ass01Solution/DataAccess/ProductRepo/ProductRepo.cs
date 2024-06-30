using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.OrderDetailRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ProductRepo
{

    public class ProductRepo : IProductRepo
    {
        private readonly ShoppingCartContext _context = new ShoppingCartContext();

        public ProductRepo() {}

        public bool Create(Product product)
        {
            if(_context.Products.Any(x => x.ProductName == product.ProductName))
            {
                return false;
            }
            _context.Products.Add(product);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Product> FindByFilter(ProductFilter filter)
        {
            var query = _context.Products.AsQueryable();

            if(filter.ProductId != null)
            {
                query = query.Where(x => x.ProductId == filter.ProductId);
            }

            if (!string.IsNullOrEmpty(filter.ProductName))
            {
                query = query.Where(x => x.ProductName.Contains(filter.ProductName));
            }

            if(filter.UnitPrice != null)
            {
                query = query.Where(x => x.UnitPrice <= filter.UnitPrice);
            }

            if(filter.UnitsInStock != null)
            {
                query = query.Where(x => x.UnitsInStock <= filter.UnitsInStock);
            }

            return query.ToList();
        }

        public Product? FindById(int id)
        {
            return _context.Products.AsNoTracking().SingleOrDefault(x => x.ProductId == id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.AsNoTracking().ToList();
        }

        public bool Remove(int id)
        {
            var product = _context.Products.FirstOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return false;
            }

            var orderDetails = _context.OrderDetails.Any(x => x.ProductId == id);

            if (orderDetails)
            {
                return false;
            }

            _context.Products.Remove(product);
            return _context.SaveChanges() > 0;
        }

        public bool Update(Product product)
        {
            var existingProduct = _context.Products.FirstOrDefault(m => m.ProductId == product.ProductId);

            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.ProductName = product.ProductName;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.UnitsInStock = product.UnitsInStock;
            existingProduct.Weight = product.Weight;

            _context.Products.Update(existingProduct);
            return _context.SaveChanges() > 0;
        }
    }
}
