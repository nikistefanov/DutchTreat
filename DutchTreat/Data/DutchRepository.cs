using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext ctx;
        private readonly ILogger<DutchRepository> logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public void AddEntity(object model)
        {
            this.ctx.Add(model);
        }

        public void AddOrder(Order newOrder)
        {
            foreach (var item in newOrder.Items)
            {
                item.Product = this.ctx.Products.Find(item.Product.Id);
            }

            AddEntity(newOrder);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return this.ctx.Orders
                        .Include(o => o.Items)
                        .ThenInclude(i => i.Product)
                        .ToList();
            }
            else
            {
                return this.ctx.Orders.ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return this.ctx.Orders
                        .Where(o => o.User.UserName == username)
                        .Include(o => o.Items)
                        .ThenInclude(i => i.Product)
                        .ToList();
            }
            else
            {
                return this.ctx.Orders
                        .Where(o => o.User.UserName == username)
                        .ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return this.ctx.Products
                            .OrderBy(p => p.Title)
                            .ToList();

            }
            catch (Exception ex)
            {
                this.logger.LogError($"Failed to get all products: {ex}");

                return null;
            }
        }

        public Order GetOrderById(string username, int id)
        {
            return this.ctx.Orders
                        .Include(o => o.Items)
                        .ThenInclude(i => i.Product)
                        .Where(o => o.Id == id && o.User.UserName == username)
                        .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductByCategory(string category)
        {
            try
            {
                return this.ctx.Products
                           .Where(p => p.Category == category)
                           .ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Failed to get product by category {category}: {ex}");

                return null;
            }
        }

        public bool SaveAll()
        {
            return this.ctx.SaveChanges() > 0;
        }
    }
}
