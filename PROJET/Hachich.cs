using System;

public class Hachich : Plantes
{
    private float croissanceActuelle = 0f;
    private float age = 0f;

    public Hachich()
    {
        Nom = "Hachich";
        EstVivace = true;
        EstComestible = true;
        TerrainPrefere = "terre";
        Espacement = 30f;
        PlaceNecessaire = 0.5f;
        VitesseCroissance = 5f;
        BesoinEau = 2.0f;
        BesoinLumiere = 8.0f;
        TempPreferee = 25f;
        EsperanceDeVie = 40f;
        Fruits = 5;
    }

    public override void Pousser(float eau, float lumiere, float temperature, string typeTerrain)
    {
        float score = 0f;

        if (eau >= BesoinEau) score += 0.25f;
        if (lumiere >= BesoinLumiere) score += 0.25f;
        if (temperature >= TempPreferee - 3 && temperature <= TempPreferee + 5) score += 0.25f;
        if (typeTerrain == TerrainPrefere) score += 0.25f;

        if (score < 0.5f) // si score < 50% des conditions, la plante souffre
        {
            EtatSante -= 10f;
            if (EtatSante <= 0)
            {
                Console.WriteLine($"{Nom} est morte.");
                return;
            }
        }

        croissanceActuelle += VitesseCroissance * score;
        age += 2f;

        // Simuler une maladie aléatoire (à compléter plus tard si besoin)
    }

   public override void Afficher()
{
    if (EtatSante <= 0)
    {
        Console.WriteLine($"{Nom} bébou est morte. 💀");
        return; // on arrête là : rien d'autre ne s'affiche
    }

    Console.WriteLine($"\n{Nom} : 🥦");
    Console.WriteLine($"Santé : {EtatSante}% | Âge : {age} semaines | Croissance : {croissanceActuelle} cm");

    // Affichage emojis simple selon la croissance
    if (croissanceActuelle < 10)
    {
        Console.WriteLine("🌱");
    }
    else if (croissanceActuelle <= 30)
    {
        Console.WriteLine("🌱");
    }
    else if (croissanceActuelle <= 40)
    {
        Console.WriteLine("🌿");
    }
    else if (croissanceActuelle <= 50)
    {
        Console.WriteLine("🥦");
    }
    else if (croissanceActuelle <= 70)
    {
        Console.WriteLine("🍃");
        Console.WriteLine("C'est la récolte les gars !!!");
    }
    else if (croissanceActuelle <= 90)
    {
        Console.WriteLine("🍂");
        Console.WriteLine("Vite vite tu perds du kiff !!! ");
    }
    else if (croissanceActuelle > 90)
    {
        Console.WriteLine("🪦");
        Console.WriteLine("Et voilà 1000 balles en moins khouya!");
        EtatSante = 0;
    }

    Console.WriteLine(); // pour l'espacement visuel
}
}