/// <summary>
/// 
/// </summary>

public abstract class Terrain
{
    private static int compteurTerrains = 0;  // Compteur partagé entre tous les terrains
    public Plantes?[,] Grille { get; private set; }
    public string Couleur { get; set; } = "\x1b[48;5;240m";
    public string? Nom { get; set; }
    public string? TypeDeSol { get; set; } // "Terre", "Sable", "Argile"
    public bool? ALaLumiere { get; set; }
    public bool? PourvuEnEau { get; set; }
    public bool? ProtégéContreIntrus { get; set; }
    public int? NbParcelles { get; set; }

    public Terrain(string typeDeSol, int nbParcelles)
    {
        compteurTerrains++;
        Nom = $"T{compteurTerrains}";
        TypeDeSol = typeDeSol;
        ALaLumiere = true;
        ProtégéContreIntrus = false;
        PourvuEnEau = false ;
        NbParcelles = nbParcelles;

      
    }

    public void Planter(int ligne, int colonne, Plantes plante)
{
    if (Grille == null)
    {
        // Initialisation de la grille si elle ne l'est pas
        Grille = new Plantes[3, 6]; // ou une taille dynamique basée sur NbParcelles si nécessaire
    }

    if (Grille[ligne, colonne] != null)
    {
        Console.WriteLine($"Il y a déjà une plante à la position ({ligne}, {colonne}).");
    }
    else
    {
        Grille[ligne, colonne] = plante;
        Console.WriteLine($"Plante {plante.Nom} plantée à ({ligne}, {colonne}) : {plante.Afficher()}");
    }
}


    public virtual void Afficher(Menu menu)
    {
       
        int lignes = 3;
        string reset = "\x1b[0m";
        for (int ligne = 0; ligne < lignes; ligne++)
        {
            // Chaque carré fait 2 lignes de hauteur pour l'effet "carré"
            for (int hauteur = 0; hauteur < 2; hauteur++)
            {
                for (int col = 0; col < lignes; col++)
                {
                    Console.Write($"{Couleur}       {reset}  "); // 3 espaces de largeur + 1 espace
                }
            }
            Console.WriteLine(); // Espace vertical entre les rangées
        }
    }

}
