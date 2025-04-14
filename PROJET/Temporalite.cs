public class Temporalite //Classe pour la temporalité classique
{
    public DateOnly DateDebut{get; protected set;} //Date de départ du simulateur en format DateDebut
    public DateOnly DateActuelle{get; protected set;} //Date au moment du tour de jeu
    public int SautsTemps{get; protected set;} // Sauts dans le temps (de 14 jours pour la version classique)

    public Temporalite(DateOnly dateDebut, int sautsTemps=14)
    {
        DateDebut = dateDebut; 
        DateActuelle = dateDebut; 
        SautsTemps = sautsTemps; 
    }

    public void AvancerTemps() //Permet d'avancer dans le temps dans le simulateur
    {
        DateActuelle = DateActuelle.AddDays(SautsTemps); //Ajout de jours
    }

    public override string ToString()
    {
        return $"Nous avons débuté notre aventure le {DateDebut} !! La date du jour est : {DateActuelle}"; //Affichage de la date actuelle
    }
}