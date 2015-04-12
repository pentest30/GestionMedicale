using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Medicaments
{

    public interface IServiceMedicmaent
    {
        IEnumerable<Medicament> ListeMedicaments();
        bool Insert(Medicament medicament);
        bool Update(Medicament medicament);
        Medicament FindSingle(int id);
        bool Delete(int id);
        bool InsertRemboussement(Remboursement remboursement, int id);
        bool InsertParamsStock(ParamStock stock,int id);
        bool UpdateRemboussement(Remboursement remboursement);
        bool UpdateParamsStock(Remboursement remboursement);
    }
}
