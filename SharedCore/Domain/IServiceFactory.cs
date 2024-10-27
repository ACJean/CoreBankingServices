namespace SharedOperations.Domain
{
    public interface IServiceFactory 
    {

        TService GetService<TService>();
        TService GetService<TService>(string serviceName);

    }
}
