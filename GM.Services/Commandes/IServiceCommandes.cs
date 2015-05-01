using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Commandes
{
    public interface IServiceCommandes
    {
        IEnumerable<Commande> Liste(long id);
        bool Insert(Commande commande);
        bool Update(Commande commande);
        Commande FindSingle(long id);
        bool Delete(long id);
    }
}
