using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedOperations.Domain
{
    public interface IUnitOfWork : IDisposable
    {

        void BeginTransaction();
        void Commit();
        void Rollback();

        int Complete();  // Guarda los cambios en la base de datos

    }
}
