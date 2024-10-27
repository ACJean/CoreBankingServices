namespace SharedOperations.Domain.Services
{
    public interface ICustomerResources
    {

        Task<string> GetName(string customerIdentity);
        Task<bool> IsExist(string customerIdentity);

    }
}
