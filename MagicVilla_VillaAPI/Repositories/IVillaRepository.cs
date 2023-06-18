using System;
using System.Linq.Expressions;

using MagicVilla_VillaAPI.Models;

namespace MagicVilla_VillaAPI.Repositories
{
    public interface IVillaRepository
    {
        Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null);
        Task<Villa> GetAsync(Expression<Func<Villa, bool>> filter = null, bool tracked = true);
        Task CreateVillaAsync(Villa villa);
        Task UpdateAsync(Villa villa);
        Task RemoveAsync(Villa villa);
        Task SaveAsync();
    }
}

