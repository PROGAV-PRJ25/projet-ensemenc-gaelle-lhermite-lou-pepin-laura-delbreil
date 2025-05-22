/// <summary>
/// 
/// Classe pr gérer passage du temps classique dans le jeu (mode normal)
/// Gère date, saison actuelle et changement de mode urgence si déclenché
/// 
/// </summary>
public class Temporalite //Classe pour la temporalité classique
{
    public DateOnly DateDebut{get; protected set;} //Date de départ du simulateur en format DateDebut
    public DateOnly DateActuelle{get; protected set;} //Date au moment du tour de jeu
    public int SautsTemps{get; protected set;} // Sauts dans le temps (de 14 jours pour la version classique)

    public Saisons SaisonActuelle {get; set;} //Saison dans laquelle on est dans le jeu
    public Saisons Ete {get; set;}
    public Saisons Automne {get; set;}
    public Saisons Hiver {get; set;}
    public Saisons Printemps {get; set;}
    public bool EtatUrgence {get; set;} // vérification de si on est en état d'urgence ou pas

    // Constructeur : init date, saut de temps (14j) et saisons
    public Temporalite(DateOnly dateDebut, int sautsTemps=14)
    {
        DateDebut = dateDebut; 
        DateActuelle = dateDebut; 
        SautsTemps = sautsTemps;
        SaisonActuelle = new Saisons ("Saison",5,5,5, 0,0,0,0);
        Ete = new Saisons("Ete",10.5,32,1,0.02,0.1,0,0.1); //10.5h de soleil, 32 degrés en moyenne, 1L d'eau par semaine/m², probaPluie = 0.02, probaSecheresse = 0.1, probaGel = 0, probaCanicule = 0.1
        Automne = new Saisons("Automne",5.6,22,3.5,0.1,0.03,0.04,0.0); //5.6h de soleil, 22 degrés en moyenne, 3.5L d'eau par semaine/m², probaPluie = 0.1, probaSecheresse = 0.03, probaGel = 0.04, probaCanicule = 0 
        Hiver = new Saisons("Hiver",5.7,10,2.6,0.06,0,0.1,0); //5.7h de soleil, 10 degrés en moyenne, 2.6L d'eau par semaine/m², probaPluie = 0.06, probaSecheresse = 0, probaGel = 0.1, probaCanicule = 0  
        Printemps = new Saisons("Printemps",9,25,2, 0.05,0.05,0.05,0.03); //9h de soleil, 25 degrés en moyenne, 2L d'eau par semaine/m², probaPluie = 0.05, probaSecheresse = 0.05, probaGel = 0.05, probaCanicule = 0.03
        EtatUrgence = false; 
        EtablirSaison();  //Permet d'établir la saison dès le premier jour de jeu
    }

    // Fct pr faire avancer le temps 
    
    public virtual void AvancerTemps() //Permet d'avancer dans le temps dans le simulateur
    {
        DateActuelle = DateActuelle.AddDays(SautsTemps);
        EtablirSaison(); //A chaque tour de jeu on réévalue la saison dans laquelle on est
    }

    // Fct pr définir saison actuelle selon la date courante
    private void EtablirSaison() //Fonction pour déterminer automatiquement la saison en fonction de la date actuelle
    {
        int mois = DateActuelle.Month;
        if (mois == 12 || mois == 1 || mois == 2)
        {
            SaisonActuelle = Hiver;
        }
        else if (mois >= 3 && mois <= 5)
        {
            SaisonActuelle = Printemps;
        }
        else if (mois >= 6 && mois <= 8)
        {
            SaisonActuelle = Ete;
        }
        else
        {
            SaisonActuelle = Automne;
        }
    }

    // Affichage formaté des infos de date + saison courante
    public override string ToString()
    {
        return $"Nous sommes actuellement le {DateActuelle}. Nous sommes en cette saison : {SaisonActuelle.Nom}";
    }
}
