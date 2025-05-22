/// <summary>
/// 
/// Classe abstraite pr définir base commune à tous les types de terrain
/// Gère type de sol, nom, nb de parcelles, options d'affichage...
/// 
/// </summary>
public abstract class Terrain
{
    private static int compteurTerrains = 0;  // Compteur partagé entre tous les terrains

    public string Couleur { get; set; } = "\x1b[48;5;240m"; //couleur de fon des terrains
    public string? Nom { get; set; }
    public string? TypeDeSol { get; set; } // "Terre", "Sable", "Argile"
    public bool? ALaLumiere { get; set; } 
    public bool? PourvuEnEau { get; set; }
    public bool? ProtégéContreIntrus { get; set; }
    public int? NbParcelles { get; set; } // Nb Parcelles est fixe à 6 pour ce jeu

    // Constructeur : initialise attributs de base du terrain (type, nom, parcelles...)
    public Terrain(string typeDeSol, int nbParcelles)
    {
        compteurTerrains++;
        Nom = $"T{compteurTerrains}";
        TypeDeSol = typeDeSol;
        ALaLumiere = true;
        ProtégéContreIntrus = false;
        PourvuEnEau = false;
        NbParcelles = nbParcelles;
    }

    // Affiche contenu d'un terrain : emoji ou vide sur fond coloré
    public virtual void Afficher(Menu menu, Plantes?[,] grilleJardin, int terrainIndex)
    {
        int colonnes = 6;
        string reset = "\x1b[0m";

        for (int col = 0; col < colonnes; col++)
        {
            var plante = grilleJardin[terrainIndex, col];

            string icone;
            var indesirable = Indesirables.IndesirableActuel; 
            if (indesirable != null && indesirable.EstPresent && indesirable.LigneTerrain == terrainIndex && indesirable.ColonneActuelle == col)
            {
                icone = indesirable.Icone;
            }
            else
            {
                icone = "  ";
            }
            if (plante != null)
            {
                string emoji = plante.Afficher();
                Console.Write($"{Couleur}{emoji}{icone} {reset}  "); //espaces pour ajuster 
            }
            else
            {
                Console.Write($"{Couleur}   {icone} {reset}  "); //idem
            }
        }
        Console.WriteLine();
    }
}
