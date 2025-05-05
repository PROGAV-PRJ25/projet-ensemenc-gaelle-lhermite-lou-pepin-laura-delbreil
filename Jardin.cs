public class Jardin
{
    public Terrain[] Terrains { get; private set; }

    public Jardin(int nombreDeTerrains)
    {
        Terrains = new Terrain[nombreDeTerrains];

        for (int i = 0; i < nombreDeTerrains; i++)
        {
            Console.WriteLine($"\nType du terrain #{i + 1} (terre / sable / argile) : ");
            string type = Console.ReadLine()!.ToLower();

            while (type != "terre" && type != "sable" && type != "argile")
            {
                Console.WriteLine("Type invalide. Veuillez entrer 'terre', 'sable' ou 'argile' : ");
                type = Console.ReadLine()!.ToLower();
            }

            Console.Write($"Nom du terrain #{i + 1} : ");
            string nom = Console.ReadLine()!;

            Terrains[i] = type switch
            {
                "terre" => new TerrainTerreux(nom),
                "sable" => new TerrainSableux(nom),
                "argile" => new TerrainArgileux(nom),
                _ => throw new Exception("Type de terrain inconnu.")
            };
        }
    }

    public void Afficher()
{
    int taille = (int)Math.Sqrt(Terrains.Length);

    Console.WriteLine($"\n🪴 Voici votre jardin ({Terrains.Length} terrain(s)) :");

    // On capture les lignes de chaque terrain une fois
    string[][] rendus = Terrains.Select(t => GetAffichageLignes(t)).ToArray();

    int maxLignes = rendus.Max(r => r.Length);

    for (int ligne = 0; ligne < taille; ligne++)
    {
        for (int iLigne = 0; iLigne < maxLignes; iLigne++)
        {
            for (int col = 0; col < taille; col++)
            {
                int index = ligne * taille + col;

                if (index < rendus.Length)
                {
                    string[] rendu = rendus[index];

                    // Affiche la ligne si elle existe, sinon une ligne vide
                    if (iLigne < rendu.Length)
                        Console.Write(rendu[iLigne]);
                    else
                        Console.Write(new string(' ', rendu[0].Length));

                    Console.Write("  "); // Espace entre les terrains
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine(); // Espace entre les rangées
    }
}
    private string[] GetAffichageLignes(Terrain terrain)
    {
        using (var writer = new StringWriter())
        {
            Console.SetOut(writer);
            terrain.Afficher();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });

            return writer.ToString().Split('\n', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
