public class Opium : Plantes
{
    private float age = 0;

    // valeurs de la plante (nom, préférences, vitesse de croissance...)
    public Opium()
    {
        Nom = "Opium";
        EstVivante = true;
        TerrainPrefere = "argile";
        VitesseCroissance = 3.5f; // un peu plus lente que Hachich
        CroissanceActuelle = 0;
        BesoinEau = 1.8f;
        BesoinLumiere = 10.0f; // aime soleil
        TempPreferee = 22;
        EsperanceDeVie = 24; // durée plus longue que Hachich
        Fruits = 3;
        EtatSante = 0.65f;
        Emoji = "";
    }

    // Fct qui gère croissance d'un Opium en fct de conditions météo
    public override void Pousser(float eau, float lumiere, float temperature, string typeTerrain, DateOnly dateActuelle)
    {
        if (!EstVivante) return;

        if (eau >= BesoinEau) EtatSante += 0.02f;
        if (lumiere >= BesoinLumiere) EtatSante += 0.01f;
        if (temperature >= TempPreferee - 2 && temperature <= TempPreferee + 2) EtatSante += 0.01f;
        if (typeTerrain == TerrainPrefere) EtatSante += 0.01f;

        if (EtatSante > 1.0f) EtatSante = 1.0f;

        age += 2;
        CroissanceActuelle += VitesseCroissance * 3 * EtatSante;

        if ((EtatSante < 0.5f) || (age > EsperanceDeVie))
        {
            EstVivante = false;
            EtatSante = 0.0f;
            Console.WriteLine($"{Nom} est morte.");
            return;
        }

        if (CroissanceActuelle > 100f) CroissanceActuelle = 100f;
    }

    // Emoji en fonction du stade
    public override string Afficher()
    {
        string emoji = "   ";

        if (EstVivante)
        {
            if (CroissanceActuelle < 15) emoji = " . ";
            else if (CroissanceActuelle < 35) emoji = "🌱 ";
            else if (CroissanceActuelle < 50) emoji = "🥬 ";
            else if (CroissanceActuelle < 75) emoji = "🌸 ";
            else if (CroissanceActuelle < 90) emoji = "🌺 ";
            else if (CroissanceActuelle < 100) emoji = "🍁 ";
            else if (CroissanceActuelle >= 100)
            {
                EstVivante = false;
                CroissanceActuelle = 0;
                EtatSante = 0.0f;
                emoji = "🪦  ";
            }
        }

        return emoji;
    }
}
