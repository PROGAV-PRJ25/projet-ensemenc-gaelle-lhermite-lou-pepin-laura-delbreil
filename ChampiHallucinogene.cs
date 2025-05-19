public class ChampiHallucinogene : Plantes
{
    private float age = 0;

    // valeurs spécifiques : croissance rapide, fragile, faible besoin lumière
    public ChampiHallucinogene()
    {
        Nom = "Champi hallucinogène";
        EstVivante = true;
        TerrainPrefere = "sable"; // pousse dans sol léger
        VitesseCroissance = 6.0f;
        CroissanceActuelle = 0;
        BesoinEau = 1.5f;
        BesoinLumiere = 4.0f;
        TempPreferee = 18;
        EsperanceDeVie = 8; // très courte
        Fruits = 1;
        EtatSante = 0.4f;
        Emoji = "";
    }

    public override void Pousser(float eau, float lumiere, float temperature, string typeTerrain)
    {
        if (!EstVivante) return;

        if (eau >= BesoinEau) EtatSante += 0.02f;
        if (lumiere >= BesoinLumiere) EtatSante += 0.01f;
        if (temperature >= TempPreferee - 2 && temperature <= TempPreferee + 3) EtatSante += 0.01f;
        if (typeTerrain == TerrainPrefere) EtatSante += 0.001f;

        if (EtatSante > 1.0f) EtatSante = 1.0f;

        age += 2;
        CroissanceActuelle += VitesseCroissance * 5 * EtatSante;

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
            else if (CroissanceActuelle < 35) emoji = "• ";
            else if (CroissanceActuelle < 60) emoji = "🍄‍🟫 ";
            else if (CroissanceActuelle < 90) emoji = "🍄 ";
            else if (CroissanceActuelle < 100) emoji = "🫚 ";
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
