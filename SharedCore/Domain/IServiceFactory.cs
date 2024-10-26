using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedOperations.Domain
{
    public interface IServiceFactory 
    {

        TService GetService<TService>();
        TService GetService<TService>(string serviceName);

    }
}
