public class Jardin
{
    public Terrain[] Terrains { get; private set; }
    private Plantes?[,] Grille;  // Tableau pour gérer la grille du jardin (6 colonnes)
    
    // Constructeur
    public Jardin(Menu menu)
    {
        int nombreDeTerrains = menu.NbTerrains; // Nombre de terrains (2, 4, ou 6 terrains)
        Terrains = new Terrain[nombreDeTerrains];  // Tableau pour stocker les terrains
        Grille = new Plantes?[nombreDeTerrains, 6]; // Tableau pour gérer les plantes dans une grille de 6 colonnes

        Random rand = new Random();

        // Créer les terrains et les stocker dans le tableau
        for (int i = 0; i < nombreDeTerrains; i++)
        {
            string[] types = { "terre", "sable", "argile" };
            string type = types[rand.Next(types.Length)];
            string nom = $"Terrain_{i + 1}"; // Nom généré automatiquement

            Terrains[i] = type switch
            {
                "terre" => new TerrainTerreux(nom, 4),
                "sable" => new TerrainSableux(nom, 4),
                "argile" => new TerrainArgileux(nom, 4),
                _ => throw new Exception("Type de terrain inconnu.")
            };
        }
    }

    // Méthode pour planter une plante dans la grille
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

        Console.WriteLine($"{plante.Nom} plantée dans le terrain {Terrains[terrainIndex].Nom} à la colonne {colonne+1}.");
    }

    // Méthode pour afficher le jardin
    public void Afficher(Menu menu)
{
    int colonnes = 6;  // Toujours 6 colonnes
    int lignes = (int)Math.Ceiling(Terrains.Length / (double)colonnes); // Calculer le nombre de lignes en fonction du nombre de terrains

    // Afficher chaque terrain dans une grille de 6 colonnes
    for (int ligne = 0; ligne < lignes; ligne++)
    {
        for (int col = 0; col < colonnes; col++)
        {
            int index = ligne * colonnes + col; // Calculer l'index du terrain
            if (index < Terrains.Length)
            {
                // Appel corrigé : on passe menu, la grille centrale et l'index du terrain
                Terrains[index].Afficher(menu, Grille, index);
                Console.WriteLine(); // Espacement entre les terrains
            }
        }
    }
}




    

    // Méthode pour naviguer entre les terrains
    public void Naviguer()
    {
        int indexSelectionne = 0; // Terrain initialement sélectionné
        int nbTerrains = Terrains.Length;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"\nTerrain sélectionné : {indexSelectionne + 1}/{nbTerrains}");

            // Navigation avec les flèches directionnelles
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (indexSelectionne > 0)
                        indexSelectionne--; // Aller vers le terrain précédent
                    break;

                case ConsoleKey.DownArrow:
                    if (indexSelectionne < nbTerrains - 1)
                        indexSelectionne++; // Aller vers le terrain suivant
                    break;

                case ConsoleKey.Enter:
                    InteragirAvecTerrain(Terrains[indexSelectionne]); // Afficher des infos sur le terrain sélectionné
                    break;
            }
        }
    }

    // Afficher les détails d'un terrain
    private void InteragirAvecTerrain(Terrain terrain)
    {
        // Afficher des informations simples sur le terrain sélectionné
        Console.WriteLine($"\nVous avez sélectionné le terrain : {terrain.Nom}");
        Console.WriteLine($"Type de sol : {terrain.TypeDeSol}");
        Console.WriteLine("Appuyez sur une touche pour revenir.");
        Console.ReadKey(true); // Attendre que l'utilisateur appuie sur une touche pour revenir
    }
}
