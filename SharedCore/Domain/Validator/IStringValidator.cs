namespace SharedOperations.Domain.Validator
{
    public interface IStringValidator
    {

        Result<Unit, Error> IsValid(string str);

    }
}
