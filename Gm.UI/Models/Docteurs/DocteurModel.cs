using System;
using GM.Core.Models;

namespace Gm.UI.Models.Docteurs
{
    public class DocteurModel
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public Cabinet Cabinet { get; set; }
    }
}