public class TerrainSableux : Terrain
{
    public TerrainSableux(string nom) : base("Sable") 
    {

    }

    public override void Afficher()
    {
        string colorCode = "\x1b[38;5;136m"; // jaune foncé
        string reset = "\x1b[0m";

        Console.WriteLine($"\n{Nom} (Sable)");

        Console.Write($"{colorCode}┌───┬───┬───┐{reset}\n");

        Console.Write("│");
        for (int i = 0; i < 3; i++)
            Console.Write($" {Parcelles[i].AfficherContenu()} │");
        Console.WriteLine();

        Console.Write("│");
        for (int i = 3; i < 6; i++)
            Console.Write($" {Parcelles[i].AfficherContenu()} │");
        Console.WriteLine();

        Console.WriteLine($"{colorCode}└───┴───┴───┘{reset}");
    }
}
