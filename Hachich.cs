public class Hachich : Plantes
{
    private float age = 0;

    public Hachich()
    {
        Nom = "Hachich";
        EstVivante = true;
        EstComestible = true;
        TerrainPrefere = "terre";
        Espacement = 30;
        PlaceNecessaire = 0.5f;
        VitesseCroissance = 5;
        CroissanceActuelle = 0;
        BesoinEau = 2.0f;
        BesoinLumiere = 8.0f;
        TempPreferee = 25;
        EsperanceDeVie = 16;
        Fruits = 5;
        EtatSante = 0.50f;
    }

    public override void Pousser(float eau, float lumiere, float temperature, string typeTerrain)
    {
        if (!EstVivante) return;

        if (eau >= BesoinEau) EtatSante += 0.01f;
        if (lumiere >= BesoinLumiere) EtatSante += 0.01f;
        if (temperature >= TempPreferee - 3 && temperature <= TempPreferee + 5) EtatSante += 0.01f;
        if (typeTerrain == TerrainPrefere) EtatSante += 0.01f;

        if (EtatSante > 1.0f) EtatSante = 1.0f;

        age += 2; // saut de 2 semaines
        CroissanceActuelle += VitesseCroissance * 4 * EtatSante;

        if ((EtatSante < 0.5f) || (age > EsperanceDeVie))
        {
            EstVivante = false;
            EtatSante = 0.0f; // Mettre santé à 0
            Console.WriteLine($"{Nom} est morte.");
            return;
        }

        if (CroissanceActuelle > 100f) CroissanceActuelle = 100f;
    }

    public override string Afficher()
    {
        string emoji = ""; // Initialisation de la variable emoji

        if (EstVivante)
        {
            if (CroissanceActuelle < 15)
            {
                emoji = "."; // Pas de croissance
            }
            else if (CroissanceActuelle < 35)
            {
                emoji = "🌱"; // Jeune pousse
            }
            else if (CroissanceActuelle < 50)
            {
                emoji = "🌿"; // Plante plus grande
            }
            else if (CroissanceActuelle < 75)
            {
                emoji = "🥦"; // Plante presque mature
            }
            else if (CroissanceActuelle < 90)
            {
                emoji = "🍃"; // Plante en pleine croissance
            }
            else if (CroissanceActuelle < 100)
            {
                emoji = "🍂"; // Plante mature
            }
            else if (CroissanceActuelle >= 100)
            {
                EstVivante = false;
                CroissanceActuelle = 0;
                EtatSante = 0.0f; // Santé à 0 à la fin du cycle
                emoji = "🪦"; // Plante morte
            }
        }
        
        return emoji; // Retourner l'emoji au lieu de l'afficher
    }
}
