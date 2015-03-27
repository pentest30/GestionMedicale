using System.Collections.Generic;

namespace GM.Core
{
    public  class Wilaya
    {
        public  int NumWilaya { get; set; }
        public  string Nom { get; set; }
        //private static IList<Wilaya> Wilayas { get; set; }

        public static IList<Wilaya> ListWilayas()
        {
            IList<Wilaya> wilayas = new List<Wilaya>();
            wilayas.Add(new Wilaya { NumWilaya = 1, Nom = "Adrar" });
            wilayas.Add(new Wilaya { NumWilaya = 2, Nom = "Chlef" });
            wilayas.Add(new Wilaya { NumWilaya = 3, Nom = "Laghouat" });
            wilayas.Add(new Wilaya { NumWilaya = 4, Nom = "d'Oum El Bouaghi" });
            wilayas.Add(new Wilaya { NumWilaya = 5, Nom = "Batna" });
            wilayas.Add(new Wilaya { NumWilaya = 6, Nom = "Béjaïa" });
            wilayas.Add(new Wilaya { NumWilaya = 7, Nom = "Biskra" });
            wilayas.Add(new Wilaya { NumWilaya = 8, Nom = "Béchar" });
            wilayas.Add(new Wilaya { NumWilaya = 9, Nom = "Blida" });
            wilayas.Add(new Wilaya { NumWilaya = 10, Nom = "Bouira" });
            wilayas.Add(new Wilaya { NumWilaya = 11, Nom = "Tamanrasset" });
            wilayas.Add(new Wilaya { NumWilaya = 12, Nom = "Tébessa" });
            wilayas.Add(new Wilaya { NumWilaya = 13, Nom = "Tlemcen" });
            wilayas.Add(new Wilaya { NumWilaya = 14, Nom = "Tiaret" });
            wilayas.Add(new Wilaya { NumWilaya = 15, Nom = "Tizi Ouzou" });
            wilayas.Add(new Wilaya { NumWilaya = 16, Nom = "Alger" });
            wilayas.Add(new Wilaya { NumWilaya = 17, Nom = "Djelfa" });
            wilayas.Add(new Wilaya { NumWilaya = 18, Nom = "Jijel" });
            wilayas.Add(new Wilaya { NumWilaya = 19, Nom = "Sétif" });
            wilayas.Add(new Wilaya { NumWilaya = 20, Nom = "Saïda" });
            wilayas.Add(new Wilaya { NumWilaya = 21, Nom = "Skikda" });
            wilayas.Add(new Wilaya { NumWilaya = 22, Nom = "Sidi Bel Abbès" });
            wilayas.Add(new Wilaya { NumWilaya = 23, Nom = "Annaba" });
            wilayas.Add(new Wilaya { NumWilaya = 24, Nom = "Guelma" });
            wilayas.Add(new Wilaya { NumWilaya = 25, Nom = "Constantine" });
            wilayas.Add(new Wilaya { NumWilaya = 26, Nom = "Médéa" });
            wilayas.Add(new Wilaya { NumWilaya = 27, Nom = "Mostaganem" });
            wilayas.Add(new Wilaya { NumWilaya = 28, Nom = "M'Sila" });
            wilayas.Add(new Wilaya { NumWilaya = 29, Nom = "Mascara" });
            wilayas.Add(new Wilaya { NumWilaya = 30, Nom = "Ouargla" });
            wilayas.Add(new Wilaya { NumWilaya = 31, Nom = "Oran" });
            wilayas.Add(new Wilaya { NumWilaya = 32, Nom = "El Bayadh" });
            wilayas.Add(new Wilaya { NumWilaya = 33, Nom = "Illizi" });
            wilayas.Add(new Wilaya { NumWilaya = 34, Nom = "Bordj Bou Arreridj" });
            wilayas.Add(new Wilaya { NumWilaya = 35, Nom = "Boumerdès" });
            wilayas.Add(new Wilaya { NumWilaya = 36, Nom = "El Tarf" });
            wilayas.Add(new Wilaya { NumWilaya = 37, Nom = "Tindouf" });
            wilayas.Add(new Wilaya { NumWilaya = 38, Nom = "Tissemsilt" });
            wilayas.Add(new Wilaya { NumWilaya = 39, Nom = "El Oued" });
            wilayas.Add(new Wilaya { NumWilaya = 40, Nom = "Khenchela" });
            wilayas.Add(new Wilaya { NumWilaya = 41, Nom = "Souk Ahras" });
            wilayas.Add(new Wilaya { NumWilaya = 42, Nom = "Tipaza" });
            wilayas.Add(new Wilaya { NumWilaya = 43, Nom = "Mila" });
            wilayas.Add(new Wilaya { NumWilaya = 44, Nom = "Aïn Defla" });
            wilayas.Add(new Wilaya { NumWilaya = 55, Nom = "Naâma" });
            wilayas.Add(new Wilaya { NumWilaya = 46, Nom = "Aïn Témouchent" });
            wilayas.Add(new Wilaya { NumWilaya = 47, Nom = "Ghardaïa" });
            wilayas.Add(new Wilaya { NumWilaya = 48, Nom = "Relizane" });
            return wilayas;
        }
    }
}
