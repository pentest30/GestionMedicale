using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Magasins
{
    public interface IServiceMagasin
    {
        IEnumerable<Magasin> Liste();
        //IEnumerable<Pays> ListePays();
        bool Insert(Magasin laboratoire);
        bool Update(Magasin laboratoire);
        Magasin FindSingle(int id);
        bool Delete(int id);
    }
}
