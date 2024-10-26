using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedOperations.Domain
{
    public class DependencyInjectionConfig
    {

        public IEnumerable<Dependency>? Dependencies { get; set; }

        public static void Start(DependencyInjectionConfig config, IServiceCollection services)
        {
            if (config.Dependencies == null) return;
            foreach (var dependency in config.Dependencies)
            {
                var serviceType = Type.GetType(dependency.Type);
                var implementationType = Type.GetType(dependency.MapTo);
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
