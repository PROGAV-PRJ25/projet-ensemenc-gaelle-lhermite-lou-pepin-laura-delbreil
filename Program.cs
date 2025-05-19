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

        // Crée instance du jardin avec choix récupérés depuis menu
        Jardin jardin = new Jardin(menu);

        // Ex d'appel a supprimer après!!!!!
        Hachich h = new Hachich();
        jardin.PlanterDansGrille(2, 4, h);

        // Affiche état actuel du jardin 
        jardin.Afficher(menu);

        // Crée temporalité à partir d'une date fixe 
        Temporalite temp = new Temporalite(DateOnly.Parse("2025-05-22"));

        // Crée objet météo 
        Meteo meteo = new Meteo();

        // Début boucle principale (1 tour = 14j), durée selon années choisies
        int nbTours = (menu.DureeAnnees * 365) / 14; // problème dans ça

        for (int tour = 0; tour < nbTours; tour++)
        {
            Console.Clear();
            Console.WriteLine($"Tour {tour + 1}/{nbTours}");
            Console.WriteLine($"📅 Date : {temp.DateActuelle} | Saison : {temp.SaisonActuelle.Nom}");

            // Appelle météo pr générer aléatoirement un événement météo
            meteo.GenererEvenement(temp.SaisonActuelle, temp);

            // Si urgence météo déclenchée, switch en mode urgence
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

            Console.WriteLine(temp is TempoUrgence ? "⚠️ MODE URGENCE" : "✅ MODE NORMAL");

            for (int ligne = 0; ligne < jardin.Terrains.Length; ligne++)
            {
                for (int col = 0; col < 6; col++)
                {
                    var plante = jardin.GetPlante(ligne, col);
                    if (plante != null)
                    {
                        string typeTerrain = jardin.Terrains[ligne].GetType().Name.ToLower();
                        plante.EvaluerCroissance(temp.SaisonActuelle, meteo, typeTerrain);

                        if (!plante.EstVivante && plante.ToursDepuisMort == 0)
                            plante.ToursDepuisMort = 1;
                    }
                }
            }

            // Affiche jardin en fin de tour (tous terrains avec leurs plantes)
            jardin.Afficher(menu);

            Console.WriteLine("\nAppuyez sur Entrée pour passer au prochain tour...");
            Console.ReadLine();

            // Passe au tour suivant (avance date de 14 jours)
            temp.AvancerTemps();
        }

        Console.WriteLine("\n🎉 Fin de la partie ! Merci d'avoir joué !");
    }
}
