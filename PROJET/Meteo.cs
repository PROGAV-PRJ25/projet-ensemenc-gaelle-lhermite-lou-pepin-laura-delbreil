public class Meteo
{
    public string EvenementMeteo { get; set; } //4 événements météo possible mais 1 seul à la fois
    public int joursRestants = 0; //Ajout d'une durée des événements météo

    public Random random = new Random();

    public void GenererEvenement(Saisons saison, Temporalite temporalite) //Permet de générer 4 nombres aléatoires pour les 4 événements météo
    {
        if (joursRestants>1){ //Pour que le jour 0 ne s'affiche pas aussi, >1
            joursRestants --;
            temporalite.EtatUrgence = true; //On est en état d'urgence tant que l'événement météo est en cours
            return; 
        }
        else
        {
            EvenementMeteo = "Temps normal"; //Si aucune proba, alors le temps est normal
            temporalite.EtatUrgence = false; //On est alors pas en état d'urgence
        }
        double hasard = random.Next(0,101); //Chiffre entre 0 et 100
        if (hasard < saison.ProbaPluieTorrentielle*100){ //Vérification de la présence de pluie en le comparant au tirage hasard
            EvenementMeteo = "Pluie torrentielle";
            joursRestants = 2; 
            temporalite.EtatUrgence = true; //Lors du déclenchement de l'événement, le booléen état d'urgence devient vrai
            hasard = random.Next(0,101);
        }
        if (hasard < saison.ProbaGel*100){ //Présence de gel
            EvenementMeteo = "Gel";
            joursRestants = 2;
            temporalite.EtatUrgence = true; 
            hasard = random.Next(0,101);
        }
        if (hasard < saison.ProbaSecheresse*100){ //Présence de sécheresse
            EvenementMeteo = "Sécheresse";
            joursRestants = 2; 
            temporalite.EtatUrgence = true;
            hasard = random.Next(0,101);
        }
        if (hasard < saison.ProbaCanicule*100){ //Présence de Canicule
            EvenementMeteo = "Canicule";
            joursRestants = 2; 
            temporalite.EtatUrgence = true;
        }
    }

    public override string ToString()
    {
        if (EvenementMeteo == "Temps normal"){
            return $"Événement météo actuel : {EvenementMeteo} (on se revoit dans 2 semaines)"; //Si on a un temps normal, on affiche pas le nombre de jours restants
        }
        else{
            return $"Événement météo actuel : {EvenementMeteo} (encore {joursRestants} jours avant que cela soit fini)"; //Si on a un événement météo, on affiche le nombre de jours avant que cela soit fini
        }
    }
}
