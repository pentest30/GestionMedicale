using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Core
{
    public interface IServiceSpecialite
    {
        IEnumerable<Specialite> ListeSpecialites();
        bool Insert(Specialite specialite);
        bool Update(Specialite specialite);
        Specialite FindSingle(int id);
        bool Delete(int id);
    }
}