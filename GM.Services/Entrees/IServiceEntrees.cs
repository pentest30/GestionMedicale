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
        LigneEntreeMagasin GetSingleLigne(long id);
        bool InsertLigne(LigneEntreeMagasin ligne);
        bool UpdateLigne(LigneEntreeMagasin ligne);
        bool DeleteLigne(int id);
        IEnumerable<LigneEntreeMagasin> GetLigneCommandes(int entreeId);
    }
}
