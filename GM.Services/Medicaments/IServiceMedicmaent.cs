using System.Collections.Generic;
using System.IO;
using GM.Core.Models;

namespace GM.Services.Medicaments
{

    public interface IServiceMedicmaent
    {
      
        IEnumerable<Medicament> ListeMedicaments();
        bool Insert(Medicament medicament , out int identity);
        bool Update(Medicament medicament);
        Medicament FindSingle(int id);
        bool Delete(int id);
        bool InsertRemboussement(Remboursement remboursement);
        bool InsertParamsStock(ParamStock stock , out int id);
        bool UpdateRemboussement(Remboursement remboursement);
        bool UpdateParamsStock(ParamStock remboursement);
        IEnumerable<Medicament> FilterListe(Medicament medicament);
        IEnumerable<Remboursement> GetListRemboursements(int? id);
        bool Existe(string nom);
        bool ImporteListe(string fileName );
        ParamStock GetParamStock(Medicament medicament, int entrpriseId);
    }
}
