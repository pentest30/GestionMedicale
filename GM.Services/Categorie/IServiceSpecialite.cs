using System.Collections.Generic;

namespace GM.Services.Categorie
{
    public interface IServiceSpecialite
    {
        IEnumerable<Core.Models.Specialite> ListeSpecialites();
        bool Insert(Core.Models.Specialite specialite);
        bool Update(Core.Models.Specialite specialite);
        Core.Models.Specialite FindSingle(int id);
        bool Delete(int id);
    }
}