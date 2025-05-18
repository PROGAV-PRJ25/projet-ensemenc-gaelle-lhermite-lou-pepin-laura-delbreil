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
        Console.Clear();
        return index;
        
    }
}
