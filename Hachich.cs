public class Hachich : Plantes
{
    private float age = 0;

    // valeurs de la plante (nom, préférences, vitesse de croissance...)
    public Hachich()
    {
        Nom = "Hachich";
        EstVivante = true;
        TerrainPrefere = "terre";
        VitesseCroissance = 5;
        CroissanceActuelle = 0;
        BesoinEau = 2.0f;
        BesoinLumiere = 8.0f;
        TempPreferee = 25;
        EsperanceDeVie = 16;
        Fruits = 5;
        EtatSante = 0.50f;
        Emoji = "";
    }

    // Fct qui gère la croissance d'un Hachich en fct de l'eau, lumière, tempé, terrain
    // Utilise EtatSante pr moduler vitesse et vérifier conditions de survie
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

    // Fct pr retourner l'emoji correspondant à l'état de croissance/mort de la plante
    public override string Afficher()
    {
        string emoji = "   ";

        if (EstVivante)
        {
            if (CroissanceActuelle < 15) emoji = " . ";
            else if (CroissanceActuelle < 35) emoji = "🌱 ";
            else if (CroissanceActuelle < 50) emoji = "🌿 ";
            else if (CroissanceActuelle < 75) emoji = "🥦 ";
            else if (CroissanceActuelle < 90) emoji = "🍃 ";
            else if (CroissanceActuelle < 100) emoji = "🍂 ";
            else if (CroissanceActuelle >= 100)
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
