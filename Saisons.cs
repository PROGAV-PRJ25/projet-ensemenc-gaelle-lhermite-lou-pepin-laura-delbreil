public class Saisons
{
    public string Nom {get; set;}
    public double TauxSoleil {get; set;} //nombre d'heures d'ensoleillement dans la journée par saison
    public double Temperature {get; set;}
    public double TauxPrecipitation {get; set;} //Litres par semaine

    private double TauxSoleilFixe {get; set;}
    private double TemperatureFixe {get; set;}
    private double TauxPrecipitationFixe {get; set;}

    //Ajout des probas des différents événements dans le constructeur des saisons
    public double ProbaPluieTorrentielle { get; set; } 
    public double ProbaSecheresse { get; set; }
    public double ProbaGel { get; set; }
    public double ProbaCanicule { get; set; }

    public Saisons(string nom, double tauxSoleil, double temperature, double tauxPrecipitation, double probaPluie, double probaSecheresse, double probaGel, double probaCanicule) //Météo bordelaise sera prise en compte pour les valeurs par saison, probabilités choisies arbitrairement
    {
        Nom = nom;
        TauxSoleil = TauxSoleilFixe = tauxSoleil; 
        TauxPrecipitation = TauxPrecipitationFixe = tauxPrecipitation; 
        Temperature = TemperatureFixe = temperature; 
        ProbaPluieTorrentielle = probaPluie;
        ProbaSecheresse = probaSecheresse;
        ProbaGel = probaGel;
        ProbaCanicule = probaCanicule;
    }

    public void RemettreConditions(){ //Pour avoir les valeurs de base pour chaque saison qui ne sont pas modifiées par les événements météo
        TauxPrecipitation = TauxPrecipitationFixe;
        TauxSoleil = TauxSoleilFixe; 
        Temperature = TemperatureFixe; 
    }

    public override string ToString()
    {
        return $"Tempe : {Temperature}, tempe fixe : {TemperatureFixe}\nPluie : {TauxPrecipitation}, pluie fixe : {TauxPrecipitationFixe}\nsoleil : {TauxSoleil}, soleil fixe : {TauxSoleilFixe}\n";
    }
}