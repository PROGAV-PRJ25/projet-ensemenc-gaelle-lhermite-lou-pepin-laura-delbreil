public class Khat : Plantes
{
    private float age = 0;

    public Khat()
    {
        Nom = "Khat";
        EstVivante = true;
        TerrainPrefere = "argile";
        VitesseCroissance = 5.0f;
        CroissanceActuelle = 0;
        BesoinEau = 1.5f;
        BesoinLumiere = 9.5f;
        TempPreferee = 27;
        EsperanceDeVie = 18;
        Fruits = 3;
        EtatSante = 0.55f;
        Emoji = "";
    }

    public override void Pousser(float eau, float lumiere, float temperature, string typeTerrain, DateOnly dateActuelle)
    {
        if (!EstVivante) return;

        if (eau >= BesoinEau) EtatSante += 0.01f;
        if (lumiere >= BesoinLumiere) EtatSante += 0.02f;
        if (temperature >= TempPreferee - 4 && temperature <= TempPreferee + 4) EtatSante += 0.01f;
        if (typeTerrain == TerrainPrefere) EtatSante += 0.005f;

        if (EtatSante > 1.0f) EtatSante = 1.0f;

        age += 2;
        CroissanceActuelle += VitesseCroissance * 3 * EtatSante;

        if ((EtatSante < 0.4f) || (age > EsperanceDeVie))
        {
            EstVivante = false;
            EtatSante = 0.0f;
            Console.WriteLine($"{Nom} est mort.");
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
            else if (CroissanceActuelle < 75) emoji = "🎍 ";
            else if (CroissanceActuelle < 90) emoji = "🌾 ";
            else if (CroissanceActuelle < 100) emoji = "🍂 ";
            else 
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
