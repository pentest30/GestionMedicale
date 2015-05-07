using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Entrees
{
    public interface IServiceEntrees
    {
        IEnumerable<Entree> Liste(long id);
        bool Insert(Entree commande);
        bool Insert(Entree commande, out long id);
        bool Update(Entree commande);
        Entree FindSingle(long id);
        bool Delete(long id);
        LigneEntree GetSingleLigne(long id);
        bool InsertLigne(LigneEntree ligne);
        bool UpdateLigne(LigneEntree ligne);
        bool DeleteLigne(int id);
        IEnumerable<LigneEntree> GetLigneCommandes(int entreeId);
        IEnumerable<LigneEntree> GetLigneCommandes();
        BonEntree FindByFactureId(long factureId);
        bool InsertLigneMagasin(LigneEntreeMagasin ligne);
        bool InsertBonMagasin(BonEntree bon, out long id);

    }
}
