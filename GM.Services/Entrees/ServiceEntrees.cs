using System;
using System.Collections.Generic;
using System.Linq;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Entrees
{
   public class ServiceEntrees:IServiceEntrees
    {
       private readonly IRepository<Entree> _repository;
       private readonly IRepository<LigneEntree> _repositoryLigne;

       public ServiceEntrees(IRepository<Entree> repository,
           IRepository<LigneEntree> repositoryLigne)
       {
           _repository = repository;
           _repositoryLigne = repositoryLigne;
       }

       public IEnumerable<Entree> Liste(long id)
       {
           var result = _repository.Find(x => x.ClientId == id);
           return result;
       }

       public bool Insert(Entree commande)
       {
           try
           {
               _repository.Insert(commande);
               return true;
           }
           catch (Exception)
           {
               return false;
           }
       }

       public bool Insert(Entree commande, out long id)
       {
           try
           {
               _repository.Insert(commande);
               return true;
           }
           catch (Exception)
           {
               return false;
           }
           finally
           {
               id = commande.Id;
           }
       }

       public bool Update(Entree commande)
       {
           try
           {
               _repository.Update(commande);
               return true;
           }
           catch (Exception)
           {
               return false;
           }
       }

       public Entree FindSingle(long id)
       {
           return _repository.FindSingle(x =>x.Id ==id);
       }

       public bool Delete(long id)
       {
           try
           {
               _repository.Delete(id);
               return true;
           }
           catch (Exception)
           {
               return false;
           }
       }

       public LigneEntree GetSingleLigne(long id)
       {
           return _repositoryLigne.SelectById(id);
       }

       public bool InsertLigne(LigneEntree ligne)
       {
           try
           {
               _repositoryLigne.Insert(ligne);
               return true;
           }
           catch (Exception)
           {
               return false;
           }
       }

       public bool UpdateLigne(LigneEntree ligne)
       {
           try
           {
               _repositoryLigne.Update(ligne);
               return true;
           }
           catch (Exception)
           {
               return false;
           }
       }

       public bool DeleteLigne(int id)
       {
           try
           {
               _repositoryLigne.Delete(id);
               return true;
           }
           catch (Exception)
           {
               return false;
           }
       }

       public IEnumerable<LigneEntree> GetLigneCommandes(int entreeId)
       {
           var result = _repositoryLigne.Find(x => x.EntreeId == entreeId);
           var ligneCommandes = result as LigneEntree[] ?? result.ToArray();
           foreach (var ligneEntree in ligneCommandes)
           {
               ligneEntree.Montant = ligneEntree.PrixAchat*ligneEntree.Qnt;
           }
           return ligneCommandes;
       }

       public IEnumerable<LigneEntree> GetLigneCommandes()
       {
           return _repositoryLigne.SelectAll();
       }
    }
}
