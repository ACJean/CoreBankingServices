namespace SharedOperations.Domain
{
    public interface IPasswordValidator
    {

        bool IsValid(string password);

    }
}
