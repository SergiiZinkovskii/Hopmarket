﻿using System.Linq;
using System.Threading.Tasks;
using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Repositories
{
    public class ProfileRepository : IBaseRepository<Profile>
    {
        private readonly ApplicationDbContext _dbContext;

        public ProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Profile entity)
        {
            await _dbContext.Profiles.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Profile> GetAll()
        {
            return _dbContext.Profiles;
        }

        public async Task Delete(Profile entity)
        {
            _dbContext.Profiles.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Profile> Update(Profile entity)
        {
            _dbContext.Profiles.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Profile> Find(long id, CancellationToken cancellationToken)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}