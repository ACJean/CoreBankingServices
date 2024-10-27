using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharedOperations.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedOperations.Domain
{
    public class DefaultServiceFactory : IServiceFactory
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly IDictionary<string, Dependency> _maps;
        private readonly IDictionary<Type, Lifetime> _lifeTimeMapPerType;

        public DefaultServiceFactory(IServiceProvider serviceProvider, IOptions<DependencyInjectionConfig> config)
        {
            _serviceProvider = serviceProvider;
            if (config.Value is not null && config.Value.Dependencies is not null)
            {
                _maps = config.Value.Dependencies.ToDictionary(dependency => dependency.Name, dependency => dependency);
                _lifeTimeMapPerType = config.Value.Dependencies.ToDictionary(dependency => Type.GetType(dependency.Type) ?? throw new ApplicationException($"Not found class {dependency.Type}"), dependency => dependency.Lifetime);
            }
            else
            {
                _maps = new Dictionary<string, Dependency>();
                _lifeTimeMapPerType = new Dictionary<Type, Lifetime>();
            }
        }

        public TService GetService<TService>()
        {
            return GetService<TService>("");
        }

        public TService GetService<TService>(string serviceName)
        {
            if (serviceName is null) throw new ArgumentNullException(nameof(serviceName));

            Lifetime lifeTime = _lifeTimeMapPerType[typeof(TService)];

            if (string.IsNullOrEmpty(serviceName))
            {
                if (lifeTime == Lifetime.Scoped || lifeTime == Lifetime.Transient)
                {
                    var scope = _serviceProvider.CreateScope();
                    return scope.ServiceProvider.GetService<TService>() ?? throw new NullReferenceException("Service not found.");
                }

                return _serviceProvider.GetService<TService>() ?? throw new NullReferenceException("Service not found.");
            }

            if (!_maps.ContainsKey(serviceName)) throw new ApplicationException("Service not found.");

            Dependency dependency = _maps[serviceName];
            var implementationType = Type.GetType(dependency.MapTo);

            if (!typeof(TService).IsAssignableFrom(implementationType)) throw new ApplicationException("Invalid service type.");

            object service;
            if (lifeTime == Lifetime.Scoped || lifeTime == Lifetime.Transient)
            {
                var scope = _serviceProvider.CreateScope();
                service = scope.ServiceProvider.GetServices<TService>()
                    .FirstOrDefault(service => typeof(TService).IsAssignableFrom(implementationType));
            }
            else
            {
                service = _serviceProvider.GetServices<TService>()
                    .FirstOrDefault(service => typeof(TService).IsAssignableFrom(implementationType));
            }

            if (service is null) throw new ApplicationException("Service not found.");

            return (TService) service;
        }
    }
}
