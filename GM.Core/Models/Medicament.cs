namespace GM.Core.Models
{
    public class Medicament
    {
        public int Id { get; set; }
        public string NomCommerciale { get; set; }
        public string Code { get; set; }
        public string NumEnregistrement { get; set; }
        public string Dose { get; set; }
        public bool Remboursable { get; set; }
        public int DciId { get; set; }

        public int SpecialiteId { get; set; }
        public int FormeId { get; set; }
        public int ConditionnementId { get; set; }
        public int LaboratoireId { get; set; }
        public Dci Dci { get; set; }
        public Specialite Specialite { get; set; }
        public Forme Forme { get; set; }
        public Conditionnement Conditionnement { get; set; }
        public Laboratoire Laboratoire { get; set; }

        //public TYPE Type { get; set; }

    }
}
