using SharedOperations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOperations.Domain
{
    public interface IPersonRepository : IRepository<Person, int>
    {

    }
}
