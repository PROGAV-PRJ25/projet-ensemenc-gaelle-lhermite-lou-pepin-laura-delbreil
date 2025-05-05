/// <summary>
/// 
/// </summary>

public abstract class Terrain
{
    private static int compteurTerrains = 0;  // Compteur partagé entre tous les terrains

    public string Nom { get; set; }
    public string TypeDeSol { get; set; } // "Terre", "Sable", "Argile"
    public bool ALaLumiere { get; set; }
    public bool PourvuEnEau { get; set; }
    public bool ProtégéContreIntrus { get; set; }
    public Parcelle[] Parcelles { get; set; } = new Parcelle[6];

    public Terrain(string typeDeSol)
    {
        compteurTerrains++;
        Nom = $"T{compteurTerrains}";
        TypeDeSol = typeDeSol;
        ALaLumiere = true;
        ProtégéContreIntrus = false;
        PourvuEnEau = false ;

        // Chaque terrain contient 6 parcelles indépendantes
        for (int i = 0; i < 6; i++)
        {
            Parcelles[i] = new Parcelle(i + 1);
        }
    }

    public virtual void Afficher()
    {
        Console.WriteLine($"Terrain générique : {Nom} - {TypeDeSol}");
    }

}
