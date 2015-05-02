using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using EntyTea.EntityQueries;
using Excel;
using GM.Core;
using GM.Core.Models;
using GM.Services.Nomenclature;

namespace GM.Services.Medicaments
{
    public class ServiceMedicament:IServiceMedicmaent
    {
        private readonly IRepository<Medicament> _repository;
        private readonly IRepository<Remboursement> _rembousementRepository;
        private readonly IRepository<ParamStock> _repositoryParamsStock;
        private readonly IServiceDci _serviceDci;
        public ServiceMedicament(IRepository<Medicament> repository,
            IRepository<Remboursement> rembousementRepository,
            IRepository<ParamStock> repositoryParamsStock , 
            IServiceDci serviceDci)
        {
            _repository = repository;
            _rembousementRepository = rembousementRepository;
            _repositoryParamsStock = repositoryParamsStock;
            _serviceDci = serviceDci;
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
            return _repository.FindSingle(x=>x.Id ==id);
        }

        public bool Existe(string nom)
        {
            return _repository.Exist(x => x.NomCommerciale .ToUpper().Equals( nom.ToUpper()) ||x.Code .Equals(nom));
        }

        public bool ImporteListe( string fileName )
        {
            var file = new FileInfo(fileName);
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                IExcelDataReader reader = null;
                if (file.Extension == ".xls")
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);

                }
                else if (file.Extension == ".xlsx")
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                if (reader == null)
                {
                  
                    return false; 
                }
                    
                var dt = reader.AsDataSet().Tables[0];
               
                var dcis = (from row in dt.AsEnumerable()
                    select new 
                    {
                        Nom = row.Field<string>(2),
                        
                    }).Distinct().AsEnumerable();
                var b = false;
                var k = 1;
                foreach (var row in dcis)
                {
                    if (row.Nom != null && row.Nom.Contains("DENOMINATION"))
                    {
                        b = true;
                        continue;
                    }
                    if (!b) continue;
                    var dci = new Dci
                    {
                        SpecialiteId = 1,
                        Nom = row.Nom,
                        Code = k++.ToString(CultureInfo.InvariantCulture)
                    };
                    _serviceDci.Insert(dci);
                   
                }
                //foreach (DataRow row in dt.Rows)
                //{
                //    Medicament medicament = new Medicament();
                //    medicament.NumEnregistrement = row["N°ENREGISTREMENT"].ToString();
                //    //medicament.DciId = row["DINOMINATION COMMUNE INTERNAIONNALE"].ToString();
                //    medicament.NumEnregistrement = row["N°ENREGISTREMENT"].ToString();
                //    medicament.NumEnregistrement = row["N°ENREGISTREMENT"].ToString();
                //    medicament.NumEnregistrement = row["N°ENREGISTREMENT"].ToString();
                //    medicament.NumEnregistrement = row["N°ENREGISTREMENT"].ToString();

                //}


                //dataGridView1.DataSource = ds;
                //dataGridView1.DataMember
            }
            return true;
        }

        public ParamStock GetParamStock(Medicament medicament, int entrpriseId)
        {
            return medicament.ParamStocks.FirstOrDefault(x => x.EntrepriseId == entrpriseId);
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

        public bool InsertParamsStock(ParamStock stock , out int id)
        {

            try
            {
                _repositoryParamsStock.Insert(stock);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
            finally
            {
                id = stock.ParmsId;
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

        public bool UpdateParamsStock(ParamStock remboursement)
        {
            try
            {
                _repositoryParamsStock.Update(remboursement);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
                where m.LaboratoireId ==(medicament.LaboratoireId) || medicament.LaboratoireId == 0
                where
                    m.NumEnregistrement.Equals(medicament.NumEnregistrement) ||
                    string.IsNullOrEmpty(medicament.NumEnregistrement)
                select m;
            return filter.Filter(_repository.SelectAll().AsQueryable());
        }

        public IEnumerable<Medicament> AutoCOmpleteListe(Medicament medicament)
        {
            var filter = from m in EntityFilter<Medicament>.AsQueryable()
                where
                    m.NomCommerciale.StartsWith(medicament.NomCommerciale)||
                    string.IsNullOrEmpty(medicament.NomCommerciale) 
                    ||m.Dci.Nom.StartsWith(medicament.Dci.Nom) 
                    ||string.IsNullOrEmpty(medicament.Dci.Nom)
                    || m.Dose.StartsWith(medicament.Dose)
                    || string.IsNullOrEmpty(medicament.Dose)
               select m;
            return filter.Filter(_repository.SelectAll().AsQueryable());
        }
    }
}
