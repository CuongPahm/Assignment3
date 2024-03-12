using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Product> GetProductList()
        {
            var products = new List<Product>();
            try
            {
                using var context = new SalesDBContext();
                products = context.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = null;
            try
            {
                using var context = new SalesDBContext();
                product = context.Products.SingleOrDefault(m => m.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public void Add(Product product)
        {
            try
            {
                if (GetProductById(product.ProductId) != null)
                {
                    throw new Exception("Product has existed");
                }
                using var context = new SalesDBContext();
                context.Products.Add(product);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Product product)
        {
            try
            {
                if (GetProductById(product.ProductId) == null)
                {
                    throw new Exception("Product does not exist");
                }
                using var context = new SalesDBContext();
                context.Products.Update(product);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void Remove(int id)
        {
            try
            {
                Product product = GetProductById(id);
                if (product == null)
                {
                    throw new Exception("Product does not exist");
                }
                using var context = new SalesDBContext();
                context.Products.Remove(product);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
