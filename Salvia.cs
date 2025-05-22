public class Salvia : PlanteVivace
{

    // valeurs spécifiques à la plante salvia
    public Salvia()
    {
        Nom = "Salvia";
        EstVivante = true;
        TerrainPrefere = "argile";
        VitesseCroissance = 4.0f;
        CroissanceActuelle = 0;
        BesoinEau = 3.0f;
        BesoinLumiere = 6.0f;
        TempPreferee = 22;
        EsperanceDeVie = 14;
        Fruits = 2;
        EtatSante = 0.65f;
        Emoji = "";
    }

    public override void Pousser(float eau, float lumiere, float temperature, string typeTerrain, DateOnly dateActuelle)
    {
        if (!EstVivante) return;

        if (eau >= BesoinEau) EtatSante += 0.005f;
        if (lumiere >= BesoinLumiere) EtatSante += 0.01f;
        if (temperature >= TempPreferee - 2 && temperature <= TempPreferee + 4) EtatSante += 0.005f;
        if (typeTerrain == TerrainPrefere) EtatSante += 0.003f;

        if (EtatSante > 1.0f) EtatSante = 1.0f;

        age += 2;
        CroissanceActuelle += VitesseCroissance * 2 * EtatSante;

        if ((EtatSante < 0.4f) || (age > EsperanceDeVie))
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
            else if (CroissanceActuelle < 75) emoji = "☘️  ";
            else if (CroissanceActuelle < 90) emoji = "🍀 ";
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
