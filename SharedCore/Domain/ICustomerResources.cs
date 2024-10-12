using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedOperations.Domain
{
    public interface ICustomerResources
    {

        Task<string?> GetName(string customerIdentity);
        Task<bool> IsExist(string customerIdentity);

    }
}
