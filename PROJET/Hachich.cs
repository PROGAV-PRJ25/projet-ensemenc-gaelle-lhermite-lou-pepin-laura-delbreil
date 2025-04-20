public class Hachich : Plantes
{
    private float croissanceActuelle = 0;
    private float age = 0;

    public Hachich()
    {
        Nom = "Hachich";
        EstVivace = true;
        Estvivante = true;
        EstComestible = true;
        TerrainPrefere = "terre";
        Espacement = 30;
        PlaceNecessaire = 0.5f;
        VitesseCroissance = 5;
        BesoinEau = 2.0f;
        BesoinLumiere = 8.0f;
        TempPreferee = 25;
        EsperanceDeVie = 16;
        Fruits = 5;
        EtatSante = 0.50f;
    }

    public override void Pousser(float eau, float lumiere, float temperature, string typeTerrain)
    {
        if (!Estvivante) return;

        if (eau >= BesoinEau) EtatSante += 0.01f;
        if (lumiere >= BesoinLumiere) EtatSante += 0.01f;
        if (temperature >= TempPreferee - 3 && temperature <= TempPreferee + 5) EtatSante += 0.01f;
        if (typeTerrain == TerrainPrefere) EtatSante += 0.01f;

        // Plafonner la santé à 1.0 maximum
        if (EtatSante > 1.0f) EtatSante = 1.0f;

        age += 2; // saut de 2 semaines

        if ((EtatSante < 0.5f) || (age > EsperanceDeVie))
        {
            Estvivante = false;
            EtatSante = 0.0f; // Mettre santé à 0
            Console.WriteLine($"{Nom} est morte.");
            return;
        }

        croissanceActuelle += VitesseCroissance * 4 * EtatSante;

        // Limiter la croissance à 100%
        if (croissanceActuelle > 100f) croissanceActuelle = 100f;
    }

    public override void Afficher()
    {
        Console.WriteLine($"[{Nom}] Croissance: {croissanceActuelle}% | Santé: {EtatSante * 100}% | Âge: {age} sem");

        if (Estvivante)
        {
            if (croissanceActuelle < 15)
            {
                Console.WriteLine(".");
            }
            else if (croissanceActuelle < 35)
            {
                Console.WriteLine("🌱");
            }
            else if (croissanceActuelle < 50)
            {
                Console.WriteLine("🌿");
            }
            else if (croissanceActuelle < 75)
            {
                Console.WriteLine("🥦");
            }
            else if (croissanceActuelle < 90)
            {
                Console.WriteLine("🍃");
                Console.WriteLine("C'est la récolte!");
            }
            else if (croissanceActuelle < 100)
            {
                Console.WriteLine("🍂");
                Console.WriteLine("Vite vite vite!!!!! ");
            }
            else if (croissanceActuelle >= 100)
            {
                Estvivante = false;
                croissanceActuelle = 0;
                EtatSante = 0.0f; // Santé à 0 à la fin du cycle
                Console.WriteLine("🪦");
                Console.WriteLine("Hachich a fini son cycle de vie");
            }
        }
    }
}
