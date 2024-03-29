﻿using _3lab_komanda32.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace _3lab_komanda32.Repositories
{
    public class ProductRepository
    {
        private readonly ApiDbContext dbContext;

        public ProductRepository(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Basic tasks

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetById(long id)
        {
            return await dbContext.Products.FirstOrDefaultAsync(e => e.Id == id);
        }

        public IEnumerable<Product> GetByName(string name)
        {
            return  dbContext.Products.Where(el => el.Name == name);
        }

        public IEnumerable<Product> GetByCategory(string category)
        {
            return dbContext.Products.Where(el => el.Category == category);
        }

        public async Task<Product> Create(Product product)
        {
            var result = await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Product?> Update(Product product)
        {
            var toChange = await dbContext.Products.FirstOrDefaultAsync(e => e.Id == product.Id);

            if (toChange == null)
                return null;

            dbContext.Entry<Product>(toChange).CurrentValues.SetValues(product);
            await dbContext.SaveChangesAsync();
            return toChange;
        }

        public async Task<Product?> RemoveById(long id)
        {
            var obj = await dbContext.Products.FirstOrDefaultAsync(el => el.Id == id);

            if (obj == null)
                return null;

            dbContext.Products.Remove(obj);
            await dbContext.SaveChangesAsync();
            return obj;
        }

        public async Task<Product> UpdateDiscount(ProductDiscount discount, Product product)
        {
            product.Discount = discount.Discount;
            product.DiscountStart = discount.DiscountStart;
            product.DiscountEnd = discount.DiscountEnd;

            dbContext.Entry(product).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return product;

        }

        public async Task<Product?> AddProductToOrder(long id, Product product)
        {
            var order = await dbContext.Orders.FirstOrDefaultAsync(el => el.Id == id);

            if (order == null) return null;

            order.Products.Add(product);

            dbContext.Entry(order).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Order?> RemoveProductFromOrder(long id, Product product)
        {
            var order = await dbContext.Orders.FirstOrDefaultAsync(el => el.Id == id);

            if (order == null)
                return null;

            var prod = order.Products.FirstOrDefault(el => el.Id == product.Id);

            if (prod == null)
                return null;

            order.Products.Remove(prod);
            await dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order?> UpdateOrderProduct(long id, Product product)
        {
            var order = await dbContext.Orders.FirstOrDefaultAsync(el => el.Id == id);

            if (order == null)
                return null;

            var prod = order.Products.FirstOrDefault(el => el.Id == product.Id);

            if (prod == null)
                return null;

            order.Products.Remove(prod);
            order.Products.Add(product);

            dbContext.Entry(order).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return order;
        }
    }
}
