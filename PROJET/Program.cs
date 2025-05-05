using System;

class Program
{
    static void Main(string[] args)
    {
        // Créer une instance de Hachich
        Hachich maPlante = new Hachich();

        // Simuler plusieurs cycles de croissance
        for (int i = 0; i < 10; i++) // 10 cycles = 20 semaines
        {
            Console.Clear(); // Nettoyer l'écran à chaque tour
                Console.WriteLine($"🌿 Cycle 🌿");
                Console.WriteLine();

                // Paramètres environnementaux (modifiables si tu veux tester des cas)
                float eau = 2.0f;
                float lumiere = 9.0f;
                float temperature = 25.0f;
                string terrain = "terre";

                maPlante.Pousser(eau, lumiere, temperature, terrain);
                maPlante.Afficher();

                Console.WriteLine("\nAppuie sur une touche pour passer au cycle suivant...");
                Console.ReadKey(); // Pause en attendant que l'utilisateur appuie sur une touche
        }
    } 
}
