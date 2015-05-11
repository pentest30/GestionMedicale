using System;
using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Cabinets
{
    public interface IServiceCabinet
    {
        int? GetCabinet(Guid id);
        bool Insert(Cabinet cabinet);
        bool Update(Cabinet cabinet);
        bool Existe(Guid id);
        Cabinet SingleCabinet(int id);
        IEnumerable<Cabinet> GetListe();
    }
}
