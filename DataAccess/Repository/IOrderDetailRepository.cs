using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetOrderDetailByOrderId(int orderId);
        OrderDetail GetOrderDetailById(int orderId, int productId);
        IEnumerable<OrderDetail> GetBetweenDays(DateTime from, DateTime to);
        void Insert(OrderDetail detail);
        void Update(OrderDetail detail);
        void Remove(int orderId, int productId);
    }
}
