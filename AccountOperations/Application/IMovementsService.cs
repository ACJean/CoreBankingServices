using AccountOperations.Domain.Entity;
using SharedOperations.Domain;

namespace AccountOperations.Application
{
    public interface IMovementsService
    {

        Result<Unit, Error> Add(Movements movement);
        Result<Movements, Error> Get(int id);

    }
}
