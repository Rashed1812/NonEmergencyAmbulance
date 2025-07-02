using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Data_Seed
{
    public interface IDataSeed
    {
        Task DataSeedAsync();
        Task IdentityDataSeedAsync();
    }
}
