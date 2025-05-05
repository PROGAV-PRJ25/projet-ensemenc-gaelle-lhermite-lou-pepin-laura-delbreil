class Program
{
    static void Main(string[] args)
{

    int[] options = { 1, 4, 9 };
    int nbTerrains = 0;

    while (!options.Contains(nbTerrains)) // On boucle tant que la valeur n'est pas valide
    {
        Console.Write("Combien de terrains voulez-vous cultiver ? (1 / 4 / 9) : ");
        string input = Console.ReadLine()!;

        if (!int.TryParse(input, out nbTerrains) || !options.Contains(nbTerrains))
        {
            Console.WriteLine("Entrée invalide. Veuillez choisir 1, 4 ou 9.");
            nbTerrains = 0;
        }
    }

    Jardin jardin = new Jardin(nbTerrains);
    jardin.Afficher();
}

}

//Tests de fonctionnement des classes de temmporalité, avec ajout de temps
Temporalite temp1 = new Temporalite(DateOnly.Parse("2025-1-1")); //Parse présent pour permettre la récupération du string en format date
Meteo meteo = new Meteo();

for (int i = 0; i<25; i++){
    meteo.GenererEvenement(temp1.SaisonActuelle, temp1); //on génére un événement météo en fonction de la saison et de la temporalité dans laquelle on est
    if (temp1.EtatUrgence && temp1.GetType() != typeof(TempoUrgence)) // si EtatUrgence est vrai mais que l'on est pas en état d'urgence, on passe en mode urgence
        {
            temp1 = new TempoUrgence(temp1.DateActuelle){ //on récupère les informations des saisons de temp1
                Ete = temp1.Ete,
                Automne = temp1.Automne,
                Hiver = temp1.Hiver,
                Printemps = temp1.Printemps,
            }; 
        }
     else if (temp1.EtatUrgence == false && temp1.GetType() == typeof(TempoUrgence)) // si EtatUrgence est faux mais que l'on est en état d'urgence, on passe en mode normal
        {
            temp1 = new Temporalite(temp1.DateActuelle){ //on récupère les informations des saisons de temp1
                Ete = temp1.Ete,
                Automne = temp1.Automne,
                Hiver = temp1.Hiver,
                Printemps = temp1.Printemps
            }; 
        }

    if (temp1.GetType() == typeof(TempoUrgence)){ //Pour afficher le mode dans lequel on est 
        Console.WriteLine("MODE URGENCE");
        meteo.ModifierValeursSaison(temp1.SaisonActuelle); //Lorsqu'on est en mode urgence, on modifie les valeurs des saisons
    }
    else{
        Console.WriteLine("MODE NORMAL"); 
    }
    Console.WriteLine();
    Console.WriteLine(temp1);
    Console.WriteLine(temp1.SaisonActuelle); // Pour vérifier la bonne modification des valeurs 
    Console.WriteLine(meteo);
    Console.WriteLine(); 
    temp1.AvancerTemps();

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
