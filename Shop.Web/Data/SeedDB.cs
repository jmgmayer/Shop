﻿namespace Shop.Web.Data
{
    using Microsoft.AspNetCore.Identity;
    using Shop.Web.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class SeedDB
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;
        private Random random;

        public SeedDB(DataContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;            
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userManager.FindByEmailAsync("juan.manuel.gutierrezm@pemex.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Juan Manuel",
                    LastName = "Gutierrez Mayer",
                    Email = "juan.manuel.gutierrezm@pemex.com",
                    UserName = "juan.manuel.gutierrezm@pemex.com",
                    PhoneNumber="38633"
                };

                var result = await this.userManager.CreateAsync(user, "P3m3x&2019");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!this.context.Products.Any())
            {
                this.AddProduct("iPhone", user);
                this.AddProduct("Magic Mouse", user);
                this.AddProduct("iWatch Series 4", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(1000),
                IsAvailabe = true,
                Stock = this.random.Next(100),
                User = user
            });
        }

    }
}
