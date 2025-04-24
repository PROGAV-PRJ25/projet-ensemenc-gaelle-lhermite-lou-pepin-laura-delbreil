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

