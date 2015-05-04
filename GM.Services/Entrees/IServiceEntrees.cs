using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Entrees
{
    public interface IServiceEntrees
    {
        IEnumerable<BonEntree> Liste(long id);
        bool Insert(BonEntree commande);
        bool Insert(BonEntree commande, out long id);
        bool Update(BonEntree commande);
        BonEntree FindSingle(long id);
        bool Delete(long id);
        LigneEntree GetSingleLigne(long id);
        bool InsertLigne(LigneEntree ligne);
        bool UpdateLigne(LigneEntree ligne);
        bool DeleteLigne(int id);
        IEnumerable<LigneEntree> GetLigneCommandes(int entreeId);
    }
}
