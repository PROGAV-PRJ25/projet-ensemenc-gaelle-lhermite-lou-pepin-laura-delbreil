public abstract class PlanteVivace : Plantes
{
    protected float age = 0;
    protected float croissanceActuelle = 0;
    protected bool estEnDormance = false;

    public override void Pousser(float eau, float lumiere, float temperature, string typeTerrain)
    {
        // Début de la dormance si l'espérance de vie est atteinte
        if (!estEnDormance && age >= EsperanceDeVie)
        {
            estEnDormance = true;
            croissanceActuelle = 0;
            Console.WriteLine($"{Nom} entre en dormance.");
            return;
        }

        // Réveil après le cycle annuel
        if (estEnDormance && age >= 54)
        {
            age = 0;
            estEnDormance = false;
            EstVivante = true;
            EtatSante = 0.5f;
            Console.WriteLine($"{Nom} sort de dormance et repart de zéro.");
            return;
        }

        // Si en dormance, ne pousse pas
        if (estEnDormance) return;

        // Sinon, pousse normalement
        if (eau >= BesoinEau) EtatSante += 0.01f;
        if (lumiere >= BesoinLumiere) EtatSante += 0.01f;
        if (temperature >= TempPreferee - 3 && temperature <= TempPreferee + 5) EtatSante += 0.01f;
        if (typeTerrain == TerrainPrefere) EtatSante += 0.01f;

        if (EtatSante > 1.0f) EtatSante = 1.0f;

        if (EtatSante < 0.5f)
        {
            EstVivante = false;
            EtatSante = 0.0f;
            Console.WriteLine($"{Nom} est trop faible et meurt temporairement.");
            return;
        }

        croissanceActuelle += VitesseCroissance * 4 * EtatSante;
        if (croissanceActuelle > 100f) croissanceActuelle = 100f;
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