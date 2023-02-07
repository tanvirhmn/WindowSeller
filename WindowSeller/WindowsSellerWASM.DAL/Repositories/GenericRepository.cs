using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.Shared.Persistance;

namespace WindowsSellerWASM.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly WindowSellerDdContext _dbcontext;

        public GenericRepository(WindowSellerDdContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbcontext.AddAsync(entity);
            //await _dbcontext.SaveChangesAsync();    
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
            //await _dbcontext.SaveChangesAsync();

        }

        public async Task<bool> DoesExistAsync(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbcontext.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbcontext.Entry(entity).State = EntityState.Modified;
            //await _dbcontext.SaveChangesAsync();    
        }
    }
}
