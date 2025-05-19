///
///  Classe pr gérer jardin (stocke terrains + grille plantes)
/// 
/// 
public class Jardin
{
    public Terrain[] Terrains { get; private set; }
    private Plantes?[,] Grille;  // Tableau pour gérer la grille du jardin (6 colonnes)
    
    // crée tableau terrains + init grille plantation
    public Jardin(Menu menu)
    {
        int nombreDeTerrains = menu.NbTerrains; // Nombre de terrains (2, 4, ou 6 terrains)
        Terrains = new Terrain[nombreDeTerrains];  // Tableau pour stocker terrains
        Grille = new Plantes?[nombreDeTerrains, 6]; // Grille pour stocker plantes par terrain + colonne

        Random rand = new Random();

        // Créer terrains et les stocker dans le tableau
        // Boucle pr générer terrain aléatoire (terre/sable/argile)
        for (int i = 0; i < nombreDeTerrains; i++)
        {
            string[] types = { "terre", "sable", "argile" };
            string type = types[rand.Next(types.Length)];
            string nom = $"Terrain_{i + 1}"; // nom auto

            Terrains[i] = type switch
            {
                "terre" => new TerrainTerreux(nom, 4),
                "sable" => new TerrainSableux(nom, 4),
                "argile" => new TerrainArgileux(nom, 4),
                _ => throw new Exception("Type de terrain inconnu.")
            };
        }
    }
    //methode accès sécurisé
    // Fct pr accéder plante ds grille à coord précises
    public Plantes? GetPlante(int terrainIndex, int colonne)
    {
        if (terrainIndex < 0 || terrainIndex >= Terrains.Length) return null;
        if (colonne < 0 || colonne >= 6) return null;
        return Grille[terrainIndex, colonne];
    }

    // Méthode pour planter une plante dans la grille
    // Fct pr planter une plante à une position (ligne/colonne)
    public void PlanterDansGrille(int terrainIndex, int colonne, Plantes plante)
    {
        if (terrainIndex < 0 || terrainIndex >= Terrains.Length)
        {
            Console.WriteLine("Erreur : index de terrain hors limites.");
            return;
        }
        if (colonne < 0 || colonne >= 5)
        {
            Console.WriteLine("Erreur : colonne hors limites.");
            return;
        }

        // Placer la plante dans la case correspondante
        Grille[terrainIndex, colonne] = plante;
        Console.Write(Grille[terrainIndex, colonne]?.Emoji ?? " ");

        Console.WriteLine($"{plante.Nom} plantée dans le terrain {Terrains[terrainIndex].Nom} à la colonne {colonne + 1}.");
    }

    // Méthode pour afficher le jardin
    // Fct pr afficher chaque terrain avec ses plantes via grille
    public void Afficher(Menu menu)
    {
        int colonnes = 6;  // Toujours 6 colonnes
        int lignes = (int)Math.Ceiling(Terrains.Length / (double)colonnes); // nb de lignes selon nb terrains

        // Afficher chaque terrain dans une grille de 6 colonnes
        for (int ligne = 0; ligne < lignes; ligne++)
        {
            for (int col = 0; col < colonnes; col++)
            {
                int index = ligne * colonnes + col; // index du terrain
                if (index < Terrains.Length)
                {
                    // Appel : on passe menu, grille centrale et index du terrain
                    Terrains[index].Afficher(menu, Grille, index);
                    Console.WriteLine(); // retour à la ligne entre terrains
                }
            }
        }
    }

    // Méthode pour naviguer entre les terrains
    public void Naviguer()
    {
        int indexSelectionne = 0; // terrain actif
        int nbTerrains = Terrains.Length;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"\nTerrain sélectionné : {indexSelectionne + 1}/{nbTerrains}");

            // Navigation avec flèches
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (indexSelectionne > 0)
                        indexSelectionne--; // terrain précédent
                    break;

                case ConsoleKey.DownArrow:
                    if (indexSelectionne < nbTerrains - 1)
                        indexSelectionne++; // terrain suivant
                    break;

                case ConsoleKey.Enter:
                    InteragirAvecTerrain(Terrains[indexSelectionne]); // affiche infos terrain
                    break;
            }
        }
    }

    // Afficher les détails d'un terrain
    private void InteragirAvecTerrain(Terrain terrain)
    {
        Console.WriteLine($"\nVous avez sélectionné le terrain : {terrain.Nom}");
        Console.WriteLine($"Type de sol : {terrain.TypeDeSol}");
        Console.WriteLine("Appuyez sur une touche pour revenir.");
        Console.ReadKey(true);
    }
}
