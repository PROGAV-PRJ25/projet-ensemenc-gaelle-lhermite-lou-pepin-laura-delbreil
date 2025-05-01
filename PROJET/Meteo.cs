public class Meteo
{
    public string EvenementMeteo { get; set; } //4 événements météo possible mais 1 seul à la fois

    private Random random = new Random();

    public void GenererEvenement(Saisons saison) //Permet de générer 4 nombres aléatoires pour les 4 événements météo
    {
        double hasard = random.Next(0,101); //Chiffre entre 0 et 100
        if (hasard < saison.ProbaPluieTorrentielle*100){ //Vérification de la présence de pluie
            EvenementMeteo = "Pluie torrentielle";
            hasard = random.Next(0,101);
        }
        else if (hasard < saison.ProbaSecheresse*100){ //Présence de sécheresse
            EvenementMeteo = "Sécheresse";
            hasard = random.Next(0,101);
        }
        else if (hasard < saison.ProbaGel*100){ //Présence de gel
            EvenementMeteo = "Gel";
            hasard = random.Next(0,101);
        }
        else if (hasard < saison.ProbaCanicule*100){ //Présence de Canicule
            EvenementMeteo = "Canicule";
        }
        else{
            EvenementMeteo = "Temps normal"; //Si aucune proba, alors le temps est norma
        }
    }

    //A ajouter = comparaison si deux événements météo ont lieu (ou faire un ordre de priorité)
    //          = passage en mode urgence quand événement météo
    //          = visualisation des événements météo

    public override string ToString()
    {
        return $"Événement météo actuel : {EvenementMeteo}";
    }
}
