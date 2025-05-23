public class TerrainSableux : Terrain // Classe qui hérite de terrain pour créer terrain argileux
{
    public TerrainSableux(string nom, int nbParcelles) : base("Sable", nbParcelles)
    {
        Nom = nom;
        Couleur = "\x1b[48;5;136m"; // couleur spécifique au type de terrain
    }
}