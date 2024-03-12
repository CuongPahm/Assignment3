using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrder();
        Order GetOrderById(int id);
        IEnumerable<Order> GetOrderByMemberId(int id);
        void Insert(Order order);
        void Update(Order order);
        void Remove(int id);
    }
}
