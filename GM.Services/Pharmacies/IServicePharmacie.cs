using System;
using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Pharmacies
{
    public interface IServicePharmacie
    {
        int? GetPharmacie(Guid id);
        bool Insert(Pharmacie pharmacie);
        bool Update(Pharmacie pharmacie);
        bool Existe(Guid id);
        Pharmacie SinglePharmacie(int id);
        IEnumerable<Pharmacie> GetListe();
    }
}
