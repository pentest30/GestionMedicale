using System;
using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Fournisseurs
{
    public interface IServiceFournisseur
    {
        int? GetFournisseur(Guid id);
        bool Insert(Fournisseur fournisseur);
        bool Update(Fournisseur fournisseur);
        bool Existe(Guid id);
        Fournisseur SingleFournisseur(int id);
        IEnumerable<Fournisseur> FounisseurNonInscript();
        IEnumerable<Fournisseur> AllFournisseurs();
        IEnumerable<Fournisseur> FounisseurInscript();

        IEnumerable<Fournisseur> SearchResult(Fournisseur fournisseur);

        bool Delete(int id);
    }
}
