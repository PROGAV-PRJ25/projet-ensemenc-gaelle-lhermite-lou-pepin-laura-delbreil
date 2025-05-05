public class TerrainTerreux : Terrain
{
    public TerrainTerreux(string nom) : base("Terre")
    {
        // Ici, rien à ajouter de plus puisque tout est hérité de Terrain
        // On a juste forcé le type de sol à "terre"
    }

    public override void Afficher()
    {
        string colorCode = "\x1b[38;5;94m"; // couleur marron spécifique pour 'terre'
        string reset = "\x1b[0m";

        Console.WriteLine($"\n{Nom} (Terre)");

        // Contour supérieur
        Console.Write($"{colorCode}┌───┬───┬───┐{reset}\n");

        // Première rangée : Parcelles 0, 1, 2
        Console.Write("│");
        for (int i = 0; i < 3; i++)
            Console.Write($" {Parcelles[i].AfficherContenu()} │");
        Console.WriteLine();

        // Deuxième rangée : Parcelles 3, 4, 5
        Console.Write("│");
        for (int i = 3; i < 6; i++)
            Console.Write($" {Parcelles[i].AfficherContenu()} │");
        Console.WriteLine();

        // Contour inférieur
        Console.WriteLine($"{colorCode}└───┴───┴───┘{reset}");
    }
}
