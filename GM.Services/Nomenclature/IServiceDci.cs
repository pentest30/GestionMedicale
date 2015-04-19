using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Nomenclature
{
    public interface IServiceDci
    {
        IEnumerable<Dci> ListeDcis();
        bool Insert(Dci dci);
        bool Update(Dci dci);
        Dci FindSingle(int id);
        bool Delete(int id);
        bool Existe(string code);
    }
}
