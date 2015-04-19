using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Medicaments
{
   public class PictureRepository:IRepository<MedicamentPicture>
   {
       public IEnumerable<MedicamentPicture> SelectAll()
       {
           throw new NotImplementedException();
       }

       public MedicamentPicture SelectById(object id)
       {
           throw new NotImplementedException();
       }

       public void Insert(MedicamentPicture item)
       {
           throw new NotImplementedException();
       }

       public void Update(MedicamentPicture item)
       {
           throw new NotImplementedException();
       }

       public void Delete(object id)
       {
           throw new NotImplementedException();
       }

       public void Save()
       {
           throw new NotImplementedException();
       }

       public IEnumerable<MedicamentPicture> Find(Func<MedicamentPicture, bool> predicate)
       {
           throw new NotImplementedException();
       }

       public MedicamentPicture FindSingle(Func<MedicamentPicture, bool> predicate)
       {
           throw new NotImplementedException();
       }

       public IEnumerable<MedicamentPicture> GetAllLazyLoad(params Expression<Func<MedicamentPicture, object>>[] children)
       {
           throw new NotImplementedException();
       }

       public bool Exist(Func<MedicamentPicture, bool> predicate)
       {
           throw new NotImplementedException();
       }
   }
}
