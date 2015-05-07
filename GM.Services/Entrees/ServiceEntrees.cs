using System;
using System.Collections.Generic;
using System.Linq;
using GM.Core;
using GM.Core.Models;
using GM.Services.Medicaments;

namespace GM.Services.Entrees
{
   public class ServiceEntrees:IServiceEntrees
    {
       private readonly IRepository<Entree> _repository;
       private readonly IRepository<LigneEntree> _repositoryLigne;
       private readonly IServiceMedicmaent _serviceMedicmaent;
       private readonly IRepository<BonEntree> _repositoryBonnetree;
       private readonly IRepository<LigneEntreeMagasin> _repositoryLigneMAgasin;

       public ServiceEntrees(IRepository<Entree> repository,
           IRepository<LigneEntree> repositoryLigne ,
           IServiceMedicmaent serviceMedicmaent ,
           IRepository<BonEntree> repositoryBonnetree  , 
           IRepository<LigneEntreeMagasin> repositoryLigneMAgasin )
       {
           _repository = repository;
           _repositoryLigne = repositoryLigne;
           _serviceMedicmaent = serviceMedicmaent;
           _repositoryBonnetree = repositoryBonnetree;
           _repositoryLigneMAgasin = repositoryLigneMAgasin;
       }

       public IEnumerable<Entree> Liste(long id)
       {
           var result = _repository.Find(x => x.ClientId == id);
           var enumerable = result as Entree[] ?? result.ToArray();
           foreach (var entree in enumerable)
           {
               entree.Tht = entree.LigneEntrees.Sum(x => x.PrixAchat*x.Qnt);
               var tax = entree.LigneEntrees.Aggregate<LigneEntree, decimal>(0, (current, ligneEntree) => current + Convert.ToInt64(_serviceMedicmaent.FindSingle(ligneEntree.MedicamentId).Tva));
               entree.Ttc = entree.LigneEntrees.Sum(x => x.PrixAchat * x.Qnt) +tax;
               entree.Tva = tax;

           }
           return enumerable;
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
           foreach (var ligneEntree in ligneCommandes) ligneEntree.Montant = ligneEntree.PrixAchat * ligneEntree.Qnt;
           return ligneCommandes;
       }

       public IEnumerable<LigneEntree> GetLigneCommandes()
       {
           return _repositoryLigne.SelectAll();
       }

       public BonEntree FindByFactureId(long factureId)
       {
           return _repositoryBonnetree.FindSingle(x => x.FactureId == factureId);
       }

       public bool InsertLigneMagasin(LigneEntreeMagasin ligne)
       {
           try
           {
               _repositoryLigneMAgasin.Insert(ligne);
               return true;
           }
           catch (Exception)
           {
               return false;
           }
       }

       public bool InsertBonMagasin(BonEntree bon ,out long id)
       {
           try
           {
               _repositoryBonnetree.Insert(bon);
               return true;
           }
           catch (Exception)
           {
               return false;
           }
           finally
           {
               id = bon.Id;
           }
       }
    }
}
