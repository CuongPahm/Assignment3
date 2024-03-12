using DataAccess.DAO;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> GetAllProduct() => ProductDAO.Instance.GetProductList();

        public Product GetProductById(int id) => ProductDAO.Instance.GetProductById(id);

        public void Insert(Product product) => ProductDAO.Instance.Add(product);

        public void Remove(int id) => ProductDAO.Instance.Remove(id);

        public void Update(Product product) => ProductDAO.Instance.Update(product);
    }
}
