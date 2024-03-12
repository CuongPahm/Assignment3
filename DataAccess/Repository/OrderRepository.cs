using DataAccess.DAO;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<Order> GetAllOrder() => OrderDAO.Instance.GetOrderList();

        public Order GetOrderById(int id) => OrderDAO.Instance.GetOrderById(id);

        public IEnumerable<Order> GetOrderByMemberId(int id) => OrderDAO.Instance.GetOrderByMemberId(id);

        public void Insert(Order order) => OrderDAO.Instance.Add(order);

        public void Remove(int id) => OrderDAO.Instance.Remove(id);

        public void Update(Order order) => OrderDAO.Instance.Update(order);
    }
}
