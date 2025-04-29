public class Saisons
{
    public string Nom {get; set;}
    public double TauxSoleil {get; protected set;} //nombre d'heures d'ensoleillement dans la journée par saison
    public double Temperature {get; set;}
    public double TauxPrecipitation {get; set;} //Litres par semaine

    public Saisons(string nom, double tauxSoleil, double temperature, double tauxPrecipitation) //Météo bordelaise sera prise en compte pour les valeurs par saison 
    {
        Nom = nom;
        TauxSoleil = tauxSoleil; 
        TauxPrecipitation = tauxPrecipitation; 
        Temperature = temperature; 
    }

    public override string ToString()
    {
        return $"texte";
    }
}