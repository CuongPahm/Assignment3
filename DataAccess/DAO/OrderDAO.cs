using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Order> GetOrderList()
        {
            var ord = new List<Order>();
            try
            {
                using var context = new SalesDBContext();
                ord = context.Orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ord;
        }

        public Order GetOrderById(int id)
        {
            Order order = null;
            try
            {
                using var context = new SalesDBContext();
                order = context.Orders
                    .Include(m => m.Member)
                    .SingleOrDefault(o => o.OrderId == id);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public void Add(Order order)
        {
            try
            {
                Order ord = GetOrderById(order.OrderId);
                if (ord == null)
                {
                    using var context = new SalesDBContext();
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The order is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Order order)
        {
            try
            {
                Order ord = GetOrderById(order.OrderId);
                if (ord != null)
                {
                    using var context = new SalesDBContext();
                    context.Orders.Update(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The order does not already exist.");
                }
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
                Order ord = GetOrderById(id);
                if (ord != null)
                {
                    using var context = new SalesDBContext();
                    context.Orders.Remove(ord);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The order does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Order> GetOrderByMemberId(int id)
        {
            var order = new List<Order>();
            try
            {
                using var context = new SalesDBContext();
                order = context.Orders
                    .Include(i => i.Member)
                    .Where(m => m.MemberId == id)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
           
        }
    }
}
