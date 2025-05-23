using System;

/// 
/// 
/// Classe pr afficher menu interactif au joueur (choix terrains + durée jeu)
/// 
/// 
public class Menu
{
    private static readonly int[] nbTerrainsOptions = { 1, 4, 9 };
    private static readonly int[] dureeAnneesOptions = { 1, 2, 3, 5, 10 };

    public int NbParcelles { get; private set; }
    public int NbTerrains { get; private set; }
    public int DureeAnnees { get; private set; }

    // Fct pr lancer menu et récupérer choix de l'utilisateur
    public void Demarrer()
    {
        // Choisir durée du jeu
        // Appel choix durée de jeu (1 à 10 ans)
        int dureeIndex = Choisir("🌱 Combien d'années voulez-vous que la partie dure ?", Array.ConvertAll(dureeAnneesOptions, x => x.ToString()));
        DureeAnnees = dureeAnneesOptions[dureeIndex];

        // Choisir nombre de terrains
        // Appel choix nb de terrains (1, 4, 9)
        int nbTerrainsIndex = Choisir("🏡 Nombre de terrains :", Array.ConvertAll(nbTerrainsOptions, x => x.ToString()));
        NbTerrains = nbTerrainsOptions[nbTerrainsIndex];
    }

    // Fct pr afficher question avec menu navigable (haut/bas + entrée pr valider)
    private int Choisir(string question, string[] options)
    {
        int index = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            JeuEnsemence.AfficherTitre();
            JeuEnsemence.CentrerTexte(question + "\n");

            for (int i = 0; i < options.Length; i++)
            {
                JeuEnsemence.CentrerTexte(i == index ? $"> {options[i]}" : $"  {options[i]}");
            }

            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.UpArrow) index = (index - 1 + options.Length) % options.Length;
            if (key == ConsoleKey.DownArrow) index = (index + 1) % options.Length;

        } while (key != ConsoleKey.Enter);
        Console.Clear();
        return index;
    }
}
