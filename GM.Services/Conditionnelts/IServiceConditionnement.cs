using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Conditionnelts
{
    public interface IServiceConditionnement
    {
        IEnumerable<Conditionnement> Liste();
        bool Insert(Conditionnement forme);
        bool Update(Conditionnement forme);
        Conditionnement FindSingle(int id);
        bool Delete(int id);
    }
}
