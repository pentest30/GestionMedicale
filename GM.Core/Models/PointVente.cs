namespace GM.Core.Models
{
   public class PointVente
    {
       public int Id { get; set; }
       public int EntrepriseId { get; set; }
       public string Nom { get; set; }
       public string Tel { get; set; }
       public string Wilaya { get; set; }
       public string Addresse { get; set; }
       public Entreprise Entreprise { get; set; }

    }
}
