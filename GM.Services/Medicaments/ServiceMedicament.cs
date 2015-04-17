using System;
using System.Collections.Generic;
using System.Linq;
using EntyTea.EntityQueries;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Medicaments
{
    public class ServiceMedicament:IServiceMedicmaent
    {
        private readonly IRepository<Medicament> _repository;
        private readonly IRepository<Remboursement> _rembousementRepository;
        private readonly IRepository<ParamStock> _repositoryParamsStock;

        public ServiceMedicament(IRepository<Medicament> repository, IRepository<Remboursement> rembousementRepository,
            IRepository<ParamStock> repositoryParamsStock)
        {
            _repository = repository;
            _rembousementRepository = rembousementRepository;
            _repositoryParamsStock = repositoryParamsStock;
        }

        public IEnumerable<Medicament> ListeMedicaments()
        {
            return _repository.SelectAll();
        }

        public bool Insert(Medicament medicament , out  int identity)
        {
            try
            {
                _repository.Insert(medicament);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                identity = medicament.Id;
            }
        }

        public bool Update(Medicament medicament)
        {
            try
            {
                _repository.Update(medicament);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Medicament FindSingle(int id)
        {
            return _repository.SelectById(id);
        }

        public bool Existe(string nom)
        {
            return _repository.Exist(x => x.NomCommerciale .ToUpper().Equals( nom.ToUpper()) ||x.Code .Equals(nom));
        }
        public bool Delete(int id)
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

        public bool InsertRemboussement(Remboursement remboursement)
        {
            
            try
            {
                _rembousementRepository.Insert(remboursement);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertParamsStock(ParamStock stock,int id)
        {
            stock.MedicamentId = id;
            try
            {
                _repositoryParamsStock.Insert(stock);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateRemboussement(Remboursement remboursement)
        {
            try
            {
                _rembousementRepository.Update(remboursement);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateParamsStock(Remboursement remboursement)
        {
            throw new NotImplementedException();
        }
         public IEnumerable<Remboursement> GetListRemboursements(int? id)
         {
             var ide = Convert.ToInt32(id);
             return _rembousementRepository.Find(x => x.MedicamentId ==ide);
         }

        public IEnumerable<Medicament> FilterListe(Medicament medicament)
        {
            var filter = from m in EntityFilter<Medicament>.AsQueryable()
                where
                    m.NomCommerciale.Contains(medicament.NomCommerciale) ||
                    string.IsNullOrEmpty(medicament.NomCommerciale)
                where m.Code.Equals(medicament.Code) || string.IsNullOrEmpty(medicament.Code)
                where m.SpecialiteId.Equals(medicament.SpecialiteId) || medicament.SpecialiteId == 0
                where m.DciId.Equals(medicament.DciId) || medicament.DciId == 0
                where
                    m.NumEnregistrement.Equals(medicament.NumEnregistrement) ||
                    string.IsNullOrEmpty(medicament.NumEnregistrement)
                select m;
            return filter.Filter(_repository.SelectAll().AsQueryable());
        }
    }
}
