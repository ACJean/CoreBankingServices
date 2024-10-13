using CustomerOperations.Domain.Entity;
using SharedOperations.Domain.Repository;

namespace CustomerOperations.Domain.Repository
{
    public interface IPersonRepository : IRepository<Person, int>
    {

    }
}
