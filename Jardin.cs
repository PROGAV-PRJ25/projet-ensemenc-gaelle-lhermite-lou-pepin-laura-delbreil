///
///  Classe pr gérer jardin (stocke terrains + grille plantes)
/// 
/// 
public class Jardin
{
    public Terrain[] Terrains { get; private set; }
    private Plantes?[,] Grille;// Tableau pour gérer la grille du jardin (6 colonnes)
    private float[,] EauParcelle; // eau en litres
    private int[,] LumiereParcelle;
    public List<Indesirables> IndesirablesDansJardin { get; set; } = new List<Indesirables>();
    private Dictionary<string, int> graines;




    // crée tableau terrains + init grille plantation
    public Jardin(Menu menu, Dictionary<string, int> grainesInitiales)
    {
        int nombreDeTerrains = menu.NbTerrains; // Nombre de terrains (2, 4, ou 6 terrains)
        this.graines = grainesInitiales;

        Terrains = new Terrain[nombreDeTerrains];  // Tableau pour stocker terrains
        Grille = new Plantes?[nombreDeTerrains, 6]; // Grille pour stocker plantes par terrain + colonne

        EauParcelle = new float[nombreDeTerrains, 6];
        for (int i = 0; i < nombreDeTerrains; i++)
            for (int j = 0; j < 6; j++)
                EauParcelle[i, j] = 1.0f; // 1L par défaut

        LumiereParcelle = new int[nombreDeTerrains, 6];

        for (int i = 0; i < nombreDeTerrains; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                EauParcelle[i, j] = 1.0f;     // 1 litre d'eau par défaut
                LumiereParcelle[i, j] = 60;   // 60% de lumière par défaut
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

        Console.WriteLine("\n--- INDESIRABLES DANS LE JARDIN ---");
        foreach (var ind in IndesirablesDansJardin)
        {
            if (ind.EstPresent)
            {
                Console.WriteLine($"🔸 {ind.Nom} {ind.Icone} sur le terrain {ind.LigneTerrain}, colonne {ind.ColonneActuelle}");
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

            Console.WriteLine($"📅 Date : {temp.DateActuelle}");
            Console.WriteLine($"🗓️ Saison : {temp.SaisonActuelle.Nom}");
            string effetMeteo = meteo.EvenementMeteo switch
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

            Console.WriteLine("🎮 Flèches = naviguer | P = planter | A = arroser | Entrée = tour suivant\n");

            string reset = "\x1b[0m";

            for (int i = 0; i < Terrains.Length; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    var plante = GetPlante(i, j);
                    string emoji = plante?.Afficher() ?? "   ";

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
                    bool estSelectionnee = (i == terrainIndex && j == colonne);
                    if (estSelectionnee)
                    {
                        Console.Write("\x1b[40m" + emoji + icone + " " + reset + "  ");
                    }
                    else
                    {
                        Console.Write(Terrains[i].Couleur + emoji + icone + " " + reset + "  ");
                    }


                }

                Console.WriteLine();
                Console.WriteLine();  // saut de ligne entre terrains
            }

            AfficherInfosParcelle(terrainIndex, colonne, temp);

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
                    GererActionParcelle(terrainIndex, colonne, "planter", temp, meteo);
                    break;
                case ConsoleKey.A:
                    GererActionParcelle(terrainIndex, colonne, "arroser", temp, meteo);
                    break;
                case ConsoleKey.R:
                    GererActionParcelle(terrainIndex, colonne, "cueillir", temp, meteo);
                    break;
                case ConsoleKey.Enter:
                    continuer = false;
                    break;
            }
        }
    }

    private void AfficherInfosParcelle(int terrainIndex, int colonne, Temporalite temp)
    {
        var terrain = Terrains[terrainIndex];
        var plante = GetPlante(terrainIndex, colonne);

        float eau = EauParcelle[terrainIndex, colonne];
        int tauxLumiere = LumiereParcelle[terrainIndex, colonne];

        Console.WriteLine($"   {Terrains[terrainIndex].Nom}  ");
        Console.WriteLine("\n -----------------------------\n");
        Console.WriteLine($"   Type de sol : {terrain.TypeDeSol}");
        Console.WriteLine($"   Eau : {eau}L ");
        Console.WriteLine($"   Lumière : {temp.SaisonActuelle.TauxSoleil} h/jour");
        Console.WriteLine(" \n-----------------------------\n");

        if (plante == null)
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


    private void GererActionParcelle(int terrainIndex, int colonne, string action, Temporalite temp, Meteo meteo)
    {
        var plante = GetPlante(terrainIndex, colonne);

        if (action == "planter")
        {
            if (plante == null)
            {
                int indexSelection = 0;
                string[] plantesDispo = { "Hachich", "Coca", "Opium", "Salvia", "Khat", "Champi hallucinogène" };

                bool enSelection = true;
                while (enSelection)
                {
                    Console.Clear();
                    AffichageGrilleAvecInfos(terrainIndex, colonne, temp, meteo);

                    Console.WriteLine("\n🌱 Sélectionnez une plante à semer :\n");
                    for (int i = 0; i < plantesDispo.Length; i++)
                    {
                        string nom = plantesDispo[i];
                        int stock = graines.ContainsKey(nom) ? graines[nom] : 0;
                        string ligne = $"{nom} ({stock} graines)";
                        Console.WriteLine(i == indexSelection ? $"> {ligne}" : $"  {ligne}");
                    }

                    Console.WriteLine("\n⬆️ / ⬇️ pour naviguer, Entrée pour valider, Échap pour annuler");

                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.UpArrow)
                        indexSelection = (indexSelection - 1 + plantesDispo.Length) % plantesDispo.Length;
                    else if (key == ConsoleKey.DownArrow)
                        indexSelection = (indexSelection + 1) % plantesDispo.Length;
                    else if (key == ConsoleKey.Enter)
                    {
                        string planteNom = plantesDispo[indexSelection];
                        if (!graines.ContainsKey(planteNom) || graines[planteNom] <= 0)
                        {
                            Console.WriteLine($"\n❌ Plus de graines disponibles pour {planteNom} !");
                            Thread.Sleep(1000);
                            continue;
                        }

                        Console.Clear();
                        AffichageGrilleAvecInfos(terrainIndex, colonne, temp, meteo);
                        Console.WriteLine($"\n🌿 Plante sélectionnée : {planteNom}\n");

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
                                Console.WriteLine($"\n✅ {planteChoisie.Nom} plantée !");
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
                Console.WriteLine("\n❌ Une plante est déjà présente !");
                Thread.Sleep(1000);
            }
        }
        else if (action == "arroser")
        {
            if (plante != null)
            {
                EauParcelle[terrainIndex, colonne] += 1.0f;
                Console.WriteLine("\n💧 Vous avez arrosé la plante ! (+1L)");
            }
            else
            {
                Console.WriteLine("\n❌ Pas de plante à arroser !");
            }

            Thread.Sleep(1000);
        }
        else if (action == "cueillir")
        {
            if (plante != null && plante.CroissanceActuelle > 75 && plante.EstVivante)
            {
                Console.WriteLine($"\n🌾 Vous avez récolté {plante.Nom} !");
                if (graines.ContainsKey(plante.Nom))
                    graines[plante.Nom] += plante.Fruits;
                else
                    graines[plante.Nom] = plante.Fruits;

                Grille[terrainIndex, colonne] = null;
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("\n❌ La plante n'est pas prête à être récoltée !");
                Thread.Sleep(1000);
            }
        }
    }



    private void AffichageGrilleAvecInfos(int terrainIndex, int colonne, Temporalite temp, Meteo meteo)
    {
        Console.WriteLine($"📅 Date : {temp.DateActuelle}");
        Console.WriteLine($"🗓️ Saison : {temp.SaisonActuelle.Nom}");
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

        Console.WriteLine("🎮 Flèches = naviguer | P = planter | A = arroser | Entrée = tour suivant\n");

        string reset = "\x1b[0m";

        for (int i = 0; i < Terrains.Length; i++)
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
                if (estSelectionnee)
                {
                    Console.Write("\x1b[40m" + emoji + icone + " " + reset + "  ");
                }
                else
                {
                    Console.Write(Terrains[i].Couleur + emoji + icone + " " + reset + "  ");
                }
            }

            Console.WriteLine();
            Console.WriteLine();  // saut de ligne entre terrains
        }

        AfficherInfosParcelle(terrainIndex, colonne, temp);
    }

    public void AppliquerLumiere(Saisons saison, Meteo meteo)
    {
        int baseLumiere = (int)saison.TauxSoleil;

        if (meteo.EvenementMeteo == "Canicule") baseLumiere += 20;
        if (meteo.EvenementMeteo == "Brouillard") baseLumiere -= 30;
        if (meteo.EvenementMeteo == "Orage") baseLumiere -= 20;

        baseLumiere = Math.Clamp(baseLumiere, 0, 100);

        for (int i = 0; i < Terrains.Length; i++)
            for (int j = 0; j < 6; j++)
                LumiereParcelle[i, j] = baseLumiere;
    }

    public void AppliquerEffetsMeteo(Meteo meteo)
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
            }
        }
    }


    public float GetEau(int terrainIndex, int colonne)
    {
        return EauParcelle[terrainIndex, colonne];
    }

    public int GetLumiere(int terrainIndex, int colonne)
    {
        return LumiereParcelle[terrainIndex, colonne];
    }
    
    public void EvaporationGenerale()
    {
        for (int i = 0; i < Terrains.Length; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                EauParcelle[i, j] -= 1.0f;
                if (EauParcelle[i, j] < 0)
                    EauParcelle[i, j] = 0;
            }
        }
    }

    
    

}