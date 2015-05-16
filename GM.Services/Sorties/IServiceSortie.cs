using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Sorties
{
    public interface IServiceSortie
    {
        IEnumerable<BonSortieMagasin> Liste(long id);
        bool Insert(BonSortieMagasin bonSortie);
        bool Insert(BonSortieMagasin bonSortie, out long id);
        bool Update(BonSortieMagasin bonSortie);
        BonSortieMagasin FindSingle(long id);
        bool Delete(long id);
    }
}
