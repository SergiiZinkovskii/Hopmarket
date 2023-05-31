﻿using System.Linq;
using System.Threading.Tasks;
using Market.DAL.Interfaces;
using Market.Domain.Entity;

namespace Market.DAL.Repositories
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Product entity)
        {
            await _db.Products.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Product> GetAll()
        {
            return _db.Products;
        }

        public async Task Delete(Product entity)
        {
            _db.Products.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Product> Update(Product entity)
        {
            _db.Products.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}