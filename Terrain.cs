public abstract class Terrain
{
    private static int compteurTerrains = 0;  // Compteur partagé entre tous les terrains


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
        PourvuEnEau = false;
        NbParcelles = nbParcelles;

        // Supprime ou commente toute initialisation de Grille ici
    }

    public virtual void Afficher(Menu menu, Plantes?[,] grilleJardin, int terrainIndex)
    {
        int colonnes = 6;
        string reset = "\x1b[0m";

        for (int col = 0; col < colonnes; col++)
        {
            var plante = grilleJardin[terrainIndex, col];

            if (plante != null)
            {
                string emoji = plante.Afficher();
                // Affiche emoji avec fond coloré et espaces avant/après pour espacer
                Console.Write($"{Couleur} {emoji} {reset}  ");
            }
            else
            {
                // Case vide avec fond coloré et 3 espaces (taille égale à emoji + espace)
                Console.Write($"{Couleur}   {reset}  ");
            }
        }
        Console.WriteLine();  // passe à la ligne après l'affichage d'une rangée
    }
}