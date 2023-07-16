﻿using System.Linq;
using System.Threading.Tasks;
using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly ApplicationDbContext _db;

        public PhotoRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Photo entity)
        {
            await _db.Photos.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Photo> GetAll()
        {
            return _db.Photos;
        }

        public async Task Delete(Photo entity)
        {
            _db.Photos.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Photo> Update(Photo entity)
        {
            _db.Photos.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        public IQueryable<Photo> GetAllPhotos()
        {
            return _db.Photos;
        }

        public async Task<Photo> Find(long id, CancellationToken cancellationToken)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
