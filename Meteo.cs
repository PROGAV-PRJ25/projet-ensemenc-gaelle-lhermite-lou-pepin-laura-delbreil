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
            saison.RemettreConditions(); //Pour revenir à la température, taux de précipitation et taux d'ensoleillement de base
            EvenementMeteo = "Temps normal"; //Si aucune proba, alors le temps est normal
            temporalite.EtatUrgence = false; //On est alors pas en état d'urgence
        }
        double hasard = random.Next(0,101); //Chiffre entre 0 et 100
        if (hasard < saison.ProbaPluieTorrentielle*100){ //Vérification de la présence de pluie en le comparant au tirage hasard
            EvenementMeteo = "Pluie torrentielle";
            joursRestants = 1; 
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
            joursRestants = 3; 
            temporalite.EtatUrgence = true;
            hasard = random.Next(0,101);
        }
        if (hasard < saison.ProbaCanicule*100){ //Présence de Canicule
            EvenementMeteo = "Canicule";
            joursRestants = 4; 
            temporalite.EtatUrgence = true;
        }
    }

    public void ModifierValeursSaison(Saisons saison) //Pour modifier les valeurs de température, taux de précipitation et taux d'ensoleillement en fonction de l'événement météo
    {
        switch (EvenementMeteo) {
            case "Pluie torrentielle":
                saison.TauxPrecipitation += 5; //Ajout de 5L lorsqu'il y a une pluie torrentielle
                saison.TauxSoleil -= 3; // 3 heures d'ensoleillement en moins lorsqu'il pleut
                break;
            case "Gel":
                saison.Temperature = 0; //Lorsqu'il y a du gel, la température drop à 0. 
                break;
            case "Sécheresse":
                saison.TauxPrecipitation = 0; //Plus aucune précipitation
                saison.Temperature += 3; //Légère augmentation de la température (3 degrés par jour)
                break;
            case "Canicule":
                saison.Temperature += 5; //Augmentation de 5 degrés / jour de la température pendant les 4 jours de l'événement
                break;
        }
    }
    
    public override string ToString()
    {
        if (EvenementMeteo == "Temps normal"){
            return $"Événement météo actuel : {EvenementMeteo} (on se revoit dans 2 semaines)\n"; //Si on a un temps normal, on affiche pas le nombre de jours restants
        }
        else{
            return $"Événement météo actuel : {EvenementMeteo} (encore {joursRestants} jours avant que cela soit fini)"; //Si on a un événement météo, on affiche le nombre de jours avant que cela soit fini
        }
    }
}
