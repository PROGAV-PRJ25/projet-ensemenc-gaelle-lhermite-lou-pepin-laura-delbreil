using System;
/// <summary>
/// 
/// Classe principale contenant point d'entrée du programme (Main)
/// Gère lancement jeu, menu, initialisation jardin et boucle principale des tours.
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

                // Lance menu et permet de récupérer les choix du joueur (durée + nb terrains)
                Menu menu = new Menu();
                menu.Demarrer();

                // Initialisation des plantes du jeu et du nombre de graines disponibles en début de partie
                Dictionary<string, int> graines = new Dictionary<string, int>
                {
                        { "Hachich", 5 },
                        { "Coca", 5 },
                        { "Opium", 5 },
                        { "Salvia", 5 },
                        { "Khat", 5 },
                        { "Champi hallucinogène", 5 }
                };

                //Création liste indésirables
                Indesirables indesirable = new Indesirables("", "", "", "", 0, menu);
                List<Indesirables> listeIndesirables = indesirable.GenererIndesirables(menu);

                // Crée temporalité à partir d'une date fixe 
                Temporalite temp = new Temporalite(DateOnly.Parse("2025-05-22"));

                // Crée objet météo 
                Meteo meteo = new Meteo();

                // Récupération des infos et création du jardin
                Jardin jardin = new Jardin(menu, graines);

                // Affiche état actuel du jardin 
                jardin.Afficher(menu);
                // Appel météo au lancement de la partie
                meteo.GenererEvenement(temp.SaisonActuelle, temp);
                meteo.ModifierValeursSaison(temp.SaisonActuelle);

                // Affichage du jardin
                jardin.AffichageInteractif(temp, meteo);

                // Début boucle principale (1 tour = 14j), durée selon années choisies
                DateOnly dateFin = temp.DateDebut.AddYears(menu.DureeAnnees);

                while (temp.DateActuelle < dateFin) // boucle de jeu
                {
                        Console.Clear();
                        jardin.AppliquerEffetsMeteo(meteo);
                        meteo.GenererEvenement(temp.SaisonActuelle, temp);
                        indesirable.GererIndesirables(jardin, temp);

                        // 
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

                        // Affichage des messages d'alertes d'urgence
                        AfficherAlertesSiUrgence(temp, meteo);

                        // Affiche le jardin avec interactions joueur
                        jardin.AffichageInteractif(temp, meteo);

                        // Croissance des plantes
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
                                                {
                                                        plante.ToursDepuisMort = 1;
                                                }
                                        }
                                }

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
                                                {
                                                        plante.ToursDepuisMort = 1;
                                                }
                                        }
                                }
                        }

                        // Passe au tour suivant et avance la date de 14 jours
                        temp.AvancerTemps();
                }
        }

        static bool alerteDejaAffichee = false; 
        static void AfficherAlertesSiUrgence(Temporalite temp, Meteo meteo) // Permet d'afficher l'alerte des situations d'urgence une seule fois
        {
                if (temp is TempoUrgence && !alerteDejaAffichee)
                {
                        bool affichage = false;

                        if (!string.IsNullOrEmpty(meteo.EvenementMeteo) && meteo.EvenementMeteo != "Temps normal")
                        {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine();
                                JeuEnsemence.CentrerTexte($"🚨 URGENCE MÉTÉO : {meteo.EvenementMeteo.ToUpper()} ! 🚨");
                                Console.ResetColor();
                                affichage = true;
                        }

                        if (Indesirables.IndesirableActuel != null && Indesirables.IndesirableActuel.EstPresent)
                        {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine();
                                JeuEnsemence.CentrerTexte($"🚨 URGENCE INTRUS : {Indesirables.IndesirableActuel.Nom.ToUpper()} DANS VOTRE JARDIN ! 🚨");
                                Console.ResetColor();
                                affichage = true;
                        }

                        if (affichage)
                        {
                                Console.WriteLine();
                                JeuEnsemence.CentrerTexte("Appuyez sur une touche pour continuer...");
                                Console.ReadKey(true);
                        }

                        alerteDejaAffichee = true;
                }

                // Réinitialise quand on sort du mode urgence pour prochaine situation
                if (!(temp is TempoUrgence))
                {
                        alerteDejaAffichee = false;
                }
        }

}


