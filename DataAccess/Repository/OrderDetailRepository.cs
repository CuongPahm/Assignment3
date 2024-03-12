using DataAccess.DAO;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public IEnumerable<OrderDetail> GetBetweenDays(DateTime from, DateTime to) => OrderDetailDAO.Instance.GetBetweenDays(from, to);

        public OrderDetail GetOrderDetailById(int orderId, int productId) => OrderDetailDAO.Instance.GetOrderDetailByID(orderId, productId);

        public IEnumerable<OrderDetail> GetOrderDetailByOrderId(int orderId) => OrderDetailDAO.Instance.GetOrderDetailByOrderId(orderId);

        public void Insert(OrderDetail detail) => OrderDetailDAO.Instance.Add(detail);

        public void Remove(int orderId, int productId) => OrderDetailDAO.Instance.Remove(orderId, productId);

        public void Update(OrderDetail detail) => OrderDetailDAO.Instance.Update(detail);
    }
}
