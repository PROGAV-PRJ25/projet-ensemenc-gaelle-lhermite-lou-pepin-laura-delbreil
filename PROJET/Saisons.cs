public class Saisons
{
    public string Nom {get; set;}
    public double TauxSoleil {get; protected set;} //nombre d'heures d'ensoleillement dans la journée par saison
    public double Temperature {get; set;}
    public double TauxPrecipitation {get; set;} //Litres par semaine

    //Ajout des probas des différents événements dans le constructeur des saisons
    public double ProbaPluieTorrentielle { get; set; } 
    public double ProbaSecheresse { get; set; }
    public double ProbaGel { get; set; }
    public double ProbaCanicule { get; set; }

    public Saisons(string nom, double tauxSoleil, double temperature, double tauxPrecipitation, double probaPluie, double probaSecheresse, double probaGel, double probaCanicule) //Météo bordelaise sera prise en compte pour les valeurs par saison, probabilités choisies arbitrairement
    {
        Nom = nom;
        TauxSoleil = tauxSoleil; 
        TauxPrecipitation = tauxPrecipitation; 
        Temperature = temperature; 
        ProbaPluieTorrentielle = probaPluie;
        ProbaSecheresse = probaSecheresse;
        ProbaGel = probaGel;
        ProbaCanicule = probaCanicule;
    }

    public override string ToString()
    {
        return $"texte";
    }
}