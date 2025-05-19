/// <summary>
/// 
/// Classe pr décrire caractéristiques climatiques d'une saison
/// Utilisée pr calculer effet sur plantes : lumière, précipitations, tempé, etc.
/// 
/// </summary>
public class Saisons
{
    public string Nom { get; set; }
    public double Temperature { get; set; } //°C moyenne sur les 4 semaines
    public double TauxPrecipitation { get; set; } //en L/m² par semaine
    public double TauxSoleil { get; set; } //en h/jour
    public double ProbaGel { get; set; } //valeur entre 0 et 1
    public double ProbaPluieTorrentielle { get; set; } //valeur entre 0 et 1
    public double ProbaCanicule { get; set; } //valeur entre 0 et 1
    public double ProbaSecheresse { get; set; } //valeur entre 0 et 1

    private double temperatureInitiale;
    private double tauxPrecipitationInitial;
    private double tauxSoleilInitial;

    // init tous les paramètres climatiques de la saison
    public Saisons(string nom, double temperature, double tauxPrecipitation, double tauxSoleil,
                   double probaGel, double probaPluieTorrentielle, double probaCanicule, double probaSecheresse)
    {
        Nom = nom;
        Temperature = temperature;
        TauxPrecipitation = tauxPrecipitation;
        TauxSoleil = tauxSoleil;
        ProbaGel = probaGel;
        ProbaPluieTorrentielle = probaPluieTorrentielle;
        ProbaCanicule = probaCanicule;
        ProbaSecheresse = probaSecheresse;

        temperatureInitiale = temperature;
        tauxPrecipitationInitial = tauxPrecipitation;
        tauxSoleilInitial = tauxSoleil;
    }

    // Fct pr réinitialiser valeurs météo à celles d'origine de la saison
    public void RemettreConditions()
    {
        Temperature = temperatureInitiale;
        TauxPrecipitation = tauxPrecipitationInitial;
        TauxSoleil = tauxSoleilInitial;
    }

    // Redéfinit affichage sous forme lisible 
    public override string ToString()
    {
        return $"Saison : {Nom}\n" +
               $"Température moyenne : {Temperature}°C\n" +
               $"Taux de précipitation : {TauxPrecipitation} L/m²\n" +
               $"Taux de soleil : {TauxSoleil} h/jour\n";
    }
}
