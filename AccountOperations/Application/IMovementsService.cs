using AccountOperations.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Application
{
    public interface IMovementsService
    {

        void Add(Movements movement);
        Movements? Get(int id);

    }
}
