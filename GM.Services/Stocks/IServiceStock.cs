using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Stocks
{
    public interface IServiceStock
    {
        IEnumerable<Stock> Liste(int id);
        bool Insert(Stock stock);
        bool Insert(Stock stock, out int id);
        bool Update(Stock stock);
        Stock FindSingle(int id, int entrepriseId);
        bool Delete(int id);
    }
}
