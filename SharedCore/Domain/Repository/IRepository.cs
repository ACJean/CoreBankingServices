using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedOperations.Domain.Repository
{
    public interface IRepository<T, TKey> where T : class
    {

        IEnumerable<T> GetAll();
        T? GetById(TKey id);
        TKey Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
