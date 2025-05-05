using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Création de la liste de plantes du jardin
        List<Plantes> jardin = new List<Plantes>
        {
            new Hachich()
        };

        // Simuler plusieurs semaines de croissance
        for (int semaine = 1; semaine <= 27; semaine++)
        {
            Console.Clear() ;
            Console.WriteLine($"\n📆 Semaine {2*semaine}");

            foreach (var plante in jardin)
            {
                // Exemple : conditions météo fixes
                plante.Pousser(eau: 2.5f, lumiere: 8f, temperature: 24f, typeTerrain: "terre");
                plante.Afficher();
            }

            Console.WriteLine("\nAppuie sur une touche pour passer à la semaine suivante...");
            Console.ReadKey(true);
        }
    }
}
