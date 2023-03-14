using UserModule.DAL.Models;
using UserModule.DAL.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace UserModule.DAL.Services
{
    public abstract class EntityService<T> : IEntityService<T> where T : class
    {
        protected UserModuleDdContext Context;
        protected DbSet<T> Set;

        protected EntityService(UserModuleDdContext context)
        {
            Context = context;
            Set = Context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Set.AsEnumerable();
        }

        public virtual IAsyncEnumerable<T> GetAllAsync()
        {
            return Set.AsAsyncEnumerable();
        }

        public virtual IQueryable<T> GetAllQueryable()
        {
            return Set.AsQueryable();
        }

        public virtual T GetById(object id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return Set.Find(id);
        }

        public virtual async Task<T?> GetAsyncById(object id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return await Set.FindAsync(id);
        }

        public virtual void Create(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            Set.Add(entity);
            Context.SaveChanges();
        }

        public virtual async Task CreateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            Set.Add(entity);
            await Context.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public virtual async Task UpdateAsync(T entity)        {
            if (entity == null) throw new ArgumentNullException("entity");
            Context.Entry(entity).State = EntityState.Modified;
            await  Context.SaveChangesAsync();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            Set.Remove(entity);
            Context.SaveChanges();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Set.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public void Delete(object id)
        {
            Delete(GetById(id));
        }
       
    }
}
