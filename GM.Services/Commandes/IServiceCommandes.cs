using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Commandes
{
    public interface IServiceCommandes
    {
        IEnumerable<Commande> Liste(long id);
        bool Insert(Commande commande);
        bool Insert(Commande commande , out long id);
        bool Update(Commande commande);
        Commande FindSingle(long id);
        bool Delete(long id);
        LigneCommande GetSingleLigne(long id);
        bool InsertLigne(LigneCommande ligne);
        bool UpdateLigne(LigneCommande ligne);
        bool DeleteLigne(int id);
        IEnumerable<LigneCommande> GetLigneCommandes(int commandeId);
    }
}
