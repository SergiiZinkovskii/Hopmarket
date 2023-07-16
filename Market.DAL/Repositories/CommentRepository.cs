using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task Create(Comment entity)
        {
            await _db.Comments.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Comment entity)
        {
            _db.Comments.Remove(entity);
            await _db.SaveChangesAsync();

        }

        public async Task<List<Comment>> FindAsync(long productId)
        {
            return await _db.Comments
       .Where(c => c.ProductId == productId)
       .ToListAsync();
        }


        public IQueryable<Comment> GetAll()
        {
            return _db.Comments;
        }

        public async Task<Comment> Update(Comment entity)
        {
            _db.Comments.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
