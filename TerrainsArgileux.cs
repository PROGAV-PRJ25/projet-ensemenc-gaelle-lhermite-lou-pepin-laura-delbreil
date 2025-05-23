public class TerrainArgileux : Terrain // Classe qui hérite de terrain pour créer terrain argileux
{
    public TerrainArgileux(string nom, int nbParcelles) : base("Argile", nbParcelles)
    {
        Nom = nom;
        Couleur = "\x1b[48;5;124m"; // couleur spécifique au type de terrain
    }
}
