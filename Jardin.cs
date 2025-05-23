///
///  Classe pr gérer jardin (stocke terrains + grille plantes)
/// 
/// 
public class Jardin
{
    public Terrain[] Terrains { get; private set; }
    private Plantes?[,] Grille;// Tableau pour gérer la grille du jardin (6 colonnes)
    private float[,] EauParcelle; // Eau en litres pour chaque parcelle
    public List<Indesirables> IndesirablesDansJardin { get; set; } = new List<Indesirables>();
    private Dictionary<string, int> graines; // Répertorier les plantes disponibles et quantité de graines
    private JeuEnsemence jeuTitre = new JeuEnsemence();

    // crée tableau terrains + init grille plantation
    public Jardin(Menu menu, Dictionary<string, int> grainesInitiales)
    {
        int nombreDeTerrains = menu.NbTerrains; // Nombre de terrains (2, 4, ou 6 terrains)
        this.graines = grainesInitiales;

        Terrains = new Terrain[nombreDeTerrains];  // Tableau pour stocker terrains
        Grille = new Plantes?[nombreDeTerrains, 6]; // Grille pour stocker plantes par terrain + colonne

        EauParcelle = new float[nombreDeTerrains, 6];
        for (int i = 0; i < nombreDeTerrains; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                EauParcelle[i, j] = 1.0f; // 1L par défaut
            }

        }
        for (int i = 0; i < nombreDeTerrains; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                EauParcelle[i, j] = 1.0f;     // 1 litre d'eau par défaut
            }
        }


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
        Console.WriteLine(Grille[terrainIndex, colonne]?.Emoji ?? " ");

        Console.WriteLine($"{plante.Nom} plantée dans le terrain {Terrains[terrainIndex].Nom} à la colonne {colonne + 1}.");
    }

    public void PlanterAutoGrille(int terrainIndex, int colonne, Plantes plante, DateOnly dateActuelle) // Pas fonctionnel
    {
        if (plante is PlanteVivace vivace)
        {
            vivace.InitialiserPlantation(dateActuelle); // initialise pour repousse
        }

        PlanterDansGrille(terrainIndex, colonne, plante);
    }

    public void ReplanterVivaces(DateOnly dateActuelle) // Pas fonctionnel
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

        Console.WriteLine("\n--- INDESIRABLES DANS LE JARDIN ---"); // inutile ?
        foreach (var ind in IndesirablesDansJardin)
        {
            if (ind.EstPresent)
            {
                Console.WriteLine($"🔸 {ind.Nom} {ind.Icone} sur le terrain {ind.LigneTerrain}, colonne {ind.ColonneActuelle}");
            }
        }

    }

    public void AffichageInteractif(Temporalite temp, Meteo meteo) // Interface utilisateur
    {
        int terrainIndex = 0;
        int colonne = 0;
        bool continuer = true;

        while (continuer)
        {
            Console.Clear();
            JeuEnsemence.AfficherTitre();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"📅 Date : {temp.DateActuelle}");
            Console.WriteLine();

            Console.WriteLine($"🗓️ Saison : {temp.SaisonActuelle.Nom}");
            Console.WriteLine();

            string effetMeteo = meteo.EvenementMeteo switch // Messages explicatifs pour les joueurs
            {
                "Pluie torrentielle" => "+2L sur toutes les parcelles",
                "Pluie" => "+1L sur toutes les parcelles",
                "Gel" => "-0.2L sur toutes les parcelles, Temp = 0°C",
                "Sécheresse" => "-0.3L, +3°C",
                "Canicule" => "-0.7L, +5°C",
                "Temps normal" or null => "aucun effet",
                _ => "effet inconnu"
            };

            Console.WriteLine($"🌤️ Météo : {meteo.EvenementMeteo ?? "Temps normal"} ({effetMeteo}) | 🌡️ Température : {temp.SaisonActuelle.Temperature}°C\n");

            Console.WriteLine("< > Flèches = naviguer | P = planter | A = arroser | R = récolter | B = bloquer | Entrée = tour suivant\n");

            for (int i = 0; i < Terrains.Length; i++) // Déplacement dans le jardin
            {
                for (int j = 0; j < 6; j++)
                {
                    var plante = GetPlante(i, j);
                    string emoji = plante?.Afficher() ?? "   ";
                    bool estSelectionnee = (i == terrainIndex && j == colonne);

                    string icone;
                    var indesirable = Indesirables.IndesirableActuel;
                    if (indesirable != null && indesirable.EstPresent && indesirable.LigneTerrain == i && indesirable.ColonneActuelle == j)
                    {
                        icone = indesirable.Icone;
                    }
                    else
                    {
                        icone = "  ";
                    }

                    string reset = "\x1b[0m";

                    if (estSelectionnee)
                    {
                        Console.Write("\x1b[47m" + emoji + icone + " " + reset + "  ");
                    }
                    else
                    {
                        Console.Write(Terrains[i].Couleur + emoji + icone + " " + reset + "  ");
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
            }

            AfficherInfosParcelle(terrainIndex, colonne, temp);

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key) //Actions possibles du joueur
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
                    GererActionParcelle(terrainIndex, colonne, "planter", temp, meteo);
                    break;
                case ConsoleKey.A:
                    GererActionParcelle(terrainIndex, colonne, "arroser", temp, meteo);
                    break;
                case ConsoleKey.R:
                    GererActionParcelle(terrainIndex, colonne, "cueillir", temp, meteo);
                    break;
                case ConsoleKey.B:
                    BloquerIndesirable(terrainIndex, colonne);
                    break;
                case ConsoleKey.Enter:
                    continuer = false;
                    break;
            }
        }
    }

    private void AfficherInfosParcelle(int terrainIndex, int colonne, Temporalite temp) // Permet d'afficher spécifiquement les informations de la parcelle sélectionnée par le joueur
    {
        var terrain = Terrains[terrainIndex];
        var plante = GetPlante(terrainIndex, colonne);

        float eau = EauParcelle[terrainIndex, colonne];

        Console.WriteLine($"   {Terrains[terrainIndex].Nom}  ");
        Console.WriteLine("\n -----------------------------\n");
        Console.WriteLine($"   Type de sol : {terrain.TypeDeSol}");
        Console.WriteLine($"   Eau : {eau}L ");
        Console.WriteLine($"   Lumière : {temp.SaisonActuelle.TauxSoleil} h/jour");
        Console.WriteLine("\n-----------------------------\n");

        if (plante == null||plante.EstVivante == false)
        {
            Console.WriteLine("🌱 Aucune plante présente.");
        }
        else
        {
            Console.WriteLine($"Semis : {plante.Nom}");
            Console.WriteLine($"Croissance : {plante.CroissanceActuelle} cm");
            Console.WriteLine($"Santé : {(int)(plante.EtatSante * 100)}%");
            Console.WriteLine($"Vivante : {(plante.EstVivante ? "Oui" : "Non")}");
            Console.WriteLine($"\n📋 BESOINS de {plante.Nom} :");
            Console.WriteLine($"Eau : {plante.BesoinEau} L/semaine");
            Console.WriteLine($"Lumière : {plante.BesoinLumiere} h/jour");
        }
    }

    private void GererActionParcelle(int terrainIndex, int colonne, string action, Temporalite temp, Meteo meteo) // Actions possibles du joueur sur la parcelle 
    {
        var plante = GetPlante(terrainIndex, colonne); //Récupérer si plante sur parcelle

        if (action == "planter") // Permet au joueur de planter une graine d'une des plantes proposées après avoir affiché les préférences de la plante 
        {
            if (plante == null||plante.EstVivante==false)
            {
                int indexSelection = 0;
                string[] plantesDispo = { "Hachich", "Coca", "Opium", "Salvia", "Khat", "Champi hallucinogène" };

                bool enSelection = true;
                while (enSelection)
                {
                    Console.Clear();
                    AffichageGrilleAvecInfos(terrainIndex, colonne, temp, meteo);

                    Console.WriteLine("\n Sélectionnez une plante à semer :\n");
                    for (int i = 0; i < plantesDispo.Length; i++)
                    {
                        string nom = plantesDispo[i];
                        int stock = graines.ContainsKey(nom) ? graines[nom] : 0;
                        string ligne = $"{nom} ({stock} graines)";
                        Console.WriteLine(i == indexSelection ? $"> {ligne}" : $"  {ligne}");
                    }

                    Console.WriteLine("\n Entrée pour valider / Échap pour annuler");

                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.UpArrow)
                    {
                        indexSelection = (indexSelection - 1 + plantesDispo.Length) % plantesDispo.Length;
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        indexSelection = (indexSelection + 1) % plantesDispo.Length;
                    }
                    else if (key == ConsoleKey.Enter)
                    {
                        string planteNom = plantesDispo[indexSelection];
                        if (!graines.ContainsKey(planteNom) || graines[planteNom] <= 0)
                        {
                            Console.WriteLine($"\n Plus de graines disponibles pour {planteNom} !");
                            Thread.Sleep(1000);
                            continue;
                        }

                        Console.Clear();
                        AffichageGrilleAvecInfos(terrainIndex, colonne, temp, meteo);
                        Console.WriteLine($"\n Plante sélectionnée : {planteNom}\n");

                        Plantes? planteChoisie = planteNom switch
                        {
                            "Hachich" => new Hachich(),
                            "Coca" => new Coca(),
                            "Opium" => new Opium(),
                            "Salvia" => new Salvia(),
                            "Khat" => new Khat(),
                            "Champi hallucinogène" => new ChampiHallucinogene(),
                            _ => null
                        };

                        if (planteChoisie != null)
                        {
                            Console.WriteLine($"Terrain favorable : {planteChoisie.TerrainPrefere}");
                            Console.WriteLine($"Besoin d’eau : {planteChoisie.BesoinEau} L/semaine");
                            Console.WriteLine($"Besoin lumière : {planteChoisie.BesoinLumiere} h/jour");
                            Console.WriteLine($"Temp. idéale : {planteChoisie.TempPreferee} °C");
                            Console.WriteLine($"Espérance de vie : {planteChoisie.EsperanceDeVie} sem.");
                            Console.WriteLine("\nEntrée pour semer | Échap pour revenir");

                            var key2 = Console.ReadKey(true).Key;

                            if (key2 == ConsoleKey.Enter)
                            {
                                PlanterDansGrille(terrainIndex, colonne, planteChoisie);
                                graines[planteNom]--;
                                Console.WriteLine($"\n {planteChoisie.Nom} plantée !");
                                Thread.Sleep(1000);
                                enSelection = false;
                            }
                            else if (key2 == ConsoleKey.Escape)
                            {
                                // retour au menu de sélection
                            }
                        }
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        enSelection = false;
                    }
                }
            }
            else
            {
                Console.WriteLine("\n Une plante est déjà présente !");
                Thread.Sleep(1000);
            }
        }
        else if (action == "arroser") // Permet au joueur d'ajouter de l'eau dans la parcelle sélectionnée
        {
            if (plante != null)
            {
                EauParcelle[terrainIndex, colonne] += 1.0f;
                Console.WriteLine("\n Vous avez arrosé la plante ! (+1L)");
            }
            else
            {
                Console.WriteLine("\n Pas de plante à arroser !");
            }

            Thread.Sleep(1000);
        }
        else if (action == "cueillir") // Permet au joueur, lorsque la plante est prête, de cueillir celle-ci et de récupérer de nouvelles graines
        {
            if (plante != null && plante.CroissanceActuelle > 75 && plante.EstVivante)
            {
                Console.WriteLine($"\n Vous avez récolté {plante.Nom} !");
                if (graines.ContainsKey(plante.Nom!))
                {
                    graines[plante.Nom!] += plante.Fruits;
                }
                else
                {
                    graines[plante.Nom!] = plante.Fruits;
                }
                Grille[terrainIndex, colonne] = null;
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("\n Patience... la plante n'est pas prête à être récoltée !");
                Thread.Sleep(1000);
            }
        }
    }

    private void AffichageGrilleAvecInfos(int terrainIndex, int colonne, Temporalite temp, Meteo meteo)
    {
        JeuEnsemence.AfficherTitre();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine($"📅 Date : {temp.DateActuelle}");
        Console.WriteLine();
        Console.WriteLine($"🗓️ Saison : {temp.SaisonActuelle.Nom}");
        Console.WriteLine();
        string effetMeteo = meteo.EvenementMeteo switch
        {
            "Pluie torrentielle" => "+2L sur toutes les parcelles",
            "Pluie" => "+1L sur toutes les parcelles",
            "Gel" => "-0.2L + Temp = 0°C",
            "Sécheresse" => "-0.3L, +3°C",
            "Canicule" => "-0.7L, +5°C",
            "Orage" => "-0.2L",
            "Brouillard" => "-30% lumière",
            "Temps normal" or null => "aucun effet",
            _ => "effet inconnu"
        };

        Console.WriteLine($"🌤️ Météo : {meteo.EvenementMeteo ?? "Temps normal"} ({effetMeteo}) | 🌡️ Température : {temp.SaisonActuelle.Temperature}°C\n");

        Console.WriteLine("< > Flèches = naviguer | P = planter | A = arroser | R = récolter | B = bloquer | Entrée = tour suivant\n");

        string reset = "\x1b[0m";

        for (int i = 0; i < Terrains.Length; i++)
        {
            string ligneComplete = "";


            for (int j = 0; j < 6; j++)
            {
                var plante = GetPlante(i, j);
                string emoji = plante?.Afficher() ?? "   ";

                bool estSelectionnee = (i == terrainIndex && j == colonne);

                string icone;
                var indesirable = Indesirables.IndesirableActuel;
                if (indesirable != null && indesirable.EstPresent && indesirable.LigneTerrain == i && indesirable.ColonneActuelle == j)
                {
                    icone = indesirable.Icone;
                }
                else
                {
                    icone = "  ";
                }

                string bloc;
                if (estSelectionnee)
                {
                    bloc = "\x1b[47m" + emoji + icone + " " + reset + "  "; // fond blanc sélectionné
                }
                else
                {
                    bloc = Terrains[i].Couleur + emoji + icone + " " + reset + "  ";
                }

                ligneComplete += bloc;
            }

            Console.WriteLine(ligneComplete);
            Console.WriteLine();  // saut de ligne entre terrains
        }

        AfficherInfosParcelle(terrainIndex, colonne, temp);
    }

    public void AppliquerEffetsMeteo(Meteo meteo) //Appliquer effet météo sur l'eau des parcelles 
    {
        for (int i = 0; i < Terrains.Length; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                // Évaporation naturelle
                EauParcelle[i, j] -= 0.3f;

                // Effets météo
                if (meteo.EvenementMeteo == "Canicule") EauParcelle[i, j] -= 0.7f;
                if (meteo.EvenementMeteo == "Gel") EauParcelle[i, j] -= 0.2f;
                if (meteo.EvenementMeteo == "Pluie") EauParcelle[i, j] += 1.0f;
                if (meteo.EvenementMeteo == "Pluie torrentielle") EauParcelle[i, j] += 2.0f;

                // Bornes de sécurité
                if (EauParcelle[i, j] < 0) EauParcelle[i, j] = 0;
                if (EauParcelle[i, j] > 10) EauParcelle[i, j] = 10;

                //Pour avoir des chiffres qui ont seulement 1 chiffre après la virgule
                EauParcelle[i, j] = (float)Math.Round(EauParcelle[i, j], 1);

            }
        }
    }

    private void BloquerIndesirable(int terrainIndex, int colonne)
    {
        var indesirable = Indesirables.IndesirableActuel;

        if (indesirable != null && indesirable.EstPresent && indesirable.LigneTerrain == terrainIndex && indesirable.ColonneActuelle == colonne)
        {
            var solutions = new Solutions("", "").GenererSolutions();
            int indexSelection = 0;
            bool enSelection = true;

            while (enSelection)
            {
                Console.Clear();
                AffichageGrilleAvecInfos(terrainIndex, colonne, new Temporalite(DateOnly.FromDateTime(DateTime.Now)), new Meteo());

                Console.WriteLine($"\n Sélectionnez un objet pour bloquer {indesirable.Nom} :\n");
                for (int i = 0; i < solutions.Count; i++)
                {
                    string ligne = $"{solutions[i].Nom} {solutions[i].Icone}";
                    Console.WriteLine(i == indexSelection ? $"> {ligne}" : $"  {ligne}");
                }

                Console.WriteLine("\nEntrée pour appliquer / Échap pour annuler");

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    indexSelection = (indexSelection - 1 + solutions.Count) % solutions.Count;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    indexSelection = (indexSelection + 1) % solutions.Count;
                }
                else if (key == ConsoleKey.Enter)
                {
                    var solutionChoisie = solutions[indexSelection];
                    if (solutionChoisie.Nom == indesirable.Solution)
                    {
                        Console.WriteLine($"\n Bravo ! {indesirable.Nom} a été bloqué avec succès !");
                        Thread.Sleep(1500);
                        indesirable.EstPresent = false;
                        Indesirables.IndesirableActuel = null;
                    }
                    else
                    {
                        Console.WriteLine("\n Ce n'est pas la bonne solution...");
                    }
                    Thread.Sleep(1500);
                    enSelection = false;
                }
                else if (key == ConsoleKey.Escape)
                {
                    enSelection = false;
                }
            }
        }
        else
        {
            Console.WriteLine("\n Aucun intrus n'est sur cette case !");
            Thread.Sleep(1500);
        }
    }
}