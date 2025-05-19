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
        Ete = new Saisons("Ete",9.4,21,13,0.1,0.4,0,0.5); //9.4h de soleil, 21 degrés en moyenne, 13L d'eau par semaine/m², probaPluie = 0.1, probaSecheresse = 0.4, probaGel = 0, probaCanicule = 0.5
        Automne = new Saisons("Automne",5.6,11,18,0.3,0.1,0.1,0.0); //5.6h de soleil, 11 degrés en moyenne, 18L d'eau par semaine/m², probaPluie = 0.3, probaSecheresse = 0.1, probaGel = 0.1, probaCanicule = 0 
        Hiver = new Saisons("Hiver",5.7,7,14,0.2,0,0.5,0); //5.7h de soleil, 7 degrés en moyenne, 14L d'eau par semaine/m², probaPluie = 0.2, probaSecheresse = 0, probaGel = 0.5, probaCanicule = 0  
        Printemps = new Saisons("Printemps",9,16,16,0.2,0.1,0.2,0.1); //9h de soleil, 16 degrés en moyenne, 16L d'eau par semaine/m², probaPluie = 0.2, probaSecheresse = 0.1, probaGel = 0.2, probaCanicule = 0.1
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
