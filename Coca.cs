public class Coca : Plantes
{
    private float age = 0;

    // valeurs de la plante coca
    public Coca()
    {
        Nom = "Coca";
        EstVivante = true;
        TerrainPrefere = "terre";
        VitesseCroissance = 4.5f;
        CroissanceActuelle = 0;
        BesoinEau = 2.5f;
        BesoinLumiere = 9.0f;
        TempPreferee = 27;
        EsperanceDeVie = 16;
        Fruits = 6;
        EtatSante = 0.55f;
        Emoji = "";
    }

    public override void Pousser(float eau, float lumiere, float temperature, string typeTerrain)
    {
        if (!EstVivante) return;

        if (eau >= BesoinEau) EtatSante += 0.01f;
        if (lumiere >= BesoinLumiere) EtatSante += 0.005f;
        if (temperature >= TempPreferee - 4 && temperature <= TempPreferee + 4) EtatSante += 0.005f;
        if (typeTerrain == TerrainPrefere) EtatSante += 0.005f;

        if (EtatSante > 1.0f) EtatSante = 1.0f;

        age += 2;
        CroissanceActuelle += VitesseCroissance * 4 * EtatSante;

        if ((EtatSante < 0.5f) || (age > EsperanceDeVie))
        {
            EstVivante = false;
            EtatSante = 0.0f;
            Console.WriteLine($"{Nom} est morte.");
            return;
        }

        if (CroissanceActuelle > 100f) CroissanceActuelle = 100f;
    }

    public override string Afficher()
    {
        string emoji = "   ";

        if (EstVivante)
        {
            if (CroissanceActuelle < 15) emoji = " . ";
            else if (CroissanceActuelle < 35) emoji = "🌱 ";
            else if (CroissanceActuelle < 50) emoji = "🌿 ";
            else if (CroissanceActuelle < 75) emoji = "🌴 ";
            else if (CroissanceActuelle < 90) emoji = "🍃 ";
            else if (CroissanceActuelle < 100) emoji = "🍂 ";
            else
            {
                EstVivante = false;
                CroissanceActuelle = 0;
                EtatSante = 0.0f;
                emoji = "🪦 ";
            }
        }

        return emoji;
    }
}
