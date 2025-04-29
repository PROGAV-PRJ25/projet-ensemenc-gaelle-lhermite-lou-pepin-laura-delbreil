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

    public Temporalite(DateOnly dateDebut, int sautsTemps=14)
    {
        DateDebut = dateDebut; 
        DateActuelle = dateDebut; 
        SautsTemps = sautsTemps;
        SaisonActuelle = new Saisons ("Saison",5,5,5);
        Ete = new Saisons("Ete",9.4,21,13); //9.4h de soleil, 21 degrés en moyenne, 13L d'eau par semaine/m² 
        Automne = new Saisons("Automne",5.6,11,18); //5.6h de soleil, 11 degrés en moyenne, 18L d'eau par semaine/m²  
        Hiver = new Saisons("Hiver",5.7,7,14); //5.7h de soleil, 7 degrés en moyenne, 14L d'eau par semaine/m²  
        Printemps = new Saisons("Printemps",9,16,16); //9h de soleil, 16 degrés en moyenne, 16L d'eau par semaine/m² 
        EtablirSaison();  //Permet d'établir la saison dès le premier jour de jeu
    }

    public void AvancerTemps() //Permet d'avancer dans le temps dans le simulateur
    {
        DateActuelle = DateActuelle.AddDays(SautsTemps); //Ajout de jours
        EtablirSaison(); //Permet de vérifier la saison à chaque avancée dans le temps
    }

    public void EtablirSaison(){
        int anneeEnCours = DateActuelle.Year; //récupération de l'année pour avoir des saisons constantes (et pas seulement de l'année 2025)
        DateOnly debutEte = new DateOnly (anneeEnCours, 06, 21); //21/06 = début de l'été
        DateOnly debutAutomne = new DateOnly (anneeEnCours, 09, 21); //21/09 = début de l'automne
        DateOnly debutHiver = new DateOnly (anneeEnCours,12,21); //21/12 = début de l'hiver
        DateOnly debutPrintemps = new DateOnly (anneeEnCours, 03, 21); //21/03 = début du printemps

        if (debutEte<=DateActuelle && DateActuelle<debutAutomne) 
        {   
            SaisonActuelle = Ete; 
        } 
        else if (debutAutomne<=DateActuelle && DateActuelle<debutHiver)
        {   
            SaisonActuelle = Automne;
        } 
        else if (debutPrintemps<=DateActuelle && DateActuelle<debutEte)
        {   
            SaisonActuelle = Printemps; 
        }  
        else
        {   
            SaisonActuelle = Hiver; //Mise en dernier parce que saison à cheval sur deux années
        }       
    
    }

    public override string ToString()
    {
        return $"Nous avons débuté notre aventure le {DateDebut} !! La date du jour est : {DateActuelle}. Nous sommes en cette saison : {SaisonActuelle.Nom}"; //Affichage de la date actuelle
    }
}