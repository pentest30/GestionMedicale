using System;
using System.Collections.Generic;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Entrees
{
   public class ServiceEntrees:IServiceEntrees
    {
       private readonly IRepository<BonEntree> _repository;

       public ServiceEntrees(IRepository<BonEntree> repository)
       {
           _repository = repository;
       }

       public IEnumerable<BonEntree> Liste(long id)
       {
           var result = _repository.Find(x => x.ClientId == id);
           return result;
       }

       public bool Insert(BonEntree commande)
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

       public bool Insert(BonEntree commande, out long id)
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

       public bool Update(BonEntree commande)
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

       public BonEntree FindSingle(long id)
       {
           throw new NotImplementedException();
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

       public LigneEntreeMagasin GetSingleLigne(long id)
       {
           throw new NotImplementedException();
       }

       public bool InsertLigne(LigneEntreeMagasin ligne)
       {
           throw new NotImplementedException();
       }

       public bool UpdateLigne(LigneEntreeMagasin ligne)
       {
           throw new NotImplementedException();
       }

       public bool DeleteLigne(int id)
       {
           throw new NotImplementedException();
       }

       public IEnumerable<LigneEntreeMagasin> GetLigneCommandes(int entreeId)
       {
           throw new NotImplementedException();
       }
    }
}
