using System;

public class Menu
{

    private static readonly int[] nbTerrainsOptions = { 1, 4, 9 };
    public int NbParcelles { get; private set; }
    public int NbTerrains { get; private set; }


    public void Demarrer()
{
  
    int nbTerrainsIndex = Choisir("Nombre de terrains :", Array.ConvertAll(nbTerrainsOptions, x => x.ToString()));

    NbTerrains = nbTerrainsOptions[nbTerrainsIndex];


    // Console.Clear();
    // Console.WriteLine($"Création de {nbTerrains} terrain(s) de {nbParcelles} parcelle(s) chacun...\n");

    // Random rnd = new Random();

    // for (int i = 0; i < nbTerrains; i++)
    // {
    //     string nom = $"T{i + 1}";
    //     int type = rnd.Next(3);
    //     Terrain terrain = type switch
    //     {
    //         //0 => new TerrainSableux(nom, nbParcellesParTerrain),
    //         //1 => new TerrainTerreux(nom, nbParcellesParTerrain),
    //         _ => new TerrainArgileux(nom, nbParcelles)
    //     };
    //     terrain.Afficher();
    //     Console.WriteLine();
    // }
}

    private int Choisir(string question, string[] options)
    {
        int index = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine(question + "\n");

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(i == index ? $"> {options[i]}" : $"  {options[i]}");
            }

            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.UpArrow) index = (index - 1 + options.Length) % options.Length;
            if (key == ConsoleKey.DownArrow) index = (index + 1) % options.Length;

        } while (key != ConsoleKey.Enter);

        return index;
    }
}
