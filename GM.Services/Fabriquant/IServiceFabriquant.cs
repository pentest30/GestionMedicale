using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Fabriquant
{
    public interface IServiceFabriquant
    {
        IEnumerable<Laboratoire> Liste();
        IEnumerable<Pays> ListePays();
        bool Insert(Laboratoire laboratoire);
        bool Update(Laboratoire laboratoire);
        Laboratoire FindSingle(int id);
        bool Delete(int id);
    }
}
