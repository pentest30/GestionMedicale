using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Core
{
    public interface IServiceDci
    {
        IEnumerable<Dci> ListeDcis();
        bool Insert(Dci dci);
        bool Update(Dci dci);
        Dci FindSingle(int id);
        bool Delete(int id);
    }
}
