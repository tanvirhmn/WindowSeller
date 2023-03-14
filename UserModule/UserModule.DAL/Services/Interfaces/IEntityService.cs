using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UserModule.DAL.Services
{
    public interface IEntityService<T> where T : class
    {
        IEnumerable<T> GetAll();
        IQueryable<T> GetAllQueryable();
        T GetById(object id);
        void Create(T entity);
   
        void Update(T entity);
  
        void Delete(T entity);

    }
}
