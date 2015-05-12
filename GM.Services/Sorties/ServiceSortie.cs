using System;
using System.Collections.Generic;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Sorties
{
   public class ServiceSortie:IServiceSortie
    {
       private readonly IRepository<BonSortieMagasin> _repository;

       public ServiceSortie(IRepository<BonSortieMagasin> repository)
       {
           _repository = repository;
       }

       public IEnumerable<BonSortieMagasin> Liste(long id)
       {
          return _repository.Find(x => x.FournisseurId == id);
       }

       public bool Insert(BonSortieMagasin bonSortie)
       {
           try
           {
               _repository.Insert(bonSortie);
               return true;
           }
           catch (Exception)
           {
               return false;
           }
       }

       public bool Insert(BonSortieMagasin bonSortie, out long id)
       {
           try
           {
               _repository.Insert(bonSortie);
               return true;
           }
           catch (Exception)
           {
               return false;
           }
           finally
           {
               id = bonSortie.Id;
           }
       }

       public bool Update(BonSortieMagasin bonSortie)
       {
           try
           {
               _repository.Insert(bonSortie);
               return true;
           }
           catch (Exception)
           {
               return false;
           }
       }

       public BonSortieMagasin FindSingle(long id)
       {
           return _repository.FindSingle(x => x.Id == id);
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
    }
}
