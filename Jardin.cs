public class Jardin
{
    public Terrain[] Terrains { get; private set; }

    // Constructeur
    public Jardin(Menu menu)
    {
        int nombreDeTerrains = menu.NbTerrains; // 2, 4, ou 6 terrains
        Terrains = new Terrain[nombreDeTerrains];  // Tableau pour stocker les terrains
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

    // Méthode pour afficher les terrains dans une grille
    public void Afficher(Menu menu)
    {
        int colonnes = 2;  // 2 colonnes
        int lignes = (int)Math.Ceiling(Terrains.Length / (double)colonnes); // Calculer le nombre de lignes en fonction du nombre de terrains

        // Afficher chaque terrain dans une grille
        for (int ligne = 0; ligne < lignes; ligne++)
        {
            for (int col = 0; col < colonnes; col++)
            {
                int index = ligne * colonnes + col; // Calculer l'index du terrain
                if (index < Terrains.Length)
                {
                    // Affichage du terrain avec la méthode Afficher() de chaque terrain
                    Terrains[index].Afficher(menu); 
                    Console.Write(""); // Espacement entre les terrains
                }
            }
            //Console.Write("  "); // Nouvelle ligne après chaque ligne de la grille
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
            //Afficher();  // Afficher les terrains sous forme de grille
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
