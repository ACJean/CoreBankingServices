using Microsoft.Extensions.DependencyInjection;

namespace SharedOperations.Domain
{
    public class DependencyInjectionConfig
    {

        public IEnumerable<Dependency> Dependencies { get; set; }

        public static void Start(DependencyInjectionConfig config, IServiceCollection services)
        {
            if (config.Dependencies == null) return;
            foreach (var dependency in config.Dependencies)
            {
                var serviceType = Type.GetType(dependency.Type) ?? throw new NullReferenceException($"Type {dependency.Type} not found");
                var implementationType = Type.GetType(dependency.MapTo) ?? throw new NullReferenceException($"Type {dependency.MapTo} not found");
                var lifetime = dependency.Lifetime switch
                {
                    Lifetime.Scoped => ServiceLifetime.Scoped,
                    Lifetime.Singleton => ServiceLifetime.Singleton,
                    Lifetime.Transient => ServiceLifetime.Transient,
                    _ => ServiceLifetime.Transient
                };

                ServiceDescriptor serviceDescriptor = new(
                        serviceType,
                        implementationType,
                        lifetime);

                services.Add(serviceDescriptor);
            }
        }

    }

    public class Dependency
    {
        public string Type { get; set; }
        public string MapTo { get; set; }
        public Lifetime Lifetime { get; set; }
        public string Name { get; set; }
    }

    public enum Lifetime
    {
        Scoped,
        Singleton,
        Transient
    }
}
