using System;
using GM.Core.Models;

namespace GM.Services.Pharmacies
{
    public interface IServicePharmacie
    {
        int? GetPharmacie(Guid id);
        bool Insert(Pharmacie pharmacie);
        bool Update(Pharmacie pharmacie);
    }
}
