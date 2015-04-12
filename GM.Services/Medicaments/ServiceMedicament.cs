using System;
using System.Collections.Generic;
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

        public bool Insert(Medicament medicament)
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

        public bool InsertRemboussement(Remboursement remboursement,int id)
        {
            remboursement.MedicamentId = id;
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
            throw new NotImplementedException();
        }

        public bool UpdateParamsStock(Remboursement remboursement)
        {
            throw new NotImplementedException();
        }
    }
}
