using System.Dynamic;


/// <summary>
/// Classe qui permet de définir les caractéristiques des plantes 
/// 
/// méthodes associées: 
///         public void Recolter(Inventaire inventaire) : 
///             méthode qui permet de récolter le sgraines de la plantes et les ajoute ds invenataire
///         public abstract void Pousser(float eau, float lumiere, float temperature, string typeTerrain);   
///             méthode qui fait pousser la plantes selon les critères de chaque plante
///         public abstract void Afficher();
///             affichage de chaque plante selon leur stade de croissance
///         public void AfficherMessages()
///             affichage des messages répétitif pour les plantes == récolte et mort
/// 
/// </summary>
public abstract class Plantes
{
     public string ?Nom { get; protected set; }
    public bool EstComestible { get; protected set; }
    public string? TerrainPrefere { get; protected set; } // sable, terre, argile, cailloux
    public float Espacement { get; protected set; } // en cm
    public float PlaceNecessaire { get; protected set; } // en m²
    public float VitesseCroissance { get; protected set; } // en cm par semaine
    public float CroissanceActuelle { get; protected set; } // en cm par semaine
    public float BesoinEau { get; protected set; } // L par semaine
    public float BesoinLumiere { get; protected set; } // heures par jour
    public float TempPreferee { get; protected set; } // °C
    public float EsperanceDeVie { get; protected set; } // en semaines
    public int Fruits { get; protected set; } // nb de fruits et/ou légumes produits
    public float EtatSante { get; protected set; } // en pourcentage 
    public bool EstVivante { get; protected set; } // bool pr savoir si plante est vivante
    public string? Emoji { get; protected set; } // apparence de la plaaaaante
    public int ToursDepuisMort { get; set; } = -1;

    
    public abstract void Pousser(float eau, float lumiere, float temperature, string typeTerrain);
    public abstract string Afficher();
    public void AfficherMessages()
    {
        if (CroissanceActuelle > 75)
        {
            Console.WriteLine($"Vite c'est la récolte de {Nom}!");
        }
    }
    
    public void EvaluerCroissance(Saisons saison, Meteo meteo, string typeTerrain)
    {
        if (!EstVivante) return;

        // Extraire les valeurs de la saison
        float eau = (float)saison.TauxPrecipitation;
        float lumiere = (float)saison.TauxSoleil;
        float temperature = (float)saison.Temperature;

        // Appliquer les effets météo
        if (meteo.EvenementMeteo == "Canicule") temperature += 5;
        if (meteo.EvenementMeteo == "Gel") temperature -= 5;
        if (meteo.EvenementMeteo == "Pluie torrentielle") eau += 5;

        // Appel à la pousse spécifique
        this.Pousser(eau, lumiere, temperature, typeTerrain);

        // Messages spécifiques si tu veux
        AfficherMessages();
    }


}