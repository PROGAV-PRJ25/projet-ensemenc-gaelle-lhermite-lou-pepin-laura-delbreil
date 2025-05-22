/// <summary>
/// Classe pour créer toutes les solutions et les appeler ensuite dans l'inventaire
/// </summary>
public class Solutions
{
    public string Nom { get; set; } //Nom de l'objet
    public string Icone { get; set; } //Icone de l'objet
    public List<Solutions> ListeSolutions { get; set; }

    public Solutions(string nom, string icone)
    {
        Nom = nom;
        Icone = icone;
        ListeSolutions = new List<Solutions>();
    }
    //Fct pour générer la liste complète des solutions contre les indésirables
    public List<Solutions> GenererSolutions()
    {
        ListeSolutions.Add(new Solutions("PistoletEau", "🔫"));
        ListeSolutions.Add(new Solutions("Steak", "🥩"));
        ListeSolutions.Add(new Solutions("Cameras", "📸"));
        ListeSolutions.Add(new Solutions("Fromage", "🧀"));
        ListeSolutions.Add(new Solutions("Flute", "🪈"));
        ListeSolutions.Add(new Solutions("VinaigreBlanc", "🍶"));
        ListeSolutions.Add(new Solutions("CharbonActif", "🌑"));
        ListeSolutions.Add(new Solutions("Ail", "🧄"));
        return ListeSolutions;
    }
}