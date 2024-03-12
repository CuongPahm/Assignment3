using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            var details = new List<OrderDetail>();
            try
            {
                using var context = new SalesDBContext();
                List<OrderDetail> list = context.OrderDetails
                    .Include(I => I.Order)
                    .Include(i => i.Product)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return details;
        }

        public OrderDetail GetOrderDetailByID(int orderId, int productId)
        {
            OrderDetail details = null;
            try
            {
                using var context = new SalesDBContext();
                OrderDetail? orderDetail = context.OrderDetails
                    .Include(I => I.Order)
                    .Include(i => i.Product)
                    .SingleOrDefault(m => m.OrderId == orderId && m.ProductId == productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return details;
        }
        public IEnumerable<OrderDetail> GetOrderDetailByOrderId(int orderId)
        {
            try
            {
                using var context = new SalesDBContext();
                return context.OrderDetails
                    .Include(I => I.Order)
                    .Include(i => i.Product)
                    .Where(d => orderId == d.OrderId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(OrderDetail orderDetail)
        {
            try
            {
                if (GetOrderDetailByID(orderDetail.OrderId, orderDetail.ProductId) != null)
                {
                    throw new Exception("OrderDetail has existed");
                }

                // Lấy giá của sản phẩm để cập nhật vào chi tiết đơn hàng
                orderDetail.UnitPrice = ProductDAO.Instance.GetProductById(orderDetail.ProductId)!.UnitPrice;
                using (var context = new SalesDBContext())
                {
                    context.OrderDetails.Add(orderDetail);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(OrderDetail orderDetail)
        {
            try
            {
                if (GetOrderDetailByID(orderDetail.OrderId, orderDetail.ProductId) == null)
                {
                    throw new Exception("OrderDetail does not existed");
                }
                orderDetail.UnitPrice = ProductDAO.Instance
                .GetProductById(orderDetail.ProductId)!
                .UnitPrice;
                using var context = new SalesDBContext();
                context.OrderDetails.Update(orderDetail);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int orderId, int productId)
        {
            try
            {
                OrderDetail detail = GetOrderDetailByID(orderId, productId);
                if (detail != null)
                {
                    using var context = new SalesDBContext();
                    context.OrderDetails.Remove(detail);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("OrderDetail does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public IEnumerable<OrderDetail> GetBetweenDays(DateTime from, DateTime to)
        {
            try
            {
                using var context = new SalesDBContext();
                return context.OrderDetails
                       .Include(d => d.Order)
                       .Include(d => d.Product)
                       .Where(d => from.Date.CompareTo(d.Order.OrderDate.Date) <= 0 && d.Order.OrderDate.Date.CompareTo(to.Date) <= 0)
                       .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
