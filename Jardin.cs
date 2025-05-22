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
        if (colonne < 0 || colonne >= 6)
        {
            Console.WriteLine("Erreur : colonne hors limites.");
            return;
        }

        // Placer la plante dans la case correspondante
        Grille[terrainIndex, colonne] = plante;
        Console.Write(Grille[terrainIndex, colonne]?.Emoji ?? " ");

        Console.WriteLine($"{plante.Nom} plantée dans le terrain {Terrains[terrainIndex].Nom} à la colonne {colonne + 1}.");
    }

    public void PlanterAutoGrille(int terrainIndex, int colonne, Plantes plante, DateOnly dateActuelle)
    {
        if (plante is PlanteVivace vivace)
        {
            vivace.InitialiserPlantation(dateActuelle); // initialise pour repousse
        }

        PlanterDansGrille(terrainIndex, colonne, plante);
    }

public void ReplanterVivaces(DateOnly dateActuelle)
{
    for (int ligne = 0; ligne < Terrains.Length; ligne++)
    {
        for (int col = 0; col < 6; col++)
        {
            var plante = GetPlante(ligne, col);
            if (plante is PlanteVivace vivace && vivace.PretAPousser(dateActuelle))
            {
                Console.WriteLine($"🌿 {plante.Nom} repousse automatiquement à [{ligne},{col}] !");
                vivace.EstVivante = true;
                vivace.EtatSante = 0.5f;
                vivace.CroissanceActuelle = 0;
                vivace.DatePlantation = dateActuelle;
                vivace.DateReplantation = dateActuelle.AddYears(1);
            }
        }
    }
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

    public void AffichageInteractif(Temporalite temp, Meteo meteo)
    {
        int terrainIndex = 0;
        int colonne = 0;
        bool continuer = true;

        while (continuer)
        {
            Console.Clear();

            // Informations générales
            Console.WriteLine($"📅 Date : {temp.DateActuelle}");
            Console.WriteLine($"🗓️ Saison : {temp.SaisonActuelle.Nom}");
            Console.WriteLine($"🌤️ Météo : {meteo.EvenementMeteo ?? "Temps normal"}\n");
            Console.WriteLine("🎮 Flèches = naviguer | P = planter | A = arroser | Entrée = tour suivant\n");

            string reset = "\x1b[0m";

            // Affichage des terrains un par un (comme dans ton code original)
            for (int i = 0; i < Terrains.Length; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    var plante = GetPlante(i, j);
                    string emoji = plante?.Afficher() ?? "   ";
                    bool estSelectionnee = (i == terrainIndex && j == colonne);

                    if (estSelectionnee)
                    {
                        Console.Write("\x1b[40m" + emoji + reset + "  "); // fond noir
                    }
                    else
                    {
                        Console.Write(Terrains[i].Couleur + emoji + reset + "  ");
                    }
                }

                Console.WriteLine();
                Console.WriteLine();  // retour à la ligne entre chaque terrain
            }

            // Affichage des infos de la parcelle sélectionnée
            AfficherInfosParcelle(terrainIndex, colonne);

            // Lecture de la touche
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.RightArrow:
                    colonne = (colonne + 1) % 6;
                    break;
                case ConsoleKey.LeftArrow:
                    colonne = (colonne - 1 + 6) % 6;
                    break;
                case ConsoleKey.UpArrow:
                    terrainIndex = (terrainIndex - 1 + Terrains.Length) % Terrains.Length;
                    break;
                case ConsoleKey.DownArrow:
                    terrainIndex = (terrainIndex + 1) % Terrains.Length;
                    break;
                case ConsoleKey.P:
                    GererActionParcelle(terrainIndex, colonne, "planter");
                    break;
                case ConsoleKey.A:
                    GererActionParcelle(terrainIndex, colonne, "arroser");
                    break;
                case ConsoleKey.Enter:
                    continuer = false; // passe au tour suivant
                    break;
            }
        }
    }



    private void AfficherInfosParcelle(int terrainIndex, int colonne)
    {
        var terrain = Terrains[terrainIndex];
        var plante = GetPlante(terrainIndex, colonne);

        Console.WriteLine("\n📍 Informations sur la parcelle sélectionnée :");
        Console.WriteLine($"🔷 Terrain : {terrain.Nom} ({terrain.TypeDeSol})");
        Console.WriteLine($"💧 Eau : {(terrain.PourvuEnEau == true ? "Oui" : "Non")} | ☀️ Lumière : {(terrain.ALaLumiere == true ? "Oui" : "Non")}");

        if (plante == null)
        {
            Console.WriteLine("🌱 Aucune plante présente.");
            Console.WriteLine("✅ Appuyez sur Entrée pour planter ici.");
        }
        else
        {
            Console.WriteLine($"🌿 Plante : {plante.Nom}");
            Console.WriteLine($"📈 Croissance : {plante.CroissanceActuelle} cm");
            Console.WriteLine($"❤️ Santé : {(int)(plante.EtatSante * 100)}%");
            Console.WriteLine($"🧬 Vivante : {(plante.EstVivante ? "Oui" : "Non")}");
            Console.WriteLine("💦 Appuyez sur Entrée pour arroser (simulation).");
        }
    }

    private void GererActionParcelle(int terrainIndex, int colonne, string action)
    {
        var plante = GetPlante(terrainIndex, colonne);

        if (action == "planter")
        {
            if (plante == null)
            {
                Console.WriteLine("\n🌱 Plantation en cours...");
                PlanterDansGrille(terrainIndex, colonne, new Hachich());
            }
            else
            {
                Console.WriteLine("\n❌ Une plante est déjà présente !");
            }
        }
        else if (action == "arroser")
        {
            if (plante != null)
            {
                Console.WriteLine("\n💦 Vous avez arrosé la plante ! (effet à coder si besoin)");
                // Tu peux ajouter ici un effet si tu veux (ex: +santé)
            }
            else
            {
                Console.WriteLine("\n❌ Pas de plante à arroser !");
            }
        }

        Thread.Sleep(1000); // petite pause
    }




}
