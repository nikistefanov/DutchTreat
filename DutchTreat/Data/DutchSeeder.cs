using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext ctx;
        private readonly IHostingEnvironment hosting;
        private readonly UserManager<StoreUser> userManager;

        public DutchSeeder(DutchContext ctx,
            IHostingEnvironment hosting,
            UserManager<StoreUser> userManager)
        {
            this.ctx = ctx;
            this.hosting = hosting;
            this.userManager = userManager;
        }

        public async Task Seed()
        {
            this.ctx.Database.EnsureCreated();

            var user = await this.userManager.FindByEmailAsync("nikistefanovbg89@gmail.com");

            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Nikolay",
                    LastName = "Stefanov",
                    UserName = "nikistefanovbg89@gmail.com",
                    Email = "nikistefanovbg89@gmail.com"
                };

                var result = await this.userManager.CreateAsync(user, "P@ssw0rd!");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("failed to create default user");
                }
            }

            if (!this.ctx.Products.Any())
            {
                // Need to create sample data
                var filepath = Path.Combine(this.hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

                this.ctx.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    User = user,
                    Items = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                this.ctx.Orders.Add(order);
                this.ctx.SaveChanges();
            }
        }
    }
}
