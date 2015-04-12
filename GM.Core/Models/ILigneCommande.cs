namespace GM.Core.Models
{
    public interface ILigneCommande
    {
        int Id { get; set; }
        int MedicamentId { get; set; }
        string NumLot { get; set; }
        int Qnt { get; set; }
        //decimal? Montant { get; set; }
        decimal PrixUnitaire { get; set; }
        Medicament Medicament { get; set; }
        //long BonEntreeId { get; set; }
        //BonEntree    BonEntree { get; set; }
    }
}
