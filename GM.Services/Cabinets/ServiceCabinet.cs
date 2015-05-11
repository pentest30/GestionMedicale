using System;
using System.Collections.Generic;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Cabinets
{
    public class ServiceCabinet : IServiceCabinet

    {
        private readonly IRepository<Cabinet> _repository;

        public ServiceCabinet(IRepository<Cabinet> repository)
        {
            _repository = repository;
        }

        public int? GetCabinet(Guid id)
        {
            var item = _repository.FindSingle(x => x.MedecinId == id);
            return (item!= null)? _repository.FindSingle(x => x.MedecinId == id).Id:0;
        }

        public bool Insert(Cabinet cabinet)
        {
            try
            {
                _repository.Insert(cabinet);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(Cabinet cabinet)
        {
            try
            {
                _repository.Update(cabinet);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Existe(Guid id)
        {
            return _repository.Exist(x=>x.MedecinId==id);
        }

        public Cabinet SingleCabinet(int id)
        {
            return _repository.SelectById(id);
        }

        public Cabinet SingleCabinet(Guid id)
        {
            return _repository.FindSingle(x => x.MedecinId == id);
        }

        public IEnumerable<Cabinet> GetListe()
        {
           return _repository.SelectAll();
        }
    }
}
