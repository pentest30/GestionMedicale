using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Formes
{
    public interface IServiceForme
    {
        IEnumerable<Forme> ListeFormes();
        bool Insert(Forme forme);
        bool Update(Forme forme);
        Forme FindSingle(int id);
        bool Delete(int id);
    }
}
