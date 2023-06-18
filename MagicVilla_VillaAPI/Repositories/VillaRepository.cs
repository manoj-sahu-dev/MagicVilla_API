using System;
using System.Linq.Expressions;

using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Repositories
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDatabaseContext _database;

        public VillaRepository(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _database = applicationDatabaseContext;
        }

        public async Task CreateVillaAsync(Villa villa)
        {
            await _database.AddAsync(villa);
            await SaveAsync();
        }

        public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null)
        {
            IQueryable<Villa> villas = _database.villas;

            if (filter != null)
            {
                villas = villas.Where(filter);
            }
            return await villas.ToListAsync();
        }

        public async Task<Villa> GetAsync(Expression<Func<Villa, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Villa> villas = _database.villas;
            if (!tracked)
            {
                villas.AsNoTracking();
            }

            if (filter != null)
            {
                villas = villas.Where(filter);
            }
            return await villas.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Villa villa)
        {
            _database.villas.Update(villa);
            await SaveAsync();
        }
        public async Task RemoveAsync(Villa villa)
        {
            _database.villas.Remove(villa);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _database.SaveChangesAsync();
        }
    }
}

