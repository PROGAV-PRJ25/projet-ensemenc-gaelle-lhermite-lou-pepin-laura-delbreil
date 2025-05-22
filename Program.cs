using System;

/// <summary>
/// 
/// Classe principale contenant point d'entrée du programme (Main)
/// Gère lancement jeu, menu, initialisation jardin, boucle principale des tours
/// 
/// </summary>
class Program
{
        // lance tout le déroulé du jeu
        static void Main(string[] args)
        {
                // Affiche intro du jeu via classe JeuEnsemence
                JeuEnsemence jeu = new JeuEnsemence();
                jeu.LancerMenuPrincipal();

                // Lance menu pr récupérer choix (durée + nb terrains)
                Menu menu = new Menu();
                menu.Demarrer();

                // Juste après la création du jardin :
                Jardin jardin = new Jardin(menu);

                // Affiche état actuel du jardin 
                jardin.Afficher(menu);

                // Crée temporalité à partir d'une date fixe 
                Temporalite temp = new Temporalite(DateOnly.Parse("2025-05-22"));



                // Crée objet météo 
                Meteo meteo = new Meteo();

                //Créer liste indésirables
                Indesirables indesirable = new Indesirables("", "", "", "", 0, menu);
                List<Indesirables> listeIndesirables = indesirable.GenererIndesirables(menu);

                // Début boucle principale (1 tour = 14j), durée selon années choisies
                DateOnly dateFin = temp.DateDebut.AddYears(menu.DureeAnnees);

                // >>> AJOUT POUR TESTER AFFICHAGE INTERACTIF <<<
                // Tu peux mettre ça TEMPORAIREMENT si tu veux tester :
                jardin.AffichageInteractif(temp, meteo);


                while (temp.DateActuelle < dateFin)
                {
                        jardin.AffichageInteractif(temp, meteo);


                        Console.Clear();


                        meteo.GenererEvenement(temp.SaisonActuelle, temp);

                        if (temp.EtatUrgence && temp.GetType() != typeof(TempoUrgence))
                        {
                                temp = new TempoUrgence(temp.DateActuelle)
                                {
                                        Ete = temp.Ete,
                                        Automne = temp.Automne,
                                        Hiver = temp.Hiver,
                                        Printemps = temp.Printemps,
                                };
                        }
                        else if (!temp.EtatUrgence && temp.GetType() == typeof(TempoUrgence))
                        {
                                temp = new Temporalite(temp.DateActuelle)
                                {
                                        Ete = temp.Ete,
                                        Automne = temp.Automne,
                                        Hiver = temp.Hiver,
                                        Printemps = temp.Printemps,
                                };
                        }

                        for (int ligne = 0; ligne < jardin.Terrains.Length; ligne++)
                        {
                                for (int col = 0; col < 6; col++)
                                {
                                        var plante = jardin.GetPlante(ligne, col);
                                        if (plante != null)
                                        {
                                                string typeTerrain = jardin.Terrains[ligne].GetType().Name.ToLower();
                                                plante.EvaluerCroissance(temp.SaisonActuelle, meteo, temp, typeTerrain);

                                                if (!plante.EstVivante && plante.ToursDepuisMort == 0)
                                                        plante.ToursDepuisMort = 1;
                                        }
                                }
                        }

                        Console.WriteLine("\nAppuyez sur Entrée pour passer au prochain tour...");
                        Console.ReadLine();

                        temp.AvancerTemps(); // Avance de 1 ou 14 jours selon mode
                }


                Console.WriteLine(temp is TempoUrgence ? "⚠️ MODE URGENCE" : "✅ MODE NORMAL");

                for (int ligne = 0; ligne < jardin.Terrains.Length; ligne++)
                {
                        for (int col = 0; col < 6; col++)
                        {
                                var plante = jardin.GetPlante(ligne, col);
                                if (plante != null)
                                {
                                        string typeTerrain = jardin.Terrains[ligne].GetType().Name.ToLower();
                                        plante.EvaluerCroissance(temp.SaisonActuelle, meteo, temp, typeTerrain);

                                        if (!plante.EstVivante && plante.ToursDepuisMort == 0)
                                                plante.ToursDepuisMort = 1;
                                }
                        }
                }

                // Passe au tour suivant (avance date de 14 jours)
                temp.AvancerTemps();

        }
    }

