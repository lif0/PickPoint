using System;
using System.Collections.Generic;
using System.Linq;
using Shop.DataLayer.Models;
using Shop.DataLayer.Repositories.Abstracts;

namespace Shop.DataLayer.Repositories
{
    public class OrderListRepository : IOrderRepository
    {
        private static readonly Lazy<OrderListRepository> LazyRepo =
            new Lazy<OrderListRepository>(() => new OrderListRepository());
        
        private readonly List<Order> _orders = new List<Order>();
        
        private readonly object _obj = new object();
        
        private OrderListRepository() { }
        
        public static OrderListRepository Instance => LazyRepo.Value;

        public Order Create(Order order)
        {
            lock (_obj)
            {
                order.Id = _orders.Count;
                _orders.Add(order);
            }

            return order;
        }

        public Order Update(Order order)
        {
            var orderForUpdate = GetById(order.Id);
            
            orderForUpdate.State = order.State;
            orderForUpdate.Cost = order.Cost;
            orderForUpdate.Products = order.Products;
            orderForUpdate.PostamatId = order.PostamatId;
            orderForUpdate.RecipientFullName = order.RecipientFullName;
            orderForUpdate.PhoneNumber = order.PhoneNumber;
            
            return orderForUpdate;
        }

        public Order FindById(int id) => _orders.SingleOrDefault(o => o.Id == id);
        
        public Order GetById(int id) => _orders.Single(o => o.Id == id);
    }
}